using HarmonyLib;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UltraVoice.Utilities;

namespace UltraVoice.Characters
{
    public class V2Character
    {

        public static AudioClip IntroFirstClip;
        public static AudioClip IntroFirstRestartClip;
        public static AudioClip IntroSecondClip;
        public static AudioClip IntroSecondRestartClip;
        public static AudioClip FastDefeatClip;
        public static AudioClip DefeatClip;
        public static AudioClip DeathClip;
        public static AudioClip FlailingClip;
        public static AudioClip EnragePatienceClip;
        public static AudioClip EnragePunchedClip;
        public static AudioClip EscapingClip;

        public static AudioClip[] ChatterClips;
        public static AudioClip[] ChatterPissedClips;
        public static AudioClip[] PainClips;

        public static AudioClip IntroFirstClipCazsu;
        public static AudioClip IntroFirstRestartClipCazsu;
        public static AudioClip IntroSecondClipCazsu;
        public static AudioClip IntroSecondRestartClipCazsu;
        public static AudioClip FastDefeatClipCazsu;
        public static AudioClip DefeatClipCazsu;
        public static AudioClip DeathClipCazsu;
        public static AudioClip FlailingClipCazsu;
        public static AudioClip EnragePatienceClipCazsu;
        public static AudioClip EnragePunchedClipCazsu;
        public static AudioClip EscapingClipCazsu;

        public static AudioClip[] ChatterClipsCazsu;
        public static AudioClip[] ChatterPissedClipsCazsu;
        public static AudioClip[] PainClipsCazsu;

        public static readonly string[] ChatterSubs =
        {
            "Do keep up with me.",
            "You can do better than that.",
            "Is this your best effort?",
            "Keep your eye open now!",
            "Mind your footing!",
        };

        public static readonly string[] ChatterPissedSubs =
        {
            "What's the matter? Does your arm hurt!?",
            "You really think you can kill me now!?",
            "I won't lose to you again!",
            "I'll make quick work of you this time!",
            "You don't stand a chance against me!"
        };

        public static Color V2Color => VoiceManager.GetEnemyTypeColor(EnemyType.V2);

        public const string LimboClimaxSceneName = "36abcaae9708abc4d9e89e6ec73a2846";
        public const string GreedClimaxSceneName = "ac1675e648695a343bd064c6d0c56e57";

        public static float V2IntroTime = -999f;
        public static bool V2CutsceneVoicePlayed = false;
        public static bool V2SecondVoiceRestartPlayed = false;
        public static bool V2DeathPlayed = false;

        public static AudioClip UseV2Clip(AudioClip rubyClip, AudioClip caszuClip)
        {
            return UltraVoicePlugin.V2VoiceActorField != null && UltraVoicePlugin.V2VoiceActorField.value == UltraVoicePlugin.V2VoiceActor.Cazsu
                ? caszuClip
                : rubyClip;
        }

        public static AudioClip[] UseV2Clips(AudioClip[] rubyClips, AudioClip[] caszuClips)
        {
            return UltraVoicePlugin.V2VoiceActorField != null && UltraVoicePlugin.V2VoiceActorField.value == UltraVoicePlugin.V2VoiceActor.Cazsu
                ? caszuClips
                : rubyClips;
        }

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            IntroFirstClip = UltraVoicePlugin.LoadClip("V2.v2_IntroFirst.wav");
            IntroFirstRestartClip = UltraVoicePlugin.LoadClip("V2.v2_RestartIntroFirst.wav");
            IntroSecondClip = UltraVoicePlugin.LoadClip("V2.v2_IntroSecond.wav");
            IntroSecondRestartClip = UltraVoicePlugin.LoadClip("V2.v2_RestartIntroSecond.wav");

