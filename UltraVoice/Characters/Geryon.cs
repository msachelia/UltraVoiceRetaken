using HarmonyLib;
using System.Collections;
using UltraVoice.Utilities;
using UnityEngine;

namespace UltraVoice.Characters
{
    public class GeryonCharacter
    {
        public static AudioClip IntroClip;
        public static AudioClip RestartClip;
        public static AudioClip EnrageClip;
        public static AudioClip DeathClip;
        public static AudioClip BigPainClip;
        public static AudioClip RecoverClip;

        public static AudioClip[] ChatterClips;
        public static AudioClip[] OverheatClips;

        public static readonly string[] ChatterSubs =
        {
            "IT MUST NOT PREVAIL!",
            "IT CANNOT ESCAPE US!",
            "NO ONE WILL SAVE YOU!",
            "RIP IT TO PIECES!",
            "CRUSH IT TO DUST!",
            "MORE BLOOD FOR US!",
            "WHY DO YOU PERSIST?",
            "ACCEPT YOUR END.",
            "SUCH POINTLESS RESISTANCE."
        };

        public static readonly string[] OverheatSubs =
        {
            "HOLD TOGETHER! HOLD TOGETHER!",
            "TOO MUCH! TOO MUCH!",
            "CALM DOWN! CALM DOWN!",
        };

        public static readonly Color AbsolutionColor = new Color(0.45f, 0.80f, 0.40f);
        public static readonly Color DissonanceColor = new Color(0.65f, 0.45f, 0.85f);
        public static readonly Color RetributionColor = new Color(0.87f, 0.30f, 0.30f);

        public static readonly Color[] ChatterColors =
        {
            RetributionColor, RetributionColor, RetributionColor,
            DissonanceColor, DissonanceColor, DissonanceColor,
            AbsolutionColor, AbsolutionColor, AbsolutionColor
        };

        public static bool EnrageLinePlayed = false;
        public static bool RestartedFight = false;
        public static bool CanRestartFight = false;

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            IntroClip = UltraVoicePlugin.LoadClip("Geryon.ger_Intro.wav");
            RestartClip = UltraVoicePlugin.LoadClip("Geryon.ger_Respawn.wav");
            EnrageClip = UltraVoicePlugin.LoadClip("Geryon.ger_Enrage.wav");
            DeathClip = UltraVoicePlugin.LoadClip("Geryon.ger_Death.wav");
            BigPainClip = UltraVoicePlugin.LoadClip("Geryon.ger_BigPain.wav");
            RecoverClip = UltraVoicePlugin.LoadClip("Geryon.ger_Recover.wav");

            ChatterClips = UltraVoicePlugin.LoadClips("Geryon.ger_Chatter{0}.wav", 9);
            OverheatClips = UltraVoicePlugin.LoadClips("Geryon.ger_Overheat{0}.wav", 3);

            logger.LogInfo("Geryon voice lines loaded successfully!");
        }

