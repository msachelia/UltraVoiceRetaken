using HarmonyLib;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UltraVoice.Utilities;

namespace UltraVoice.Characters
{
    public class FerrymanCharacter
    {

        public static AudioClip BossIntroClip;
        public static AudioClip CoinSkipClip;
        public static AudioClip CoinFightClip;
        public static AudioClip PhaseChangeClip;
        public static AudioClip ApproachClip;
        public static AudioClip AgonisIntroClip;
        public static AudioClip RudrakshaIntroClip;
        public static AudioClip AgonisDownedClip;
        public static AudioClip RudrakshaDownedClip;

        public static AudioClip[] SpawnClips;
        public static AudioClip[] ChatterClips;
        public static AudioClip[] DeathClips;

        public static AudioClip BossIntroClipSoil;
        public static AudioClip CoinSkipClipSoil;
        public static AudioClip CoinFightClipSoil;
        public static AudioClip PhaseChangeClipSoil;
        public static AudioClip ApproachClipSoil;
        public static AudioClip AgonisIntroClipSoil;
        public static AudioClip RudrakshaIntroClipSoil;
        public static AudioClip AgonisDownedClipSoil;
        public static AudioClip RudrakshaDownedClipSoil;

        public static AudioClip[] SpawnClipsSoil;
        public static AudioClip[] ChatterClipsSoil;
        public static AudioClip[] DeathClipsSoil;

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

        public const string FerrymanBossSceneName = "e964cf0ffaa9e0e4e940b5c738983796";

        public static Color FerrymanColor => VoiceManager.GetEnemyTypeColor(EnemyType.Ferryman);

        public static bool FerrymanCoinTossed = false;
        public static bool FerrymanPhaseChangePlayed = false;

        public static bool IsAgonisOrRudraksha(Ferryman ferryman)
        {
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

            return eid.mirrorOnly;
        }

        public static bool IsRudraksha(Ferryman ferryman)
        {
            if (!IsAgonisOrRudraksha(ferryman))
                return false;

            EnemyIdentifier eid = ferryman.GetComponent<EnemyIdentifier>();

            return !eid.mirrorOnly;
        }

        public static AudioClip UseFerrymanClip(AudioClip melClip, AudioClip soilClip)
        {
            return UltraVoicePlugin.FerrymanVoiceActorField != null && UltraVoicePlugin.FerrymanVoiceActorField.value == UltraVoicePlugin.FerrymanVoiceActor.Soil
                ? soilClip
                : melClip;
        }

        public static AudioClip[] UseFerrymanClips(AudioClip[] melClips, AudioClip[] soilClips)
        {
            return UltraVoicePlugin.FerrymanVoiceActorField != null && UltraVoicePlugin.FerrymanVoiceActorField.value == UltraVoicePlugin.FerrymanVoiceActor.Soil
                ? soilClips
                : melClips;
        }

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            BossIntroClip = UltraVoicePlugin.LoadClip("Ferryman.ferry_FightStarted.wav");
            CoinSkipClip = UltraVoicePlugin.LoadClip("Ferryman.Ferry_CoinSkip.wav");
            CoinFightClip = UltraVoicePlugin.LoadClip("Ferryman.ferry_CoinFight.wav");
            PhaseChangeClip = UltraVoicePlugin.LoadClip("Ferryman.ferry_PhaseChange.wav");
            ApproachClip = UltraVoicePlugin.LoadClip("Ferryman.ferry_Approach.wav");
            AgonisIntroClip = UltraVoicePlugin.LoadClip("Ferryman.ferry_SpawnSpecialAgonis.wav");
            RudrakshaIntroClip = UltraVoicePlugin.LoadClip("Ferryman.ferry_SpawnSpecialRudraksha.wav");
            AgonisDownedClip = UltraVoicePlugin.LoadClip("Ferryman.ferry_DownedAgonis.wav");
            RudrakshaDownedClip = UltraVoicePlugin.LoadClip("Ferryman.ferry_DownedRudraksha.wav");

            SpawnClips = UltraVoicePlugin.LoadClips("Ferryman.ferry_Spawn{0}.wav", 5);
            ChatterClips = UltraVoicePlugin.LoadClips("Ferryman.Ferry_Chatter{0}.wav", 4);
            DeathClips = UltraVoicePlugin.LoadClips("Ferryman.ferry_Death{0}.wav", 3);

