using HarmonyLib;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UltraVoice.Utilities;

namespace UltraVoice.Characters
{
    public class GuttertankCharacter
    {
        public static AudioClip[] SpawnClips;
        public static AudioClip[] AttackClips;
        public static AudioClip[] LandmineClips;
        public static AudioClip[] PunchClips;
        public static AudioClip[] PunchHitClips;
        public static AudioClip[] FrustratedClips;
        public static AudioClip[] DeathClips;
        public static AudioClip[] TripPainClips;

        public static AudioClip[] SpawnClipsVirchew;
        public static AudioClip[] AttackClipsVirchew;
        public static AudioClip[] LandmineClipsVirchew;
        public static AudioClip[] PunchClipsVirchew;
        public static AudioClip[] PunchHitClipsVirchew;
        public static AudioClip[] FrustratedClipsVirchew;
        public static AudioClip[] DeathClipsVirchew;
        public static AudioClip[] TripPainClipsVirchew;

        public static readonly string[] SpawnSubs =
        {
            "WEAPONS ONLINE.",
            "READY FOR COMBAT.",
            "ENGAGING PROTOCOLS.",
            "ALL SYSTEMS OPERATIONAL."
        };

        public static readonly string[] AttackSubs =
        {
            "FIRE!",
            "SHOOT!",
            "SCHIEẞEN!",
        };

        public static readonly string[] LandmineSubs =
        {
            "WATCH YOUR STEP.",
            "HOPPER IS DOWN.",
            "HÜPFER IST DA."
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

        public const string FraudSecondSceneName = "45addc6c3730dae418321e00af1116c5";

        public static bool GuttertankSpawnInMirror = false;

        public static AudioClip[] UseGuttertankClips(AudioClip[] melClips, AudioClip[] virchewClips)
        {
            return UltraVoicePlugin.GuttertankVoiceActorField != null && UltraVoicePlugin.GuttertankVoiceActorField.value == UltraVoicePlugin.GuttertankVoiceActor.Virchew
                ? virchewClips
                : melClips;
        }

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            SpawnClips = UltraVoicePlugin.LoadClips("Guttertank.gt_Spawn{0}.wav", 4);
            AttackClips = UltraVoicePlugin.LoadClips("Guttertank.gt_Attack{0}.wav", 3);
            LandmineClips = UltraVoicePlugin.LoadClips("Guttertank.gt_Landmine{0}.wav", 3);
            PunchClips = UltraVoicePlugin.LoadClips("Guttertank.gt_Punch{0}.wav", 3);
            PunchHitClips = UltraVoicePlugin.LoadClips("Guttertank.gt_PunchHit{0}.wav", 3);
            FrustratedClips = UltraVoicePlugin.LoadClips("Guttertank.gt_PunchTrip{0}.wav", 4);
            DeathClips = UltraVoicePlugin.LoadClips("Guttertank.gt_Death{0}.wav", 3);
            TripPainClips = UltraVoicePlugin.LoadClips("Guttertank.gt_TripPain{0}.wav", 3);

            SpawnClipsVirchew = UltraVoicePlugin.LoadClips("Guttertank.gt_SpawnVirchew{0}.wav", 4);
            AttackClipsVirchew = UltraVoicePlugin.LoadClips("Guttertank.gt_AttackVirchew{0}.wav", 3);
            LandmineClipsVirchew = UltraVoicePlugin.LoadClips("Guttertank.gt_LandmineVirchew{0}.wav", 3);
            PunchClipsVirchew = UltraVoicePlugin.LoadClips("Guttertank.gt_PunchVirchew{0}.wav", 3);
            PunchHitClipsVirchew = UltraVoicePlugin.LoadClips("Guttertank.gt_PunchHitVirchew{0}.wav", 3);
            FrustratedClipsVirchew = UltraVoicePlugin.LoadClips("Guttertank.gt_PunchTripVirchew{0}.wav", 4);
            DeathClipsVirchew = UltraVoicePlugin.LoadClips("Guttertank.gt_DeathVirchew{0}.wav", 3);
            TripPainClipsVirchew = UltraVoicePlugin.LoadClips("Guttertank.gt_TripPainVirchew{0}.wav", 3);

            logger.LogInfo("Guttertank voice lines loaded successfully!");
        }
    }

