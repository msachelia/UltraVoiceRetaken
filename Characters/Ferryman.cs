using HarmonyLib;
using System.Collections;
using UnityEngine;
using UltraVoice.Utilities;

namespace UltraVoice.Characters
{
    public class FerrymanCharacter
    {
        // Voice line storage
        public static AudioClip BossIntroClip;
        public static AudioClip CoinSkipClip;
        public static AudioClip CoinFightClip;
        public static AudioClip PhaseChangeClip;
        public static AudioClip ApproachClip;

        public static AudioClip[] SpawnClips;
        public static AudioClip[] ChatterClips;
        public static AudioClip[] ParryClips;
        public static AudioClip[] DeathClips;

        // Subtitle storage
        public static readonly string[] ChatterSubs =
        {
            "Have at you!",
            "Come on!",
            "Come at me!",
            "Try me!"
        };

        public static readonly string[] SpawnSubs =
        {
            "Another soul to face my wrath.",
            "If you seek death, you have found it.",
            "You are not the first. You will not be the last.",
            "I have no mercy left to give.",
            "I grow weary of your kind."
        };

        public static bool IsAgonisOrRudraksha(Ferryman ferryman)
        {
            if (ferryman == null)
                return false;

            Transform t = ferryman.transform;

            while (t != null)
            {
                if (t.name.Contains("10S - Secret Arena"))
                    return true;

                t = t.parent;
            }

            return false;
        }

        public static bool IsAgonis(Ferryman ferryman)
        {
            if (!IsAgonisOrRudraksha(ferryman))
                return false;

            EnemyIdentifier eid = ferryman.GetComponent<EnemyIdentifier>();
            if (eid == null)
                return false;

            return eid.mirrorOnly;
        }

        public static bool IsRudraksha(Ferryman ferryman)
        {
            if (!IsAgonisOrRudraksha(ferryman))
                return false;

            EnemyIdentifier eid = ferryman.GetComponent<EnemyIdentifier>();
            if (eid == null)
                return false;

            return !eid.mirrorOnly;
        }

        public static void LoadVoiceLines(AssetBundle bundle, BepInEx.Logging.ManualLogSource logger)
        {
            BossIntroClip = UltraVoicePlugin.LoadClip(bundle, "ferry_FightStarted");
            CoinSkipClip = UltraVoicePlugin.LoadClip(bundle, "ferry_CoinSkip");
            CoinFightClip = UltraVoicePlugin.LoadClip(bundle, "ferry_CoinFight");
            PhaseChangeClip = UltraVoicePlugin.LoadClip(bundle, "ferry_PhaseChange");
            ApproachClip = UltraVoicePlugin.LoadClip(bundle, "ferry_Approach");

            SpawnClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip(bundle, "ferry_Spawn1"),
                UltraVoicePlugin.LoadClip(bundle, "ferry_Spawn2"),
                UltraVoicePlugin.LoadClip(bundle, "ferry_Spawn3"),
                UltraVoicePlugin.LoadClip(bundle, "ferry_Spawn4"),
                UltraVoicePlugin.LoadClip(bundle, "ferry_Spawn5")
            };

            ChatterClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip(bundle, "ferry_Chatter1"),
                UltraVoicePlugin.LoadClip(bundle, "ferry_Chatter2"),
                UltraVoicePlugin.LoadClip(bundle, "ferry_Chatter3"),
                UltraVoicePlugin.LoadClip(bundle, "ferry_Chatter4"),
            };

            ParryClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip(bundle, "ferry_Parry1"),
                UltraVoicePlugin.LoadClip(bundle, "ferry_Parry2"),
                UltraVoicePlugin.LoadClip(bundle, "ferry_Parry3"),
                UltraVoicePlugin.LoadClip(bundle, "ferry_Parry4"),
            };

            DeathClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip(bundle, "ferry_Death1"),
                UltraVoicePlugin.LoadClip(bundle, "ferry_Death2"),
                UltraVoicePlugin.LoadClip(bundle, "ferry_Death3"),
            };

            logger.LogInfo("Ferryman voice lines loaded successfully!");
        }

        public static bool FerrymanCoinTossed = false;
        public static bool FerrymanPhaseChangePlayed = false;
    }

    // FERRYMAN PATCHES

    [HarmonyPatch(typeof(Ferryman), "Start")]
    class FerrymanSpawnPatch
    {
        static void Postfix(Ferryman __instance)
        {
            if (!UltraVoicePlugin.FerrymanVoiceEnabled.value) return;

            if (__instance == null)
                return;

            if (StatsManager.Instance.restarts > 0)
                return;

            VoiceManager.enemySpawnTimes[__instance] = Time.time;

            if (FerrymanCharacter.IsAgonisOrRudraksha(__instance))
                return;

            if (__instance.bossVersion)
                if (FerrymanCharacter.FerrymanCoinTossed)
                {
                    UltraVoicePlugin.Instance.StartCoroutine(PlayCoin(__instance));
                    VoiceManager.enemySpawnTimes[__instance] = Time.time;
                    VoiceManager.spawnVoiceEndTimes[__instance] = Time.time + FerrymanCharacter.CoinFightClip.length;
                }
                else
                {
                    UltraVoicePlugin.Instance.StartCoroutine(PlayNoCoin(__instance));
                    VoiceManager.enemySpawnTimes[__instance] = Time.time;
                    VoiceManager.spawnVoiceEndTimes[__instance] = Time.time + FerrymanCharacter.BossIntroClip.length;
                }
            else
                VoiceManager.PlayRandomVoice(__instance, "Ferryman",
                    FerrymanCharacter.SpawnClips,
                    FerrymanCharacter.SpawnSubs
                );

            static IEnumerator PlayNoCoin(Ferryman ferry)
            {
                var src = VoiceManager.CreateVoiceSource(
                    ferry,
                    "FerrymanIntro",
                    FerrymanCharacter.BossIntroClip,
                    null,
                    true
                );

                if (src == null)
                    yield break;

                if (!src) yield break;
                VoiceManager.ShowSubtitle("Gabriel warned me of you and your likes", src, new Color(0f, 0.66f, 0.77f));

                yield return new WaitForSeconds(3f);

                if (!src) yield break;
                VoiceManager.ShowSubtitle("I will not make the same mistakes as him!", src, new Color(0f, 0.66f, 0.77f));
            }

            static IEnumerator PlayCoin(Ferryman ferry)
            {
                var src = VoiceManager.CreateVoiceSource(
                    ferry,
                    "FerrymanIntro",
                    FerrymanCharacter.CoinFightClip
                );

                VoiceManager.spawnVoiceEndTimes[ferry] = Time.time + FerrymanCharacter.CoinFightClip.length;

                if (src == null)
                    yield break;

                if (!src) yield break;
                VoiceManager.ShowSubtitle("You SCOUNDREL", src, new Color(0f, 0.66f, 0.77f));

                yield return new WaitForSeconds(1.5f);

                if (!src) yield break;
                VoiceManager.ShowSubtitle("I should have never trusted a machine like you!", src, new Color(0f, 0.66f, 0.77f));
            }
        }
    }

    [HarmonyPatch(typeof(FerrymanFake), "CoinCatch")]
    class FerrymanCoinDetectedPatch
    {
        static void Postfix(FerrymanFake __instance)
        {
            if (!UltraVoicePlugin.FerrymanVoiceEnabled.value) return;

            FerrymanCharacter.FerrymanCoinTossed = true;

            UltraVoicePlugin.Instance.StartCoroutine(Play(__instance));

            static IEnumerator Play(FerrymanFake ferry)
            {
                yield return new WaitForSeconds(0.25f);

                var src = VoiceManager.CreateVoiceSource(
                    ferry,
                    "FerrymanSkip",
                    FerrymanCharacter.CoinSkipClip,
                    "Hm?",
                    true
                );

                if (src == null)
                    yield break;

                yield return new WaitForSeconds(1.75f);

                if (!src) yield break;
                VoiceManager.ShowSubtitle("This shall do", src, new Color(0f, 0.66f, 0.77f));

                yield return new WaitForSeconds(1.5f);

                if (!src) yield break;
                VoiceManager.ShowSubtitle("You may pass", src, new Color(0f, 0.66f, 0.77f));
            }
        }
    }

    [HarmonyPatch(typeof(Ferryman), "Update")]
    class FerrymanChatterPatch
    {
        static void Postfix(Ferryman __instance)
        {
            if (!UltraVoicePlugin.FerrymanVoiceEnabled.value) return;

            if (ULTRAKILL.Cheats.BlindEnemies.Blind)
                return;

            if (__instance == null)
                return;

            if (__instance.currentWindup != null)
                return;

            if (!VoiceManager.CheckCooldown(__instance, 6f))
                return;

            if (FerrymanCharacter.IsAgonisOrRudraksha(__instance))
                return;

            if (VoiceManager.TooSoonAfterSpawn(__instance, 3f))
                return;

            if (Random.Range(0f, 1f) < 0.75f)
                return;

            VoiceManager.PlayRandomVoice(__instance, "Ferryman",
                FerrymanCharacter.ChatterClips,
                FerrymanCharacter.ChatterSubs
            );
        }
    }

    [HarmonyPatch(typeof(Ferryman), "PhaseChange")]
    class FerrymanPhaseChangePatch
    {
        static void Postfix(Ferryman __instance)
        {
            if (!UltraVoicePlugin.FerrymanVoiceEnabled.value) return;

            if (__instance == null)
                return;

            if (FerrymanCharacter.IsAgonisOrRudraksha(__instance))
                return;

            if (VoiceManager.TooSoonAfterSpawn(__instance, 0.25f))
                return;

            if (!FerrymanCharacter.FerrymanPhaseChangePlayed)
                UltraVoicePlugin.Instance.StartCoroutine(PlayPhaseChange(__instance));

            static IEnumerator PlayPhaseChange(Ferryman ferry)
            {
                new WaitForSeconds(0.1f);

                var src = VoiceManager.CreateVoiceSource(
                    ferry,
                    "FerrymanPhase",
                    FerrymanCharacter.PhaseChangeClip,
                    null,
                    true
                );

                FerrymanCharacter.FerrymanPhaseChangePlayed = true;

                VoiceManager.spawnVoiceEndTimes[ferry] = Time.time + FerrymanCharacter.BossIntroClip.length;

                yield return new WaitForSeconds(1f);

                VoiceManager.ShowSubtitle("I am not finished with you!", src, new Color(0f, 0.66f, 0.77f));
            }
        }
    }

    [HarmonyPatch(typeof(Ferryman), "OnGoLimp")]
    class FerrymanDeathPatch
    {
        static void Postfix(Ferryman __instance)
        {
            if (!UltraVoicePlugin.FerrymanVoiceEnabled.value) return;

            if (__instance == null)
                return;

            if (FerrymanCharacter.IsAgonisOrRudraksha(__instance))
                return;

            VoiceManager.PlayRandomVoice(__instance, "Ferryman",
                FerrymanCharacter.DeathClips,
                null,
                true
            );
        }
    }

    [HarmonyPatch(typeof(FerrymanFake), "Update")]
    class FerrymanFakeProximityPatch
    {
        static void Postfix(FerrymanFake __instance)
        {
            if (!UltraVoicePlugin.FerrymanVoiceEnabled.value) return;

            if (__instance == null)
                return;

            if (StatsManager.Instance.restarts > 0)
                return;

            var player = MonoSingleton<NewMovement>.Instance;

            int id = __instance.GetInstanceID();

            float dist = UnityEngine.Vector3.Distance(
                __instance.transform.position,
                player.transform.position
            );

            if (dist > 60f)
                return;

            if (!VoiceManager.CheckCooldown(__instance, 999f))
                return;

            VoiceManager.CreateVoiceSource(
                __instance,
                "FerrymanApproach",
                FerrymanCharacter.ApproachClip,
                "Who goes there?",
                subtitleColor: new Color(0f, 0.66f, 0.77f)
            );
        }
    }
}