            BossIntroClipSoil = UltraVoicePlugin.LoadClip("Ferryman.ferry_FightStartedSoil.wav");
            CoinSkipClipSoil = UltraVoicePlugin.LoadClip("Ferryman.Ferry_CoinSkipSoil.wav");
            CoinFightClipSoil = UltraVoicePlugin.LoadClip("Ferryman.ferry_CoinFightSoil.wav");
            PhaseChangeClipSoil = UltraVoicePlugin.LoadClip("Ferryman.ferry_PhaseChangeSoil.wav");
            ApproachClipSoil = UltraVoicePlugin.LoadClip("Ferryman.ferry_ApproachSoil.wav");
            AgonisIntroClipSoil = UltraVoicePlugin.LoadClip("Ferryman.ferry_SpawnSpecialAgonisSoil.wav");
            RudrakshaIntroClipSoil = UltraVoicePlugin.LoadClip("Ferryman.ferry_SpawnSpecialRudrakshaSoil.wav");
            AgonisDownedClipSoil = UltraVoicePlugin.LoadClip("Ferryman.ferry_DownedAgonisSoil.wav");
            RudrakshaDownedClipSoil = UltraVoicePlugin.LoadClip("Ferryman.ferry_DownedRudrakshaSoil.wav");

            SpawnClipsSoil = UltraVoicePlugin.LoadClips("Ferryman.ferry_Spawn{0}Soil.wav", 5);
            ChatterClipsSoil = UltraVoicePlugin.LoadClips("Ferryman.Ferry_Chatter{0}Soil.wav", 4);
            DeathClipsSoil = UltraVoicePlugin.LoadClips("Ferryman.ferry_Death{0}Soil.wav", 3);

            logger.LogInfo("Ferryman voice lines loaded successfully!");
        }

