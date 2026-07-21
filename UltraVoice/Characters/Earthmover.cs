using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UltraVoice.Utilities;

namespace UltraVoice.Characters
{
    public class EarthmoverCharacter
    {

        public static AudioClip IntroClip;
        public static AudioClip BloodVentClip;
        public static AudioClip DefenseSystemClip;
        public static AudioClip BloodFloodClip;
        public static AudioClip BrainBattleClip;
        public static AudioClip BrainKilledClip;

        public const string EarthmoverSceneName = "6e981b1865c649749a610aafc471e198";

        public static float lastBloodFloodTime;
        public static float lastBrainBattleTime;
        public static float lastSelfDestructTime;

        public static bool InEarthmoverLevel()
        {
            return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == EarthmoverSceneName;
        }

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            IntroClip = UltraVoicePlugin.LoadClip("Earthmover.em_Intro.wav");
            BloodVentClip = UltraVoicePlugin.LoadClip("Earthmover.em_BloodVent.wav");
            DefenseSystemClip = UltraVoicePlugin.LoadClip("Earthmover.em_DefenseSystem.wav");
            BloodFloodClip = UltraVoicePlugin.LoadClip("Earthmover.em_BloodFlood.wav");
            BrainBattleClip = UltraVoicePlugin.LoadClip("Earthmover.em_BrainBattle.wav");
            BrainKilledClip = UltraVoicePlugin.LoadClip("Earthmover.em_BrainKilled.wav");

            logger.LogInfo("Earthmover voice lines loaded successfully!");
        }

