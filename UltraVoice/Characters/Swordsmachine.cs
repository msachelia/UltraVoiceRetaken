using HarmonyLib;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UltraVoice.Utilities;

namespace UltraVoice.Characters
{
    public class SwordsmachineCharacter
    {

        public static AudioClip IntroClip;
        public static AudioClip IntroClipSecond;
        public static AudioClip BigPainClip;
        public static AudioClip LungeClip;
        public static AudioClip ComboClip;
        public static AudioClip DeathClip;
        public static AudioClip KnockdownClipSpecial;
        public static AudioClip KnockdownClipSpecialBrutal;

        public static AudioClip AgonySpawnClip;
        public static AudioClip TundraSpawnClip;
        public static AudioClip AgonyKnockdownClip;
        public static AudioClip TundraKnockdownClip;

        public static AudioClip[] SpawnClips;
        public static AudioClip[] EnrageClips;
        public static AudioClip[] KnockdownClips;
        public static AudioClip[] RangedClips;

        public static AudioClip IntroClipNoto;
        public static AudioClip IntroClipSecondNoto;
        public static AudioClip BigPainClipNoto;
        public static AudioClip LungeClipNoto;
        public static AudioClip ComboClipNoto;
        public static AudioClip DeathClipNoto;
        public static AudioClip KnockdownClipSpecialNoto;
        public static AudioClip KnockdownClipSpecialBrutalNoto;

        public static AudioClip AgonySpawnClipNoto;
        public static AudioClip TundraSpawnClipNoto;
        public static AudioClip AgonyKnockdownClipNoto;
        public static AudioClip TundraKnockdownClipNoto;

        public static AudioClip[] SpawnClipsNoto;
        public static AudioClip[] EnrageClipsNoto;
        public static AudioClip[] KnockdownClipsNoto;
        public static AudioClip[] RangedClipsNoto;

        public static readonly string[] EnrageSubs =
        {
            "GRRR...",
            "GRRR...",
            "MOTHERFUCKER!",
            "OHOHO, YOU ARE SO DEAD!"
        };

        public static readonly string[] EnrageSubs2 =
        {
            "I'LL KICK YOUR ASS!",
            "I'LL KILL YOU!",
            null,
            null
        };

        public static readonly string[] SpawnSubs =
        {
            "LET'S SEE SOME BLOOD!",
            "COME ON, COME ON!",
            "WHO'S READY TO FIGHT!",
            "I SMELL BLOOD!",
            "I'LL CUT YOU ALL DOWN!",
            "FRESH BLOOD!",
            "MORE MEAT FOR THE SLAUGHTER!",
        };

        public static readonly string[] KnockdownSubs =
        {
            "IS THAT ALL YOU GOT?",
            "I AIN'T DONE WITH YOU YET!",
            "I'M JUST GETTING STARTED."
        };

        public static readonly string[] RangedSubs =
        {
            "CATCH THIS!",
            "EAT THIS!",
        };

        public static Color SwordsmachineColor => VoiceManager.GetEnemyTypeColor(EnemyType.Swordsmachine);
        public static Color AgonyColor = new Color(0.79f, 0.17f, 0.17f);
        public static Color TundraColor = new Color(0.2f, 0.73f, 0.87f);

        public const string PreludeSecondSceneName = "7927c42db92e4164cae682a55e6b7725";
        public const string PreludeThirdSceneName = "5bcb2e0461e7fce408badfcb6778c271";

        public static bool FirstFightDone = false;
        public static bool FirstFightLinePlayed = false;

        public static bool IsAgony(SwordsMachine sm)
        {
            return sm.gameObject.name.Contains("Agony");
        }

        public static bool IsTundra(SwordsMachine sm)
        {
            return sm.gameObject.name.Contains("Tundra");
        }

        public static bool IsAgonyOrTundra(SwordsMachine sm)
        {
            return IsAgony(sm) || IsTundra(sm);
        }

        public static Color? GetColorOverride(SwordsMachine sm)
        {
            if (IsAgony(sm))
                return AgonyColor;

            if (IsTundra(sm))
                return TundraColor;

            return SwordsmachineColor;
        }

        public static AudioClip UseSwordsmachineClip(AudioClip mofClip, AudioClip notoClip)
        {
            return UltraVoicePlugin.SwordsmachineVoiceActorField != null && UltraVoicePlugin.SwordsmachineVoiceActorField.value == UltraVoicePlugin.SwordsmachineVoiceActor.Noto
                ? notoClip
                : mofClip;
        }

