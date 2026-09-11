using HarmonyLib;
using System.Collections;
using UltraVoice.Utilities;
using UnityEngine;

namespace UltraVoice.Characters
{
    public class CerberusCharacter
    {

        public static AudioClip PreludeClip;
        public static AudioClip[] AwakenClips;
        public static AudioClip[] EnrageClips;
        public static AudioClip[] ThrowClips;
        public static AudioClip[] StompClips;
        public static AudioClip[] TackleClips;
        public static AudioClip[] DeathClips;

        public static AudioClip PreludeClipSoil;
        public static AudioClip[] AwakenClipsSoil;
        public static AudioClip[] EnrageClipsSoil;
        public static AudioClip[] ThrowClipsSoil;
        public static AudioClip[] StompClipsSoil;
        public static AudioClip[] TackleClipsSoil;
        public static AudioClip[] DeathClipsSoil;

        public static AudioClip PreludeClipRotund;
        public static AudioClip[] AwakenClipsRotund;
        public static AudioClip[] EnrageClipsRotund;
        public static AudioClip[] ThrowClipsRotund;
        public static AudioClip[] StompClipsRotund;
        public static AudioClip[] TackleClipsRotund;
        public static AudioClip[] DeathClipsRotund;

        public static readonly string[] AwakenSubs =
        {
            "Who dares awaken me?",
            "You will go no further",
            "Your story ends here",
            "Your defiance shall be punished",
            "Be gone, or be destroyed",
            "You stand condemned"
        };

        public static readonly string[] EnrageSubs =
        {
            "You will regret crossing me!",
            "Your arrogance will cost you!",
            "I will avenge my brother!",
            "This shall not go unpunished!"
        };

        public static readonly string[] ThrowSubs =
        {
            "Perish",
            "Die"
        };

        public static readonly string[] StompSubs =
        {
            "Tremble",
            "Fall"
        };

        public static readonly string[] TackleSubs =
        {
            "Face me",
            "You cannot run"
        };

        public static Color CerberusColor => VoiceManager.GetEnemyTypeColor(EnemyType.Cerberus);

        public static AudioClip UseCerberusClip(AudioClip melClip, AudioClip soilClip, AudioClip rotundClip)
        {
            if (UltraVoicePlugin.CerberusVoiceActorField == null)
                return melClip;

            switch (UltraVoicePlugin.CerberusVoiceActorField.Value)
            {
                case UltraVoicePlugin.CerberusVoiceActor.Soil:
                    return soilClip;
                case UltraVoicePlugin.CerberusVoiceActor.Rotund:
                    return rotundClip;
                default:
                    return melClip;
            }
        }

        public static AudioClip[] UseCerberusClips(AudioClip[] melClips, AudioClip[] soilClips, AudioClip[] rotundClips)
        {
            if (UltraVoicePlugin.CerberusVoiceActorField == null)
                return melClips;

            switch (UltraVoicePlugin.CerberusVoiceActorField.Value)
            {
                case UltraVoicePlugin.CerberusVoiceActor.Soil:
                    return soilClips;
                case UltraVoicePlugin.CerberusVoiceActor.Rotund:
                    return rotundClips;
                default:
                    return melClips;
            }
        }

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            PreludeClip = UltraVoicePlugin.LoadClip("assets/resources/cerberus/mel/cerb_wakeupspecial.wav");
            AwakenClips = UltraVoicePlugin.LoadClips("assets/resources/cerberus/mel/cerb_wakeup{0}.wav", 6);
            EnrageClips = UltraVoicePlugin.LoadClips("assets/resources/cerberus/mel/cerb_enrage{0}.wav", 4);
            ThrowClips = UltraVoicePlugin.LoadClips("assets/resources/cerberus/mel/cerb_orb{0}.wav", 2);
            StompClips = UltraVoicePlugin.LoadClips("assets/resources/cerberus/mel/cerb_stomp{0}.wav", 2);
            TackleClips = UltraVoicePlugin.LoadClips("assets/resources/cerberus/mel/cerb_tackle{0}.wav", 2);
            DeathClips = UltraVoicePlugin.LoadClips("assets/resources/cerberus/mel/cerb_death{0}.wav", 3);

