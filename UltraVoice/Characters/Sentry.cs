using HarmonyLib;
using UnityEngine;
using UltraVoice.Utilities;

namespace UltraVoice.Characters
{
    public class SentryCharacter
    {

        public static AudioClip[] DigInClips;
        public static AudioClip[] ShootClips;
        public static AudioClip[] InterruptPainClips;
        public static AudioClip[] InterruptClips;
        public static AudioClip[] KickClips;
        public static AudioClip[] DeathClips;

        public static AudioClip[] DigInClipsGoob;
        public static AudioClip[] ShootClipsGoob;
        public static AudioClip[] InterruptPainClipsGoob;
        public static AudioClip[] InterruptClipsGoob;
        public static AudioClip[] KickClipsGoob;
        public static AudioClip[] DeathClipsGoob;

        public static readonly string[] DigInSubs =
        {
            "This'll do!",
            "This spot's perfect!",
            "Now... to wait.",
            "Let's make this quick.",
            "Lockin' in.",
            "Holding here!",
        };

        public static readonly string[] ShootSubs =
        {
            "Dodge this!",
            "Now!",
            "Fire!"
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
            "The hell!?",
            "Oh, you little...",
            "Oh, come on!",
            "Seriously!?"
        };

        public static AudioClip[] UseSentryClips(AudioClip[] notoClips, AudioClip[] goobClips)
        {
            return UltraVoicePlugin.SentryVoiceActorField != null && UltraVoicePlugin.SentryVoiceActorField.value == UltraVoicePlugin.SentryVoiceActor.Goober
                ? goobClips
                : notoClips;
        }

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            DigInClips = UltraVoicePlugin.LoadClips("Sentry.tur_DigIn{0}.wav", 6);
            InterruptPainClips = UltraVoicePlugin.LoadClips("Sentry.tur_Pain{0}.wav", 2);
            InterruptClips = UltraVoicePlugin.LoadClips("Sentry.tur_Interrupt{0}.wav", 5);
            ShootClips = UltraVoicePlugin.LoadClips("Sentry.tur_Fire{0}.wav", 3);
            KickClips = UltraVoicePlugin.LoadClips("Sentry.tur_Kick{0}.wav", 3);
            DeathClips = UltraVoicePlugin.LoadClips("Sentry.tur_Death{0}.wav", 2);

            DigInClipsGoob = UltraVoicePlugin.LoadClips("Sentry.tur_DigIn{0}Goob.wav", 6);
            InterruptPainClipsGoob = UltraVoicePlugin.LoadClips("Sentry.tur_Pain{0}Goob.wav", 2);
            InterruptClipsGoob = UltraVoicePlugin.LoadClips("Sentry.tur_Interrupt{0}Goob.wav", 5);
            ShootClipsGoob = UltraVoicePlugin.LoadClips("Sentry.tur_Fire{0}Goob.wav", 3);
            KickClipsGoob = UltraVoicePlugin.LoadClips("Sentry.tur_Kick{0}Goob.wav", 3);
            DeathClipsGoob = UltraVoicePlugin.LoadClips("Sentry.tur_Death{0}Goob.wav", 2);

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
                SentryCharacter.UseSentryClips(SentryCharacter.DigInClips, SentryCharacter.DigInClipsGoob),
                SentryCharacter.DigInSubs,
                false,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(Turret), "Shoot")]
    class SentryShootPatch
    {
        static void Postfix(Turret __instance)
        {
            if (!UltraVoicePlugin.SentryVoiceEnabled.value) return;

            VoiceManager.PlayRandomVoice(__instance, "Sentry",
                SentryCharacter.UseSentryClips(SentryCharacter.ShootClips, SentryCharacter.ShootClipsGoob),
                SentryCharacter.ShootSubs,
                false,
                randomPitch: true
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
                SentryCharacter.UseSentryClips(SentryCharacter.KickClips, SentryCharacter.KickClipsGoob),
                SentryCharacter.KickSubs,
                false,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(Turret), "Interrupt")]
    class SentryInterruptPatch
    {
        static void Postfix(Turret __instance)
        {
            if (!UltraVoicePlugin.SentryVoiceEnabled.value) return;

            VoiceManager.PlayRandomVoice(__instance, "Sentry",
                SentryCharacter.UseSentryClips(SentryCharacter.InterruptPainClips, SentryCharacter.InterruptPainClipsGoob),
                null,
                true,
                randomPitch: true
            );

            VoiceManager.PlayRandomVoiceDelayed(0.75f, __instance, "Sentry",
                SentryCharacter.UseSentryClips(SentryCharacter.InterruptClips, SentryCharacter.InterruptClipsGoob),
                SentryCharacter.InterruptSubs,
                interrupt: true,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(Turret), nameof(Turret.ShouldKnockback))]
    class SentryShouldKnockbackPatch
    {
        static void Postfix(Turret __instance)
        {
            if (!UltraVoicePlugin.SentryVoiceEnabled.value) return;

            if (__instance.aiming) return;

            if (!VoiceManager.CheckCooldown(__instance, 2f))
                return;

            VoiceManager.PlayRandomVoice(
                __instance,
                "Sentry",
                SentryCharacter.UseSentryClips(
                    SentryCharacter.InterruptPainClips,
                    SentryCharacter.InterruptPainClipsGoob
                ),
                null,
                true,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(Turret), "OnDeath")]
    class SentryDeathPatch
    {
        static void Postfix(Turret __instance)
        {
            if (!UltraVoicePlugin.SentryVoiceEnabled.value) return;

            VoiceManager.PlayRandomVoice(__instance, "Sentry",
                SentryCharacter.UseSentryClips(SentryCharacter.DeathClips, SentryCharacter.DeathClipsGoob),
                null,
                true,
                randomPitch: true
            );
        }
    }
}