        public static void PlayAnnouncement(float startDelay, string sourceName, AudioClip clip, string firstSub, params (float delay, string text)[] followUps)
        {
            UltraVoicePlugin.Instance.StartCoroutine(Routine());

            IEnumerator Routine()
            {
                yield return new WaitForSeconds(startDelay);

                var src = VoiceManager.CreateVoiceSource(
                    MonoSingleton<NewMovement>.Instance,
                    sourceName,
                    clip,
                    firstSub,
                    true,
                    spatialBlend: 0f
                );

                if (src == null)
                    yield break;

                foreach (var (delay, text) in followUps)
                {
                    yield return new WaitForSeconds(delay);

                    if (src == null)
                        yield break;

                    VoiceManager.ShowSubtitle(text, src);
                }
            }
        }
    }

    [HarmonyPatch(typeof(AudioSource))]
    class EarthmoverPlayerDetectedPatch
    {
        static IEnumerable<MethodBase> TargetMethods()
        {
            return AccessTools
                .GetDeclaredMethods(typeof(AudioSource))
                .Where(method => !method.IsStatic && method.Name.Contains("Play"));
        }

        static void Postfix(AudioSource __instance)
        {
            if (!UltraVoicePlugin.EarthmoverVoiceEnabled.value)
                return;

            if (__instance == null || __instance.clip == null)
                return;

            if (!EarthmoverCharacter.InEarthmoverLevel())
                return;

            if (__instance.clip.name != "Centaur A-1")
                return;

            if (!VoiceManager.CheckCooldown(__instance, 0.1f))
                return;

            EarthmoverCharacter.PlayAnnouncement(1f, "Earthmover_Intro", EarthmoverCharacter.IntroClip,
                "MULTIPLE MACHINE SIGNATURES PRESENT ON RADAR.",
                (3f, "THREAT LEVEL: MAXIMAL."),
                (1.5f, "READYING WEAPONS...")
            );
        }
    }

    [HarmonyPatch(typeof(Crossfade), "StartFade")]
    class EarthmoverBloodVentPatch
    {
        static void Postfix(Crossfade __instance)
        {
            if (!UltraVoicePlugin.EarthmoverVoiceEnabled.value)
                return;

            if (!EarthmoverCharacter.InEarthmoverLevel())
                return;

            if (__instance.to.clip.name != "Centaur A-2")
                return;

            if (!VoiceManager.CheckCooldown(__instance, 0.1f))
                return;

            EarthmoverCharacter.PlayAnnouncement(0.5f, "Earthmover_BloodVent", EarthmoverCharacter.BloodVentClip,
                "CONTAMINATED BLOOD DETECTED IN FUEL TANK B.",
                (3.25f, "OPENING VENTS...")
            );
        }
    }

    [HarmonyPatch(typeof(CombinedBossBar), "OnEnable")]
    class EarthmoverDefenseSystemPatch
    {
        static void Postfix(CombinedBossBar __instance)
        {
            if (!UltraVoicePlugin.EarthmoverVoiceEnabled.value)
                return;

            if (!EarthmoverCharacter.InEarthmoverLevel())
                return;

            if (__instance.name != "SecuritySystem")
                return;

            if (!VoiceManager.CheckCooldown(__instance, 0.1f))
                return;

            EarthmoverCharacter.PlayAnnouncement(0.1f, "Earthmover_DefenseSystem", EarthmoverCharacter.DefenseSystemClip,
                "UNAUTHORIZED PERSONNEL DETECTED AT INTERIOR ENTRANCE.",
                (3.75f, "ENGAGING DEFENSE SYSTEM...")
            );
        }
    }

    [HarmonyPatch(typeof(GameObject), "SetActive")]
    class EarthmoverBloodFloodPatch
    {
        static void Postfix(GameObject __instance)
        {
            if (!UltraVoicePlugin.EarthmoverVoiceEnabled.value)
                return;

            if (__instance == null)
                return;

            if (!EarthmoverCharacter.InEarthmoverLevel())
                return;

            if (__instance.name != "SecondBeats")
                return;

            if (Time.time - EarthmoverCharacter.lastBloodFloodTime < 0.1f)
                return;

            Transform alarmTransform = __instance.transform.Find("Alarm");
            if (alarmTransform == null)
                return;

            AudioSource alarmSource = alarmTransform.GetComponent<AudioSource>();
            if (alarmSource == null || !alarmSource.isPlaying)
                return;

            EarthmoverCharacter.lastBloodFloodTime = Time.time;

            EarthmoverCharacter.PlayAnnouncement(0f, "Earthmover_BloodFlood", EarthmoverCharacter.BloodFloodClip,
                "UNAUTHORIZED PERSONNEL HAS BREACHED INTERIOR ENTRANCE.",
                (2.75f, "FLUSHING INTERIOR...")
            );
        }
    }

    [HarmonyPatch(typeof(GameObject), "SetActive")]
    class EarthmoverBrainBattlePatch
    {
        static void Postfix(GameObject __instance)
        {
            if (!UltraVoicePlugin.EarthmoverVoiceEnabled.value)
                return;

            if (__instance == null || !__instance.activeSelf)
                return;

            if (!EarthmoverCharacter.InEarthmoverLevel())
                return;

            if (__instance.name != "Beams" || __instance.transform.parent.parent.name != "LaserRing")
                return;

            if (Time.time - EarthmoverCharacter.lastBrainBattleTime < 0.1f)
                return;

            EarthmoverCharacter.lastBrainBattleTime = Time.time;

            EarthmoverCharacter.PlayAnnouncement(0f, "Earthmover_BrainBattle", EarthmoverCharacter.BrainBattleClip,
                "UNAUTHORIZED PERSONNEL HAS BREACHED CENTRAL CONTROL TOWER.",
                (3f, "ENGAGING DEFENSE PROTOCOLS...")
            );
        }
    }

    [HarmonyPatch(typeof(GameObject), "SetActive")]
    class EarthmoverSelfDestructPatch
    {
        static void Postfix(GameObject __instance)
        {
            if (__instance == null || !__instance.activeSelf)
                return;

            if (!UltraVoicePlugin.EarthmoverVoiceEnabled.value)
                return;

            if (!EarthmoverCharacter.InEarthmoverLevel())
                return;

            if (__instance.name != "Countdown")
                return;

            if (Time.time - EarthmoverCharacter.lastSelfDestructTime < 0.1f)
                return;

            EarthmoverCharacter.lastSelfDestructTime = Time.time;

            EarthmoverCharacter.PlayAnnouncement(0.25f, "Earthmover_SelfDestruct", EarthmoverCharacter.BrainKilledClip,
                "CRITICAL DAMAGE DETECTED IN CENTRAL CONTROL TOWER!",
                (3.25f, "SELF-DESTRUCT SEQUENCE INITIATED,"),
                (2.25f, "ALL PERSONNEL EVACUATE IMMEDIATELY!")
            );
        }
    }
}
