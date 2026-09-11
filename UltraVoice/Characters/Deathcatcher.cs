using HarmonyLib;
using UltraVoice.Utilities;
using UnityEngine;

namespace UltraVoice.Characters
{
    public class DeathcatcherCharacter
    {

        public static AudioClip[] ReviveClips;

        public static readonly string[] ReviveSubs =
        {
            "No, no, no...",
            "I promised...",
            "I'm sorry...",
            "The pieces...",
            "Protected... see?",
            "Stay with me..."
        };


        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            ReviveClips = UltraVoicePlugin.LoadClips("Deathcatcher.Test.dc_Revive{0}.wav", 6);

            logger.LogInfo("Deathcatcher voice lines loaded successfully!");
        }
    }

    [HarmonyPatch(typeof(Deathcatcher), nameof(Deathcatcher.RespawnPuppets))]
    class DeathcatcherRevivePatch
    {
        static void Postfix(Deathcatcher __instance)
        {
            if (!UltraVoicePlugin.DeathcatcherVoiceEnabled.Value)
                return;

            if (__instance.chargeAud.isPlaying)
                VoiceManager.PlayRandomVoice(__instance, "Deathcatcher",
                    DeathcatcherCharacter.ReviveClips,
                    DeathcatcherCharacter.ReviveSubs,
                    randomPitch: true
                );
        }
    }
}
