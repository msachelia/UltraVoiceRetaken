using HarmonyLib;
using System;
using System.Collections;
using UnityEngine;
using UltraVoice.Utilities;

namespace UltraVoice.Characters
{
    public class EarthmoverCharacter
    {
        // Voice line storage
        public static AudioClip IntroClip;

        public static void LoadVoiceLines(AssetBundle bundle, BepInEx.Logging.ManualLogSource logger)
        {
            IntroClip = UltraVoicePlugin.LoadClip(bundle, "em_Intro");

            logger.LogInfo("Earthmover voice lines loaded successfully!");
        }
    }

    [HarmonyPatch(typeof(AudioSource), nameof(AudioSource.Play), new Type[] {typeof(ulong)})]
    class EarthmoverPlayerDetectedPatch
    {
        static void Postfix(AudioSource __instance)
        {
            if (!UltraVoicePlugin.EarthmoverVoiceEnabled.value)
                return;

            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "6e981b1865c649749a610aafc471e198")
                return;

            if (__instance.clip.name == "Centaur A-1")
            {
                UltraVoicePlugin.Instance.StartCoroutine(Detected());
            }

            static IEnumerator Detected()
            {
                yield return new WaitForSeconds(1f);

                VoiceManager.CreateVoiceSource(
                    MonoSingleton<NewMovement>.Instance,
                    "Earthmover",
                    EarthmoverCharacter.IntroClip,
                    "MULTIPLE MACHINE SIGNATURES PRESENT ON RADAR.",
                    spatialBlend: 0f
                );
            }
        }
    }
}