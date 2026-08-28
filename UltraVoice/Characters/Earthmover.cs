using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UltraVoice.Utilities;

namespace UltraVoice.Characters
{
    public class EarthmoverCharacter
    {

        public static AudioClip IntroClip;
        public static AudioClip ShootClip;
        public static AudioClip DeathClip;
        public static AudioClip IdolClip;
        public static AudioClip BloodVentClip;
        public static AudioClip DefenseSystemClip;
        public static AudioClip BloodFloodClip;
        public static AudioClip BrainBattleClip;
        public static AudioClip BrainKilledClip;

        public static AudioClip IntroClipTondar;
        public static AudioClip ShootClipTondar;
        public static AudioClip DeathClipTondar;
        public static AudioClip IdolClipTondar;
        public static AudioClip BloodVentClipTondar;
        public static AudioClip DefenseSystemClipTondar;
        public static AudioClip BloodFloodClipTondar;
        public static AudioClip BrainBattleClipTondar;
        public static AudioClip BrainKilledClipTondar;

        public static AudioClip IntroClipMegaslash;
        public static AudioClip ShootClipMegaslash;
        public static AudioClip DeathClipMegaslash;
        public static AudioClip IdolClipMegaslash;
        public static AudioClip BloodVentClipMegaslash;
        public static AudioClip DefenseSystemClipMegaslash;
        public static AudioClip BloodFloodClipMegaslash;
        public static AudioClip BrainBattleClipMegaslash;
        public static AudioClip BrainKilledClipMegaslash;

        public static UltraVoicePlugin.EarthmoverVoiceActor CurrentActor =>
            UltraVoicePlugin.EarthmoverVoiceActorField != null
                ? UltraVoicePlugin.EarthmoverVoiceActorField.Value
                : UltraVoicePlugin.EarthmoverVoiceActor.Rotund;

        public static AudioClip UseEarthmoverClip(AudioClip rotundClip, AudioClip tondarClip, AudioClip megaslashClip)
        {
            switch (CurrentActor)
            {
                case UltraVoicePlugin.EarthmoverVoiceActor.Tondar:
                    return tondarClip;
                case UltraVoicePlugin.EarthmoverVoiceActor.Megaslash:
                    return megaslashClip;
                default:
                    return rotundClip;
            }
        }

        public static float UseEarthmoverDelay(float rotundDelay, float tondarDelay, float megaslashDelay)
        {
            switch (CurrentActor)
            {
                case UltraVoicePlugin.EarthmoverVoiceActor.Tondar:
                    return tondarDelay;
                case UltraVoicePlugin.EarthmoverVoiceActor.Megaslash:
                    return megaslashDelay;
                default:
                    return rotundDelay;
            }
        }

        public const string EarthmoverSceneName = "6e981b1865c649749a610aafc471e198";

        public static float lastBloodFloodTime;
        public static float lastBrainBattleTime;
        public static float lastSelfDestructTime;

        public static bool IntroPlayed = false;

        public static bool DeathPlayed = false;

        public static float lastIdolTime = -999f;

        public static void TryIntro(string bossName)
        {
            if (IntroPlayed)
                return;

            if (string.IsNullOrEmpty(bossName) || !bossName.Contains("EARTHMOVER"))
                return;

            if (!UltraVoicePlugin.EarthmoverVoiceEnabled.Value)
                return;

            IntroPlayed = true;

            PlayAnnouncement(3f, "Earthmover_Intro", UseEarthmoverClip(IntroClip, IntroClipTondar, IntroClipMegaslash),
                "HIGH-PRIORITY TARGET DETECTED.",
                (2.15f, 2.55f, 2.9f, "READYING WEAPONS.")
            );
        }

        public static bool InEarthmoverLevel()
        {
            return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == EarthmoverSceneName;
        }

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            IntroClip = UltraVoicePlugin.LoadClip("Earthmover.Rotund.em_Intro.wav");
            ShootClip = UltraVoicePlugin.LoadClip("Earthmover.Rotund.em_Shoot.wav");
            DeathClip = UltraVoicePlugin.LoadClip("Earthmover.Rotund.em_Death.wav");
            IdolClip = UltraVoicePlugin.LoadClip("Earthmover.Rotund.em_Idol.wav");
            BloodVentClip = UltraVoicePlugin.LoadClip("Earthmover.Rotund.em_BloodVent.wav");
            DefenseSystemClip = UltraVoicePlugin.LoadClip("Earthmover.Rotund.em_DefenseSystem.wav");
            BloodFloodClip = UltraVoicePlugin.LoadClip("Earthmover.Rotund.em_BloodFlood.wav");
            BrainBattleClip = UltraVoicePlugin.LoadClip("Earthmover.Rotund.em_BrainBattle.wav");
            BrainKilledClip = UltraVoicePlugin.LoadClip("Earthmover.Rotund.em_BrainKilled.wav");