        public static void PlayScriptedLine(Component enemy, string sourceName, AudioClip clip, string firstSub, bool interrupt, float startDelay, params (float delay, string text)[] followUps)
        {
            UltraVoicePlugin.Instance.StartCoroutine(Routine());

            IEnumerator Routine()
            {
                if (startDelay > 0f)
                    yield return new WaitForSeconds(startDelay);

                if (enemy == null)
                    yield break;

                var src = VoiceManager.CreateVoiceSource(enemy, sourceName, clip, firstSub, interrupt, FerrymanColor);

                if (src == null)
                    yield break;

                foreach (var (delay, text) in followUps)
                {
                    yield return new WaitForSeconds(delay);

                    if (!src)
                        yield break;

                    VoiceManager.ShowSubtitle(text, src, FerrymanColor);
                }
            }
        }
    }

    [HarmonyPatch(typeof(Ferryman), "Start")]
    class FerrymanSpawnPatch
    {
        static void Postfix(Ferryman __instance)
        {
            if (!UltraVoicePlugin.FerrymanVoiceEnabled.value) return;

            if (__instance == null)
                return;

            VoiceManager.enemySpawnTimes[__instance] = Time.time;

            FerrymanCharacter.FerrymanPhaseChangePlayed = false;

            if (SceneManager.GetActiveScene().name == FerrymanCharacter.FerrymanBossSceneName)
            {
                if (FerrymanCharacter.FerrymanCoinTossed)
                {
                    AudioClip clip = FerrymanCharacter.UseFerrymanClip(FerrymanCharacter.CoinFightClip, FerrymanCharacter.CoinFightClipSoil);

                    VoiceManager.spawnVoiceEndTimes[__instance] = Time.time + clip.length;

                    FerrymanCharacter.PlayScriptedLine(__instance, "FerrymanIntro", clip,
                        "You WRETCH", false, 0f,
                        (1.5f, "I granted you passage, and you repay me with DECEIT!?")
                    );
                }
                else
                {
                    AudioClip clip = FerrymanCharacter.UseFerrymanClip(FerrymanCharacter.BossIntroClip, FerrymanCharacter.BossIntroClipSoil);

                    VoiceManager.spawnVoiceEndTimes[__instance] = Time.time + clip.length;

                    FerrymanCharacter.PlayScriptedLine(__instance, "FerrymanIntro", clip,
                        "Gabriel warned me of your kind,", true, 0f,
                        (2.75f, "I will not share his failure.")
                    );
                }
            }
            else if (!FerrymanCharacter.IsAgonisOrRudraksha(__instance))
                VoiceManager.PlayRandomVoice(__instance, "Ferryman",
                    FerrymanCharacter.UseFerrymanClips(FerrymanCharacter.SpawnClips, FerrymanCharacter.SpawnClipsSoil),
                    FerrymanCharacter.SpawnSubs,
                    randomPitch: true
                );
            else if (FerrymanCharacter.IsAgonis(__instance))
                VoiceManager.CreateVoiceSource(__instance, "Ferryman",
                    FerrymanCharacter.UseFerrymanClip(FerrymanCharacter.AgonisIntroClip, FerrymanCharacter.AgonisIntroClipSoil)
                );
            else if (FerrymanCharacter.IsRudraksha(__instance))
                VoiceManager.CreateVoiceSource(__instance, "Ferryman",
                    FerrymanCharacter.UseFerrymanClip(FerrymanCharacter.RudrakshaIntroClip, FerrymanCharacter.RudrakshaIntroClipSoil),
                    "Leave us alone!"
                );
        }
    }

    [HarmonyPatch(typeof(FerrymanFake), "CoinCatch")]
    class FerrymanCoinDetectedPatch
    {
        static void Postfix(FerrymanFake __instance)
        {
            if (!UltraVoicePlugin.FerrymanVoiceEnabled.value) return;

            FerrymanCharacter.FerrymanCoinTossed = true;

            FerrymanCharacter.PlayScriptedLine(__instance, "FerrymanSkip",
                FerrymanCharacter.UseFerrymanClip(FerrymanCharacter.CoinSkipClip, FerrymanCharacter.CoinSkipClipSoil),
                "Hm?", true, 0.25f,
                (1.5f, "This shall do."),
                (1.5f, "You may pass.")
            );
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

            if (FerrymanCharacter.IsAgonis(__instance))
                return;

            if (VoiceManager.TooSoonAfterSpawn(__instance, 3f))
                return;

            if (Random.Range(0f, 1f) < 0.75f)
                return;

            VoiceManager.PlayRandomVoice(__instance, "Ferryman",
                FerrymanCharacter.UseFerrymanClips(FerrymanCharacter.ChatterClips, FerrymanCharacter.ChatterClipsSoil),
                FerrymanCharacter.ChatterSubs,
                randomPitch: true
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

            if (FerrymanCharacter.FerrymanPhaseChangePlayed)
                return;

            FerrymanCharacter.FerrymanPhaseChangePlayed = true;

            FerrymanCharacter.PlayScriptedLine(__instance, "FerrymanPhase",
                FerrymanCharacter.UseFerrymanClip(FerrymanCharacter.PhaseChangeClip, FerrymanCharacter.PhaseChangeClipSoil),
                null, true, 0f,
                (1.5f, "I am not finished with you!")
            );
        }
    }

    [HarmonyPatch(typeof(Ferryman), "OnDeath")]
    class FerrymanDeathPatch
    {
        static void Prefix(Ferryman __instance)
        {
            if (!UltraVoicePlugin.FerrymanVoiceEnabled.value) return;

            if (FerrymanCharacter.IsAgonis(__instance))
                return;

            VoiceManager.PlayRandomVoice(__instance, "Ferryman",
                FerrymanCharacter.UseFerrymanClips(FerrymanCharacter.DeathClips, FerrymanCharacter.DeathClipsSoil),
                null,
                true,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(Ferryman), "Knockdown")]
    class FerrymanKnockdownSpecialPatch
    {
        static void Postfix(Ferryman __instance)
        {
            if (!UltraVoicePlugin.FerrymanVoiceEnabled.value) return;

            if (FerrymanCharacter.IsAgonis(__instance))
                VoiceManager.CreateVoiceSource(__instance, "Ferryman",
                    FerrymanCharacter.UseFerrymanClip(FerrymanCharacter.AgonisDownedClip, FerrymanCharacter.AgonisDownedClipSoil)
                );
            else if (FerrymanCharacter.IsRudraksha(__instance))
                FerrymanCharacter.PlayScriptedLine(__instance, "FerrymanDowned",
                    FerrymanCharacter.UseFerrymanClip(FerrymanCharacter.RudrakshaDownedClip, FerrymanCharacter.RudrakshaDownedClipSoil),
                    null, true, 0f,
                    (1.5f, "No, not now...")
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

            float dist = Vector3.Distance(
                __instance.transform.position,
                MonoSingleton<NewMovement>.Instance.transform.position
            );

            if (dist > 60f)
                return;

            if (!VoiceManager.CheckCooldown(__instance, 999f))
                return;

            VoiceManager.CreateVoiceSource(
                __instance,
                "FerrymanApproach",
                FerrymanCharacter.UseFerrymanClip(FerrymanCharacter.ApproachClip, FerrymanCharacter.ApproachClipSoil),
                "Who goes there?",
                subtitleColor: FerrymanCharacter.FerrymanColor
            );
        }
    }
}