        public static void PlayScriptedLine(Geryon geryon, AudioClip clip, string firstSub, Color? firstColor, bool interrupt, float startDelay, params (float delay, string text, Color? color)[] followUps)
        {
            UltraVoicePlugin.Instance.StartCoroutine(Routine());

            IEnumerator Routine()
            {
                if (startDelay > 0f)
                    yield return new WaitForSeconds(startDelay);

                if (geryon == null || !geryon.active)
                    yield break;

                var src = VoiceManager.CreateVoiceSource(geryon, "Geryon", clip, firstSub, interrupt, firstColor, spatialBlend: 0f);

                if (src == null)
                    yield break;

                foreach (var (delay, text, color) in followUps)
                {
                    yield return new WaitForSeconds(delay);

                    if (geryon == null || !geryon.active || !src)
                        yield break;

                    VoiceManager.ShowSubtitle(text, src, color);
                }
            }
        }
    }

    [HarmonyPatch(typeof(Geryon), "Start")]
    class GeryonIntroPatch
    {
        static void Postfix(Geryon __instance)
        {
            if (!UltraVoicePlugin.GeryonVoiceEnabled.value)
                return;

            GeryonCharacter.EnrageLinePlayed = false;
            GeryonCharacter.CanRestartFight = true;

            VoiceManager.enemySpawnTimes[__instance] = Time.time;

            if (!GeryonCharacter.RestartedFight)
                GeryonCharacter.PlayScriptedLine(__instance, GeryonCharacter.IntroClip,
                    "FOOLISH...", GeryonCharacter.RetributionColor, false, 0f,
                    (1.25f, "OR BRAVE?", GeryonCharacter.DissonanceColor),
                    (1.5f, "IT MATTERS NOT...", GeryonCharacter.AbsolutionColor),
                    (1.7f, "FOR WE SHALL STRIKE YOU DOWN!", null)
                );
            else
                GeryonCharacter.PlayScriptedLine(__instance, GeryonCharacter.RestartClip,
                    "IT REAPPEARS...", GeryonCharacter.AbsolutionColor, false, 0f,
                    (1.8f, "IT WANTS MORE?", GeryonCharacter.DissonanceColor),
                    (1.4f, "THEN MORE IT SHALL RECEIVE!", GeryonCharacter.RetributionColor)
                );
        }
    }

    [HarmonyPatch(typeof(StatsManager), nameof(StatsManager.Restart))]
    class GeryonRestartPatch
    {
        static void Postfix(StatsManager __instance)
        {
            if (!UltraVoicePlugin.GeryonVoiceEnabled.value)
                return;

            if (GeryonCharacter.CanRestartFight)
            {
                GeryonCharacter.RestartedFight = true;
            }
        }
    }

    [HarmonyPatch(typeof(Geryon), "Stun")]
    class GeryonOverheatPatch
    {
        static void Postfix(Geryon __instance)
        {
            if (!UltraVoicePlugin.GeryonVoiceEnabled.value)
                return;

            VoiceManager.CreateVoiceSource(
                __instance,
                "Geryon",
                GeryonCharacter.BigPainClip,
                null,
                true,
                spatialBlend: 0f
            );

            VoiceManager.PlayRandomVoiceDelayed(1f, __instance, "Geryon",
                GeryonCharacter.OverheatClips,
                GeryonCharacter.OverheatSubs,
                interrupt: true,
                spatialBlend: 0f
            );
        }
    }

    [HarmonyPatch(typeof(Geryon), "Update")]
    class GeryonChatterPatch
    {
        static void Postfix(Geryon __instance)
        {
            if (!UltraVoicePlugin.GeryonVoiceEnabled.value)
                return;

            if (__instance.stunned || !__instance.active)
                return;

            if (!VoiceManager.CheckCooldown(__instance, 6f))
                return;

            if (VoiceManager.TooSoonAfterSpawn(__instance, 6f))
                return;

            if (Random.Range(0f, 1f) >= 0.75f)
                return;

            if (GeryonCharacter.ChatterClips == null || GeryonCharacter.ChatterClips.Length == 0)
                return;

            int i = Random.Range(0, GeryonCharacter.ChatterClips.Length);

            VoiceManager.CreateVoiceSource(
                __instance,
                "Geryon",
                GeryonCharacter.ChatterClips[i],
                i < GeryonCharacter.ChatterSubs.Length ? GeryonCharacter.ChatterSubs[i] : null,
                subtitleColor: i < GeryonCharacter.ChatterColors.Length ? GeryonCharacter.ChatterColors[i] : (Color?)null,
                spatialBlend: 0f
            );
        }
    }

    [HarmonyPatch(typeof(GameObject), "SetActive")]
    class GeryonDeathPatch
    {
        static void Postfix(GameObject __instance, bool value)
        {
            if (!UltraVoicePlugin.GeryonVoiceEnabled.value)
                return;

            if (__instance.name != "Geryon_Rig" || __instance.transform.parent.name != "Theatre (1)")
                return;

            UltraVoicePlugin.Instance.StartCoroutine(PlayDeath(__instance));

            static IEnumerator PlayDeath(GameObject geryon)
            {
                yield return new WaitForSeconds(0.2f);

                if (geryon == null)
                    yield break;

                VoiceManager.CreateVoiceSource(
                    geryon.GetComponent(typeof(Animator)),
                    "Geryon",
                    GeryonCharacter.DeathClip,
                    null,
                    true,
                    spatialBlend: 0f
                );
            }
        }
    }

    [HarmonyPatch(typeof(Geryon), "Death")]
    class GeryonFallPatch
    {
        static void Postfix(Geryon __instance)
        {
            if (!UltraVoicePlugin.GeryonVoiceEnabled.value)
                return;

            VoiceManager.CreateVoiceSource(
                __instance,
                "Geryon",
                GeryonCharacter.BigPainClip,
                null,
                true,
                spatialBlend: 0f
            );
        }
    }

    [HarmonyPatch(typeof(Geryon), "Unstun")]
    class GeryonEnragePatch
    {
        static void Postfix(Geryon __instance)
        {
            if (!UltraVoicePlugin.GeryonVoiceEnabled.value)
                return;

            VoiceManager.CreateVoiceSource(
                __instance,
                "Geryon",
                GeryonCharacter.RecoverClip,
                null,
                true,
                spatialBlend: 0f
            );

            if (!__instance.secondPhase || GeryonCharacter.EnrageLinePlayed)
                return;

            GeryonCharacter.EnrageLinePlayed = true;

            GeryonCharacter.PlayScriptedLine(__instance, GeryonCharacter.EnrageClip,
                "INSUFFERABLE...", GeryonCharacter.RetributionColor, true, 0.75f,
                (1.9f, "INFURIATING!", GeryonCharacter.RetributionColor),
                (2.3f, "DIE!", GeryonCharacter.RetributionColor)
            );
        }
    }
}
