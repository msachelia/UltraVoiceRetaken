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
        public static AudioClip[] DeathClips;

        public static AudioClip[] SpawnClipsMasc;
        public static AudioClip[] ChatterClipsMasc;
        public static AudioClip[] MeleeClipsMasc;
        public static AudioClip[] EnrageClipsMasc;
        public static AudioClip[] DeathClipsMasc;

        public static readonly string[] SpawnSubs =
        {
            "Bleed for me.",
            "Witness my perfection!",
            "Behold, true elegance!",
            "Submit to your better!",
            "You're simply inferior!"
        };

        public static readonly string[] ChatterSubs =
        {
            "How unrefined!",
            "How barbaric!",
            "You disgust me.",
            "This is beneath me.",
            "So crude!"
        };

        public static readonly string[] MeleeSubs =
        {
            "Don't touch me!",
            "Do you mind!?",
            "Hands off!",
            "No touching, please!"
        };

        public static readonly string[] EnrageSubs =
        {
            "YOU'VE RUINED ME!",
            "YOU DARE MARE PERFECTION!?",
            "LOOK WHAT YOU'VE DONE TO ME!",
            "I WILL NOT BE VANDALIZED!"
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
            SpawnClips = UltraVoicePlugin.LoadClips("Mindflayer.Fem.mf_Spawn{0}.wav", 5);
            ChatterClips = UltraVoicePlugin.LoadClips("Mindflayer.Fem.mf_Chatter{0}.wav", 5);
            MeleeClips = UltraVoicePlugin.LoadClips("Mindflayer.Fem.mf_Melee{0}.wav", 4);
            EnrageClips = UltraVoicePlugin.LoadClips("Mindflayer.Fem.mf_Enrage{0}.wav", 4);
            DeathClips = UltraVoicePlugin.LoadClips("Mindflayer.Fem.mf_Death{0}.wav", 3);

            SpawnClipsMasc = UltraVoicePlugin.LoadClips("Mindflayer.Masc.mf_Spawn{0}.wav", 5);
            ChatterClipsMasc = UltraVoicePlugin.LoadClips("Mindflayer.Masc.mf_Chatter{0}.wav", 4);
            MeleeClipsMasc = UltraVoicePlugin.LoadClips("Mindflayer.Masc.mf_Melee{0}.wav", 3);
            EnrageClipsMasc = UltraVoicePlugin.LoadClips("Mindflayer.Masc.mf_Enrage{0}.wav", 4);
            DeathClipsMasc = UltraVoicePlugin.LoadClips("Mindflayer.Masc.mf_Death{0}.wav", 3);

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

            if (!VoiceManager.CheckCooldown(__instance, 8f))
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
            VoiceManager.PlayRandomVoice(__instance, "Mindflayer",
                MindflayerCharacter.UseMindflayerClips(__instance, MindflayerCharacter.DeathClips, MindflayerCharacter.DeathClipsMasc),
                null,
                interrupt: true,
                randomPitch: true
            );
        }
    }
}
