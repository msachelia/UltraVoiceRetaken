using HarmonyLib;
using System.Collections;
using UnityEngine;
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

        public static bool GuttertankSpawnInMirror = false;

        public static AudioClip[] UseGuttertankClips(AudioClip[] melClips, AudioClip[] virchewClips)
        {
            return UltraVoicePlugin.GuttertankVoiceActorField != null && UltraVoicePlugin.GuttertankVoiceActorField.value == UltraVoicePlugin.GuttertankVoiceActor.Virchew
                ? virchewClips
                : melClips;
        }

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            SpawnClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip("Guttertank.gt_Spawn1.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_Spawn2.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_Spawn3.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_Spawn4.wav")
            };

            AttackClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip("Guttertank.gt_Attack1.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_Attack2.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_Attack3.wav")
            };

            LandmineClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip("Guttertank.gt_Landmine1.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_Landmine2.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_Landmine3.wav")
            };

            PunchClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip("Guttertank.gt_Punch1.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_Punch2.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_Punch3.wav")
            };

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

            SpawnClipsVirchew = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip("Guttertank.gt_SpawnVirchew1.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_SpawnVirchew2.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_SpawnVirchew3.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_SpawnVirchew4.wav")
            };

            AttackClipsVirchew = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip("Guttertank.gt_AttackVirchew1.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_AttackVirchew2.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_AttackVirchew3.wav")
            };

            LandmineClipsVirchew = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip("Guttertank.gt_LandmineVirchew1.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_LandmineVirchew2.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_LandmineVirchew3.wav")
            };

            PunchClipsVirchew = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip("Guttertank.gt_PunchVirchew1.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_PunchVirchew2.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_PunchVirchew3.wav")
            };

            PunchHitClipsVirchew = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip("Guttertank.gt_PunchHitVirchew1.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_PunchHitVirchew2.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_PunchHitVirchew3.wav")
            };

            FrustratedClipsVirchew = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip("Guttertank.gt_PunchTripVirchew1.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_PunchTripVirchew2.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_PunchTripVirchew3.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_PunchTripVirchew4.wav")
            };

            DeathClipsVirchew = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip("Guttertank.gt_DeathVirchew1.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_DeathVirchew2.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_DeathVirchew3.wav")
            };

            TripPainClipsVirchew = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip("Guttertank.gt_TripPainVirchew1.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_TripPainVirchew2.wav"),
                UltraVoicePlugin.LoadClip("Guttertank.gt_TripPainVirchew3.wav"),
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
                    GuttertankCharacter.UseGuttertankClips(GuttertankCharacter.SpawnClips, GuttertankCharacter.SpawnClipsVirchew),
                    GuttertankCharacter.SpawnSubs,
                    false
                ),
                () => GuttertankCharacter.SpawnClips != null && GuttertankCharacter.SpawnClips.Length > 0,
                __instance
            ));
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

            if (Random.Range(0f, 1f) < 0.75)
                UltraVoicePlugin.Instance.StartCoroutine(UltraVoicePlugin.DelayedVox(() =>
                        VoiceManager.PlayRandomVoice(__instance, "Guttertank",
                            GuttertankCharacter.UseGuttertankClips(GuttertankCharacter.PunchHitClips, GuttertankCharacter.PunchHitClipsVirchew),
                            GuttertankCharacter.PunchHitSubs
                        ),
                    () => GuttertankCharacter.PunchHitClips != null && GuttertankCharacter.PunchHitClips.Length > 0,
                    __instance
                ));
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

            if (Random.Range(0f, 1f) < 0.5)
                UltraVoicePlugin.Instance.StartCoroutine(UltraVoicePlugin.DelayedVox(() =>
                        VoiceManager.PlayRandomVoice(__instance, "Guttertank",
                            GuttertankCharacter.UseGuttertankClips(GuttertankCharacter.LandmineClips, GuttertankCharacter.LandmineClipsVirchew),
                            GuttertankCharacter.LandmineSubs
                        ),
                    () => GuttertankCharacter.LandmineClips != null && GuttertankCharacter.LandmineClips.Length > 0,
                    __instance
                ));
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

            if (Random.Range(0f, 1f) < 0.5)
                UltraVoicePlugin.Instance.StartCoroutine(UltraVoicePlugin.DelayedVox(() =>
                        VoiceManager.PlayRandomVoice(__instance, "Guttertank",
                            GuttertankCharacter.UseGuttertankClips(GuttertankCharacter.AttackClips, GuttertankCharacter.AttackClipsVirchew),
                            GuttertankCharacter.AttackSubs
                        ),
                    () => GuttertankCharacter.AttackClips != null && GuttertankCharacter.AttackClips.Length > 0,
                    __instance
                ));
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

            UltraVoicePlugin.Instance.StartCoroutine(UltraVoicePlugin.DelayedVox(() =>
                        VoiceManager.PlayRandomVoice(__instance, "Guttertank",
                            GuttertankCharacter.UseGuttertankClips(GuttertankCharacter.PunchClips, GuttertankCharacter.PunchClipsVirchew),
                            null
                        ),
                    () => GuttertankCharacter.PunchClips != null && GuttertankCharacter.PunchClips.Length > 0,
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
                            GuttertankCharacter.UseGuttertankClips(GuttertankCharacter.TripPainClips, GuttertankCharacter.TripPainClipsVirchew),
                            null,
                            true
                        ),
                    () => GuttertankCharacter.TripPainClips != null && GuttertankCharacter.TripPainClips.Length > 0,
                    __instance
                ));

            UltraVoicePlugin.Instance.StartCoroutine(Frustration(__instance));

            static IEnumerator Frustration(Guttertank tank)
            {
                yield return new WaitForSeconds(0.5f);

                if (tank.dead || tank == null || tank.eid.dead) yield break;

                UltraVoicePlugin.Instance.StartCoroutine(UltraVoicePlugin.DelayedVox(() =>
                            VoiceManager.PlayRandomVoice(tank, "Guttertank",
                                GuttertankCharacter.UseGuttertankClips(GuttertankCharacter.FrustratedClips, GuttertankCharacter.FrustratedClipsVirchew),
                                GuttertankCharacter.FrustratedSubs,
                                true
                            ),
                        () => GuttertankCharacter.FrustratedClips != null && GuttertankCharacter.FrustratedClips.Length > 0,
                        tank
                    ));
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

            UltraVoicePlugin.Instance.StartCoroutine(UltraVoicePlugin.DelayedVox(() =>
                        VoiceManager.PlayRandomVoice(__instance, "Guttertank",
                            GuttertankCharacter.UseGuttertankClips(GuttertankCharacter.DeathClips, GuttertankCharacter.DeathClipsVirchew),
                            null,
                            true
                        ),
                    () => GuttertankCharacter.DeathClips != null && GuttertankCharacter.DeathClips.Length > 0,
                    __instance
                ));
        }
    }
}