    [HarmonyPatch(typeof(Guttertank), "Start")]
    class GuttertankSpawnPatch
    {
        static void Postfix(Guttertank __instance)
        {
            if (!UltraVoicePlugin.GuttertankVoiceEnabled.value) return;

            if (__instance == null)
                return;

            VoiceManager.enemySpawnTimes[__instance] = Time.time;

            if (SceneManager.GetActiveScene().name == GuttertankCharacter.FraudSecondSceneName && !GuttertankCharacter.GuttertankSpawnInMirror)
            {
                GuttertankCharacter.GuttertankSpawnInMirror = true;
                return;
            }

            VoiceManager.PlayRandomVoice(__instance, "Guttertank",
                GuttertankCharacter.UseGuttertankClips(GuttertankCharacter.SpawnClips, GuttertankCharacter.SpawnClipsVirchew),
                GuttertankCharacter.SpawnSubs,
                false
            );
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

            if (Random.Range(0f, 1f) >= 0.75f)
                return;

            VoiceManager.PlayRandomVoice(__instance, "Guttertank",
                GuttertankCharacter.UseGuttertankClips(GuttertankCharacter.PunchHitClips, GuttertankCharacter.PunchHitClipsVirchew),
                GuttertankCharacter.PunchHitSubs
            );
        }
    }

    [HarmonyPatch(typeof(Guttertank), nameof(Guttertank.PlaceMine))]
    class GuttertankLandminePatch
    {
        static void Postfix(Guttertank __instance)
        {
            if (!UltraVoicePlugin.GuttertankVoiceEnabled.value) return;

            if (__instance == null)
                return;

            if (Random.Range(0f, 1f) >= 0.5f)
                return;

            VoiceManager.PlayRandomVoice(__instance, "Guttertank",
                GuttertankCharacter.UseGuttertankClips(GuttertankCharacter.LandmineClips, GuttertankCharacter.LandmineClipsVirchew),
                GuttertankCharacter.LandmineSubs
            );
        }
    }

    [HarmonyPatch(typeof(Guttertank), nameof(Guttertank.FireRocket))]
    class GuttertankRocketPatch
    {
        static void Postfix(Guttertank __instance)
        {
            if (!UltraVoicePlugin.GuttertankVoiceEnabled.value) return;

            if (__instance == null)
                return;

            if (Random.Range(0f, 1f) >= 0.5f)
                return;

            VoiceManager.PlayRandomVoice(__instance, "Guttertank",
                GuttertankCharacter.UseGuttertankClips(GuttertankCharacter.AttackClips, GuttertankCharacter.AttackClipsVirchew),
                GuttertankCharacter.AttackSubs
            );
        }
    }

    [HarmonyPatch(typeof(Guttertank), nameof(Guttertank.Punch))]
    class GuttertankPunchPatch
    {
        static void Postfix(Guttertank __instance)
        {
            if (!UltraVoicePlugin.GuttertankVoiceEnabled.value) return;

            if (__instance == null)
                return;

            VoiceManager.PlayRandomVoice(__instance, "Guttertank",
                GuttertankCharacter.UseGuttertankClips(GuttertankCharacter.PunchClips, GuttertankCharacter.PunchClipsVirchew),
                null
            );
        }
    }

    [HarmonyPatch(typeof(Guttertank), "FallImpact")]
    class GuttertankFallImpactPatch
    {
        static void Postfix(Guttertank __instance)
        {
            if (!UltraVoicePlugin.GuttertankVoiceEnabled.value) return;

            if (__instance == null)
                return;

            VoiceManager.PlayRandomVoice(__instance, "Guttertank",
                GuttertankCharacter.UseGuttertankClips(GuttertankCharacter.TripPainClips, GuttertankCharacter.TripPainClipsVirchew),
                null,
                true
            );

            UltraVoicePlugin.Instance.StartCoroutine(Frustration(__instance));

            static IEnumerator Frustration(Guttertank tank)
            {
                yield return new WaitForSeconds(0.5f);

                if (tank == null || tank.dead || tank.eid.dead)
                    yield break;

                VoiceManager.PlayRandomVoice(tank, "Guttertank",
                    GuttertankCharacter.UseGuttertankClips(GuttertankCharacter.FrustratedClips, GuttertankCharacter.FrustratedClipsVirchew),
                    GuttertankCharacter.FrustratedSubs,
                    true
                );
            }
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

            VoiceManager.PlayRandomVoice(__instance, "Guttertank",
                GuttertankCharacter.UseGuttertankClips(GuttertankCharacter.DeathClips, GuttertankCharacter.DeathClipsVirchew),
                null,
                true
            );
        }
    }
}
