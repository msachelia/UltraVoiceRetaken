using HarmonyLib;
using UnityEngine;
using UltraVoice.Utilities;

namespace UltraVoice.Characters
{
    public class MirrorReaperCharacter
    {
        public static AudioClip[] SpawnClips;
        public static AudioClip[] LaughClips;
        public static AudioClip[] MirrorTauntClips;
        public static AudioClip[] PuppetHandClips;
        public static AudioClip[] DeathClips;

        public static AudioClip[] SpawnClipsRotund;
        public static AudioClip[] LaughClipsRotund;
        public static AudioClip[] MirrorTauntClipsRotund;
        public static AudioClip[] PuppetHandClipsRotund;
        public static AudioClip[] DeathClipsRotund;

        public static readonly string[] SpawnSubs =
        {
            "I FOUND YOU!",
            "THERE YOU ARE!",
            "FOUND YOU! FOUND YOU!",
            "YOU CAN'T HIDE!"
        };

        public static readonly string[] MirrorTauntSubs =
        {
            "TURN AROUND...",
            "LOOK IN THE MIRROR...",
            "DO YOU SEE ME? DO YOU SEE ME?!"
        };

        public static readonly string[] PuppetHandSubs =
        {
            "TAKE HOLD!",
            "NO ESCAPE!",
            "FIND THEM!",
            "SEEK THEM OUT!"
        };

        public static AudioClip[] UseMirrorReaperClips(AudioClip[] notoClips, AudioClip[] rotundClips)
        {
            return UltraVoicePlugin.MirrorReaperVoiceActorField != null && UltraVoicePlugin.MirrorReaperVoiceActorField.value == UltraVoicePlugin.MirrorReaperVoiceActor.Rotund
                ? rotundClips
                : notoClips;
        }

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            SpawnClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip("MirrorReaper.mr_SpawnSpecial.wav"),
                UltraVoicePlugin.LoadClip("MirrorReaper.mr_Spawn1.wav"),
                UltraVoicePlugin.LoadClip("MirrorReaper.mr_Spawn2.wav"),
                UltraVoicePlugin.LoadClip("MirrorReaper.mr_Spawn3.wav"),
            };

            LaughClips = UltraVoicePlugin.LoadClips("MirrorReaper.mr_Laugh{0}.wav", 3);
            MirrorTauntClips = UltraVoicePlugin.LoadClips("MirrorReaper.mr_MirrorTaunt{0}.wav", 3);
            PuppetHandClips = UltraVoicePlugin.LoadClips("MirrorReaper.mr_PuppetHand{0}.wav", 4);
            DeathClips = UltraVoicePlugin.LoadClips("MirrorReaper.mr_Death{0}.wav", 3);

