using HarmonyLib;
using UltraVoice.Utilities;
using UnityEngine;

namespace UltraVoice.Characters
{
    public class ProvidenceCharacter
    {

        public static AudioClip[] SpawnClips;
        public static AudioClip[] AttackClips;
        public static AudioClip[] DodgeClips;

        public static readonly string[] SpawnSubs =
        {
            "The light descends.",
            "The light finds you.",
            "You cannot hide from the light.",
            "He must be found.",
            "His silence calls us here."
        };

        public static readonly string[] AttackSubs =
        {
            "Your end is written.",
            "You will answer.",
            "You chose this.",
            "This ends here.",
            "I will unmake you."
        };

        public static readonly string[] DodgeSubs =
        {
            "Foolish.",
            "Unwise.",
            "You know nothing."
        };

        public static bool IsProvidence(Drone d)
        {
            if (d == null || d.eid == null)
                return false;

            if (d.GetComponent<DroneFlesh>() != null)
                return false;

            return d.eid.enemyType == EnemyType.Providence;
        }

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            SpawnClips = UltraVoicePlugin.LoadClips("Providence.prov_Spawn{0}.wav", 5);
            AttackClips = UltraVoicePlugin.LoadClips("Providence.prov_Chatter{0}.wav", 5);
            DodgeClips = UltraVoicePlugin.LoadClips("Providence.prov_Dodge{0}.wav", 3);

            logger.LogInfo("Providence voice lines loaded successfully!");
        }

        public static void PlayAttackVoice(Drone drone)
        {
            if (!UltraVoicePlugin.ProvidenceVoiceEnabled.value) return;

            if (!IsProvidence(drone))
                return;

            if (!VoiceManager.CheckCooldown(drone, 5f))
                return;

            if (VoiceManager.TooSoonAfterSpawn(drone, 2f))
                return;

            VoiceManager.PlayRandomVoice(drone, "Providence",
                AttackClips,
                AttackSubs,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(Drone), "Start")]
    class ProvidenceSpawnPatch
    {
        static void Postfix(Drone __instance)
        {
            if (!UltraVoicePlugin.ProvidenceVoiceEnabled.value) return;

            if (!ProvidenceCharacter.IsProvidence(__instance))
                return;

            VoiceManager.enemySpawnTimes[__instance] = Time.time;

            VoiceManager.PlayRandomVoice(__instance, "Providence",
                ProvidenceCharacter.SpawnClips,
                ProvidenceCharacter.SpawnSubs,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(Drone), "Shoot")]
    class ProvidenceShootPatch
    {
        static void Postfix(Drone __instance)
        {
            ProvidenceCharacter.PlayAttackVoice(__instance);
        }
    }

    [HarmonyPatch(typeof(Drone), "ShootSecondary")]
    class ProvidencePincerPatch
    {
        static void Postfix(Drone __instance)
        {
            ProvidenceCharacter.PlayAttackVoice(__instance);
        }
    }

    [HarmonyPatch(typeof(Drone), "DodgeLaugh")]
    class ProvidenceDodgePatch
    {
        static void Postfix(Drone __instance)
        {
            if (!UltraVoicePlugin.ProvidenceVoiceEnabled.value) return;

            if (!ProvidenceCharacter.IsProvidence(__instance))
                return;

            if (!VoiceManager.CheckCooldown(__instance, 4f))
                return;

            if (VoiceManager.TooSoonAfterSpawn(__instance, 4f))
                return;

            VoiceManager.PlayRandomVoice(__instance, "Providence",
                ProvidenceCharacter.DodgeClips,
                ProvidenceCharacter.DodgeSubs,
                randomPitch: true
            );
        }
    }
}