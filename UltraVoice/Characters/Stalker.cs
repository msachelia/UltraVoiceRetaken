using HarmonyLib;
using UnityEngine;
using UltraVoice.Utilities;

namespace UltraVoice.Characters
{
    public class StalkerCharacter
    {
        public static AudioClip[] ChatterClips;
        public static AudioClip[] AttackClips;

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            ChatterClips = UltraVoicePlugin.LoadClips("assets/resources/stalker/mitzi/stalker_chatter{0}.wav", 5);

            AttackClips = UltraVoicePlugin.LoadClips("assets/resources/stalker/mitzi/stalker_explode{0}.wav", 3);

            logger.LogInfo("Stalker voice lines loaded successfully!");
        }

        public static readonly string[] ChatterSubs =
        {
            "I am… burdened…", 
            "So… heavy…", 
            "This… weight…", 
            "Let me… rest…", 
            "My… back…"
        };

        public static readonly string[] AttackSubs =
        {
            "No more!",
            "Freedom!",
            "Free me!"
        };
    }

    [HarmonyPatch(typeof(Stalker), "Update")]
    class StalkerChatterPatch
    {
        static void Postfix(Stalker __instance)
        {
            if (!UltraVoicePlugin.StalkerVoiceEnabled.Value) return;

            if (ULTRAKILL.Cheats.BlindEnemies.Blind)
                return;

            if (!VoiceManager.CheckCooldown(__instance, 7f))
                return;

            VoiceManager.PlayRandomVoice(__instance, "Stalker",
                StalkerCharacter.ChatterClips,
                StalkerCharacter.ChatterSubs,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(Stalker), nameof(Stalker.Countdown))]
    class StalkerExplodePatch
    {
        static void Postfix(Stalker __instance)
        {
            if (!UltraVoicePlugin.StalkerVoiceEnabled.Value) return;

            VoiceManager.PlayRandomVoice(__instance, "Stalker",
                StalkerCharacter.AttackClips,
                StalkerCharacter.AttackSubs,
                randomPitch: true
            );
        }
    }
}