            FastDefeatClip = UltraVoicePlugin.LoadClip("V2.v2_FastDefeat.wav");
            DefeatClip = UltraVoicePlugin.LoadClip("V2.v2_Defeat.wav");
            DeathClip = UltraVoicePlugin.LoadClip("V2.v2_Death.wav");
            FlailingClip = UltraVoicePlugin.LoadClip("V2.v2_Flailing.wav");
            EnragePatienceClip = UltraVoicePlugin.LoadClip("V2.v2_EnragePatience.wav");
            EnragePunchedClip = UltraVoicePlugin.LoadClip("V2.v2_EnragePunched.wav");
            EscapingClip = UltraVoicePlugin.LoadClip("V2.v2_Escaping.wav");

            ChatterClips = UltraVoicePlugin.LoadClips("V2.v2_Chatter{0}.wav", 5);
            ChatterPissedClips = UltraVoicePlugin.LoadClips("V2.v2_ChatterPissed{0}.wav", 5);
            PainClips = UltraVoicePlugin.LoadClips("V2.v2_Pain{0}.wav", 5);

            IntroFirstClipCazsu = UltraVoicePlugin.LoadClip("V2.v2_IntroFirstCazsu.wav");
            IntroFirstRestartClipCazsu = UltraVoicePlugin.LoadClip("V2.v2_RestartIntroFirstCazsu.wav");
            IntroSecondClipCazsu = UltraVoicePlugin.LoadClip("V2.v2_IntroSecondCazsu.wav");
            IntroSecondRestartClipCazsu = UltraVoicePlugin.LoadClip("V2.v2_RestartIntroSecondCazsu.wav");

            FastDefeatClipCazsu = UltraVoicePlugin.LoadClip("V2.v2_FastDefeatCazsu.wav");
            DefeatClipCazsu = UltraVoicePlugin.LoadClip("V2.v2_DefeatCazsu.wav");
            DeathClipCazsu = UltraVoicePlugin.LoadClip("V2.v2_DeathCazsu.wav");
            FlailingClipCazsu = UltraVoicePlugin.LoadClip("V2.v2_FlailingCazsu.wav");
            EnragePatienceClipCazsu = UltraVoicePlugin.LoadClip("V2.v2_EnragePatienceCazsu.wav");
            EnragePunchedClipCazsu = UltraVoicePlugin.LoadClip("V2.v2_EnragePunchedCazsu.wav");
            EscapingClipCazsu = UltraVoicePlugin.LoadClip("V2.v2_EscapingCazsu.wav");

            ChatterClipsCazsu = UltraVoicePlugin.LoadClips("V2.v2_ChatterCazsu{0}.wav", 5);
            ChatterPissedClipsCazsu = UltraVoicePlugin.LoadClips("V2.v2_ChatterPissedCazsu{0}.wav", 5);
            PainClipsCazsu = UltraVoicePlugin.LoadClips("V2.v2_PainCazsu{0}.wav", 5);

