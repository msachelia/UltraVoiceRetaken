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

        public static AudioClip IntroClipMel;
        public static AudioClip RestartClipMel;
        public static AudioClip EnrageClipMel;
        public static AudioClip DeathClipMel;
        public static AudioClip BigPainClipMel;
        public static AudioClip RecoverClipMel;

        public static AudioClip[] ChatterClipsMel;
        public static AudioClip[] OverheatClipsMel;

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
            IntroClip = UltraVoicePlugin.LoadClip("assets/resources/geryon/theratman_valin_megaslash/ger_intro.wav");
            IntroClipMel = UltraVoicePlugin.LoadClip("assets/resources/geryon/mel/ger_intro.wav");
            RestartClip = UltraVoicePlugin.LoadClip("assets/resources/geryon/theratman_valin_megaslash/ger_respawn.wav");
            RestartClipMel = UltraVoicePlugin.LoadClip("assets/resources/geryon/mel/ger_respawn.wav");
            EnrageClip = UltraVoicePlugin.LoadClip("assets/resources/geryon/theratman_valin_megaslash/ger_enrage.wav");
            EnrageClipMel = UltraVoicePlugin.LoadClip("assets/resources/geryon/mel/ger_enrage.wav");
            DeathClip = UltraVoicePlugin.LoadClip("assets/resources/geryon/theratman_valin_megaslash/ger_death.wav");
            DeathClipMel = UltraVoicePlugin.LoadClip("assets/resources/geryon/mel/ger_death.wav");
            BigPainClip = UltraVoicePlugin.LoadClip("assets/resources/geryon/theratman_valin_megaslash/ger_bigpain.wav");
            BigPainClipMel = UltraVoicePlugin.LoadClip("assets/resources/geryon/mel/ger_bigpain.wav");
            RecoverClip = UltraVoicePlugin.LoadClip("assets/resources/geryon/theratman_valin_megaslash/ger_recover.wav");
            RecoverClipMel = UltraVoicePlugin.LoadClip("assets/resources/geryon/mel/ger_recover.wav");

            ChatterClips = UltraVoicePlugin.LoadClips("assets/resources/geryon/theratman_valin_megaslash/ger_chatter{0}.wav", 9);
            ChatterClipsMel = UltraVoicePlugin.LoadClips("assets/resources/geryon/mel/ger_chatter{0}.wav", 9);
            OverheatClips = UltraVoicePlugin.LoadClips("assets/resources/geryon/theratman_valin_megaslash/ger_overheat{0}.wav", 3);
            OverheatClipsMel = UltraVoicePlugin.LoadClips("assets/resources/geryon/mel/ger_overheat{0}.wav", 3);

            logger.LogInfo("Geryon voice lines loaded successfully!");
        }

        public static bool UsingMel =>
            UltraVoicePlugin.GeryonVoiceActorField != null
            && UltraVoicePlugin.GeryonVoiceActorField.Value == UltraVoicePlugin.GeryonVoiceActor.Mel;

        public static AudioClip[] UseGeryonClips(AudioClip[] trioClips, AudioClip[] melClips)
        {
            return UsingMel ? melClips : trioClips;
        }

        public static AudioClip UseGeryonClip(AudioClip trioClip, AudioClip melClip)
        {
            return UsingMel ? melClip : trioClip;
        }

        public static void PlayScriptedLine(Geryon geryon, AudioClip clip, string firstSub, Color? firstColor, bool interrupt, float startDelay, params (float trioDelay, float melDelay, string text, Color? color)[] followUps)
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

                foreach (var (trioDelay, melDelay, text, color) in followUps)
                {
                    yield return new WaitForSeconds(UsingMel ? melDelay : trioDelay);

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
            if (!UltraVoicePlugin.GeryonVoiceEnabled.Value)
                return;

            GeryonCharacter.EnrageLinePlayed = false;
            GeryonCharacter.CanRestartFight = true;

            VoiceManager.enemySpawnTimes[__instance] = Time.time;

            if (!GeryonCharacter.RestartedFight)
                GeryonCharacter.PlayScriptedLine(__instance, GeryonCharacter.UseGeryonClip(GeryonCharacter.IntroClip, GeryonCharacter.IntroClipMel),
                    "FOOLISH...", GeryonCharacter.RetributionColor, false, 0f,
                    (1.25f, 1.3f, "OR BRAVE?", GeryonCharacter.DissonanceColor),
                    (1.5f, 1.55f, "IT MATTERS NOT...", GeryonCharacter.AbsolutionColor),
                    (1.7f, 1.76f, "FOR WE SHALL STRIKE YOU DOWN!", null)
                );
            else
                GeryonCharacter.PlayScriptedLine(__instance, GeryonCharacter.UseGeryonClip(GeryonCharacter.RestartClip, GeryonCharacter.RestartClipMel),
                    "IT REAPPEARS...", GeryonCharacter.AbsolutionColor, false, 0f,
                    (1.8f, 2.04f, "IT WANTS MORE?", GeryonCharacter.DissonanceColor),
                    (1.4f, 1.59f, "THEN MORE IT SHALL RECEIVE!", GeryonCharacter.RetributionColor)
                );
        }
    }

    [HarmonyPatch(typeof(StatsManager), nameof(StatsManager.Restart))]
    class GeryonRestartPatch
    {
        static void Postfix(StatsManager __instance)
        {
            if (!UltraVoicePlugin.GeryonVoiceEnabled.Value)
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
            if (!UltraVoicePlugin.GeryonVoiceEnabled.Value)
                return;

            VoiceManager.CreateVoiceSource(
                __instance,
                "Geryon",
                GeryonCharacter.UseGeryonClip(GeryonCharacter.BigPainClip, GeryonCharacter.BigPainClipMel),
                null,
                true,
                spatialBlend: 0f
            );

            VoiceManager.PlayRandomVoiceDelayed(1f, __instance, "Geryon",
                GeryonCharacter.UseGeryonClips(GeryonCharacter.OverheatClips, GeryonCharacter.OverheatClipsMel),
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
            if (!UltraVoicePlugin.GeryonVoiceEnabled.Value)
                return;

            if (__instance.stunned || !__instance.active)
                return;

            if (!VoiceManager.CheckCooldown(__instance, 6f))
                return;

            if (VoiceManager.TooSoonAfterSpawn(__instance, 6f))
                return;

            if (Random.Range(0f, 1f) >= 0.75f)
                return;

            if (GeryonCharacter.UseGeryonClips(GeryonCharacter.ChatterClips, GeryonCharacter.ChatterClipsMel) == null || GeryonCharacter.UseGeryonClips(GeryonCharacter.ChatterClips, GeryonCharacter.ChatterClipsMel).Length == 0)
                return;

            int i = Random.Range(0, GeryonCharacter.UseGeryonClips(GeryonCharacter.ChatterClips, GeryonCharacter.ChatterClipsMel).Length);

            VoiceManager.CreateVoiceSource(
                __instance,
                "Geryon",
                GeryonCharacter.UseGeryonClips(GeryonCharacter.ChatterClips, GeryonCharacter.ChatterClipsMel)[i],
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
            if (!UltraVoicePlugin.GeryonVoiceEnabled.Value)
                return;

            if (__instance.name != "Geryon_Rig")
                return;

            if (__instance.transform.parent == null || __instance.transform.parent.name != "Theatre (1)")
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
                    GeryonCharacter.UseGeryonClip(GeryonCharacter.DeathClip, GeryonCharacter.DeathClipMel),
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
            if (!UltraVoicePlugin.GeryonVoiceEnabled.Value)
                return;

            VoiceManager.CreateVoiceSource(
                __instance,
                "Geryon",
                GeryonCharacter.UseGeryonClip(GeryonCharacter.BigPainClip, GeryonCharacter.BigPainClipMel),
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
            if (!UltraVoicePlugin.GeryonVoiceEnabled.Value)
                return;

            VoiceManager.CreateVoiceSource(
                __instance,
                "Geryon",
                GeryonCharacter.UseGeryonClip(GeryonCharacter.RecoverClip, GeryonCharacter.RecoverClipMel),
                null,
                true,
                spatialBlend: 0f
            );

            if (!__instance.secondPhase || GeryonCharacter.EnrageLinePlayed)
                return;

            GeryonCharacter.EnrageLinePlayed = true;

            GeryonCharacter.PlayScriptedLine(__instance, GeryonCharacter.UseGeryonClip(GeryonCharacter.EnrageClip, GeryonCharacter.EnrageClipMel),
                "INSUFFERABLE...", GeryonCharacter.RetributionColor, true, 0.75f,
                (1.9f, 1.39f, "INFURIATING!", GeryonCharacter.RetributionColor),
                (2.3f, 1.69f, "DIE!", GeryonCharacter.RetributionColor)
            );
        }
    }
}
