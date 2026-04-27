using HarmonyLib;
using System.Collections;
using UnityEngine;
using UltraVoice.Utilities;

namespace UltraVoice.Characters
{
    public class CerberusCharacter
    {
        // Voice line storage
        public static AudioClip PreludeClip;
        public static AudioClip[] AwakenClips;
        public static AudioClip[] EnrageClips;
        public static AudioClip[] ThrowClips;
        public static AudioClip[] StompClips;
        public static AudioClip[] TackleClips;
        public static AudioClip[] DeathClips;

        // Subtitle storage
        public static readonly string[] AwakenSubs =
        {
            "Who dares awaken me",
            "You will go no further",
            "Your story ends here",
            "Your defiance shall be punished",
            "Be gone, or be destroyed",
            "You stand condemned"
        };

        public static readonly string[] EnrageSubs =
        {
            "You will regret crossing me",
            "Your arrogance will cost you",
            "I will avenge my brother",
            "This shall not go unpunished"
        };

        public static readonly string[] ThrowSubs =
        {
            "Perish",
            "Die"
        };

        public static readonly string[] StompSubs =
        {
            "Tremble",
            "Fall"
        };

        public static readonly string[] TackleSubs =
        {
            "Face me",
            "You cannot run"
        };

        public static void LoadVoiceLines(AssetBundle bundle, BepInEx.Logging.ManualLogSource logger)
        {
            PreludeClip = UltraVoicePlugin.LoadClip(bundle, "cerb_WakeUpSpecial");

            AwakenClips = new[]
            {
                UltraVoicePlugin.LoadClip(bundle, "cerb_WakeUp1"),
                UltraVoicePlugin.LoadClip(bundle, "cerb_WakeUp2"),
                UltraVoicePlugin.LoadClip(bundle, "cerb_WakeUp3"),
                UltraVoicePlugin.LoadClip(bundle, "cerb_WakeUp4"),
                UltraVoicePlugin.LoadClip(bundle, "cerb_WakeUp5"),
                UltraVoicePlugin.LoadClip(bundle, "cerb_WakeUp6")
            };

            EnrageClips = new[]
            {
                UltraVoicePlugin.LoadClip(bundle, "cerb_Enrage1"),
                UltraVoicePlugin.LoadClip(bundle, "cerb_Enrage2"),
                UltraVoicePlugin.LoadClip(bundle, "cerb_Enrage3"),
                UltraVoicePlugin.LoadClip(bundle, "cerb_Enrage4")
            };

            ThrowClips = new[]
            {
                UltraVoicePlugin.LoadClip(bundle, "cerb_Orb1"),
                UltraVoicePlugin.LoadClip(bundle, "cerb_Orb2")
            };

            StompClips = new[]
            {
                UltraVoicePlugin.LoadClip(bundle, "cerb_Stomp1"),
                UltraVoicePlugin.LoadClip(bundle, "cerb_Stomp2")
            };

            TackleClips = new[]
            {
                UltraVoicePlugin.LoadClip(bundle, "cerb_Tackle1"),
                UltraVoicePlugin.LoadClip(bundle, "cerb_Tackle2")
            };

            DeathClips = new[]
            {
                UltraVoicePlugin.LoadClip(bundle, "cerb_Death1"),
                UltraVoicePlugin.LoadClip(bundle, "cerb_Death2"),
                UltraVoicePlugin.LoadClip(bundle, "cerb_Death3")
            };

            logger.LogInfo("Cerberus voice lines loaded successfully!");
        }

    }

    // CERBERUS PATCHES

    [HarmonyPatch(typeof(StatueFake), "SlowStart")]
    class CerberusIntroVoice
    {
        static void Postfix(StatueFake __instance)
        {
            if (__instance.quickSpawn)
                return;

            if (!UltraVoicePlugin.CerberusVoiceEnabled.value)
                return;

            UltraVoicePlugin.Instance.StartCoroutine(PlayIntro(__instance));
        }

        static IEnumerator PlayIntro(StatueFake cerb)
        {
            yield return new WaitForSeconds(3.5f);

            AudioClip clip = CerberusCharacter.PreludeClip;

            if (clip == null)
                yield break;

            GameObject obj = new GameObject("UltraVoice_CerberusIntro");
            obj.transform.position = cerb.transform.position;

            var src = obj.AddComponent<AudioSource>();

            src.clip = clip;
            src.spatialBlend = 1f;
            src.volume = 1f;
            src.minDistance = 50f;
            src.maxDistance = 500f;
            src.dopplerLevel = 0;

            var mixer = MonoSingleton<AudioMixerController>.Instance;
            src.outputAudioMixerGroup = mixer.allGroup;

            src.Play();

            if (src == null)
                yield break;

            VoiceManager.ShowSubtitle(
                "You tread forbidden ground, machine",
                src,
                new Color(0.65f, 0.65f, 0.65f)
            );

            yield return new WaitForSeconds(3.75f);

            VoiceManager.ShowSubtitle(
                "BEGONE",
                src,
                new Color(0.65f, 0.65f, 0.65f)
            );
        }
    }

