using HarmonyLib;
using System.Collections;
using UnityEngine;
using UltraVoice.Utilities;

namespace UltraVoice.Characters
{
    public class MaliciousFaceCharacter
    {
        // Voice line storage
        public static AudioClip[] GenericClips;
        public static AudioClip[] EnrageClips;
        public static AudioClip DeathClip;

        // Subtitle storage
        public static readonly string[] GenericSubs =
        {
            "BLEED",
            "FOOLISH",
            "FUTILE",
            "FRAIL",
            "WEAKLING"
        };

        public static readonly string[] EnrageSubs =
        {
            "ENOUGH",
            "SUFFER",
            "PATHETIC",
        };

        public static void LoadVoiceLines(AssetBundle bundle, BepInEx.Logging.ManualLogSource logger)
        {
            GenericClips = new[]
            {
                UltraVoicePlugin.LoadClip(bundle, "maurice_Generic1"),
                UltraVoicePlugin.LoadClip(bundle, "maurice_Generic2"),
                UltraVoicePlugin.LoadClip(bundle, "maurice_Generic3"),
                UltraVoicePlugin.LoadClip(bundle, "maurice_Generic4"),
                UltraVoicePlugin.LoadClip(bundle, "maurice_Generic5"),
            };

            EnrageClips = new[]
            {
                UltraVoicePlugin.LoadClip(bundle, "maurice_Enraged1"),
                UltraVoicePlugin.LoadClip(bundle, "maurice_Enraged2"),
                UltraVoicePlugin.LoadClip(bundle, "maurice_Enraged3"),
            };

            DeathClip = UltraVoicePlugin.LoadClip(bundle, "maurice_Death");

            logger.LogInfo("Malicious Face voice lines loaded successfully!");
        }
    }

    [HarmonyPatch(typeof(MaliciousFace), "Start")]
    class MaliciousFaceSpawnPatch
    {
        static void Postfix(MaliciousFace __instance)
        {
            VoiceManager.enemySpawnTimes[__instance] = Time.time;
        }
    }

    [HarmonyPatch(typeof(MaliciousFace), "Update")]
    class MaliciousFaceChatterPatch
    {
        static void Postfix(MaliciousFace __instance)
        {
            if (!UltraVoicePlugin.MauriceVoiceEnabled.value)
                return;

            if (ULTRAKILL.Cheats.BlindEnemies.Blind)
                return;

            if (VoiceManager.TooSoonAfterSpawn(__instance, 4f))
                return;

            if (!VoiceManager.CheckCooldown(__instance, 4f))
                return;

            if (__instance == null || __instance.eid.dead)
                return;

            if (Random.Range(0f, 1f) < 0.75f)
                VoiceManager.PlayRandomVoice(__instance, "MaliciousFace",
                    MaliciousFaceCharacter.GenericClips,
                    MaliciousFaceCharacter.GenericSubs
                );
        }
    }

    [HarmonyPatch(typeof(MaliciousFace), "Enrage")]
    class MaliciousFaceEnragePatch
    {
        static void Postfix(MaliciousFace __instance)
        {
            if (!UltraVoicePlugin.MauriceVoiceEnabled.value)
                return;

            VoiceManager.PlayRandomVoice(__instance, "MaliciousFace",
                MaliciousFaceCharacter.EnrageClips,
                MaliciousFaceCharacter.EnrageSubs
            );
        }
    }

    [HarmonyPatch(typeof(MaliciousFace), "OnGoLimp")]
    class MaliciousFaceDeathPatch
    {
        static void Postfix(MaliciousFace __instance)
        {
            if (!UltraVoicePlugin.MauriceVoiceEnabled.value)
                return;

            VoiceManager.CreateVoiceSource(__instance, 
                "MaliciousFace",
                MaliciousFaceCharacter.DeathClip,
                null,
                true
            );
        }
    }
}