        public static AudioClip[] UseSwordsmachineClips(AudioClip[] mofClips, AudioClip[] notoClips)
        {
            return UltraVoicePlugin.SwordsmachineVoiceActorField != null && UltraVoicePlugin.SwordsmachineVoiceActorField.value == UltraVoicePlugin.SwordsmachineVoiceActor.Noto
                ? notoClips
                : mofClips;
        }

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            IntroClip = UltraVoicePlugin.LoadClip("Swordsmachine.sm_SpawnSpecial.wav");
            IntroClipSecond = UltraVoicePlugin.LoadClip("Swordsmachine.sm_SpawnSpecial2.wav");
            BigPainClip = UltraVoicePlugin.LoadClip("Swordsmachine.sm_BigPain.wav");
            LungeClip = UltraVoicePlugin.LoadClip("Swordsmachine.sm_Lunge.wav");
            ComboClip = UltraVoicePlugin.LoadClip("Swordsmachine.sm_Combo.wav");
            DeathClip = UltraVoicePlugin.LoadClip("Swordsmachine.sm_Death.wav");
            KnockdownClipSpecial = UltraVoicePlugin.LoadClip("Swordsmachine.sm_KnockdownSpecial.wav");
            KnockdownClipSpecialBrutal = UltraVoicePlugin.LoadClip("Swordsmachine.sm_PieceOfShit.wav");

            AgonySpawnClip = UltraVoicePlugin.LoadClip("Swordsmachine.sm_SpawnSpecialAgony.wav");
            TundraSpawnClip = UltraVoicePlugin.LoadClip("Swordsmachine.sm_SpawnSpecialTundra.wav");
            AgonyKnockdownClip = UltraVoicePlugin.LoadClip("Swordsmachine.sm_DownedAgony.wav");
            TundraKnockdownClip = UltraVoicePlugin.LoadClip("Swordsmachine.sm_DownedTundra.wav");

            SpawnClips = UltraVoicePlugin.LoadClips("Swordsmachine.sm_Spawn{0}.wav", 7);
            EnrageClips = UltraVoicePlugin.LoadClips("Swordsmachine.sm_Enrage{0}.wav", 4);
            KnockdownClips = UltraVoicePlugin.LoadClips("Swordsmachine.sm_Knockdown{0}.wav", 3);
            RangedClips = UltraVoicePlugin.LoadClips("Swordsmachine.sm_Ranged{0}.wav", 2);

            IntroClipNoto = UltraVoicePlugin.LoadClip("Swordsmachine.sm_SpawnSpecialNoto.wav");
            IntroClipSecondNoto = UltraVoicePlugin.LoadClip("Swordsmachine.sm_SpawnSpecial2Noto.wav");
            BigPainClipNoto = UltraVoicePlugin.LoadClip("Swordsmachine.sm_BigPainNoto.wav");
            LungeClipNoto = UltraVoicePlugin.LoadClip("Swordsmachine.sm_LungeNoto.wav");
            ComboClipNoto = UltraVoicePlugin.LoadClip("Swordsmachine.sm_ComboNoto.wav");
            DeathClipNoto = UltraVoicePlugin.LoadClip("Swordsmachine.sm_DeathNoto.wav");
            KnockdownClipSpecialNoto = UltraVoicePlugin.LoadClip("Swordsmachine.sm_KnockdownSpecialNoto.wav");
            KnockdownClipSpecialBrutalNoto = UltraVoicePlugin.LoadClip("Swordsmachine.sm_PieceOfShitNoto.wav");

            AgonySpawnClipNoto = UltraVoicePlugin.LoadClip("Swordsmachine.sm_SpawnSpecialAgonyNoto.wav");
            TundraSpawnClipNoto = UltraVoicePlugin.LoadClip("Swordsmachine.sm_SpawnSpecialTundraNoto.wav");
            AgonyKnockdownClipNoto = UltraVoicePlugin.LoadClip("Swordsmachine.sm_DownedAgonyNoto.wav");
            TundraKnockdownClipNoto = UltraVoicePlugin.LoadClip("Swordsmachine.sm_DownedTundraNoto.wav");

            SpawnClipsNoto = UltraVoicePlugin.LoadClips("Swordsmachine.sm_Spawn{0}Noto.wav", 7);
            EnrageClipsNoto = UltraVoicePlugin.LoadClips("Swordsmachine.sm_Enrage{0}Noto.wav", 4);
            KnockdownClipsNoto = UltraVoicePlugin.LoadClips("Swordsmachine.sm_Knockdown{0}Noto.wav", 3);
            RangedClipsNoto = UltraVoicePlugin.LoadClips("Swordsmachine.sm_Ranged{0}Noto.wav", 2);

