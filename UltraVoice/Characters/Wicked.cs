using HarmonyLib;
using System.Collections;
using UltraVoice.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UltraVoice.Characters
{
    public class WickedCharacter
    {
        public static AudioClip[] ChaseClips;
        public static AudioClip SkullPickupClip;
        public static AudioClip DeathClip;

        public static readonly string[] ChaseSubs =
        {
            "LEAVE...",
            "GET OUT...",
            "UNWELCOME...",
            "TURN... BACK..."
        };

        public static readonly string SkullPickupSub = "NO...";
        public static readonly string DeathSub = "RELEASE...";

        public static readonly Color WickedColor = new Color(0.2f, 0.2f, 0.2f);

        public const string WickedSceneName = "07b47256f0da7f941947e74905ad16b4";

        public static bool InWickedLevel()
        {
            return SceneManager.GetActiveScene().name == WickedSceneName;
        }

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            ChaseClips = UltraVoicePlugin.LoadClips("Wicked.wicked_Chase{0}.wav", 4);

            SkullPickupClip = UltraVoicePlugin.LoadClip("Wicked.wicked_SkullPickup.wav");
            DeathClip = UltraVoicePlugin.LoadClip("Wicked.wicked_Death.wav");

            logger.LogInfo("Wicked voice lines loaded successfully!");
        }
    }

    [HarmonyPatch(typeof(Wicked), "Update")]
    class WickedChasePatch
    {
        static void Postfix(Wicked __instance)
        {
            if (!UltraVoicePlugin.WickedVoiceEnabled.value) return;

            if (ULTRAKILL.Cheats.BlindEnemies.Blind)
                return;

            if (!WickedCharacter.InWickedLevel())
                return;

            if (!VoiceManager.CheckCooldown(__instance, 3f))
                return;

            if (Random.Range(0f, 1f) < 0.5f)
                return;

            if (WickedCharacter.ChaseClips == null || WickedCharacter.ChaseClips.Length == 0)
                return;

            int i = Random.Range(0, WickedCharacter.ChaseClips.Length);
            string sub = i < WickedCharacter.ChaseSubs.Length ? WickedCharacter.ChaseSubs[i] : null;

            VoiceManager.CreateVoiceSource(
                MonoSingleton<NewMovement>.Instance,
                "Wicked_Chase",
                WickedCharacter.ChaseClips[i],
                sub,
                subtitleColor: WickedCharacter.WickedColor,
                randomPitch: true,
                spatialBlend: 0f
            );
        }
    }

    [HarmonyPatch(typeof(ItemIdentifier), "PickUp")]
    class WickedSkullPickupPatch
    {
        static void Postfix(ItemIdentifier __instance)
        {
            if (!UltraVoicePlugin.WickedVoiceEnabled.value) return;

            if (__instance == null || __instance.itemType != ItemType.SkullRed)
                return;

            if (!WickedCharacter.InWickedLevel())
                return;

            UltraVoicePlugin.Instance.StartCoroutine(PlaySkullPickup());
        }

        static IEnumerator PlaySkullPickup()
        {
            yield return new WaitForSeconds(1f);

            if (WickedCharacter.SkullPickupClip == null)
                yield break;

            VoiceManager.CreateVoiceSource(
                MonoSingleton<NewMovement>.Instance,
                "Wicked_SkullPickup",
                WickedCharacter.SkullPickupClip,
                WickedCharacter.SkullPickupSub,
                subtitleColor: WickedCharacter.WickedColor,
                randomPitch: true,
                spatialBlend: 0f
            );
        }
    }

    [HarmonyPatch(typeof(Skull), "OnCorrectUse")]
    class WickedSkullPlacedPatch
    {
        static void Postfix(Skull __instance)
        {
            if (!UltraVoicePlugin.WickedVoiceEnabled.value) return;

            if (__instance == null)
                return;

            if (!WickedCharacter.InWickedLevel())
                return;

            ItemIdentifier item = __instance.GetComponent<ItemIdentifier>();
            if (item == null || item.itemType != ItemType.SkullRed)
                return;

            if (WickedCharacter.DeathClip == null)
                return;

            VoiceManager.CreateVoiceSource(
                MonoSingleton<NewMovement>.Instance,
                "Wicked_Death",
                WickedCharacter.DeathClip,
                WickedCharacter.DeathSub,
                shouldInterrupt: true,
                subtitleColor: WickedCharacter.WickedColor,
                randomPitch: true,
                spatialBlend: 0f
            );
        }
    }
}
