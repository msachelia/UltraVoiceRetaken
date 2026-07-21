using HarmonyLib;
using System.Collections;
using UnityEngine;
using UltraVoice.Utilities;

namespace UltraVoice.Characters
{
    public class HideousMassCharacter
    {
        public static AudioClip AwakenClip;
        public static AudioClip[] ChatterClips;
        public static AudioClip[] EnrageClips;
        public static AudioClip[] HarpoonClips;
        public static AudioClip[] ParryClips;
        public static AudioClip[] DeathClips;

        public static readonly string[] ChatterSubs =
        {
            "KILL, KILL!",
            "HURT, HURT!",
            "DIE, DIE!",
            "NO MERCY!",
            "CRUSH, CRUSH!"
        };

        public static readonly string[] EnrageSubs =
        {
            "HATE, HATE!",
            "JUST DIE!",
            null,
            null
        };

        public static readonly string[] HarpoonSubs =
        {
            "STAY STILL!",
            "STOP MOVING!",
            "DON'T MOVE!"
        };

        public static readonly string[] ParrySubs =
        {
            null,
            null,
            "PAIN!"
        };

        public static Color HideousMassColor => VoiceManager.GetEnemyTypeColor(EnemyType.HideousMass);

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            AwakenClip = UltraVoicePlugin.LoadClip("HideousMass.mass_SpawnSpecial.wav");

            ChatterClips = UltraVoicePlugin.LoadClips("HideousMass.mass_Generic{0}.wav", 5);
            EnrageClips = UltraVoicePlugin.LoadClips("HideousMass.mass_Enrage{0}.wav", 4);
            HarpoonClips = UltraVoicePlugin.LoadClips("HideousMass.mass_Harpoon{0}.wav", 3);
            ParryClips = UltraVoicePlugin.LoadClips("HideousMass.mass_Parried{0}.wav", 3);
            DeathClips = UltraVoicePlugin.LoadClips("HideousMass.mass_Death{0}.wav", 2);

            logger.LogInfo("Hideous Mass voice lines loaded successfully!");
        }
    }

    [HarmonyPatch(typeof(Mass), "Update")]
    class MassChatterPatch
    {
        static void Postfix(Mass __instance)
        {
            if (!UltraVoicePlugin.MassVoiceEnabled.value)
                return;

            if (ULTRAKILL.Cheats.BlindEnemies.Blind)
                return;

            if (__instance == null || __instance.isDead)
                return;

            if (!VoiceManager.CheckCooldown(__instance, 6f))
                return;

            if (Random.Range(0f, 1f) >= 0.75f)
                return;

            VoiceManager.PlayRandomVoice(__instance, "HideousMass",
                HideousMassCharacter.ChatterClips,
                HideousMassCharacter.ChatterSubs,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(Mass), nameof(Mass.ReadySpear))]
    class MassHarpoonPatch
    {
        static void Postfix(Mass __instance)
        {
            if (!UltraVoicePlugin.MassVoiceEnabled.value)
                return;

            if (__instance == null || __instance.eid.dead)
                return;

            VoiceManager.PlayRandomVoice(__instance, "HideousMass",
                HideousMassCharacter.HarpoonClips,
                HideousMassCharacter.HarpoonSubs,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(FakeMassActivator), "OnEnable")]
    class MassActivatePatch
    {
        static void Postfix(FakeMassActivator __instance)
        {
            if (!UltraVoicePlugin.MassVoiceEnabled.value)
                return;

            if (__instance == null)
                return;

            UltraVoicePlugin.Instance.StartCoroutine(PlayAwaken(__instance));

            static IEnumerator PlayAwaken(FakeMassActivator activator)
            {
                yield return new WaitForSeconds(0.75f);

                if (activator == null)
                    yield break;

                VoiceManager.CreateVoiceSource(activator, "HideousMass",
                    HideousMassCharacter.AwakenClip,
                    "WHO DARES DISTURB ME?",
                    subtitleColor: HideousMassCharacter.HideousMassColor,
                    randomPitch: true
                );
            }
        }
    }

    [HarmonyPatch(typeof(Mass), nameof(Mass.SpearParried))]
    class MassSpearParryPatch
    {
        static void Postfix(Mass __instance)
        {
            if (!UltraVoicePlugin.MassVoiceEnabled.value)
                return;

            if (__instance == null || __instance.eid.dead)
                return;

            VoiceManager.PlayRandomVoice(__instance, "HideousMass",
                HideousMassCharacter.ParryClips,
                HideousMassCharacter.ParrySubs,
                true,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(Mass), nameof(Mass.Enrage))]
    class MassEnragePatch
    {
        static void Postfix(Mass __instance)
        {
            if (!UltraVoicePlugin.MassVoiceEnabled.value)
                return;

            if (__instance == null || __instance.eid.dead)
                return;

            VoiceManager.PlayRandomVoice(__instance, "HideousMass",
                HideousMassCharacter.EnrageClips,
                HideousMassCharacter.EnrageSubs,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(Mass), nameof(Mass.OnGoLimp))]
    class MassDeathPatch
    {
        static void Postfix(Mass __instance)
        {
            if (!UltraVoicePlugin.MassVoiceEnabled.value)
                return;

            if (__instance == null || __instance.eid.dead)
                return;

            VoiceManager.PlayRandomVoiceDelayed(1f, __instance, "HideousMass",
                HideousMassCharacter.DeathClips,
                null,
                interrupt: true,
                randomPitch: true
            );
        }
    }
}