            logger.LogInfo("Swordsmachine voice lines loaded successfully!");
        }

        public static void PlayRangedVoice(SwordsMachine sm)
        {
            if (!UltraVoicePlugin.SwordsmachineVoiceEnabled.value)
                return;

            if (!VoiceManager.CheckCooldown(sm, 3f))
                return;

            if (VoiceManager.IsEnemyVoicePlaying(sm))
                return;

            if (VoiceManager.TooSoonAfterSpawn(sm, 2f))
                return;

            VoiceManager.PlayRandomVoice(sm, "Swordsmachine",
                UseSwordsmachineClips(RangedClips, RangedClipsNoto),
                RangedSubs,
                colorOverride: GetColorOverride(sm),
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(SwordsMachine), "Start")]
    class SwordsmachineSpawnPatch
    {
        static void Postfix(SwordsMachine __instance)
        {
            VoiceManager.enemySpawnTimes[__instance] = Time.time;

            if (!UltraVoicePlugin.SwordsmachineVoiceEnabled.value)
                return;

            if (SwordsmachineCharacter.IsAgonyOrTundra(__instance))
                return;

            if (__instance.bossVersion)
            {
                if (SceneManager.GetActiveScene().name != SwordsmachineCharacter.PreludeThirdSceneName)
                    return;

                UltraVoicePlugin.Instance.StartCoroutine(PlayBossIntro(__instance));
                return;
            }

            if (SceneManager.GetActiveScene().name == SwordsmachineCharacter.PreludeSecondSceneName)
                return;

            VoiceManager.PlayRandomVoice(__instance, "Swordsmachine",
                SwordsmachineCharacter.UseSwordsmachineClips(SwordsmachineCharacter.SpawnClips, SwordsmachineCharacter.SpawnClipsNoto),
                SwordsmachineCharacter.SpawnSubs,
                true
            );
        }

        static IEnumerator PlayBossIntro(SwordsMachine sm)
        {
            yield return null;

            AudioClip clip;
            string subtitle;

            if (!SwordsmachineCharacter.FirstFightDone)
            {
                clip = SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.IntroClip, SwordsmachineCharacter.IntroClipNoto);
                subtitle = "YOU WANT A FIGHT? LET'S FIGHT!";
            }
            else
            {
                clip = SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.IntroClipSecond, SwordsmachineCharacter.IntroClipSecondNoto);
                subtitle = "DID YOU THINK I FORGOT ABOUT YOU?";
            }

            var src = VoiceManager.CreateVoiceSource(sm, "SwordsmachineIntro", clip, subtitle, true);