            IntroClipTondar = UltraVoicePlugin.LoadClip("Earthmover.Tondar.em_Intro.wav");
            ShootClipTondar = UltraVoicePlugin.LoadClip("Earthmover.Tondar.em_Shoot.wav");
            DeathClipTondar = UltraVoicePlugin.LoadClip("Earthmover.Tondar.em_Death.wav");
            IdolClipTondar = UltraVoicePlugin.LoadClip("Earthmover.Tondar.em_Idol.wav");
            BloodVentClipTondar = UltraVoicePlugin.LoadClip("Earthmover.Tondar.em_BloodVent.wav");
            DefenseSystemClipTondar = UltraVoicePlugin.LoadClip("Earthmover.Tondar.em_DefenseSystem.wav");
            BloodFloodClipTondar = UltraVoicePlugin.LoadClip("Earthmover.Tondar.em_BloodFlood.wav");
            BrainBattleClipTondar = UltraVoicePlugin.LoadClip("Earthmover.Tondar.em_BrainBattle.wav");
            BrainKilledClipTondar = UltraVoicePlugin.LoadClip("Earthmover.Tondar.em_BrainKilled.wav");

            IntroClipMegaslash = UltraVoicePlugin.LoadClip("Earthmover.Megaslash.em_Intro.wav");
            ShootClipMegaslash = UltraVoicePlugin.LoadClip("Earthmover.Megaslash.em_Shoot.wav");
            DeathClipMegaslash = UltraVoicePlugin.LoadClip("Earthmover.Megaslash.em_Death.wav");
            IdolClipMegaslash = UltraVoicePlugin.LoadClip("Earthmover.Megaslash.em_Idol.wav");
            BloodVentClipMegaslash = UltraVoicePlugin.LoadClip("Earthmover.Megaslash.em_BloodVent.wav");
            DefenseSystemClipMegaslash = UltraVoicePlugin.LoadClip("Earthmover.Megaslash.em_DefenseSystem.wav");
            BloodFloodClipMegaslash = UltraVoicePlugin.LoadClip("Earthmover.Megaslash.em_BloodFlood.wav");
            BrainBattleClipMegaslash = UltraVoicePlugin.LoadClip("Earthmover.Megaslash.em_BrainBattle.wav");
            BrainKilledClipMegaslash = UltraVoicePlugin.LoadClip("Earthmover.Megaslash.em_BrainKilled.wav");

            logger.LogInfo("Earthmover voice lines loaded successfully!");
        }

