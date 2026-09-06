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
            return UltraVoicePlugin.MirrorReaperVoiceActorField != null && UltraVoicePlugin.MirrorReaperVoiceActorField.Value == UltraVoicePlugin.MirrorReaperVoiceActor.Rotund
                ? rotundClips
                : notoClips;
        }

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            SpawnClips = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip("MirrorReaper.Noto.mr_SpawnSpecial.wav"),
                UltraVoicePlugin.LoadClip("MirrorReaper.Noto.mr_Spawn1.wav"),
                UltraVoicePlugin.LoadClip("MirrorReaper.Noto.mr_Spawn2.wav"),
                UltraVoicePlugin.LoadClip("MirrorReaper.Noto.mr_Spawn3.wav"),
            };

            LaughClips = UltraVoicePlugin.LoadClips("MirrorReaper.Noto.mr_Laugh{0}.wav", 3);
            MirrorTauntClips = UltraVoicePlugin.LoadClips("MirrorReaper.Noto.mr_MirrorTaunt{0}.wav", 3);
            PuppetHandClips = UltraVoicePlugin.LoadClips("MirrorReaper.Noto.mr_PuppetHand{0}.wav", 4);
            DeathClips = UltraVoicePlugin.LoadClips("MirrorReaper.Noto.mr_Death{0}.wav", 3);

            SpawnClipsRotund = new AudioClip[]
            {
                UltraVoicePlugin.LoadClip("MirrorReaper.Rotund.mr_SpawnSpecial.wav"),
                UltraVoicePlugin.LoadClip("MirrorReaper.Rotund.mr_Spawn1.wav"),
                UltraVoicePlugin.LoadClip("MirrorReaper.Rotund.mr_Spawn2.wav"),
                UltraVoicePlugin.LoadClip("MirrorReaper.Rotund.mr_Spawn3.wav"),
            };

            LaughClipsRotund = UltraVoicePlugin.LoadClips("MirrorReaper.Rotund.mr_Laugh{0}.wav", 3);
            MirrorTauntClipsRotund = UltraVoicePlugin.LoadClips("MirrorReaper.Rotund.mr_MirrorTaunt{0}.wav", 3);
            PuppetHandClipsRotund = UltraVoicePlugin.LoadClips("MirrorReaper.Rotund.mr_PuppetHand{0}.wav", 4);
            DeathClipsRotund = UltraVoicePlugin.LoadClips("MirrorReaper.Rotund.mr_Death{0}.wav", 3);

            logger.LogInfo("Mirror Reaper voice lines loaded successfully!");
        }

        public static void PlaySwingLaugh(MirrorReaper reaper)
        {
            if (!UltraVoicePlugin.MirrorReaperVoiceEnabled.Value)
                return;

            if (reaper == null || reaper.eid.dead)
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
            if (!UltraVoicePlugin.MirrorReaperVoiceEnabled.Value)
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
            if (!UltraVoicePlugin.MirrorReaperVoiceEnabled.Value)
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
            if (!UltraVoicePlugin.MirrorReaperVoiceEnabled.Value) return;

            if (!VoiceManager.CheckCooldown(__instance, 4f))
                return;

            VoiceManager.PlayRandomVoice(__instance, "MirrorReaper",
                MirrorReaperCharacter.UseMirrorReaperClips(MirrorReaperCharacter.PuppetHandClips, MirrorReaperCharacter.PuppetHandClipsRotund),
                MirrorReaperCharacter.PuppetHandSubs,
                interrupt: true,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(MirrorReaper), "Death")]
    class MirrorReaperDeathPatch
    {
        static void Postfix(MirrorReaper __instance)
        {
            if (!UltraVoicePlugin.MirrorReaperVoiceEnabled.Value) return;

            VoiceManager.PlayRandomVoice(__instance, "MirrorReaper",
                MirrorReaperCharacter.UseMirrorReaperClips(MirrorReaperCharacter.DeathClips, MirrorReaperCharacter.DeathClipsRotund),
                null,
                true,
                randomPitch: true
            );
        }
    }
}