            if (src != null)
                VoiceManager.spawnVoiceEndTimes[sm] = Time.time + clip.length;
        }
    }

    [HarmonyPatch(typeof(SwordsMachine), "Start")]
    class SwordsmachineSpecialSpawnPatch
    {
        static void Postfix(SwordsMachine __instance)
        {
            if (!UltraVoicePlugin.SwordsmachineVoiceEnabled.value)
                return;

            if (!SwordsmachineCharacter.IsAgonyOrTundra(__instance))
                return;

            VoiceManager.enemySpawnTimes[__instance] = Time.time;

            if (SwordsmachineCharacter.IsAgony(__instance))
                VoiceManager.CreateVoiceSource(
                    __instance,
                    "AgonySpawn",
                    SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.AgonySpawnClip, SwordsmachineCharacter.AgonySpawnClipNoto),
                    "JUMP 'EM!",
                    true,
                    SwordsmachineCharacter.AgonyColor
                );
            else if (SwordsmachineCharacter.IsTundra(__instance))
                VoiceManager.CreateVoiceSource(
                    __instance,
                    "TundraSpawn",
                    SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.TundraSpawnClip, SwordsmachineCharacter.TundraSpawnClipNoto),
                    "THERE THEY ARE!",
                    true,
                    SwordsmachineCharacter.TundraColor
                );
        }
    }

    [HarmonyPatch(typeof(SwordsMachine), "Enrage")]
    class SwordsmachineEnragePatch
    {
        static void Prefix(SwordsMachine __instance)
        {
            if (!UltraVoicePlugin.SwordsmachineVoiceEnabled.value)
                return;

            if (SwordsmachineCharacter.IsAgonyOrTundra(__instance))
                return;

            if (__instance.enraged)
                return;

            UltraVoicePlugin.Instance.StartCoroutine(PlayEnrage(__instance));
        }

        static IEnumerator PlayEnrage(SwordsMachine sm)
        {
            yield return new WaitForSeconds(0.75f);

            if (sm == null || !sm.enraged)
                yield break;

            int i = Random.Range(0, SwordsmachineCharacter.EnrageClips.Length);

            var src = VoiceManager.CreateVoiceSource(
                sm,
                "SwordsmachineEnrage",
                SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.EnrageClips[i], SwordsmachineCharacter.EnrageClipsNoto[i]),
                SwordsmachineCharacter.EnrageSubs[i],
                true,
                randomPitch: true
            );

            if (src == null || !src.isPlaying || !sm.active)
                yield break;

            if (!string.IsNullOrEmpty(SwordsmachineCharacter.EnrageSubs2[i]))
            {
                yield return new WaitForSeconds(0.75f);

                VoiceManager.ShowSubtitle(
                    SwordsmachineCharacter.EnrageSubs2[i],
                    src,
                    color: SwordsmachineCharacter.SwordsmachineColor
                );
            }
        }
    }

    [HarmonyPatch(typeof(SwordsMachine), "EndFirstPhase")]
    class SwordsmachinePhaseChangePatch
    {
        static void Postfix(SwordsMachine __instance)
        {
            if (!UltraVoicePlugin.SwordsmachineVoiceEnabled.value)
                return;

            if (SwordsmachineCharacter.IsAgonyOrTundra(__instance))
                return;

            VoiceManager.CreateVoiceSource(
                __instance,
                "SwordsmachineBigPain",
                SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.BigPainClip, SwordsmachineCharacter.BigPainClipNoto),
                null,
                true
            );

            bool firstFightSpecial = __instance.bossVersion
                && SceneManager.GetActiveScene().name == SwordsmachineCharacter.PreludeThirdSceneName
                && !SwordsmachineCharacter.FirstFightLinePlayed;

            if (firstFightSpecial && __instance.difficulty <= 2)
                UltraVoicePlugin.Instance.StartCoroutine(PlayKnockdownSpecial(__instance, 0.75f));
            else if (firstFightSpecial && __instance.difficulty == 3)
                UltraVoicePlugin.Instance.StartCoroutine(PlayKnockdownSpecial(__instance, 0.5f));
            else if (firstFightSpecial && __instance.difficulty == 4)
                UltraVoicePlugin.Instance.StartCoroutine(PieceOfShit(__instance));
            else
                UltraVoicePlugin.Instance.StartCoroutine(PlayKnockdown(__instance));
        }

        static IEnumerator PlayKnockdown(SwordsMachine sm)
        {
            yield return new WaitForSeconds(0.75f);

            VoiceManager.PlayRandomVoice(
                sm,
                "SwordsmachineKnockdownSpecial",
                SwordsmachineCharacter.UseSwordsmachineClips(SwordsmachineCharacter.KnockdownClips, SwordsmachineCharacter.KnockdownClipsNoto),
                SwordsmachineCharacter.KnockdownSubs,
                true,
                true,
                colorOverride: SwordsmachineCharacter.GetColorOverride(sm)
            );
        }

        static IEnumerator PlayKnockdownSpecial(SwordsMachine sm, float delay)
        {
            yield return new WaitForSeconds(delay);

            SwordsmachineCharacter.FirstFightLinePlayed = true;

            var src = VoiceManager.CreateVoiceSource(
                sm,
                "SwordsmachineKnockdownSpecial",
                SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.KnockdownClipSpecial, SwordsmachineCharacter.KnockdownClipSpecialNoto),
                null,
                true,
                SwordsmachineCharacter.GetColorOverride(sm)
            );

            yield return new WaitForSeconds(1f);

            if (src == null)
                yield break;

            VoiceManager.ShowSubtitle(
                "YOU THINK YOU'RE SO MUCH BETTER THAN ME, HUH?!",
                src,
                SwordsmachineCharacter.GetColorOverride(sm)
            );
        }

        static IEnumerator PieceOfShit(SwordsMachine sm)
        {
            yield return new WaitForSeconds(0.85f);

            SwordsmachineCharacter.FirstFightLinePlayed = true;

            VoiceManager.CreateVoiceSource(
                sm,
                "SwordsmachineKnockdown",
                SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.KnockdownClipSpecialBrutal, SwordsmachineCharacter.KnockdownClipSpecialBrutalNoto),
                "PIECE OF SHIT!",
                true,
                SwordsmachineCharacter.GetColorOverride(sm)
            );
        }
    }

    [HarmonyPatch(typeof(SwordsMachine), "Knockdown")]
    class SwordsmachineKnockdownPatch
    {
        static void Postfix(SwordsMachine __instance)
        {
            if (!UltraVoicePlugin.SwordsmachineVoiceEnabled.value)
                return;

            if (!VoiceManager.CheckCooldown(__instance, 0.1f))
                return;

            VoiceManager.CreateVoiceSource(
                __instance,
                "SwordsmachineBigPain",
                SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.BigPainClip, SwordsmachineCharacter.BigPainClipNoto),
                null,
                true,
                randomPitch: true
            );

            UltraVoicePlugin.Instance.StartCoroutine(PlaySpecialKnockdown(__instance));
        }

        static IEnumerator PlaySpecialKnockdown(SwordsMachine sm)
        {
            yield return new WaitForSeconds(0.75f);

            if (sm == null)
                yield break;

            if (SwordsmachineCharacter.IsAgony(sm))
                VoiceManager.CreateVoiceSource(
                    sm,
                    "AgonyKnockdown",
                    SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.AgonyKnockdownClip, SwordsmachineCharacter.AgonyKnockdownClipNoto),
                    "DAMMIT!",
                    true,
                    SwordsmachineCharacter.AgonyColor
                );
            else if (SwordsmachineCharacter.IsTundra(sm))
                VoiceManager.CreateVoiceSource(
                    sm,
                    "TundraKnockdown",
                    SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.TundraKnockdownClip, SwordsmachineCharacter.TundraKnockdownClipNoto),
                    "COVER ME!",
                    true,
                    SwordsmachineCharacter.TundraColor
                );
        }
    }

    [HarmonyPatch(typeof(SwordsMachine), "TeleportAway")]
    class SwordsmachineTeleportPatch
    {
        static void Postfix(SwordsMachine __instance)
        {
            SwordsmachineCharacter.FirstFightDone = true;
            VoiceManager.InterruptVoices(__instance);
        }
    }

    [HarmonyPatch(typeof(SwordsMachine), "ShootGun")]
    class SwordsmachineShotgunPatch
    {
        static void Postfix(SwordsMachine __instance)
        {
            SwordsmachineCharacter.PlayRangedVoice(__instance);
        }
    }

    [HarmonyPatch(typeof(SwordsMachine), "SwordThrow")]
    class SwordsmachineSwordThrowPatch
    {
        static void Postfix(SwordsMachine __instance)
        {
            SwordsmachineCharacter.PlayRangedVoice(__instance);
        }
    }

    [HarmonyPatch(typeof(SwordsMachine), "SwordSpiral")]
    class SwordsmachineSwordSpiralPatch
    {
        static void Postfix(SwordsMachine __instance)
        {
            SwordsmachineCharacter.PlayRangedVoice(__instance);
        }
    }

    [HarmonyPatch(typeof(SwordsMachine), "Combo")]
    class SwordsmachineComboPatch
    {
        static void Postfix(SwordsMachine __instance)
        {
            if (!UltraVoicePlugin.SwordsmachineVoiceEnabled.value)
                return;

            if (!VoiceManager.CheckCooldown(__instance, 3f))
                return;

            if (VoiceManager.IsEnemyVoicePlaying(__instance))
                return;

            VoiceManager.CreateVoiceSource(
                __instance,
                "SwordsmachineCombo",
                SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.ComboClip, SwordsmachineCharacter.ComboClipNoto),
                "DIE, DIE, DIE!",
                subtitleColor: SwordsmachineCharacter.GetColorOverride(__instance),
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(SwordsMachine), "RunningSwing")]
    class SwordsmachineLungePatch
    {
        static void Postfix(SwordsMachine __instance)
        {
            if (!UltraVoicePlugin.SwordsmachineVoiceEnabled.value)
                return;

            if (!VoiceManager.CheckCooldown(__instance, 3f))
                return;

            if (VoiceManager.IsEnemyVoicePlaying(__instance))
                return;

            VoiceManager.CreateVoiceSource(
                __instance,
                "SwordsmachineLunge",
                SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.LungeClip, SwordsmachineCharacter.LungeClipNoto),
                "DIE!",
                subtitleColor: SwordsmachineCharacter.GetColorOverride(__instance),
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(SwordsMachine), "OnGoLimp")]
    class SwordsmachineDeathPatch
    {
        static void Postfix(SwordsMachine __instance)
        {
            if (!UltraVoicePlugin.SwordsmachineVoiceEnabled.value)
                return;

            VoiceManager.CreateVoiceSource(
                __instance,
                "SwordsmachineDeath",
                SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.DeathClip, SwordsmachineCharacter.DeathClipNoto),
                null,
                true,
                subtitleColor: SwordsmachineCharacter.GetColorOverride(__instance),
                randomPitch: true
            );
        }
    }
}
