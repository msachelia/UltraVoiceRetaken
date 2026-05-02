using HarmonyLib;
using System.Collections;
using UnityEngine;
using UltraVoice.Utilities;

namespace UltraVoice.Characters
{
    public class GuttertankCharacter
    {
        public static AudioClip[] SpawnClips;
        public static AudioClip AttackClip;
        public static AudioClip[] PunchHitClips;
        public static AudioClip[] FrustratedClips;
        public static AudioClip[] DeathClips;
        public static AudioClip[] TripPainClips;

        public static readonly string[] SpawnSubs =
        {
            "WEAPONS ONLINE.",
            "READY FOR COMBAT.",
            "ENGAGING PROTOCOLS.",
            "ALL SYSTEMS OPERATIONAL."
        };

        public static readonly string[] PunchHitSubs =
        {
            "DIRECT HIT.",
            "HIT CONFIRMED.",
            "TRY THAT AGAIN."
        };

        public static readonly string[] FrustratedSubs =
        {
            "DAMN IT!",
            "VERDAMMT!",
            "UGH, MY HEAD…",
            "SCHEIẞE!"
        };

        public static bool GuttertankSpawnInMirror = false;

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            SpawnClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip("Guttertank.gt_Spawn1.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_Spawn2.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_Spawn3.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_Spawn4.wav")
            };

            AttackClip = UltraVoicePlugin.LoadClip("Guttertank.gt_Attack.wav");

            PunchHitClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip("Guttertank.gt_PunchHit1.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_PunchHit2.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_PunchHit3.wav")
            };

            FrustratedClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip("Guttertank.gt_PunchTrip1.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_PunchTrip2.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_PunchTrip3.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_PunchTrip4.wav")
            };

            DeathClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip("Guttertank.gt_Death1.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_Death2.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_Death3.wav")
            };

            TripPainClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip("Guttertank.gt_TripPain1.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_TripPain2.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_TripPain3.wav"),
            };

            logger.LogInfo("Guttertank voice lines loaded successfully!");
        }

}

    // GUTTERTANK PATCHES

    [HarmonyPatch(typeof(Guttertank), "Start")]
    class GuttertankSpawnPatch
    {
        static void Postfix(Guttertank __instance)
        {
            if (!UltraVoicePlugin.GuttertankVoiceEnabled.value) return;

            if (__instance == null)
                return;

            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "45addc6c3730dae418321e00af1116c5" && !GuttertankCharacter.GuttertankSpawnInMirror) // Fraud Second Scene
            {
                GuttertankCharacter.GuttertankSpawnInMirror = true;
                VoiceManager.enemySpawnTimes[__instance] = Time.time;
                return;
            }

            VoiceManager.enemySpawnTimes[__instance] = Time.time;

            UltraVoicePlugin.Instance.StartCoroutine(UltraVoicePlugin.DelayedVox(() =>
                VoiceManager.PlayRandomVoice(__instance, "Guttertank",
                    GuttertankCharacter.SpawnClips,
                    GuttertankCharacter.SpawnSubs,
                    false
                ),
                () => GuttertankCharacter.SpawnClips != null && GuttertankCharacter.SpawnClips.Length > 0,
                __instance
            ));

            __instance.dead = false;
            __instance.eid.dead = false;
        }
    }

    [HarmonyPatch(typeof(Guttertank), "TargetBeenHit")]
    class GuttertankPunchHitPatch
    {
        static void Postfix(Guttertank __instance)
        {
            if (!UltraVoicePlugin.GuttertankVoiceEnabled.value) return;

            if (__instance == null)
                return;

            UltraVoicePlugin.Instance.StartCoroutine(UltraVoicePlugin.DelayedVox(() =>
                        VoiceManager.PlayRandomVoice(__instance, "Guttertank",
                            GuttertankCharacter.PunchHitClips,
                            GuttertankCharacter.PunchHitSubs
                        ),
                    () => GuttertankCharacter.PunchHitClips != null && GuttertankCharacter.PunchHitClips.Length > 0,
                    __instance
                ));
        }
    }

    [HarmonyPatch(typeof(Guttertank), "FallImpact")]
    class GuttertankFallImpactPatch
    {
        static void Postfix(Guttertank __instance)
        {
            if (!UltraVoicePlugin.GuttertankVoiceEnabled.value) return;

            UltraVoicePlugin.Instance.StartCoroutine(UltraVoicePlugin.DelayedVox(() =>
                        VoiceManager.PlayRandomVoice(__instance, "Guttertank",
                            GuttertankCharacter.TripPainClips,
                            null
                        ),
                    () => GuttertankCharacter.TripPainClips != null && GuttertankCharacter.TripPainClips.Length > 0,
                    __instance
                ));

            new WaitUntil(() => __instance.mach.parryable == false);

            VoiceManager.PlayRandomVoice(__instance, "Guttertank",
                GuttertankCharacter.FrustratedClips,
                GuttertankCharacter.FrustratedSubs
            );
        }
    }

    [HarmonyPatch(typeof(Guttertank), "Death")]
    class GuttertankDeathPatch
    {
        static void Postfix(Guttertank __instance)
        {
            if (!UltraVoicePlugin.GuttertankVoiceEnabled.value) return;

            if (__instance == null)
                return;

            UltraVoicePlugin.Instance.StartCoroutine(UltraVoicePlugin.DelayedVox(() =>
                        VoiceManager.PlayRandomVoice(__instance, "Guttertank",
                            GuttertankCharacter.DeathClips,
                            null,
                            true
                        ),
                    () => GuttertankCharacter.DeathClips != null && GuttertankCharacter.DeathClips.Length > 0,
                    __instance
                ));
        }
    }
}