            logger.LogInfo("V2 voice lines loaded successfully!");
        }
    }

    [HarmonyPatch(typeof(V2), "Start")]
    class V2IntroFirstPatch
    {
        static void Postfix(V2 __instance)
        {
            if (StatsManager.Instance.restarts >= 1)
                return;

            if (!UltraVoicePlugin.V2VoiceEnabled.value)
                return;

            if (__instance.secondEncounter)
                return;

            if (SceneManager.GetActiveScene().name != V2Character.LimboClimaxSceneName)
                return;

            if (!__instance.inIntro)
                return;

            VoiceManager.enemySpawnTimes[__instance] = Time.time;
            UltraVoicePlugin.Instance.StartCoroutine(PlayIntroFirst(__instance));
        }

        static IEnumerator PlayIntroFirst(V2 v2)
        {
            yield return new WaitForSeconds(0.9f);

            AudioClip clip = V2Character.UseV2Clip(V2Character.IntroFirstClip, V2Character.IntroFirstClipCazsu);

            var src = VoiceManager.CreateVoiceSource(
                v2,
                "V2Intro",
                clip
            );

            if (src == null)
                yield break;

            V2Character.V2IntroTime = Time.time;
            VoiceManager.spawnVoiceEndTimes[v2] = Time.time + clip.length;

            VoiceManager.ShowSubtitle("So, you're my predecessor.", src, V2Character.V2Color);

            yield return new WaitForSeconds(2.25f);

            if (v2 == null || !v2.inIntro)
                yield break;

            VoiceManager.ShowSubtitle("How quaint...", src, V2Character.V2Color);

            yield return new WaitForSeconds(1.25f);

            if (v2 == null || !v2.inIntro)
                yield break;

            VoiceManager.ShowSubtitle("I suppose I'll have to show you what an upgrade looks like.", src, V2Character.V2Color);
        }
    }

    [HarmonyPatch(typeof(V2), "Start")]
    class V2IntroRetrySectionPatch
    {
        static void Postfix(V2 __instance)
        {
            if (StatsManager.Instance.restarts == 0)
                return;

            if (!UltraVoicePlugin.V2VoiceEnabled.value)
                return;

            if (!__instance.secondEncounter)
                UltraVoicePlugin.Instance.StartCoroutine(PlayRestartFirst(__instance));
            else if (!V2Character.V2SecondVoiceRestartPlayed)
                UltraVoicePlugin.Instance.StartCoroutine(PlayRestartSecond(__instance));

            VoiceManager.enemySpawnTimes[__instance] = Time.time;
        }

        static IEnumerator PlayRestartFirst(V2 v2)
        {
            if (SceneManager.GetActiveScene().name != V2Character.LimboClimaxSceneName)
                yield break;

            yield return new WaitForSeconds(0.75f);

            AudioClip clip = V2Character.UseV2Clip(V2Character.IntroFirstRestartClip, V2Character.IntroFirstRestartClipCazsu);

            var src = VoiceManager.CreateVoiceSource(
                v2,
                "V2IntroFirstRestart",
                clip,
                "Back so soon?",
                true
            );

            if (src == null)
                yield break;

            VoiceManager.spawnVoiceEndTimes[v2] = Time.time + clip.length;
        }

        static IEnumerator PlayRestartSecond(V2 v2)
        {
            if (V2Character.V2SecondVoiceRestartPlayed)
                yield break;

            if (SceneManager.GetActiveScene().name != V2Character.GreedClimaxSceneName)
                yield break;

            AudioClip clip = V2Character.UseV2Clip(V2Character.IntroSecondRestartClip, V2Character.IntroSecondRestartClipCazsu);

            var src = VoiceManager.CreateVoiceSource(
                v2,
                "V2IntroSecondRestart",
                clip,
                "Just stay down!",
                true
            );

            if (src == null)
                yield break;

            VoiceManager.spawnVoiceEndTimes[v2] = Time.time + clip.length;
            V2Character.V2SecondVoiceRestartPlayed = true;
        }
    }

    [HarmonyPatch(typeof(V2), "Update")]
    class V2CombatChatterPatch
    {
        static void Postfix(V2 __instance)
        {
            if (__instance == null)
                return;

            if (!UltraVoicePlugin.V2VoiceEnabled.value)
                return;

            if (ULTRAKILL.Cheats.BlindEnemies.Blind)
                return;

            if (!__instance.active || __instance.target == null)
                return;

            if (__instance.inIntro || __instance.enraged)
                return;

            if (VoiceManager.IsSpawnVoicePlaying(__instance))
                return;

            if (VoiceManager.TooSoonAfterSpawn(__instance, 6f))
                return;

            if (!VoiceManager.CheckCooldown(__instance, 6f))
                return;

            if (Random.Range(0f, 1f) >= 0.75f)
                return;

            if (!__instance.secondEncounter)
                VoiceManager.PlayRandomVoice(__instance, "V2",
                    V2Character.UseV2Clips(V2Character.ChatterClips, V2Character.ChatterClipsCazsu),
                    V2Character.ChatterSubs
                );
            else
                VoiceManager.PlayRandomVoice(__instance, "V2",
                    V2Character.UseV2Clips(V2Character.ChatterPissedClips, V2Character.ChatterPissedClipsCazsu),
                    V2Character.ChatterPissedSubs
                );
        }
    }

    [HarmonyPatch(typeof(V2), "KnockedOut")]
    class V2DefeatPatch
    {
        static void Prefix(V2 __instance)
        {
            if (!UltraVoicePlugin.V2VoiceEnabled.value)
                return;

            float timeSinceIntro = Time.time - V2Character.V2IntroTime;

            if (!__instance.secondEncounter)
            {
                if (__instance.inIntro || timeSinceIntro < 15f)
                    UltraVoicePlugin.Instance.StartCoroutine(PlayFastDefeat(__instance));
                else
                    UltraVoicePlugin.Instance.StartCoroutine(PlayDefeat(__instance));
            }
            else
            {
                if (!__instance.dead)
                    UltraVoicePlugin.Instance.StartCoroutine(PlayEscape(__instance));
                else
                    UltraVoicePlugin.Instance.StartCoroutine(PlayRealization(__instance));
            }
        }

        static IEnumerator PlayFastDefeat(V2 v2)
        {
            var src = VoiceManager.CreateVoiceSource(
                v2,
                "V2FastDefeat",
                V2Character.UseV2Clip(V2Character.FastDefeatClip, V2Character.FastDefeatClipCazsu),
                null,
                true
            );

            if (src == null)
                yield break;

            VoiceManager.ShowSubtitle("Enough!", src, V2Character.V2Color);

            yield return new WaitForSeconds(1.5f);

            if (!src)
                yield break;

            VoiceManager.ShowSubtitle("You've proven your point...", src, V2Character.V2Color);
        }

        static IEnumerator PlayDefeat(V2 v2)
        {
            yield return new WaitForSeconds(1.25f);

            if (v2 == null)
                yield break;

            var src = VoiceManager.CreateVoiceSource(
                v2,
                "V2Defeat",
                V2Character.UseV2Clip(V2Character.DefeatClip, V2Character.DefeatClipCazsu),
                null,
                true
            );

            if (src == null)
                yield break;

            VoiceManager.ShowSubtitle("This isn't over! Mark my words...", src, V2Character.V2Color);
        }

        static IEnumerator PlayEscape(V2 v2)
        {
            yield return new WaitForSeconds(1f);

            if (v2 == null)
                yield break;

            VoiceManager.CreateVoiceSource(
                v2,
                "V2Escape",
                V2Character.UseV2Clip(V2Character.EscapingClip, V2Character.EscapingClipCazsu),
                "I won't give you the PLEASURE of killing me!",
                true
            );
        }

        static IEnumerator PlayRealization(V2 v2)
        {
            yield return new WaitForSeconds(0.5f);

            if (v2 == null)
                yield break;

            VoiceManager.CreateVoiceSource(
                v2.transform,
                "V2Realization",
                V2Character.UseV2Clip(V2Character.FlailingClip, V2Character.FlailingClipCazsu),
                "No... NO!",
                true
            );
        }
    }

    [HarmonyPatch(typeof(V2), "OnDamage")]
    class V2PainPatch
    {
        static void Postfix(V2 __instance, ref DamageData data)
        {
            if (data.damage <= 2f)
                return;

            if (__instance.dead)
                return;

            if (!UltraVoicePlugin.V2VoiceEnabled.value)
                return;

            if (!VoiceManager.CheckCooldown(__instance, 0.1f))
                return;

            VoiceManager.PlayRandomVoice(__instance, "V2Pain",
                V2Character.UseV2Clips(V2Character.PainClips, V2Character.PainClipsCazsu),
                null
            );
        }
    }

    [HarmonyPatch(typeof(V2), "InstaEnrage")]
    class V2InstaEnragePatch
    {
        static void Prefix(V2 __instance)
        {
            if (!UltraVoicePlugin.V2VoiceEnabled.value)
                return;

            if (__instance.enraged)
                return;

            float delay = 0f;

            if (UltraVoicePlugin.V1VoiceEnabled.value && V1Character.EnrageV2Clip != null)
                delay = V1Character.EnrageV2Clip.length + 0.2f;

            AudioClip clip = V2Character.UseV2Clip(V2Character.EnragePunchedClip, V2Character.EnragePunchedClipCazsu);

            if (delay <= 0f)
            {
                VoiceManager.CreateVoiceSource(__instance, "V2Enrage", clip, "EXCUSE ME?!", true);
                return;
            }

            UltraVoicePlugin.Instance.StartCoroutine(Routine());

            IEnumerator Routine()
            {
                yield return new WaitForSeconds(delay);

                if (__instance == null)
                    yield break;

                VoiceManager.CreateVoiceSource(__instance, "V2Enrage", clip, "EXCUSE ME?!", true);
            }
        }
    }

    [HarmonyPatch(typeof(V2), "Enrage", new System.Type[] { })]
    class V2PatienceEnragePatch
    {
        static void Postfix(V2 __instance)
        {
            if (!UltraVoicePlugin.V2VoiceEnabled.value)
                return;

            VoiceManager.CreateVoiceSource(
                __instance,
                "V2Enrage",
                V2Character.UseV2Clip(V2Character.EnragePatienceClip, V2Character.EnragePatienceClipCazsu),
                "COME HERE!"
            );
        }
    }

    [HarmonyPatch(typeof(Animator), "Play",
                new System.Type[] { typeof(string), typeof(int), typeof(float) })]
    class V2CutsceneVoicePatch
    {
        static void Postfix(Animator __instance, string stateName, int layer, float normalizedTime)
        {
            if (!UltraVoicePlugin.V2VoiceEnabled.value)
                return;

            if (__instance == null)
                return;

            if (__instance.gameObject.name != "v2_GreenArm")
                return;

            if (V2Character.V2CutsceneVoicePlayed)
                return;

            UltraVoicePlugin.Instance.StartCoroutine(PlayCutsceneVoice(__instance));
        }

        static IEnumerator PlayCutsceneVoice(Animator v2)
        {
            yield return new WaitForSeconds(0.25f);

            V2Character.V2CutsceneVoicePlayed = true;

            var src = VoiceManager.CreateVoiceSource(
                v2,
                "V2Cutscene",
                V2Character.UseV2Clip(V2Character.IntroSecondClip, V2Character.IntroSecondClipCazsu)
            );

            if (src == null)
                yield break;

            VoiceManager.ShowSubtitle("There you are.", src, V2Character.V2Color);

            yield return new WaitForSeconds(1.25f);

            if (v2 == null || !src || !src.isPlaying)
                yield break;

            VoiceManager.ShowSubtitle("I was wondering how long you'd keep my arm...", src, V2Character.V2Color);

            yield return new WaitForSeconds(2.75f);

            if (v2 == null || !src || !src.isPlaying)
                yield break;

            VoiceManager.ShowSubtitle("Don't worry, I'll pry it off you myself.", src, V2Character.V2Color);
        }
    }

    [HarmonyPatch(typeof(GameObject), "SetActive")]
    class V2DeathPatch
    {
        static void Postfix(GameObject __instance, bool value)
        {
            if (__instance.name != "v2_GreenArm")
                return;

            if (!UltraVoicePlugin.V2VoiceEnabled.value)
                return;

            if (V2Character.V2DeathPlayed) return;

            Transform t = __instance.transform;
            bool found = false;

            while (t != null)
            {
                if (t.name.Contains("8 Stuff(Clone)(Clone)"))
                {
                    found = true;
                    break;
                }
                t = t.parent;
            }

            if (!found)
                return;

            V2Character.V2DeathPlayed = true;

            VoiceManager.CreateVoiceSource(
                __instance.transform,
                "V2Death",
                V2Character.UseV2Clip(V2Character.DeathClip, V2Character.DeathClipCazsu),
                "NOOOOOOO!",
                true,
                subtitleColor: V2Character.V2Color
            );
        }
    }
}
