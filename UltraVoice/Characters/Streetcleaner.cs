using HarmonyLib;
using UnityEngine;
using UltraVoice.Utilities;

namespace UltraVoice.Characters
{
    public class StreetcleanerCharacter
    {

        public static AudioClip[] ChatterClips;
        public static AudioClip[] AttackClips;
        public static AudioClip[] ParryClips;

        public static readonly string[] ChatterSubs =
        {
            "IMPURITY.",
            "SANITIZE.",
            "EXTERMINATE.",
            "PURGE.",
            "UNCLEAN."
        };

        public static readonly string[] AttackSubs =
        {
            "CLEANSING...",
            "PURIFYING...",
            "TO ASH...",
            "QUIT MOVING.",
            "STOP RESISTING."
        };

        public static readonly string[] ParrySubs =
        {
            "DENIED.",
            "DEFLECTED.",
            "HA, HA."
        };

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            ChatterClips = UltraVoicePlugin.LoadClips("assets/resources/streetcleaner/kyljoi/sc_chatter{0}.wav", 5);
            AttackClips = UltraVoicePlugin.LoadClips("assets/resources/streetcleaner/kyljoi/sc_attack{0}.wav", 5);
            ParryClips = UltraVoicePlugin.LoadClips("assets/resources/streetcleaner/kyljoi/sc_parry{0}.wav", 3);

            logger.LogInfo("Streetcleaner voice lines loaded successfully!");
        }
    }

    [HarmonyPatch(typeof(Streetcleaner), "Update")]
    class StreetcleanerChatterPatch
    {
        static void Postfix(Streetcleaner __instance)
        {
            if (!UltraVoicePlugin.StreetcleanerVoiceEnabled.Value)
                return;

            if (ULTRAKILL.Cheats.BlindEnemies.Blind)
                return;

            if (__instance == null)
                return;

            if (__instance.dead)
                return;

            if (!VoiceManager.CheckCooldown(__instance, 6f))
                return;

            if (Random.Range(0f, 1f) >= 0.75f)
                return;

            VoiceManager.PlayRandomVoice(__instance, "Streetcleaner",
                StreetcleanerCharacter.ChatterClips,
                StreetcleanerCharacter.ChatterSubs,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(Streetcleaner), "StartFire")]
    class StreetcleanerFlameAttackPatch
    {
        static void Postfix(Streetcleaner __instance)
        {
            if (!UltraVoicePlugin.StreetcleanerVoiceEnabled.Value)
                return;

            if (Random.Range(0f, 1f) >= 0.75f)
                return;

            VoiceManager.PlayRandomVoice(__instance, "Streetcleaner",
                StreetcleanerCharacter.AttackClips,
                StreetcleanerCharacter.AttackSubs,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(Streetcleaner), "DeflectShot")]
    class StreetcleanerParryPatch
    {
        static void Postfix(Streetcleaner __instance)
        {
            if (!UltraVoicePlugin.StreetcleanerVoiceEnabled.Value)
                return;

            if (Random.Range(0f, 1f) >= 0.5f)
                return;

            VoiceManager.PlayRandomVoice(__instance, "Streetcleaner",
                StreetcleanerCharacter.ParryClips,
                StreetcleanerCharacter.ParrySubs,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(Streetcleaner), "OnGoLimp")]
    class StreetcleanerDeathInterruptPatch
    {
        static void Postfix(Streetcleaner __instance)
        {
            VoiceManager.InterruptVoices(__instance);
        }
    }
}