            PreludeClipSoil = UltraVoicePlugin.LoadClip("assets/resources/cerberus/soil/cerb_wakeupspecial.wav");
            AwakenClipsSoil = UltraVoicePlugin.LoadClips("assets/resources/cerberus/soil/cerb_wakeup{0}.wav", 6);
            EnrageClipsSoil = UltraVoicePlugin.LoadClips("assets/resources/cerberus/soil/cerb_enrage{0}.wav", 4);
            ThrowClipsSoil = UltraVoicePlugin.LoadClips("assets/resources/cerberus/soil/cerb_orb{0}.wav", 2);
            StompClipsSoil = UltraVoicePlugin.LoadClips("assets/resources/cerberus/soil/cerb_stomp{0}.wav", 2);
            TackleClipsSoil = UltraVoicePlugin.LoadClips("assets/resources/cerberus/soil/cerb_tackle{0}.wav", 2);
            DeathClipsSoil = UltraVoicePlugin.LoadClips("assets/resources/cerberus/soil/cerb_death{0}.wav", 3);

            PreludeClipRotund = UltraVoicePlugin.LoadClip("assets/resources/cerberus/rotund/cerb_wakeupspecial.wav");
            AwakenClipsRotund = UltraVoicePlugin.LoadClips("assets/resources/cerberus/rotund/cerb_wakeup{0}.wav", 6);
            EnrageClipsRotund = UltraVoicePlugin.LoadClips("assets/resources/cerberus/rotund/cerb_enrage{0}.wav", 4);
            ThrowClipsRotund = UltraVoicePlugin.LoadClips("assets/resources/cerberus/rotund/cerb_orb{0}.wav", 2);
            StompClipsRotund = UltraVoicePlugin.LoadClips("assets/resources/cerberus/rotund/cerb_stomp{0}.wav", 2);
            TackleClipsRotund = UltraVoicePlugin.LoadClips("assets/resources/cerberus/rotund/cerb_tackle{0}.wav", 2);
            DeathClipsRotund = UltraVoicePlugin.LoadClips("assets/resources/cerberus/rotund/cerb_death{0}.wav", 3);

