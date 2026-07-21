using HarmonyLib;
using UltraVoice.Utilities;
using UnityEngine;

namespace UltraVoice.Characters
{
    public class SoldierCharacter
    {
        public static AudioClip[] ChatterClips;
        public static AudioClip AttackClip;
        public static AudioClip DeathClip;

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            ChatterClips = UltraVoicePlugin.LoadClips("Soldier.sol_Chatter{0}.wav", 3);

            AttackClip = UltraVoicePlugin.LoadClip("Soldier.sol_Attack.wav");
            DeathClip = UltraVoicePlugin.LoadClip("Soldier.sol_Death.wav");

            logger.LogInfo("Soldier voice lines loaded successfully!");
        }
    }

    [HarmonyPatch(typeof(ZombieProjectiles), "Update")]
    class SoldierChatterPatch
    {
        static void Postfix(ZombieProjectiles __instance)
        {
            if (!UltraVoicePlugin.SoldierVoiceEnabled.value) return;

            if (ULTRAKILL.Cheats.BlindEnemies.Blind)
                return;

            if (__instance.eid.enemyType != EnemyType.Soldier) return;

            if (!VoiceManager.CheckCooldown(__instance, 4f))
                return;

            if (Random.Range(0f, 1f) < 0.75f)
                return;

            VoiceManager.PlayRandomVoice(__instance, "Soldier",
                SoldierCharacter.ChatterClips,
                null,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(ZombieProjectiles), nameof(ZombieProjectiles.ShootProjectile))]
    class SoldierThrowPatch
    {
        static void Postfix(ZombieProjectiles __instance)
        {
            if (!UltraVoicePlugin.SoldierVoiceEnabled.value) return;

            if (__instance.eid.enemyType != EnemyType.Soldier) return;

            VoiceManager.CreateVoiceSource(__instance, "Soldier",
                SoldierCharacter.AttackClip,
                null,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(ZombieProjectiles), nameof(ZombieProjectiles.Melee))]
    class SoldierKickPatch
    {
        static void Postfix(ZombieProjectiles __instance)
        {
            if (!UltraVoicePlugin.SoldierVoiceEnabled.value) return;

            if (__instance.eid.enemyType != EnemyType.Soldier) return;

            if (__instance.tr != null)
            {
                AudioSource kickSource = __instance.tr.GetComponent<AudioSource>();
                if (kickSource != null)
                    kickSource.Stop();
            }

            VoiceManager.CreateVoiceSource(__instance, "Soldier",
                SoldierCharacter.AttackClip,
                null,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(ZombieProjectiles), nameof(ZombieProjectiles.OnGoLimp))]
    class SoldierDeathPatch
    {
        static void Postfix(ZombieProjectiles __instance)
        {
            if (!UltraVoicePlugin.SoldierVoiceEnabled.value) return;

            if (__instance.eid.enemyType != EnemyType.Soldier) return;

            VoiceManager.CreateVoiceSource(__instance, "Soldier",
                SoldierCharacter.DeathClip,
                null,
                true,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(Enemy), "Awake")]
    class ZombieMuteOnSoldierPatch
    {
        static void Postfix(Enemy __instance)
        {
            if (__instance == null || __instance.eid == null) return;

            if (__instance.eid.enemyType != EnemyType.Soldier) return;

            __instance.hurtSounds = new AudioClip[0];
            __instance.deathSound = null;
        }
    }
}
