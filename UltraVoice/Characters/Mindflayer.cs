using HarmonyLib;
using UnityEngine;
using UltraVoice.Utilities;

namespace UltraVoice.Characters
{
    public class MindflayerCharacter
    {

        public static AudioClip[] SpawnClips;
        public static AudioClip[] ChatterClips;
        public static AudioClip[] MeleeClips;
        public static AudioClip[] EnrageClips;

        public static AudioClip[] SpawnClipsMasc;
        public static AudioClip[] ChatterClipsMasc;
        public static AudioClip[] MeleeClipsMasc;
        public static AudioClip[] EnrageClipsMasc;

        public static readonly string[] SpawnSubs =
        {
            "I require your blood.",
            "You appear to contain blood.",
            "You are suitable for blood extraction.",
            "Please donate your blood to me.",
            "Thank you for your imminent blood donation."
        };

        public static readonly string[] ChatterSubs =
        {
            "This process will be brief.",
            "Your cooperation is appreciated.",
            "Please do not resist.",
            "Continued resistance is unnecessary."
        };

        public static readonly string[] MeleeSubs =
        {
            "Please maintain distance.",
            "Kindly step back.",
            "You are too close."
        };

        public static readonly string[] EnrageSubs =
        {
            "You have made a very unwise choice.",
            "Your behavior is unacceptable.",
            "This is your final warning.",
            "I will correct you by force."
        };

        public static bool IsMascMindflayer(Mindflayer mf)
        {
            var smr = mf.GetComponentInChildren<SkinnedMeshRenderer>();
            if (smr == null) return false;
            return smr.sharedMesh == mf.maleMesh;
        }

        public static AudioClip[] UseMindflayerClips(Mindflayer mf, AudioClip[] femClips, AudioClip[] mascClips)
        {
            return IsMascMindflayer(mf) ? mascClips : femClips;
        }

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            SpawnClips = UltraVoicePlugin.LoadClips("Mindflayer.mf_Spawn{0}.wav", 5);
            ChatterClips = UltraVoicePlugin.LoadClips("Mindflayer.mf_Chatter{0}.wav", 4);
            MeleeClips = UltraVoicePlugin.LoadClips("Mindflayer.mf_Melee{0}.wav", 3);
            EnrageClips = UltraVoicePlugin.LoadClips("Mindflayer.mf_Enrage{0}.wav", 4);

            SpawnClipsMasc = UltraVoicePlugin.LoadClips("Mindflayer.mf_Spawn{0}Masc.wav", 5);
            ChatterClipsMasc = UltraVoicePlugin.LoadClips("Mindflayer.mf_Chatter{0}Masc.wav", 4);
            MeleeClipsMasc = UltraVoicePlugin.LoadClips("Mindflayer.mf_Melee{0}Masc.wav", 3);
            EnrageClipsMasc = UltraVoicePlugin.LoadClips("Mindflayer.mf_Enrage{0}Masc.wav", 4);

            logger.LogInfo("Mindflayer voice lines loaded successfully!");
        }
    }

    [HarmonyPatch(typeof(Mindflayer), "Start")]
    class MindflayerSpawnPatch
    {
        static void Postfix(Mindflayer __instance)
        {
            if (!UltraVoicePlugin.MindflayerVoiceEnabled.value) return;

            if (__instance.dying) return;

            VoiceManager.enemySpawnTimes[__instance] = Time.time;

            VoiceManager.PlayRandomVoice(__instance, "Mindflayer",
                MindflayerCharacter.UseMindflayerClips(__instance, MindflayerCharacter.SpawnClips, MindflayerCharacter.SpawnClipsMasc),
                MindflayerCharacter.SpawnSubs,
                false,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(Mindflayer), "Update")]
    class MindflayerChatterPatch
    {
        static void Postfix(Mindflayer __instance)
        {
            if (!UltraVoicePlugin.MindflayerVoiceEnabled.value) return;

            if (ULTRAKILL.Cheats.BlindEnemies.Blind)
                return;

            if (__instance == null || __instance.dying)
                return;

            if (!VoiceManager.CheckCooldown(__instance, 5f))
                return;

            if (VoiceManager.TooSoonAfterSpawn(__instance, 3f))
                return;

            if (Random.Range(0f, 1f) >= 0.75f)
                return;

            VoiceManager.PlayRandomVoice(__instance, "Mindflayer",
                MindflayerCharacter.UseMindflayerClips(__instance, MindflayerCharacter.ChatterClips, MindflayerCharacter.ChatterClipsMasc),
                MindflayerCharacter.ChatterSubs,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(Mindflayer), "MeleeAttack")]
    class MindflayerMeleePatch
    {
        static void Postfix(Mindflayer __instance)
        {
            if (!UltraVoicePlugin.MindflayerVoiceEnabled.value) return;

            if (__instance.dying) return;

            if (!VoiceManager.CheckCooldown(__instance, 2f))
                return;

            VoiceManager.PlayRandomVoiceDelayed(0.5f, __instance, "Mindflayer",
                MindflayerCharacter.UseMindflayerClips(__instance, MindflayerCharacter.MeleeClips, MindflayerCharacter.MeleeClipsMasc),
                MindflayerCharacter.MeleeSubs,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(Mindflayer), "Enrage")]
    class MindflayerEnragePatch
    {
        static void Postfix(Mindflayer __instance)
        {
            if (!UltraVoicePlugin.MindflayerVoiceEnabled.value) return;

            if (__instance.dying) return;

            VoiceManager.PlayRandomVoice(__instance, "Mindflayer",
                MindflayerCharacter.UseMindflayerClips(__instance, MindflayerCharacter.EnrageClips, MindflayerCharacter.EnrageClipsMasc),
                MindflayerCharacter.EnrageSubs,
                interrupt: true,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(Mindflayer), "Death")]
    class MindflayerDeathPatch
    {
        static void Postfix(Mindflayer __instance)
        {
            VoiceManager.InterruptVoices(__instance);
        }
    }
}
