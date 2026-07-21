using HarmonyLib;
using UnityEngine;
using UltraVoice.Utilities;

namespace UltraVoice.Characters
{
    public class MannequinCharacter
    {

        public static AudioClip[] ChatterClips;
        public static AudioClip[] DeathClips;

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            ChatterClips = UltraVoicePlugin.LoadClips("Mannequin.mq_Laugh{0}.wav", 5);
            DeathClips = UltraVoicePlugin.LoadClips("Mannequin.mq_Death{0}.wav", 2);

            logger.LogInfo("Mannequin voice lines loaded successfully!");
        }
    }

    [HarmonyPatch(typeof(Mannequin), "Update")]
    class MannequinChatterPatch
    {
        static void Postfix(Mannequin __instance)
        {
            if (!UltraVoicePlugin.MannequinVoiceEnabled.value) return;

            if (ULTRAKILL.Cheats.BlindEnemies.Blind)
                return;

            if (!VoiceManager.CheckCooldown(__instance, 4f))
                return;

            if (Random.Range(0f, 1f) < 0.75f)
                return;

            VoiceManager.PlayRandomVoice(__instance, "Mannequin",
                MannequinCharacter.ChatterClips,
                null,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(Mannequin), "MeleeAttack")]
    class MannequinSwingPatch
    {
        static void Postfix(Mannequin __instance)
        {
            if (!UltraVoicePlugin.MannequinVoiceEnabled.value) return;

            VoiceManager.PlayRandomVoice(__instance, "Mannequin",
                MannequinCharacter.ChatterClips,
                null,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(Mannequin), "OnDeath")]
    class MannequinDeathPatch
    {
        static void Postfix(Mannequin __instance)
        {
            if (!UltraVoicePlugin.MannequinVoiceEnabled.value) return;

            VoiceManager.PlayRandomVoice(__instance, "Mannequin",
                MannequinCharacter.DeathClips,
                null,
                true,
                randomPitch: true
            );
        }
    }
}