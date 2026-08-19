using HarmonyLib;
using UnityEngine;
using UltraVoice.Utilities;

namespace UltraVoice.Characters
{
    public class FilthCharacter
    {
        public static AudioClip[] ChatterClips;
        public static AudioClip AttackClip;
        public static AudioClip DeathClip;

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            ChatterClips = UltraVoicePlugin.LoadClips("Filth.CFanim.fil_Chatter{0}.wav", 3);

            AttackClip = UltraVoicePlugin.LoadClip("Filth.CFanim.fil_Attack.wav");
            DeathClip = UltraVoicePlugin.LoadClip("Filth.CFanim.fil_Death.wav");

            logger.LogInfo("Filth voice lines loaded successfully!");
        }
    }

    [HarmonyPatch(typeof(ZombieMelee), nameof(ZombieMelee.Swing))]
    class FilthAttackPatch
    {
        static void Postfix(ZombieMelee __instance)
        {
            if (!UltraVoicePlugin.FilthVoiceEnabled.Value) return;

            VoiceManager.CreateVoiceSource(
                __instance, "Filth",
                FilthCharacter.AttackClip,
                null,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(ZombieMelee), nameof(ZombieMelee.JumpAttack))]
    class FilthJumpAttackPatch
    {
        static void Postfix(ZombieMelee __instance)
        {
            if (!UltraVoicePlugin.FilthVoiceEnabled.Value) return;

            VoiceManager.CreateVoiceSource(
                __instance, "Filth",
                FilthCharacter.AttackClip,
                null,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(ZombieMelee), nameof(ZombieMelee.OnGoLimp))]
    class FilthDeathPatch
    {
        static void Postfix(ZombieMelee __instance)
        {
            if (!UltraVoicePlugin.FilthVoiceEnabled.Value) return;

            VoiceManager.CreateVoiceSource(
                __instance, "Filth",
                FilthCharacter.DeathClip,
                null,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(ZombieMelee), "Update")]
    class FilthChatterPatch
    {
        static void Postfix(ZombieMelee __instance)
        {
            if (!UltraVoicePlugin.FilthVoiceEnabled.Value) return;

            if (ULTRAKILL.Cheats.BlindEnemies.Blind)
                return;

            if (!VoiceManager.CheckCooldown(__instance, 4f))
                return;

            if (Random.Range(0f, 1f) < 0.75f)
                return;

            VoiceManager.PlayRandomVoice(__instance, "Filth",
                FilthCharacter.ChatterClips,
                null,
                randomPitch: true
            );
        }
    }
}
