using HarmonyLib;
using System.Collections;
using UnityEngine;
using UltraVoice.Utilities;

namespace UltraVoice.Characters
{
    public class SentryCharacter
    {
        // Voice line storage
        public static AudioClip[] DigInClips;
        public static AudioClip[] InterruptClips;
        public static AudioClip[] KickClips;
        public static AudioClip[] DeathClips;

        public static readonly string[] DigInSubs =
        {
            "This'll do!",
            "This spot's perfect!",
            "Now… to wait.",
            "Let's make this quick.",
            "Lockin' in.",
            "Holdin' here!",
        };

        public static readonly string[] KickSubs =
        {
            "Outta my face!",
            "Back off!",
            "Shoo!",
        };

        public static readonly string[] InterruptSubs =
        {
            "What the- hey!",
            "Agh! The hell!?",
            "Oh, you little...",
            "Oh, come on!",
            "Seriously!?"
        };

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            DigInClips = new[]
            {
                UltraVoicePlugin.LoadClip("Sentry.tur_DigIn1.wav"),
                UltraVoicePlugin.LoadClip("Sentry.tur_DigIn2.wav"),
                UltraVoicePlugin.LoadClip("Sentry.tur_DigIn3.wav"),
                UltraVoicePlugin.LoadClip("Sentry.tur_DigIn4.wav"),
                UltraVoicePlugin.LoadClip("Sentry.tur_DigIn5.wav"),
                UltraVoicePlugin.LoadClip("Sentry.tur_DigIn6.wav")
            };

            InterruptClips = new[]
            {
                UltraVoicePlugin.LoadClip("Sentry.tur_Interrupt1.wav"),
                UltraVoicePlugin.LoadClip("Sentry.tur_Interrupt2.wav"),
                UltraVoicePlugin.LoadClip("Sentry.tur_Interrupt3.wav"),
                UltraVoicePlugin.LoadClip("Sentry.tur_Interrupt4.wav"),
                UltraVoicePlugin.LoadClip("Sentry.tur_Interrupt5.wav")
            };

            KickClips = new[]
            {
                UltraVoicePlugin.LoadClip("Sentry.tur_Kick1.wav"),
                UltraVoicePlugin.LoadClip("Sentry.tur_Kick2.wav"),
                UltraVoicePlugin.LoadClip("Sentry.tur_Kick3.wav")
            };

            DeathClips = new[]
            {
                UltraVoicePlugin.LoadClip("Sentry.tur_Death1.wav"),
                UltraVoicePlugin.LoadClip("Sentry.tur_Death2.wav"),
            };

            logger.LogInfo("Sentry voice lines loaded successfully!");
        }
    }

    [HarmonyPatch(typeof(Turret), "StartAiming")]
    class SentryStartAimingPatch
    {
        static void Postfix(Turret __instance)
        {
            if (!UltraVoicePlugin.SentryVoiceEnabled.value) return;

            VoiceManager.PlayRandomVoice(__instance, "Sentry",
                SentryCharacter.DigInClips,
                SentryCharacter.DigInSubs,
                false
            );
        }
    }

    [HarmonyPatch(typeof(Turret), "Kick")]
    class SentryKickPatch
    {
        static void Postfix(Turret __instance)
        {
            if (!UltraVoicePlugin.SentryVoiceEnabled.value) return;

            VoiceManager.PlayRandomVoice(__instance, "Sentry",
                SentryCharacter.KickClips,
                SentryCharacter.KickSubs,
                false
            );
        }
    }

    [HarmonyPatch(typeof(Turret), "Interrupt")]
    class SentryInterruptPatch
    {
        static void Postfix(Turret __instance)
        {
            if (!UltraVoicePlugin.SentryVoiceEnabled.value) return;
            VoiceManager.InterruptVoices(__instance);
            UltraVoicePlugin.Instance.StartCoroutine(Interrupt(__instance));

            static IEnumerator Interrupt(Turret turret)
            {
                yield return new WaitForSeconds(0.5f);

                if (turret == null)
                    yield break;

                VoiceManager.PlayRandomVoice(turret, "Sentry",
                    SentryCharacter.InterruptClips,
                    SentryCharacter.InterruptSubs
                );
            }
        }
    }

    [HarmonyPatch(typeof(Turret), "OnDeath")]
    class SentryDeathPatch
    {
        static void Postfix(Turret __instance)
        {
            if (!UltraVoicePlugin.SentryVoiceEnabled.value) return;

            VoiceManager.PlayRandomVoice(__instance, "Sentry",
                SentryCharacter.DeathClips,
                null,
                true
            );
        }
    }
}