    [HarmonyPatch(typeof(StatueFake), "Activate")]
    class CerberusWakePatch
    {
        static void Postfix(StatueFake __instance)
        {
            if (!__instance.quickSpawn)
                return;

            if (!UltraVoicePlugin.CerberusVoiceEnabled.value)
                return;

            VoiceManager.enemySpawnTimes[__instance] = Time.time;

            UltraVoicePlugin.Instance.StartCoroutine(PlayAwaken(__instance));
        }

        static IEnumerator PlayAwaken(StatueFake cerb)
        {
            yield return new WaitForSeconds(Random.Range(0f, 1f));

            VoiceManager.PlayRandomVoice(cerb, "Cerberus", CerberusCharacter.AwakenClips, CerberusCharacter.AwakenSubs, colorOverride: new Color(0.65f, 0.65f, 0.65f));
        }
    }

    [HarmonyPatch(typeof(StatueBoss), "Start")]
    class CerberusSpawnTracker
    {
        static void Postfix(StatueBoss __instance)
        {
            VoiceManager.enemySpawnTimes[__instance] = Time.time;
        }
    }

    [HarmonyPatch(typeof(StatueBoss), "Enrage")]
    class CerberusEnragePatch
    {
        static void Postfix(StatueBoss __instance)
        {
            if (!UltraVoicePlugin.CerberusVoiceEnabled.value)
                return;

            UltraVoicePlugin.Instance.StartCoroutine(PlayEnrage(__instance));
        }

        static IEnumerator PlayEnrage(StatueBoss cerb)
        {
            yield return new WaitForSeconds(0.1f);
            VoiceManager.PlayRandomVoice(cerb, "Cerberus", CerberusCharacter.EnrageClips, CerberusCharacter.EnrageSubs);
            VoiceManager.spawnVoiceEndTimes[cerb] = Time.time + 3;
        }
    }

    [HarmonyPatch(typeof(StatueBoss), "Throw")]
    class CerberusThrowPatch
    {
        static void Postfix(StatueBoss __instance)
        {
            if (!UltraVoicePlugin.CerberusVoiceEnabled.value)
                return;

            if (VoiceManager.IsSpawnVoicePlaying(__instance))
                return;

            if (!VoiceManager.CheckCooldown(__instance, 3f))
                return;

            UltraVoicePlugin.Instance.StartCoroutine(PlayThrowVoice(__instance));
        }

        static IEnumerator PlayThrowVoice(StatueBoss cerberus)
        {
            yield return new WaitForSeconds(0.2f);
            VoiceManager.PlayRandomVoice(cerberus, "Cerberus", CerberusCharacter.ThrowClips, CerberusCharacter.ThrowSubs);
        }
    }

    [HarmonyPatch(typeof(StatueBoss), "Stomp")]
    class CerberusStompPatch
    {
        static void Postfix(StatueBoss __instance)
        {
            if (!UltraVoicePlugin.CerberusVoiceEnabled.value)
                return;

            if (VoiceManager.IsSpawnVoicePlaying(__instance))
                return;

            if (!VoiceManager.CheckCooldown(__instance, 3f))
                return;

            UltraVoicePlugin.Instance.StartCoroutine(PlayStompVoice(__instance));
        }

        static IEnumerator PlayStompVoice(StatueBoss cerberus)
        {
            yield return new WaitForSeconds(0.2f);
            VoiceManager.PlayRandomVoice(cerberus, "Cerberus", CerberusCharacter.StompClips, CerberusCharacter.StompSubs);
        }
    }

    [HarmonyPatch(typeof(StatueBoss), "Tackle")]
    class CerberusTacklePatch
    {
        static void Postfix(StatueBoss __instance)
        {
            if (!UltraVoicePlugin.CerberusVoiceEnabled.value)
                return;

            if (VoiceManager.IsSpawnVoicePlaying(__instance))
                return;

            if (!VoiceManager.CheckCooldown(__instance, 3f))
                return;

            UltraVoicePlugin.Instance.StartCoroutine(PlayTackleVoice(__instance));
        }

        static IEnumerator PlayTackleVoice(StatueBoss cerberus)
        {
            yield return new WaitForSeconds(0.2f);
            VoiceManager.PlayRandomVoice(cerberus, "Cerberus", CerberusCharacter.TackleClips, CerberusCharacter.TackleSubs);
        }
    }

    [HarmonyPatch(typeof(StatueBoss), "OnGoLimp")]
    class CerberusDeathPatch
    {
        static void Postfix(StatueBoss __instance)
        {
            if (!UltraVoicePlugin.CerberusVoiceEnabled.value)
                return;

            if (CerberusCharacter.DeathClips == null || CerberusCharacter.DeathClips.Length == 0)
                return;

            VoiceManager.PlayRandomVoice(__instance, "Cerberus", CerberusCharacter.DeathClips, null, interrupt: true);
        }
    }
}