            logger.LogInfo("Cerberus voice lines loaded successfully!");
        }

    }

    [HarmonyPatch(typeof(StatueFake), "SlowStart")]
    class CerberusIntroPatch
    {
        static void Postfix(StatueFake __instance)
        {
            if (__instance.quickSpawn)
                return;

            if (!UltraVoicePlugin.CerberusVoiceEnabled.Value)
                return;

            UltraVoicePlugin.Instance.StartCoroutine(PlayIntro(__instance));
        }

        static IEnumerator PlayIntro(StatueFake cerb)
        {
            yield return new WaitForSeconds(3.5f);

            if (cerb == null)
                yield break;

            AudioSource src = VoiceManager.CreateVoiceSource(
                cerb,
                "CerberusIntro",
                CerberusCharacter.UseCerberusClip(CerberusCharacter.PreludeClip, CerberusCharacter.PreludeClipSoil, CerberusCharacter.PreludeClipRotund),
                "You tread forbidden ground, machine",
                shouldInterrupt: true,
                subtitleColor: CerberusCharacter.CerberusColor,
                parentToEnemy: false,
                maxDistance: 500f
            );

            if (src == null)
                yield break;

            yield return new WaitForSeconds(3.75f);

            if (src == null)
                yield break;

            VoiceManager.ShowSubtitle("BEGONE", src, CerberusCharacter.CerberusColor);
        }
    }

    [HarmonyPatch(typeof(StatueFake), "Activate")]
    class CerberusWakePatch
    {
        static void Postfix(StatueFake __instance)
        {
            if (!__instance.quickSpawn)
                return;

            if (!UltraVoicePlugin.CerberusVoiceEnabled.Value)
                return;

            if (!VoiceManager.CheckCooldown(__instance, 3f))
                return;

            VoiceManager.enemySpawnTimes[__instance] = Time.time;

            UltraVoicePlugin.Instance.StartCoroutine(PlayAwaken(__instance));
        }

        static IEnumerator PlayAwaken(StatueFake cerb)
        {
            yield return new WaitForSeconds(Random.Range(0f, 1f));

            if (cerb == null)
                yield break;

            AudioClip[] awakenClips = CerberusCharacter.UseCerberusClips(CerberusCharacter.AwakenClips, CerberusCharacter.AwakenClipsSoil, CerberusCharacter.AwakenClipsRotund);

            int i = Random.Range(0, awakenClips.Length);

            VoiceManager.CreateVoiceSource(
                cerb,
                "CerberusAwaken",
                awakenClips[i],
                CerberusCharacter.AwakenSubs[i],
                shouldInterrupt: true,
                subtitleColor: CerberusCharacter.CerberusColor,
                parentToEnemy: false,
                maxDistance: 500f
            );
        }
    }

    [HarmonyPatch(typeof(StatueBoss), "Start")]
    class CerberusSpawnTracker
    {
        static void Postfix(StatueBoss __instance)
        {
            VoiceManager.enemySpawnTimes[__instance] = Time.time;
        }
    }

    [HarmonyPatch(typeof(StatueBoss), "Enrage")]
    class CerberusEnragePatch
    {
        static void Postfix(StatueBoss __instance)
        {
            if (!UltraVoicePlugin.CerberusVoiceEnabled.Value)
                return;

            VoiceManager.PlayRandomVoiceDelayed(Random.Range(0f, 0.4f), __instance, "Cerberus",
                CerberusCharacter.UseCerberusClips(CerberusCharacter.EnrageClips, CerberusCharacter.EnrageClipsSoil, CerberusCharacter.EnrageClipsRotund),
                CerberusCharacter.EnrageSubs,
                interrupt: true,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(StatueBoss), "Throw")]
    class CerberusThrowPatch
    {
        static void Postfix(StatueBoss __instance)
        {
            if (!UltraVoicePlugin.CerberusVoiceEnabled.Value)
                return;

            if (VoiceManager.IsSpawnVoicePlaying(__instance))
                return;

            if (!VoiceManager.CheckCooldown(__instance, 3f))
                return;

            VoiceManager.PlayRandomVoiceDelayed(0.2f, __instance, "Cerberus",
                CerberusCharacter.UseCerberusClips(CerberusCharacter.ThrowClips, CerberusCharacter.ThrowClipsSoil, CerberusCharacter.ThrowClipsRotund),
                CerberusCharacter.ThrowSubs,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(StatueBoss), "Stomp")]
    class CerberusStompPatch
    {
        static void Postfix(StatueBoss __instance)
        {
            if (!UltraVoicePlugin.CerberusVoiceEnabled.Value)
                return;

            if (VoiceManager.IsSpawnVoicePlaying(__instance))
                return;

            if (!VoiceManager.CheckCooldown(__instance, 3f))
                return;

            VoiceManager.PlayRandomVoiceDelayed(0.2f, __instance, "Cerberus",
                CerberusCharacter.UseCerberusClips(CerberusCharacter.StompClips, CerberusCharacter.StompClipsSoil, CerberusCharacter.StompClipsRotund),
                CerberusCharacter.StompSubs,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(StatueBoss), "Tackle")]
    class CerberusTacklePatch
    {
        static void Postfix(StatueBoss __instance)
        {
            if (!UltraVoicePlugin.CerberusVoiceEnabled.Value)
                return;

            if (VoiceManager.IsSpawnVoicePlaying(__instance))
                return;

            if (!VoiceManager.CheckCooldown(__instance, 3f))
                return;

            VoiceManager.PlayRandomVoiceDelayed(0.2f, __instance, "Cerberus",
                CerberusCharacter.UseCerberusClips(CerberusCharacter.TackleClips, CerberusCharacter.TackleClipsSoil, CerberusCharacter.TackleClipsRotund),
                CerberusCharacter.TackleSubs,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(StatueBoss), "OnGoLimp")]
    class CerberusDeathPatch
    {
        static void Postfix(StatueBoss __instance)
        {
            if (!UltraVoicePlugin.CerberusVoiceEnabled.Value)
                return;

            VoiceManager.PlayRandomVoice(__instance, "Cerberus",
                CerberusCharacter.UseCerberusClips(CerberusCharacter.DeathClips, CerberusCharacter.DeathClipsSoil, CerberusCharacter.DeathClipsRotund),
                null,
                interrupt: true,
                randomPitch: true
            );
        }
    }
}
