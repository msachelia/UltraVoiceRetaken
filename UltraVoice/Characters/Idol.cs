using HarmonyLib;
using System.Collections.Generic;
using UltraVoice.Utilities;
using UnityEngine;

namespace UltraVoice.Characters
{
    public class IdolCharacter
    {
        public static AudioClip[] BlessClips;
        public static AudioClip[] VulnerableClips;

        public static AudioClip[] BlessClipsVirchew;
        public static AudioClip[] VulnerableClipsVirchew;

        public static readonly string[] BlessSubs =
        {
            "Peace be upon thee...",
            "Thou art safe...",
            "I bless thee...",
            "No harm shall befall thee...",
            "A kindness for thee, sinner..."
        };

        public static readonly string[] VulnerableSubs =
        {
            "I am vulnerable.",
            "I ask for thy mercy.",
            "I deserve not thy ire.",
            "Spare me thy wrath.",
        };

        public static AudioClip[] UseIdolClips(AudioClip[] voinviClips, AudioClip[] virchewClips)
        {
            return UltraVoicePlugin.IdolVoiceActorField != null && UltraVoicePlugin.IdolVoiceActorField.value == UltraVoicePlugin.IdolVoiceActor.Virchew
                ? virchewClips
                : voinviClips;
        }

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            BlessClips = UltraVoicePlugin.LoadClips("Idol.idol_Spawn{0}.wav", 5);
            VulnerableClips = UltraVoicePlugin.LoadClips("Idol.idol_Vulnerable{0}.wav", 4);

            BlessClipsVirchew = UltraVoicePlugin.LoadClips("Idol.idol_Spawn{0}Virchew.wav", 5);
            VulnerableClipsVirchew = UltraVoicePlugin.LoadClips("Idol.idol_Vulnerable{0}Virchew.wav", 4);

            logger.LogInfo("Idol voice lines loaded successfully!");
        }

        public static bool CanPlayerSeeIdol(NewMovement player, Idol idol)
        {
            Vector3 playerPos = player.transform.position;
            Vector3 idolPos = idol.transform.position;

            Vector3 direction = idolPos - playerPos;
            float distance = direction.magnitude;

            if (distance > 20f)
                return false;

            if (Physics.Raycast(playerPos, direction.normalized, out RaycastHit hit, distance))
            {
                return hit.transform == idol.transform ||
                       hit.transform.IsChildOf(idol.transform);
            }

            return false;
        }

        private static HashSet<Idol> visibleLastFrame = new HashSet<Idol>();

        public static bool JustBecameVisible(NewMovement player, Idol idol)
        {
            bool visible = CanPlayerSeeIdol(player, idol);

            if (visible && !visibleLastFrame.Contains(idol))
            {
                visibleLastFrame.Add(idol);
                return true;
            }

            if (!visible)
            {
                visibleLastFrame.Remove(idol);
            }

            return false;
        }
    }

    [HarmonyPatch(typeof(Idol), nameof(Idol.ChangeTarget))]
    class IdolProtectPatch
    {
        static void Postfix(Idol __instance)
        {
            if (!UltraVoicePlugin.IdolVoiceEnabled.value)
                return;

            if (SceneHelper.CurrentScene == "Level 7-4")
                return;

            VoiceManager.PlayRandomVoice(__instance, "Idol",
                    IdolCharacter.UseIdolClips(IdolCharacter.BlessClips, IdolCharacter.BlessClipsVirchew),
                    IdolCharacter.BlessSubs,
                    randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(Idol), nameof(Idol.Update))]
    class IdolVulnerablePatch
    {
        static void Postfix(Idol __instance)
        {
            if (!UltraVoicePlugin.IdolVoiceEnabled.value)
                return;

            if (!IdolCharacter.JustBecameVisible(NewMovement.Instance, __instance)) return;

            VoiceManager.PlayRandomVoiceDelayed(0.1f, __instance, "Idol",
                IdolCharacter.UseIdolClips(IdolCharacter.VulnerableClips, IdolCharacter.VulnerableClipsVirchew),
                IdolCharacter.VulnerableSubs,
                randomPitch: true
            );
        }
    }
}
