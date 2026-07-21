using HarmonyLib;
using UnityEngine;
using System.Collections;
using UltraVoice.Utilities;

namespace UltraVoice.Characters
{
    public class GuttermanCharacter
    {

        public static AudioClip[] SpawnClips;
        public static AudioClip[] ShieldBreakClips;
        public static AudioClip[] EnrageClips;
        public static AudioClip[] DeathClips;
        public static AudioClip[] ParryClips;
        public static AudioClip[] PunchClips;

        public static AudioClip[] SpawnClipsLemen;
        public static AudioClip[] ShieldBreakClipsLemen;
        public static AudioClip[] EnrageClipsLemen;
        public static AudioClip[] DeathClipsLemen;
        public static AudioClip[] ParryClipsLemen;
        public static AudioClip[] PunchClipsLemen;

        public static readonly string[] SpawnSubs =
        {
            "THIS WILL NOT TAKE LONG.",
            "WHO MUST I KILL TODAY?",
            "IT IS TIME TO KILL.",
            "FOR THE MOTHERLAND!",
            "SAY YOUR PRAYERS.",
            "ЗА РОДИНУ!"
        };

        public static readonly string[] ShieldBreakSubs =
        {
            "SHIELD DOWN!",
            "SHIELD BROKEN!",
            "SHIELD DESTROYED!",
        };

        public static readonly string[] EnrageSubs =
        {
            "THAT COST MONEY!",
            "YOU WILL PAY FOR THIS!",
            "OH, YOU MAKE ME SO MAD…",
            "ЧЁРТ ПОБЕРИ!",
            "СУКИН СЫН!"
        };

        public static AudioClip[] UseGuttermanClips(AudioClip[] melClips, AudioClip[] lemenClips)
        {
            return UltraVoicePlugin.GuttermanVoiceActorField != null && UltraVoicePlugin.GuttermanVoiceActorField.value == UltraVoicePlugin.GuttermanVoiceActor.Lemen
                ? lemenClips
                : melClips;
        }

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            SpawnClips = UltraVoicePlugin.LoadClips("Gutterman.gm_Spawn{0}.wav", 6);
            PunchClips = UltraVoicePlugin.LoadClips("Gutterman.gm_Punch{0}.wav", 3);
            ShieldBreakClips = UltraVoicePlugin.LoadClips("Gutterman.gm_GuardBreak{0}.wav", 3);
            EnrageClips = UltraVoicePlugin.LoadClips("Gutterman.gm_Enrage{0}.wav", 5);
            DeathClips = UltraVoicePlugin.LoadClips("Gutterman.gm_Death{0}.wav", 3);
            ParryClips = UltraVoicePlugin.LoadClips("Gutterman.gm_Parry{0}.wav", 3);

            SpawnClipsLemen = UltraVoicePlugin.LoadClips("Gutterman.gm_Spawn{0}Lemen.wav", 6);
            PunchClipsLemen = UltraVoicePlugin.LoadClips("Gutterman.gm_Punch{0}Lemen.wav", 3);
            ShieldBreakClipsLemen = UltraVoicePlugin.LoadClips("Gutterman.gm_GuardBreak{0}Lemen.wav", 3);
            EnrageClipsLemen = UltraVoicePlugin.LoadClips("Gutterman.gm_Enrage{0}Lemen.wav", 5);
            DeathClipsLemen = UltraVoicePlugin.LoadClips("Gutterman.gm_Death{0}Lemen.wav", 3);
            ParryClipsLemen = UltraVoicePlugin.LoadClips("Gutterman.gm_Parry{0}Lemen.wav", 3);

            logger.LogInfo("Gutterman voice lines loaded successfully!");
        }
    }

    [HarmonyPatch(typeof(Gutterman), "Start")]
    class GuttermanSpawnPatch
    {
        static void Postfix(Gutterman __instance)
        {
            if (!UltraVoicePlugin.GuttermanVoiceEnabled.value) return;

            if (__instance.dead) return;

            VoiceManager.enemySpawnTimes[__instance] = Time.time;

            VoiceManager.PlayRandomVoice(__instance, "Gutterman",
                GuttermanCharacter.UseGuttermanClips(GuttermanCharacter.SpawnClips, GuttermanCharacter.SpawnClipsLemen),
                GuttermanCharacter.SpawnSubs,
                false,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(Gutterman), "ShieldBreak")]
    class GuttermanShieldBreakPatch
    {
        static void Postfix(Gutterman __instance)
        {
            if (!UltraVoicePlugin.GuttermanVoiceEnabled.value) return;

            if (__instance.enraged)
                VoiceManager.PlayRandomVoice(__instance, "Gutterman",
                    GuttermanCharacter.UseGuttermanClips(GuttermanCharacter.EnrageClips, GuttermanCharacter.EnrageClipsLemen),
                    GuttermanCharacter.EnrageSubs,
                    true,
                    randomPitch: true
                );
            else
                VoiceManager.PlayRandomVoice(__instance, "Gutterman",
                    GuttermanCharacter.UseGuttermanClips(GuttermanCharacter.ShieldBreakClips, GuttermanCharacter.ShieldBreakClipsLemen),
                    GuttermanCharacter.ShieldBreakSubs,
                    true,
                    randomPitch: true
                );
        }
    }

    [HarmonyPatch(typeof(Gutterman), "Death")]
    class GuttermanDeathPatch
    {
        static void Postfix(Gutterman __instance)
        {
            if (!UltraVoicePlugin.GuttermanVoiceEnabled.value) return;

            VoiceManager.PlayRandomVoice(__instance, "Gutterman",
                GuttermanCharacter.UseGuttermanClips(GuttermanCharacter.DeathClips, GuttermanCharacter.DeathClipsLemen),
                null,
                true,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(Gutterman), "GotParried")]
    class GuttermanParryPatch
    {
        static void Postfix(Gutterman __instance)
        {
            if (!UltraVoicePlugin.GuttermanVoiceEnabled.value) return;

            if (!VoiceManager.CheckCooldown(__instance, 3f))
                return;

            VoiceManager.PlayRandomVoice(__instance, "Gutterman",
                GuttermanCharacter.UseGuttermanClips(GuttermanCharacter.ParryClips, GuttermanCharacter.ParryClipsLemen),
                null,
                true,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(Gutterman), nameof(Gutterman.ShieldBash))]
    class GuttermanShieldBashPatch
    {
        static void Postfix(Gutterman __instance)
        {
            if (!UltraVoicePlugin.GuttermanVoiceEnabled.value) return;

            VoiceManager.PlayRandomVoice(__instance, "Gutterman",
                GuttermanCharacter.UseGuttermanClips(GuttermanCharacter.PunchClips, GuttermanCharacter.PunchClipsLemen),
                null,
                false,
                randomPitch: true
            );
        }
    }
}