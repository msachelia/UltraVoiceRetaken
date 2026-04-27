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
            "This’ll do!",
            "This spot’s perfect!",
            "Now… to wait.",
            "Let’s make this quick.",
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

        public static void LoadVoiceLines(AssetBundle bundle, BepInEx.Logging.ManualLogSource logger)
        {
            DigInClips = new[]
            {
                UltraVoicePlugin.LoadClip(bundle, "tur_DigIn1"),
                UltraVoicePlugin.LoadClip(bundle, "tur_DigIn2"),
                UltraVoicePlugin.LoadClip(bundle, "tur_DigIn3"),
                UltraVoicePlugin.LoadClip(bundle, "tur_DigIn4"),
                UltraVoicePlugin.LoadClip(bundle, "tur_DigIn5"),
                UltraVoicePlugin.LoadClip(bundle, "tur_DigIn6")
            };

            InterruptClips = new[]
            {
                UltraVoicePlugin.LoadClip(bundle, "tur_Interrupt1"),
                UltraVoicePlugin.LoadClip(bundle, "tur_Interrupt2"),
                UltraVoicePlugin.LoadClip(bundle, "tur_Interrupt3"),
                UltraVoicePlugin.LoadClip(bundle, "tur_Interrupt4"),
                UltraVoicePlugin.LoadClip(bundle, "tur_Interrupt5")
            };

            KickClips = new[]
            {
                UltraVoicePlugin.LoadClip(bundle, "tur_Kick1"),
                UltraVoicePlugin.LoadClip(bundle, "tur_Kick2"),
                UltraVoicePlugin.LoadClip(bundle, "tur_Kick3")
            };

            DeathClips = new[]
            {
                UltraVoicePlugin.LoadClip(bundle, "tur_Death1"),
                UltraVoicePlugin.LoadClip(bundle, "tur_Death2"),
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
