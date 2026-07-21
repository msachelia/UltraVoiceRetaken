using HarmonyLib;
using UnityEngine;
using UltraVoice.Utilities;

namespace UltraVoice.Characters
{
    public class MaliciousFaceCharacter
    {

        public static AudioClip[] GenericClips;
        public static AudioClip[] EnrageClips;
        public static AudioClip DeathClip;

        public static readonly string[] GenericSubs =
        {
            "BLEED...",
            "FOOLISH...",
            "FUTILE...",
            "FRAIL...",
            "WEAKLING..."
        };

        public static readonly string[] EnrageSubs =
        {
            "ENOUGH.",
            "SUFFER.",
            "PATHETIC."
        };

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            GenericClips = UltraVoicePlugin.LoadClips("MaliciousFace.maurice_Generic{0}.wav", 5);
            EnrageClips = UltraVoicePlugin.LoadClips("MaliciousFace.maurice_Enraged{0}.wav", 3);

            DeathClip = UltraVoicePlugin.LoadClip("MaliciousFace.maurice_Death.wav");

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

            if (__instance == null || __instance.eid.dead)
                return;

            if (!VoiceManager.CheckCooldown(__instance, 6f))
                return;

            if (Random.Range(0f, 1f) >= 0.75f)
                return;

            VoiceManager.PlayRandomVoice(__instance, "MaliciousFace",
                MaliciousFaceCharacter.GenericClips,
                MaliciousFaceCharacter.GenericSubs,
                randomPitch: true
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
                MaliciousFaceCharacter.EnrageSubs,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(MaliciousFace), "OnGoLimp")]
    class MaliciousFaceDeathPatch
    {
        static void Prefix(MaliciousFace __instance)
        {
            if (!UltraVoicePlugin.MauriceVoiceEnabled.value)
                return;

            VoiceManager.CreateVoiceSource(__instance,
                "MaliciousFace",
                MaliciousFaceCharacter.DeathClip,
                null,
                true,
                randomPitch: true
            );
        }
    }
}