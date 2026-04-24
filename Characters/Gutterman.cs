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

        public static void LoadVoiceLines(AssetBundle bundle, BepInEx.Logging.ManualLogSource logger)
        {
            SpawnClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip(bundle, "gm_Spawn1"),
                UltraVoicePlugin.LoadClip(bundle, "gm_Spawn2"),
                UltraVoicePlugin.LoadClip(bundle, "gm_Spawn3"),
                UltraVoicePlugin.LoadClip(bundle, "gm_Spawn4"),
                UltraVoicePlugin.LoadClip(bundle, "gm_Spawn5")
            };

            SpinUpClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip(bundle, "gm_RevUp1"),
                UltraVoicePlugin.LoadClip(bundle, "gm_RevUp2"),
                UltraVoicePlugin.LoadClip(bundle, "gm_RevUp3"),
                UltraVoicePlugin.LoadClip(bundle, "gm_RevUp4")
            };

            LostSightClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip(bundle, "gm_LostSight1"),
                UltraVoicePlugin.LoadClip(bundle, "gm_LostSight2"),
                UltraVoicePlugin.LoadClip(bundle, "gm_LostSight3")
            };

            ShieldBreakClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip(bundle, "gm_GuardBreak1"),
                UltraVoicePlugin.LoadClip(bundle, "gm_GuardBreak2"),
                UltraVoicePlugin.LoadClip(bundle, "gm_GuardBreak3"),
            };

            EnrageClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip(bundle, "gm_Enrage1"),
                UltraVoicePlugin.LoadClip(bundle, "gm_Enrage2"),
                UltraVoicePlugin.LoadClip(bundle, "gm_Enrage3"),
                UltraVoicePlugin.LoadClip(bundle, "gm_Enrage4")
            };

            DeathClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip(bundle, "gm_Death1"),
                UltraVoicePlugin.LoadClip(bundle, "gm_Death2"),
                UltraVoicePlugin.LoadClip(bundle, "gm_Death3"),
            };

            ParryClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip(bundle, "gm_Parry1"),
                UltraVoicePlugin.LoadClip(bundle, "gm_Parry2"),
                UltraVoicePlugin.LoadClip(bundle, "gm_Parry3"),
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
