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

        public static void PlayScriptedLine(Component v2, string name, AudioClip clip, string firstSub, bool interrupt, params (float delay, string text)[] followUps)
        {
            UltraVoicePlugin.Instance.StartCoroutine(Routine());

            IEnumerator Routine()
            {
                Color color = VoiceManager.GetEnemyTypeColor(EnemyType.V2);

                var src = VoiceManager.CreateVoiceSource(v2, name, clip, firstSub, interrupt, color);

                if (src == null)
                    yield break;

                foreach (var (delay, text) in followUps)
                {
                    yield return new WaitForSeconds(delay);

                    if (v2 == null || !src)
                        yield break;

                    VoiceManager.ShowSubtitle(text, src, color);
                }
            }
        }

        public const string LimboClimaxSceneName = "36abcaae9708abc4d9e89e6ec73a2846";
        public const string GreedClimaxSceneName = "ac1675e648695a343bd064c6d0c56e57";

        public static float V2IntroTime = -999f;
        public static bool V2CutsceneVoicePlayed = false;
        public static bool V2SecondVoiceRestartPlayed = false;
        public static bool V2DeathPlayed = false;

        public static AudioClip UseV2Clip(AudioClip rubyClip, AudioClip caszuClip)
        {
            return UltraVoicePlugin.V2VoiceActorField != null && UltraVoicePlugin.V2VoiceActorField.Value == UltraVoicePlugin.V2VoiceActor.Cazsu
                ? caszuClip
                : rubyClip;
        }

        public static AudioClip[] UseV2Clips(AudioClip[] rubyClips, AudioClip[] caszuClips)
        {
            return UltraVoicePlugin.V2VoiceActorField != null && UltraVoicePlugin.V2VoiceActorField.Value == UltraVoicePlugin.V2VoiceActor.Cazsu
                ? caszuClips
                : rubyClips;
        }

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            IntroFirstClip = UltraVoicePlugin.LoadClip("assets/resources/v2/ruby/v2_introfirst.wav");
            IntroFirstRestartClip = UltraVoicePlugin.LoadClip("assets/resources/v2/ruby/v2_restartintrofirst.wav");
            IntroSecondClip = UltraVoicePlugin.LoadClip("assets/resources/v2/ruby/v2_introsecond.wav");
            IntroSecondRestartClip = UltraVoicePlugin.LoadClip("assets/resources/v2/ruby/v2_restartintrosecond.wav");

            FastDefeatClip = UltraVoicePlugin.LoadClip("assets/resources/v2/ruby/v2_fastdefeat.wav");
            DefeatClip = UltraVoicePlugin.LoadClip("assets/resources/v2/ruby/v2_defeat.wav");
            DeathClip = UltraVoicePlugin.LoadClip("assets/resources/v2/ruby/v2_death.wav");
            FlailingClip = UltraVoicePlugin.LoadClip("assets/resources/v2/ruby/v2_flailing.wav");
            EnragePatienceClip = UltraVoicePlugin.LoadClip("assets/resources/v2/ruby/v2_enragepatience.wav");
            EnragePunchedClip = UltraVoicePlugin.LoadClip("assets/resources/v2/ruby/v2_enragepunched.wav");
            EscapingClip = UltraVoicePlugin.LoadClip("assets/resources/v2/ruby/v2_escaping.wav");

            ChatterClips = UltraVoicePlugin.LoadClips("assets/resources/v2/ruby/v2_chatter{0}.wav", 5);
            ChatterPissedClips = UltraVoicePlugin.LoadClips("assets/resources/v2/ruby/v2_chatterpissed{0}.wav", 5);
            PainClips = UltraVoicePlugin.LoadClips("assets/resources/v2/ruby/v2_pain{0}.wav", 5);

            IntroFirstClipCazsu = UltraVoicePlugin.LoadClip("assets/resources/v2/cazsu/v2_introfirst.wav");
            IntroFirstRestartClipCazsu = UltraVoicePlugin.LoadClip("assets/resources/v2/cazsu/v2_restartintrofirst.wav");
            IntroSecondClipCazsu = UltraVoicePlugin.LoadClip("assets/resources/v2/cazsu/v2_introsecond.wav");
            IntroSecondRestartClipCazsu = UltraVoicePlugin.LoadClip("assets/resources/v2/cazsu/v2_restartintrosecond.wav");

            FastDefeatClipCazsu = UltraVoicePlugin.LoadClip("assets/resources/v2/cazsu/v2_fastdefeat.wav");
            DefeatClipCazsu = UltraVoicePlugin.LoadClip("assets/resources/v2/cazsu/v2_defeat.wav");
            DeathClipCazsu = UltraVoicePlugin.LoadClip("assets/resources/v2/cazsu/v2_death.wav");
            FlailingClipCazsu = UltraVoicePlugin.LoadClip("assets/resources/v2/cazsu/v2_flailing.wav");
            EnragePatienceClipCazsu = UltraVoicePlugin.LoadClip("assets/resources/v2/cazsu/v2_enragepatience.wav");
            EnragePunchedClipCazsu = UltraVoicePlugin.LoadClip("assets/resources/v2/cazsu/v2_enragepunched.wav");
            EscapingClipCazsu = UltraVoicePlugin.LoadClip("assets/resources/v2/cazsu/v2_escaping.wav");

            ChatterClipsCazsu = UltraVoicePlugin.LoadClips("assets/resources/v2/cazsu/v2_chatter{0}.wav", 5);
            ChatterPissedClipsCazsu = UltraVoicePlugin.LoadClips("assets/resources/v2/cazsu/v2_chatterpissed{0}.wav", 5);
            PainClipsCazsu = UltraVoicePlugin.LoadClips("assets/resources/v2/cazsu/v2_pain{0}.wav", 5);

            logger.LogInfo("V2 voice lines loaded successfully!");
        }
    }

    [HarmonyPatch(typeof(V2), "Start")]
    class V2IntroFirstPatch
    {
        static void Postfix(V2 __instance)
        {
            StatsManager stats = MonoSingleton<StatsManager>.Instance;

            if (stats == null || stats.restarts >= 1)
                return;

            if (!UltraVoicePlugin.V2VoiceEnabled.Value)
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

            V2Character.V2IntroTime = Time.time;
            VoiceManager.spawnVoiceEndTimes[v2] = Time.time + clip.length;

            V2Character.PlayScriptedLine(v2, "V2Intro", clip,
                "So, you're my predecessor.", false,
                (2.25f, "How quaint..."),
                (1.25f, "I suppose I'll have to show you what an upgrade looks like.")
            );
        }
    }

    [HarmonyPatch(typeof(V2), "Start")]
    class V2IntroRetrySectionPatch
    {
        static void Postfix(V2 __instance)
        {
            StatsManager stats = MonoSingleton<StatsManager>.Instance;

            if (stats == null || stats.restarts == 0)
                return;

            if (!UltraVoicePlugin.V2VoiceEnabled.Value)
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

            if (!UltraVoicePlugin.V2VoiceEnabled.Value)
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
            if (!UltraVoicePlugin.V2VoiceEnabled.Value)
                return;

            float timeSinceIntro = Time.time - V2Character.V2IntroTime;

            if (!__instance.secondEncounter)
            {
                if (__instance.inIntro || timeSinceIntro < 15f)
                    PlayFastDefeat(__instance);
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

        static void PlayFastDefeat(V2 v2)
        {
            V2Character.PlayScriptedLine(v2, "V2FastDefeat",
                V2Character.UseV2Clip(V2Character.FastDefeatClip, V2Character.FastDefeatClipCazsu),
                "Enough!", true,
                (1.5f, "You've proven your point...")
            );
        }

        static IEnumerator PlayDefeat(V2 v2)
        {
            yield return new WaitForSeconds(1.25f);

            if (v2 == null)
                yield break;

            VoiceManager.CreateVoiceSource(
                v2,
                "V2Defeat",
                V2Character.UseV2Clip(V2Character.DefeatClip, V2Character.DefeatClipCazsu),
                "This isn't over! Mark my words...",
                true
            );
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

            if (!UltraVoicePlugin.V2VoiceEnabled.Value)
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
            if (!UltraVoicePlugin.V2VoiceEnabled.Value)
                return;

            if (__instance.enraged)
                return;

            AudioClip clip = V2Character.UseV2Clip(V2Character.EnragePunchedClip, V2Character.EnragePunchedClipCazsu);

            VoiceManager.CreateVoiceSource(__instance, "V2Enrage", clip, "EXCUSE ME?!", true);
        }
    }

    [HarmonyPatch(typeof(V2), "Enrage", new System.Type[] { })]
    class V2PatienceEnragePatch
    {
        static void Postfix(V2 __instance)
        {
            if (!UltraVoicePlugin.V2VoiceEnabled.Value)
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
            if (!UltraVoicePlugin.V2VoiceEnabled.Value)
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

            V2Character.PlayScriptedLine(v2, "V2Cutscene",
                V2Character.UseV2Clip(V2Character.IntroSecondClip, V2Character.IntroSecondClipCazsu),
                "There you are.", false,
                (1.25f, "I was wondering how long you'd keep my arm..."),
                (2.75f, "Don't worry, I'll pry it off you myself.")
            );
        }
    }

    [HarmonyPatch(typeof(GameObject), "SetActive")]
    class V2DeathPatch
    {
        static void Postfix(GameObject __instance, bool value)
        {
            if (__instance.name != "v2_GreenArm")
                return;

            if (!UltraVoicePlugin.V2VoiceEnabled.Value)
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
                VoiceManager.GetEnemyTypeColor(EnemyType.V2)
            );
        }
    }
}