        public static void PlayAnnouncement(float startDelay, string sourceName, AudioClip clip, string firstSub, params (float rotundDelay, float tondarDelay, float megaslashDelay, string text)[] followUps)
        {
            UltraVoicePlugin.Instance.StartCoroutine(Routine());

            IEnumerator Routine()
            {
                yield return new WaitForSeconds(startDelay);

                var src = VoiceManager.CreateVoiceSource(
                    MonoSingleton<NewMovement>.Instance,
                    sourceName,
                    clip,
                    firstSub,
                    true,
                    spatialBlend: 0f
                );

                if (src == null)
                    yield break;

                foreach (var (rotundDelay, tondarDelay, megaslashDelay, text) in followUps)
                {
                    yield return new WaitForSeconds(UseEarthmoverDelay(rotundDelay, tondarDelay, megaslashDelay));

                    if (src == null)
                        yield break;

                    VoiceManager.ShowSubtitle(text, src);
                }
            }
        }
    }

    [HarmonyPatch(typeof(EnemyIdentifier), nameof(EnemyIdentifier.Bless))]
    class EarthmoverIdolPatch
    {
        static void Prefix(EnemyIdentifier __instance, out bool __state)
        {
            __state = __instance != null && __instance.blessed;
        }

        static void Postfix(EnemyIdentifier __instance, bool ignorePrevious, bool __state)
        {
            if (__instance == null || __instance.enemyType != EnemyType.Centaur)
                return;

            if (__state || ignorePrevious || !__instance.blessed)
                return;

            if (!EarthmoverCharacter.InEarthmoverLevel())
                return;

            if (!UltraVoicePlugin.EarthmoverVoiceEnabled.Value)
                return;

            if (Time.time - EarthmoverCharacter.lastIdolTime < 5f)
                return;

            EarthmoverCharacter.lastIdolTime = Time.time;

            EarthmoverCharacter.PlayAnnouncement(0f, "Earthmover_Idol", EarthmoverCharacter.UseEarthmoverClip(EarthmoverCharacter.IdolClip, EarthmoverCharacter.IdolClipTondar, EarthmoverCharacter.IdolClipMegaslash), "ENGAGING SHIELD.");
        }
    }

    [HarmonyPatch(typeof(Countdown), "Update")]
    class EarthmoverDeathPatch
    {
        static void Postfix(Countdown __instance)
        {
            if (EarthmoverCharacter.DeathPlayed)
                return;

            if (!UltraVoicePlugin.EarthmoverVoiceEnabled.Value)
                return;

            if (!EarthmoverCharacter.InEarthmoverLevel())
                return;

            if (__instance.name != "Countdown" || __instance.paused || __instance.time > 2.7f)
                return;

            EarthmoverCharacter.DeathPlayed = true;

            EarthmoverCharacter.PlayAnnouncement(0f, "Earthmover_Death", EarthmoverCharacter.UseEarthmoverClip(EarthmoverCharacter.DeathClip, EarthmoverCharacter.DeathClipTondar, EarthmoverCharacter.DeathClipMegaslash), "WELL-FOUGHT.");
        }
    }

    [HarmonyPatch(typeof(AudioSource))]
    class EarthmoverShootPatch
    {
        static IEnumerable<MethodBase> TargetMethods()
        {
            return AccessTools
                .GetDeclaredMethods(typeof(AudioSource))
                .Where(method => !method.IsStatic && !method.IsSpecialName && method.Name.StartsWith("Play"));
        }

        static void Postfix(AudioSource __instance)
        {
            if (!UltraVoicePlugin.EarthmoverVoiceEnabled.Value)
                return;

            if (__instance == null || __instance.clip == null)
                return;

            if (!EarthmoverCharacter.InEarthmoverLevel())
                return;

            if (__instance.clip.name != "Centaur A-1")
                return;

            if (!VoiceManager.CheckEventCooldown("Earthmover_Shoot", 1f))
                return;

            EarthmoverCharacter.PlayAnnouncement(3f, "Earthmover_Shoot", EarthmoverCharacter.UseEarthmoverClip(EarthmoverCharacter.ShootClip, EarthmoverCharacter.ShootClipTondar, EarthmoverCharacter.ShootClipMegaslash),
                "FIRING SEQUENCE INTERRUPTED.",
                (2.1f, 2.05f, 2.55f, "EXTERIOR HULL INTEGRITY:"),
                (1.5f, 1.6f, 2.4f, "60%.")
            );
        }
    }

    [HarmonyPatch(typeof(BossHealthBar), "OnEnable")]
    class EarthmoverIntroPatch
    {
        static void Postfix(BossHealthBar __instance)
        {
            EarthmoverCharacter.TryIntro(__instance.bossName);
        }
    }

    [HarmonyPatch(typeof(CombinedBossBar), "OnEnable")]
    class EarthmoverIntroCombinedPatch
    {
        static void Postfix(CombinedBossBar __instance)
        {
            EarthmoverCharacter.TryIntro(__instance.fullName);
        }
    }

    [HarmonyPatch(typeof(Crossfade), "StartFade")]
    class EarthmoverBloodVentPatch
    {
        static void Postfix(Crossfade __instance)
        {
            if (!UltraVoicePlugin.EarthmoverVoiceEnabled.Value)
                return;

            if (!EarthmoverCharacter.InEarthmoverLevel())
                return;

            if (__instance == null || __instance.to == null || __instance.to.clip == null)
                return;

            if (__instance.to.clip.name != "Centaur A-2")
                return;

            if (!VoiceManager.CheckEventCooldown("Earthmover_BloodVent", 1f))
                return;

            EarthmoverCharacter.PlayAnnouncement(0.5f, "Earthmover_BloodVent", EarthmoverCharacter.UseEarthmoverClip(EarthmoverCharacter.BloodVentClip, EarthmoverCharacter.BloodVentClipTondar, EarthmoverCharacter.BloodVentClipMegaslash),
                "CONTAMINATED BLOOD DETECTED IN FUEL TANK B.",
                (3.1f, 3.2f, 4.2f, "OPENING VENTS...")
            );
        }
    }

    [HarmonyPatch(typeof(CombinedBossBar), "OnEnable")]
    class EarthmoverDefenseSystemPatch
    {
        static void Postfix(CombinedBossBar __instance)
        {
            if (!UltraVoicePlugin.EarthmoverVoiceEnabled.Value)
                return;

            if (!EarthmoverCharacter.InEarthmoverLevel())
                return;

            if (__instance.name != "SecuritySystem")
                return;

            if (!VoiceManager.CheckCooldown(__instance, 0.1f))
                return;

            EarthmoverCharacter.PlayAnnouncement(0.1f, "Earthmover_DefenseSystem", EarthmoverCharacter.UseEarthmoverClip(EarthmoverCharacter.DefenseSystemClip, EarthmoverCharacter.DefenseSystemClipTondar, EarthmoverCharacter.DefenseSystemClipMegaslash),
                "UNAUTHORIZED PERSONNEL DETECTED AT INTERIOR ENTRANCE.",
                (3.1f, 3.55f, 4.9f, "ENGAGING DEFENSE SYSTEM...")
            );
        }
    }

    [HarmonyPatch(typeof(GameObject), "SetActive")]
    class EarthmoverBloodFloodPatch
    {
        static void Postfix(GameObject __instance)
        {
            if (!UltraVoicePlugin.EarthmoverVoiceEnabled.Value)
                return;

            if (__instance == null)
                return;

            if (!EarthmoverCharacter.InEarthmoverLevel())
                return;

            if (__instance.name != "SecondBeats")
                return;

            if (Time.time - EarthmoverCharacter.lastBloodFloodTime < 0.1f)
                return;

            Transform alarmTransform = __instance.transform.Find("Alarm");
            if (alarmTransform == null)
                return;

            AudioSource alarmSource = alarmTransform.GetComponent<AudioSource>();
            if (alarmSource == null || !alarmSource.isPlaying)
                return;

            EarthmoverCharacter.lastBloodFloodTime = Time.time;

            EarthmoverCharacter.PlayAnnouncement(0f, "Earthmover_BloodFlood", EarthmoverCharacter.UseEarthmoverClip(EarthmoverCharacter.BloodFloodClip, EarthmoverCharacter.BloodFloodClipTondar, EarthmoverCharacter.BloodFloodClipMegaslash),
                "UNAUTHORIZED PERSONNEL HAS BREACHED INTERIOR ENTRANCE.",
                (2.85f, 3.45f, 4.4f, "FLUSHING INTERIOR...")
            );
        }
    }

    [HarmonyPatch(typeof(GameObject), "SetActive")]
    class EarthmoverBrainBattlePatch
    {
        static void Postfix(GameObject __instance)
        {
            if (!UltraVoicePlugin.EarthmoverVoiceEnabled.Value)
                return;

            if (__instance == null || !__instance.activeSelf)
                return;

            if (!EarthmoverCharacter.InEarthmoverLevel())
                return;

            if (__instance.name != "Beams")
                return;

            Transform grandparent = __instance.transform.parent != null ? __instance.transform.parent.parent : null;

            if (grandparent == null || grandparent.name != "LaserRing")
                return;

            if (Time.time - EarthmoverCharacter.lastBrainBattleTime < 0.1f)
                return;

            EarthmoverCharacter.lastBrainBattleTime = Time.time;

            EarthmoverCharacter.PlayAnnouncement(0f, "Earthmover_BrainBattle", EarthmoverCharacter.UseEarthmoverClip(EarthmoverCharacter.BrainBattleClip, EarthmoverCharacter.BrainBattleClipTondar, EarthmoverCharacter.BrainBattleClipMegaslash),
                "UNAUTHORIZED PERSONNEL HAS BREACHED CENTRAL CONTROL TOWER.",
                (3f, 3.7f, 5.2f, "ENGAGING DEFENSE PROTOCOLS...")
            );
        }
    }

    [HarmonyPatch(typeof(GameObject), "SetActive")]
    class EarthmoverSelfDestructPatch
    {
        static void Postfix(GameObject __instance)
        {
            if (__instance == null || !__instance.activeSelf)
                return;

            if (!UltraVoicePlugin.EarthmoverVoiceEnabled.Value)
                return;

            if (!EarthmoverCharacter.InEarthmoverLevel())
                return;

            if (__instance.name != "Countdown")
                return;

            if (Time.time - EarthmoverCharacter.lastSelfDestructTime < 0.1f)
                return;

            EarthmoverCharacter.lastSelfDestructTime = Time.time;

            EarthmoverCharacter.PlayAnnouncement(0.25f, "Earthmover_SelfDestruct", EarthmoverCharacter.UseEarthmoverClip(EarthmoverCharacter.BrainKilledClip, EarthmoverCharacter.BrainKilledClipTondar, EarthmoverCharacter.BrainKilledClipMegaslash),
                "CRITICAL DAMAGE DETECTED IN CENTRAL CONTROL TOWER!",
                (3.3f, 3.65f, 4.4f, "SELF-DESTRUCT SEQUENCE INITIATED,"),
                (2.05f, 2.6f, 3.55f, "ALL PERSONNEL EVACUATE IMMEDIATELY!")
            );
        }
    }
}
