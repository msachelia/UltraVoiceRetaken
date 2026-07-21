using HarmonyLib;
using UnityEngine;
using UltraVoice.Utilities;

namespace UltraVoice.Characters
{
    public static class LeviathanCharacter
    {
        public static AudioClip[] ChatterClips;
        public static AudioClip BiteClip;
        public static AudioClip BiteWindupClip;
        public static AudioClip ParryClip;
        public static AudioClip RoarClip;
        public static AudioClip IntroRoarClip;
        public static AudioClip DeathClip;
        public static AudioClip DeathEndClip;

        public static readonly string[] ChatterSubs =
        {
            "DEVOUR IT!",
            "TEAR IT APART!",
            "SINK IN DESPAIR!",
            "THE STYX WILL CLAIM YOU!",
            "WE ARE HUNGRY!",
        };

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            ChatterClips = UltraVoicePlugin.LoadClips("Leviathan.lev_Chatter{0}.wav", 5);

            BiteClip = UltraVoicePlugin.LoadClip("Leviathan.lev_Bite.wav");
            BiteWindupClip = UltraVoicePlugin.LoadClip("Leviathan.lev_BiteWindup.wav");
            ParryClip = UltraVoicePlugin.LoadClip("Leviathan.lev_Parried.wav");
            RoarClip = UltraVoicePlugin.LoadClip("Leviathan.lev_Roar.wav");
            IntroRoarClip = UltraVoicePlugin.LoadClip("Leviathan.lev_IntroRoar.wav");
            DeathClip = UltraVoicePlugin.LoadClip("Leviathan.lev_Death.wav");
            DeathEndClip = UltraVoicePlugin.LoadClip("Leviathan.lev_DeathEnd.wav");

            logger.LogInfo("Leviathan voice lines loaded successfully!");
        }

        public static void PlayClip(LeviathanHead head, AudioClip clip)
        {
            if (!UltraVoicePlugin.LeviathanVoiceEnabled.value)
                return;

            VoiceManager.CreateVoiceSource(head, "Leviathan", clip, null, true);
        }
    }

    [HarmonyPatch(typeof(LeviathanHead), nameof(LeviathanHead.StopAction))]
    class LeviathanChatterPatch
    {
        static void Prefix(LeviathanHead __instance)
        {
            if (!UltraVoicePlugin.LeviathanVoiceEnabled.value)
                return;

            if (!__instance.active)
                return;

            if (!VoiceManager.CheckCooldown(__instance, 15f))
                return;

            __instance.attackCooldown = 3.5f;

            VoiceManager.PlayRandomVoice(__instance, "Leviathan",
                LeviathanCharacter.ChatterClips,
                LeviathanCharacter.ChatterSubs,
                true
            );
        }
    }

    [HarmonyPatch(typeof(LeviathanHead), nameof(LeviathanHead.Bite))]
    class LeviathanBiteWindupPatch
    {
        static void Postfix(LeviathanHead __instance)
        {
            LeviathanCharacter.PlayClip(__instance, LeviathanCharacter.BiteWindupClip);
        }
    }

    [HarmonyPatch(typeof(LeviathanHead), nameof(LeviathanHead.Roar))]
    class LeviathanRoarPatch
    {
        static void Postfix(LeviathanHead __instance)
        {
            LeviathanCharacter.PlayClip(__instance, LeviathanCharacter.IntroRoarClip);
        }
    }

    [HarmonyPatch(typeof(LeviathanHead), nameof(LeviathanHead.Ascend))]
    class LeviathanAscendPatch
    {
        static void Postfix(LeviathanHead __instance)
        {
            LeviathanCharacter.PlayClip(__instance, LeviathanCharacter.IntroRoarClip);
        }
    }

    [HarmonyPatch(typeof(LeviathanHead), nameof(LeviathanHead.BiteDamageStart))]
    class LeviathanBitePatch
    {
        static void Postfix(LeviathanHead __instance)
        {
            LeviathanCharacter.PlayClip(__instance, LeviathanCharacter.BiteClip);
        }
    }

    [HarmonyPatch(typeof(LeviathanHead), nameof(LeviathanHead.GotParried))]
    class LeviathanParryPatch
    {
        static void Postfix(LeviathanHead __instance)
        {
            if (!__instance.active)
                return;

            LeviathanCharacter.PlayClip(__instance, LeviathanCharacter.ParryClip);
        }
    }

    [HarmonyPatch(typeof(LeviathanHead), nameof(LeviathanHead.ProjectileBurst))]
    class LeviathanBarragePatch
    {
        static void Postfix(LeviathanHead __instance)
        {
            LeviathanCharacter.PlayClip(__instance, LeviathanCharacter.RoarClip);
        }
    }

    [HarmonyPatch(typeof(LeviathanHead), nameof(LeviathanHead.BeamAttack))]
    class LeviathanBeamPatch
    {
        static void Postfix(LeviathanHead __instance)
        {
            LeviathanCharacter.PlayClip(__instance, LeviathanCharacter.RoarClip);
        }
    }

    [HarmonyPatch(typeof(LeviathanHead), nameof(LeviathanHead.Death))]
    class LeviathanDeathPatch
    {
        static void Postfix(LeviathanHead __instance)
        {
            LeviathanCharacter.PlayClip(__instance, LeviathanCharacter.DeathClip);
        }
    }

    [HarmonyPatch(typeof(LeviathanHead), nameof(LeviathanHead.HeadExplode))]
    class LeviathanDeathEndPatch
    {
        static void Postfix(LeviathanHead __instance)
        {
            LeviathanCharacter.PlayClip(__instance, LeviathanCharacter.DeathEndClip);
        }
    }
}
