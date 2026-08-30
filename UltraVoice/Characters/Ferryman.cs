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
        public static AudioClip BossIntroClipNicky;
        public static AudioClip CoinSkipClipSoil;
        public static AudioClip CoinSkipClipNicky;
        public static AudioClip CoinFightClipSoil;
        public static AudioClip CoinFightClipNicky;
        public static AudioClip PhaseChangeClipSoil;
        public static AudioClip PhaseChangeClipNicky;
        public static AudioClip ApproachClipSoil;
        public static AudioClip ApproachClipNicky;
        public static AudioClip AgonisIntroClipSoil;
        public static AudioClip AgonisIntroClipNicky;
        public static AudioClip RudrakshaIntroClipSoil;
        public static AudioClip RudrakshaIntroClipNicky;
        public static AudioClip AgonisDownedClipSoil;
        public static AudioClip AgonisDownedClipNicky;
        public static AudioClip RudrakshaDownedClipSoil;
        public static AudioClip RudrakshaDownedClipNicky;

        public static AudioClip[] SpawnClipsSoil;
        public static AudioClip[] SpawnClipsNicky;
        public static AudioClip[] ChatterClipsSoil;
        public static AudioClip[] ChatterClipsNicky;
        public static AudioClip[] DeathClipsSoil;
        public static AudioClip[] DeathClipsNicky;

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

        public static UltraVoicePlugin.FerrymanVoiceActor CurrentActor =>
            UltraVoicePlugin.FerrymanVoiceActorField != null
                ? UltraVoicePlugin.FerrymanVoiceActorField.Value
                : UltraVoicePlugin.FerrymanVoiceActor.Mel;

        public static AudioClip UseFerrymanClip(AudioClip melClip, AudioClip soilClip, AudioClip nickyClip)
        {
            switch (CurrentActor)
            {
                case UltraVoicePlugin.FerrymanVoiceActor.Soil:
                    return soilClip;
                case UltraVoicePlugin.FerrymanVoiceActor.Nicky:
                    return nickyClip;
                default:
                    return melClip;
            }
        }

        public static AudioClip[] UseFerrymanClips(AudioClip[] melClips, AudioClip[] soilClips, AudioClip[] nickyClips)
        {
            switch (CurrentActor)
            {
                case UltraVoicePlugin.FerrymanVoiceActor.Soil:
                    return soilClips;
                case UltraVoicePlugin.FerrymanVoiceActor.Nicky:
                    return nickyClips;
                default:
                    return melClips;
            }
        }

        public static float UseFerrymanDelay(float melDelay, float soilDelay, float nickyDelay)
        {
            switch (CurrentActor)
            {
                case UltraVoicePlugin.FerrymanVoiceActor.Soil:
                    return soilDelay;
                case UltraVoicePlugin.FerrymanVoiceActor.Nicky:
                    return nickyDelay;
                default:
                    return melDelay;
            }
        }

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            BossIntroClip = UltraVoicePlugin.LoadClip("Ferryman.Mel.ferry_FightStarted.wav");
            CoinSkipClip = UltraVoicePlugin.LoadClip("Ferryman.Mel.Ferry_CoinSkip.wav");
            CoinFightClip = UltraVoicePlugin.LoadClip("Ferryman.Mel.ferry_CoinFight.wav");
            PhaseChangeClip = UltraVoicePlugin.LoadClip("Ferryman.Mel.ferry_PhaseChange.wav");
            ApproachClip = UltraVoicePlugin.LoadClip("Ferryman.Mel.ferry_Approach.wav");
            AgonisIntroClip = UltraVoicePlugin.LoadClip("Ferryman.Mel.ferry_SpawnSpecialAgonis.wav");
            RudrakshaIntroClip = UltraVoicePlugin.LoadClip("Ferryman.Mel.ferry_SpawnSpecialRudraksha.wav");
            AgonisDownedClip = UltraVoicePlugin.LoadClip("Ferryman.Mel.ferry_DownedAgonis.wav");
            RudrakshaDownedClip = UltraVoicePlugin.LoadClip("Ferryman.Mel.ferry_DownedRudraksha.wav");

            SpawnClips = UltraVoicePlugin.LoadClips("Ferryman.Mel.ferry_Spawn{0}.wav", 5);
            ChatterClips = UltraVoicePlugin.LoadClips("Ferryman.Mel.Ferry_Chatter{0}.wav", 4);
            DeathClips = UltraVoicePlugin.LoadClips("Ferryman.Mel.ferry_Death{0}.wav", 3);

            BossIntroClipSoil = UltraVoicePlugin.LoadClip("Ferryman.Soil.ferry_FightStarted.wav");
            BossIntroClipNicky = UltraVoicePlugin.LoadClip("Ferryman.Nicky.ferry_FightStarted.wav");
            CoinSkipClipSoil = UltraVoicePlugin.LoadClip("Ferryman.Soil.Ferry_CoinSkip.wav");
            CoinSkipClipNicky = UltraVoicePlugin.LoadClip("Ferryman.Nicky.Ferry_CoinSkip.wav");
            CoinFightClipSoil = UltraVoicePlugin.LoadClip("Ferryman.Soil.ferry_CoinFight.wav");
            CoinFightClipNicky = UltraVoicePlugin.LoadClip("Ferryman.Nicky.ferry_CoinFight.wav");
            PhaseChangeClipSoil = UltraVoicePlugin.LoadClip("Ferryman.Soil.ferry_PhaseChange.wav");
            PhaseChangeClipNicky = UltraVoicePlugin.LoadClip("Ferryman.Nicky.ferry_PhaseChange.wav");
            ApproachClipSoil = UltraVoicePlugin.LoadClip("Ferryman.Soil.ferry_Approach.wav");
            ApproachClipNicky = UltraVoicePlugin.LoadClip("Ferryman.Nicky.ferry_Approach.wav");
            AgonisIntroClipSoil = UltraVoicePlugin.LoadClip("Ferryman.Soil.ferry_SpawnSpecialAgonis.wav");
            AgonisIntroClipNicky = UltraVoicePlugin.LoadClip("Ferryman.Nicky.ferry_SpawnSpecialAgonis.wav");
            RudrakshaIntroClipSoil = UltraVoicePlugin.LoadClip("Ferryman.Soil.ferry_SpawnSpecialRudraksha.wav");
            RudrakshaIntroClipNicky = UltraVoicePlugin.LoadClip("Ferryman.Nicky.ferry_SpawnSpecialRudraksha.wav");
            AgonisDownedClipSoil = UltraVoicePlugin.LoadClip("Ferryman.Soil.ferry_DownedAgonis.wav");
            AgonisDownedClipNicky = UltraVoicePlugin.LoadClip("Ferryman.Nicky.ferry_DownedAgonis.wav");
            RudrakshaDownedClipSoil = UltraVoicePlugin.LoadClip("Ferryman.Soil.ferry_DownedRudraksha.wav");
            RudrakshaDownedClipNicky = UltraVoicePlugin.LoadClip("Ferryman.Nicky.ferry_DownedRudraksha.wav");

            SpawnClipsSoil = UltraVoicePlugin.LoadClips("Ferryman.Soil.ferry_Spawn{0}.wav", 5);
            SpawnClipsNicky = UltraVoicePlugin.LoadClips("Ferryman.Nicky.ferry_Spawn{0}.wav", 5);
            ChatterClipsSoil = UltraVoicePlugin.LoadClips("Ferryman.Soil.Ferry_Chatter{0}.wav", 4);
            ChatterClipsNicky = UltraVoicePlugin.LoadClips("Ferryman.Nicky.Ferry_Chatter{0}.wav", 4);
            DeathClipsSoil = UltraVoicePlugin.LoadClips("Ferryman.Soil.ferry_Death{0}.wav", 3);
            DeathClipsNicky = UltraVoicePlugin.LoadClips("Ferryman.Nicky.ferry_Death{0}.wav", 3);

            logger.LogInfo("Ferryman voice lines loaded successfully!");
        }

        public static void PlayScriptedLine(Component enemy, string sourceName, AudioClip clip, string firstSub, bool interrupt, float startDelay, params (float melDelay, float soilDelay, float nickyDelay, string text)[] followUps)
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

                foreach (var (melDelay, soilDelay, nickyDelay, text) in followUps)
                {
                    yield return new WaitForSeconds(UseFerrymanDelay(melDelay, soilDelay, nickyDelay));

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
            if (!UltraVoicePlugin.FerrymanVoiceEnabled.Value) return;

            if (__instance == null)
                return;

            VoiceManager.enemySpawnTimes[__instance] = Time.time;

            FerrymanCharacter.FerrymanPhaseChangePlayed = false;

            if (SceneManager.GetActiveScene().name == FerrymanCharacter.FerrymanBossSceneName)
            {
                if (FerrymanCharacter.FerrymanCoinTossed)
                {
                    AudioClip clip = FerrymanCharacter.UseFerrymanClip(FerrymanCharacter.CoinFightClip, FerrymanCharacter.CoinFightClipSoil, FerrymanCharacter.CoinFightClipNicky);

                    VoiceManager.spawnVoiceEndTimes[__instance] = Time.time + clip.length;

                    FerrymanCharacter.PlayScriptedLine(__instance, "FerrymanIntro", clip,
                        "You WRETCH", false, 0f,
                        (1.2f, 1.25f, 1.5f, "I granted you passage, and you repay me with DECEIT!?")
                    );
                }
                else
                {
                    AudioClip clip = FerrymanCharacter.UseFerrymanClip(FerrymanCharacter.BossIntroClip, FerrymanCharacter.BossIntroClipSoil, FerrymanCharacter.BossIntroClipNicky);

                    VoiceManager.spawnVoiceEndTimes[__instance] = Time.time + clip.length;

                    FerrymanCharacter.PlayScriptedLine(__instance, "FerrymanIntro", clip,
                        "Gabriel warned me of your kind,", true, 0f,
                        (2.85f, 2.9f, 2.5f, "I will not share his failure.")
                    );
                }
            }
            else if (!FerrymanCharacter.IsAgonisOrRudraksha(__instance))
                VoiceManager.PlayRandomVoice(__instance, "Ferryman",
                    FerrymanCharacter.UseFerrymanClips(FerrymanCharacter.SpawnClips, FerrymanCharacter.SpawnClipsSoil, FerrymanCharacter.SpawnClipsNicky),
                    FerrymanCharacter.SpawnSubs,
                    randomPitch: true
                );
            else if (FerrymanCharacter.IsAgonis(__instance))
                VoiceManager.CreateVoiceSource(__instance, "Ferryman",
                    FerrymanCharacter.UseFerrymanClip(FerrymanCharacter.AgonisIntroClip, FerrymanCharacter.AgonisIntroClipSoil, FerrymanCharacter.AgonisIntroClipNicky)
                );
            else if (FerrymanCharacter.IsRudraksha(__instance))
                VoiceManager.CreateVoiceSource(__instance, "Ferryman",
                    FerrymanCharacter.UseFerrymanClip(FerrymanCharacter.RudrakshaIntroClip, FerrymanCharacter.RudrakshaIntroClipSoil, FerrymanCharacter.RudrakshaIntroClipNicky),
                    "Leave us alone!"
                );
        }
    }

    [HarmonyPatch(typeof(FerrymanFake), "CoinCatch")]
    class FerrymanCoinDetectedPatch
    {
        static void Postfix(FerrymanFake __instance)
        {
            if (!UltraVoicePlugin.FerrymanVoiceEnabled.Value) return;

            FerrymanCharacter.FerrymanCoinTossed = true;

            FerrymanCharacter.PlayScriptedLine(__instance, "FerrymanSkip",
                FerrymanCharacter.UseFerrymanClip(FerrymanCharacter.CoinSkipClip, FerrymanCharacter.CoinSkipClipSoil, FerrymanCharacter.CoinSkipClipNicky),
                "Hm?", true, 0.25f,
                (1.35f, 1.35f, 1.2f, "This shall do."),
                (1.6f, 1.65f, 1.85f, "You may pass.")
            );
        }
    }

    [HarmonyPatch(typeof(Ferryman), "Update")]
    class FerrymanChatterPatch
    {
        static void Postfix(Ferryman __instance)
        {
            if (!UltraVoicePlugin.FerrymanVoiceEnabled.Value) return;

            if (ULTRAKILL.Cheats.BlindEnemies.Blind)
                return;

            if (__instance == null)
                return;

            if (!VoiceManager.CheckCooldown(__instance, 5f))
                return;

            if (FerrymanCharacter.IsAgonis(__instance))
                return;

            VoiceManager.PlayRandomVoice(__instance, "Ferryman",
                FerrymanCharacter.UseFerrymanClips(FerrymanCharacter.ChatterClips, FerrymanCharacter.ChatterClipsSoil, FerrymanCharacter.ChatterClipsNicky),
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
            if (!UltraVoicePlugin.FerrymanVoiceEnabled.Value) return;

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
                FerrymanCharacter.UseFerrymanClip(FerrymanCharacter.PhaseChangeClip, FerrymanCharacter.PhaseChangeClipSoil, FerrymanCharacter.PhaseChangeClipNicky),
                null, true, 0f,
                (1.4f, 1.3f, 1f, "I am not finished with you!")
            );
        }
    }

    [HarmonyPatch(typeof(Ferryman), "OnDeath")]
    class FerrymanDeathPatch
    {
        static void Prefix(Ferryman __instance)
        {
            if (!UltraVoicePlugin.FerrymanVoiceEnabled.Value) return;

            if (FerrymanCharacter.IsAgonis(__instance))
                return;

            VoiceManager.PlayRandomVoice(__instance, "Ferryman",
                FerrymanCharacter.UseFerrymanClips(FerrymanCharacter.DeathClips, FerrymanCharacter.DeathClipsSoil, FerrymanCharacter.DeathClipsNicky),
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
            if (!UltraVoicePlugin.FerrymanVoiceEnabled.Value) return;

            if (FerrymanCharacter.IsAgonis(__instance))
                VoiceManager.CreateVoiceSource(__instance, "Ferryman",
                    FerrymanCharacter.UseFerrymanClip(FerrymanCharacter.AgonisDownedClip, FerrymanCharacter.AgonisDownedClipSoil, FerrymanCharacter.AgonisDownedClipNicky)
                );
            else if (FerrymanCharacter.IsRudraksha(__instance))
                FerrymanCharacter.PlayScriptedLine(__instance, "FerrymanDowned",
                    FerrymanCharacter.UseFerrymanClip(FerrymanCharacter.RudrakshaDownedClip, FerrymanCharacter.RudrakshaDownedClipSoil, FerrymanCharacter.RudrakshaDownedClipNicky),
                    null, true, 0f,
                    (1.25f, 1.35f, 1.25f, "No, not now...")
                );
        }
    }

    [HarmonyPatch(typeof(FerrymanFake), "Update")]
    class FerrymanFakeProximityPatch
    {
        static void Postfix(FerrymanFake __instance)
        {
            if (!UltraVoicePlugin.FerrymanVoiceEnabled.Value) return;

            if (__instance == null)
                return;

            StatsManager stats = MonoSingleton<StatsManager>.Instance;

            if (stats == null || stats.restarts > 0)
                return;

            NewMovement player = MonoSingleton<NewMovement>.Instance;

            if (player == null)
                return;

            float dist = Vector3.Distance(
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
                FerrymanCharacter.UseFerrymanClip(FerrymanCharacter.ApproachClip, FerrymanCharacter.ApproachClipSoil, FerrymanCharacter.ApproachClipNicky),
                "Who goes there?",
                subtitleColor: FerrymanCharacter.FerrymanColor
            );
        }
    }
}