            SpawnClipsRotund = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip("MirrorReaper.mr_SpawnSpecialRotund.wav"),
                UltraVoicePlugin.LoadClip("MirrorReaper.mr_Spawn1Rotund.wav"),
                UltraVoicePlugin.LoadClip("MirrorReaper.mr_Spawn2Rotund.wav"),
                UltraVoicePlugin.LoadClip("MirrorReaper.mr_Spawn3Rotund.wav"),
            };

            LaughClipsRotund = UltraVoicePlugin.LoadClips("MirrorReaper.mr_Laugh{0}Rotund.wav", 3);
            MirrorTauntClipsRotund = UltraVoicePlugin.LoadClips("MirrorReaper.mr_MirrorTaunt{0}Rotund.wav", 3);
            PuppetHandClipsRotund = UltraVoicePlugin.LoadClips("MirrorReaper.mr_PuppetHand{0}Rotund.wav", 4);
            DeathClipsRotund = UltraVoicePlugin.LoadClips("MirrorReaper.mr_Death{0}Rotund.wav", 3);

            logger.LogInfo("Mirror Reaper voice lines loaded successfully!");
        }

        public static void PlaySwingLaugh(MirrorReaper reaper)
        {
            if (!UltraVoicePlugin.MirrorReaperVoiceEnabled.value)
                return;

            if (reaper == null || reaper.eid.dead)
                return;

            if (Random.Range(0f, 1f) < 0.5f)
                return;

            VoiceManager.PlayRandomVoice(reaper, "MirrorReaper",
                UseMirrorReaperClips(LaughClips, LaughClipsRotund),
                null,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(BossHealthBar), "OnEnable")]
    class MirrorReaperBossSpawnPatch
    {
        static void Postfix(BossHealthBar __instance)
        {
            if (!UltraVoicePlugin.MirrorReaperVoiceEnabled.value)
                return;

            if (SceneHelper.CurrentScene != "Level 8-2")
                return;

            MirrorReaper reaper = __instance.GetComponentInParent<MirrorReaper>();

            if (reaper == null)
                return;

            VoiceManager.enemySpawnTimes[reaper] = Time.time;

            VoiceManager.PlayRandomVoice(reaper, "MirrorReaper",
                MirrorReaperCharacter.UseMirrorReaperClips(MirrorReaperCharacter.SpawnClips, MirrorReaperCharacter.SpawnClipsRotund),
                MirrorReaperCharacter.SpawnSubs,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(MirrorReaper), "Start")]
    class MirrorReaperCommonSpawnPatch
    {
        static void Postfix(MirrorReaper __instance)
        {
            if (!UltraVoicePlugin.MirrorReaperVoiceEnabled.value)
                return;

            if (SceneHelper.CurrentScene == "Level 8-2")
                return;

            VoiceManager.enemySpawnTimes[__instance] = Time.time;

            VoiceManager.PlayRandomVoice(__instance, "MirrorReaper",
                MirrorReaperCharacter.UseMirrorReaperClips(MirrorReaperCharacter.SpawnClips, MirrorReaperCharacter.SpawnClipsRotund),
                MirrorReaperCharacter.SpawnSubs,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(MirrorReaper), "Update")]
    class MirrorReaperChatterPatch
    {
        static void Postfix(MirrorReaper __instance)
        {
            if (!UltraVoicePlugin.MirrorReaperVoiceEnabled.value) return;

            if (ULTRAKILL.Cheats.BlindEnemies.Blind)
                return;

            if (__instance == null || __instance.eid.dead)
                return;

            if (!VoiceManager.CheckCooldown(__instance, 4f))
                return;

            if (Random.Range(0f, 1f) >= 0.5f)
                return;

            if (__instance.inMirrorPhase)
                VoiceManager.PlayRandomVoice(__instance, "MirrorReaper",
                    MirrorReaperCharacter.UseMirrorReaperClips(MirrorReaperCharacter.MirrorTauntClips, MirrorReaperCharacter.MirrorTauntClipsRotund),
                    MirrorReaperCharacter.MirrorTauntSubs,
                    randomPitch: true
                );
            else
                VoiceManager.PlayRandomVoice(__instance, "MirrorReaper",
                    MirrorReaperCharacter.UseMirrorReaperClips(MirrorReaperCharacter.LaughClips, MirrorReaperCharacter.LaughClipsRotund),
                    null,
                    randomPitch: true
                );
        }
    }

    [HarmonyPatch(typeof(MirrorReaper), "SwingTriple")]
    class MirrorReaperSwingTriplePatch
    {
        static void Postfix(MirrorReaper __instance)
        {
            MirrorReaperCharacter.PlaySwingLaugh(__instance);
        }
    }

    [HarmonyPatch(typeof(MirrorReaper), "SwingVertical")]
    class MirrorReaperSwingVerticalPatch
    {
        static void Postfix(MirrorReaper __instance)
        {
            MirrorReaperCharacter.PlaySwingLaugh(__instance);
        }
    }

    [HarmonyPatch(typeof(MirrorReaper), "SwingSpree")]
    class MirrorReaperSwingSpreePatch
    {
        static void Postfix(MirrorReaper __instance)
        {
            MirrorReaperCharacter.PlaySwingLaugh(__instance);
        }
    }

    [HarmonyPatch(typeof(MirrorReaper), nameof(MirrorReaper.GroundWave))]
    class MirrorReaperHandPatch
    {
        static void Postfix(MirrorReaper __instance)
        {
            if (!UltraVoicePlugin.MirrorReaperVoiceEnabled.value) return;

            if (!VoiceManager.CheckCooldown(__instance, 4f))
                return;

            VoiceManager.PlayRandomVoice(__instance, "MirrorReaper",
                MirrorReaperCharacter.UseMirrorReaperClips(MirrorReaperCharacter.PuppetHandClips, MirrorReaperCharacter.PuppetHandClipsRotund),
                MirrorReaperCharacter.PuppetHandSubs,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(MirrorReaper), "Death")]
    class MirrorReaperDeathPatch
    {
        static void Postfix(MirrorReaper __instance)
        {
            if (!UltraVoicePlugin.MirrorReaperVoiceEnabled.value) return;

            VoiceManager.PlayRandomVoice(__instance, "MirrorReaper",
                MirrorReaperCharacter.UseMirrorReaperClips(MirrorReaperCharacter.DeathClips, MirrorReaperCharacter.DeathClipsRotund),
                null,
                true,
                randomPitch: true
            );
        }
    }
}
