using HarmonyLib;
using System.Collections;
using UltraVoice.Utilities;
using UnityEngine;

namespace UltraVoice.Characters
{
    public class VirtueCharacter
    {

        public static AudioClip[] SpawnClips;
        public static AudioClip[] AttackClips;
        public static AudioClip[] EnrageClips;
        public static AudioClip[] DeathClips;

        public static AudioClip[] SpawnClipsVirchew;
        public static AudioClip[] AttackClipsVirchew;
        public static AudioClip[] EnrageClipsVirchew;
        public static AudioClip[] DeathClipsVirchew;

        public static readonly string[] SpawnSubs =
        {
            "It comes to this.",
            "I will do what must be done.",
            "I pray this ends swiftly.",
            "My orders have been given.",
            "Forgive me, machine."
        };

        public static readonly string[] AttackSubs =
        {
            "Why must it be this way?",
            "I take no joy in this.",
            "Do not make this harder for yourself.",
            "Must purpose demand such cruelty?",
            "I would spare you, if I could."
        };

        public static readonly string[] EnrageSubs =
        {
            "You force my hand!",
            "You leave me no choice!",
            "So be it..."
        };

        public static bool IsVirtue(Drone d)
        {
            if (d == null || d.eid == null)
                return false;

            if (d.GetComponent<DroneFlesh>() != null)
                return false;

            return d.eid.enemyType == EnemyType.Virtue;
        }

        public static AudioClip[] UseVirtueClips(AudioClip[] notoClips, AudioClip[] virchewClips)
        {
            return UltraVoicePlugin.VirtueVoiceActorField != null && UltraVoicePlugin.VirtueVoiceActorField.value == UltraVoicePlugin.VirtueVoiceActor.Virchew
                ? virchewClips
                : notoClips;
        }

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            SpawnClips = UltraVoicePlugin.LoadClips("Virtue.virtue_Spawn{0}.wav", 5);
            AttackClips = UltraVoicePlugin.LoadClips("Virtue.virtue_Attack{0}.wav", 5);
            EnrageClips = UltraVoicePlugin.LoadClips("Virtue.virtue_Enrage{0}.wav", 3);
            DeathClips = UltraVoicePlugin.LoadClips("Virtue.virtue_Death{0}.wav", 3);

            SpawnClipsVirchew = UltraVoicePlugin.LoadClips("Virtue.virtue_Spawn{0}Virchew.wav", 5);
            AttackClipsVirchew = UltraVoicePlugin.LoadClips("Virtue.virtue_Attack{0}Virchew.wav", 5);
            EnrageClipsVirchew = UltraVoicePlugin.LoadClips("Virtue.virtue_Enrage{0}Virchew.wav", 3);
            DeathClipsVirchew = UltraVoicePlugin.LoadClips("Virtue.virtue_Death{0}Virchew.wav", 3);

            logger.LogInfo("Virtue voice lines loaded successfully!");
        }
    }

    [HarmonyPatch(typeof(Drone), "Start")]
    class VirtueSpawnPatch
    {
        static void Postfix(Drone __instance)
        {
            if (!UltraVoicePlugin.VirtueVoiceEnabled.value) return;

            if (!VirtueCharacter.IsVirtue(__instance))
                return;

            VoiceManager.enemySpawnTimes[__instance] = Time.time;

            VoiceManager.PlayRandomVoice(__instance, "Virtue",
                VirtueCharacter.UseVirtueClips(VirtueCharacter.SpawnClips, VirtueCharacter.SpawnClipsVirchew),
                VirtueCharacter.SpawnSubs,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(VirtueInsignia), "Activating")]
    class VirtueAttackPatch
    {
        static void Postfix(VirtueInsignia __instance)
        {
            if (!UltraVoicePlugin.VirtueVoiceEnabled.value) return;

            Drone drone = null;

            if (__instance.parentEnemy)
                __instance.parentEnemy.TryGetComponent(out drone);
            else if (__instance.parentDrone)
                drone = __instance.parentDrone;

            if (drone == null || !VirtueCharacter.IsVirtue(drone))
                return;

            if (drone.isEnraged == true)
                return;

            if (!VoiceManager.CheckCooldown(drone, 4f))
                return;

            if (VoiceManager.TooSoonAfterSpawn(drone, 0.5f))
                return;

            if (Random.Range(0f, 1f) < 0.5f)
                VoiceManager.PlayRandomVoice(drone, "Virtue",
                        VirtueCharacter.UseVirtueClips(VirtueCharacter.AttackClips, VirtueCharacter.AttackClipsVirchew),
                        VirtueCharacter.AttackSubs,
                        randomPitch: true
                );
        }
    }

    [HarmonyPatch(typeof(Drone), "Enrage")]
    class VirtueEnragePatch
    {
        static void Postfix(Drone __instance)
        {
            if (!UltraVoicePlugin.VirtueVoiceEnabled.value) return;

            if (!VirtueCharacter.IsVirtue(__instance))
                return;

            UltraVoicePlugin.Instance.StartCoroutine(PlayEnrage(__instance));
        }

        static IEnumerator PlayEnrage(Drone drone)
        {
            yield return new WaitForSeconds(0.1f);

            VoiceManager.PlayRandomVoice(drone, "Virtue",
                VirtueCharacter.UseVirtueClips(VirtueCharacter.EnrageClips, VirtueCharacter.EnrageClipsVirchew),
                VirtueCharacter.EnrageSubs,
                interrupt: true,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(Drone), "Death")]
    class VirtueDeathPatch
    {
        static void Postfix(Drone __instance)
        {
            if (!UltraVoicePlugin.VirtueVoiceEnabled.value) return;

            if (!VirtueCharacter.IsVirtue(__instance))
                return;

            UltraVoicePlugin.Instance.StartCoroutine(PlayDeath(__instance));
        }

        static IEnumerator PlayDeath(Drone drone)
        {
            yield return new WaitForSeconds(0f);

            VoiceManager.PlayRandomVoice(drone, "Virtue",
                VirtueCharacter.UseVirtueClips(VirtueCharacter.DeathClips, VirtueCharacter.DeathClipsVirchew),
                null,
                interrupt: true,
                randomPitch: true
            );
        }
    }
}