using HarmonyLib;
using UnityEngine;
using UltraVoice.Utilities;

namespace UltraVoice.Characters
{
    public class GuttermanCharacter
    {
        // Voice line storage
        public static AudioClip[] SpawnClips;
        public static AudioClip[] SpinUpClips;
        public static AudioClip[] LostSightClips;
        public static AudioClip[] ShieldBreakClips;
        public static AudioClip[] EnrageClips;
        public static AudioClip[] DeathClips;
        public static AudioClip[] ParryClips;

        // Subtitle storage
        public static readonly string[] SpawnSubs =
        {
            "THIS WILL NOT TAKE LONG.",
            "WHO MUST I KILL TODAY?",
            "IT IS TIME TO KILL.",
            "FOR THE MOTHERLAND!",
            "SAY YOUR PRAYERS."
        };

        public static readonly string[] SpinUpSubs =
        {
            "THERE YOU ARE!",
            "TARGET SIGHTED!"
        };

        public static readonly string[] LostSightSubs =
        {
            "COME OUT AND FACE ME!",
            "SHOW YOURSELF, COWARD!",
            "DO NOT WASTE MY TIME."
        };

        public static readonly string[] ShieldBreakSubs =
        {
            "SHIELD DOWN!",
            "SHIELD DESTROYED!",
            "SHIELD BROKEN!",
            "DEFENSES BREACHED!"
        };

        public static readonly string[] EnrageSubs =
        {
            "THAT COST MONEY!",
            "YOU WILL PAY FOR THIS!",
            "I WILL TURN YOU TO SCRAP!",
            "OH, YOU MAKE ME SO MAD…"
        };

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            SpawnClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip("Gutterman.gm_Spawn1.wav"),
                UltraVoicePlugin.LoadClip("Gutterman.gm_Spawn2.wav"),
                UltraVoicePlugin.LoadClip("Gutterman.gm_Spawn3.wav"),
                UltraVoicePlugin.LoadClip("Gutterman.gm_Spawn4.wav"),
                UltraVoicePlugin.LoadClip("Gutterman.gm_Spawn5.wav")
            };

            SpinUpClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip("Gutterman.gm_RevUp1.wav"),
                UltraVoicePlugin.LoadClip("Gutterman.gm_RevUp2.wav"),
                UltraVoicePlugin.LoadClip("Gutterman.gm_RevUp3.wav"),
                UltraVoicePlugin.LoadClip("Gutterman.gm_RevUp4.wav")
            };

            LostSightClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip("Gutterman.gm_LostSight1.wav"),
                UltraVoicePlugin.LoadClip("Gutterman.gm_LostSight2.wav"),
                UltraVoicePlugin.LoadClip("Gutterman.gm_LostSight3.wav")
            };

            ShieldBreakClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip("Gutterman.gm_GuardBreak1.wav"),
                UltraVoicePlugin.LoadClip("Gutterman.gm_GuardBreak2.wav"),
                UltraVoicePlugin.LoadClip("Gutterman.gm_GuardBreak3.wav"),
            };

            EnrageClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip("Gutterman.gm_Enrage1.wav"),
                UltraVoicePlugin.LoadClip("Gutterman.gm_Enrage2.wav"),
                UltraVoicePlugin.LoadClip("Gutterman.gm_Enrage3.wav"),
                UltraVoicePlugin.LoadClip("Gutterman.gm_Enrage4.wav")
            };

            DeathClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip("Gutterman.gm_Death1.wav"),
                UltraVoicePlugin.LoadClip("Gutterman.gm_Death2.wav"),
                UltraVoicePlugin.LoadClip("Gutterman.gm_Death3.wav"),
            };

            ParryClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip("Gutterman.gm_Parry1.wav"),
                UltraVoicePlugin.LoadClip("Gutterman.gm_Parry2.wav"),
                UltraVoicePlugin.LoadClip("Gutterman.gm_Parry3.wav"),
            };

            logger.LogInfo("Gutterman voice lines loaded successfully!");
        }

}

    // GUTTERMAN PATCHES

    [HarmonyPatch(typeof(Gutterman), "Start")]
    class GuttermanSpawnPatch
    {
        static void Postfix(Gutterman __instance)
        {
            if (!UltraVoicePlugin.GuttermanVoiceEnabled.value) return;

            if (__instance.dead) return;

            VoiceManager.enemySpawnTimes[__instance] = Time.time;

            VoiceManager.PlayRandomVoice(__instance, "Gutterman",
                GuttermanCharacter.SpawnClips,
                GuttermanCharacter.SpawnSubs,
                false
            );
        }
    }

    [HarmonyPatch(typeof(Gutterman), "ShieldBreak")]
    class GuttermanShieldBreakPatch
    {
        static void Postfix(Gutterman __instance)
        {
            if (!UltraVoicePlugin.GuttermanVoiceEnabled.value) return;

            if (__instance.enraged)
                VoiceManager.PlayRandomVoice(__instance, "Gutterman",
                GuttermanCharacter.EnrageClips,
                GuttermanCharacter.EnrageSubs,
                true
            );
            else VoiceManager.PlayRandomVoice(__instance, "Gutterman",
                GuttermanCharacter.ShieldBreakClips,
                GuttermanCharacter.ShieldBreakSubs,
                false
            );
        }
    }

    [HarmonyPatch(typeof(Gutterman), "Death")]
    class GuttermanDeathPatch
    {
        static void Postfix(Gutterman __instance)
        {
            if (!UltraVoicePlugin.GuttermanVoiceEnabled.value) return;

            VoiceManager.PlayRandomVoice(__instance, "Gutterman",
                GuttermanCharacter.DeathClips,
                null,
                true
            );
        }
    }

    [HarmonyPatch(typeof(Gutterman), "GotParried")]
    class GuttermanParryPatch
    {
        static void Postfix(Gutterman __instance)
        {
            if (!UltraVoicePlugin.GuttermanVoiceEnabled.value) return;

            if (!VoiceManager.CheckCooldown(__instance, 3f))
                return;

            VoiceManager.PlayRandomVoice(__instance, "Gutterman",
                GuttermanCharacter.ParryClips,
                null,
                true
            );
        }
    }
}