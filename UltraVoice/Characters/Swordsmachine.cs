using HarmonyLib;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UltraVoice.Utilities;
using System.ComponentModel.Design;

namespace UltraVoice.Characters
{
    public class SwordsmachineCharacter
    {

        public static AudioClip IntroClip;
        public static AudioClip IntroClipSecond;
        public static AudioClip SpawnClipSecret;
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
        public static AudioClip[] SandedClips;

        public static AudioClip IntroClipNoto;
        public static AudioClip IntroClipSoil;
        public static AudioClip IntroClipSecondNoto;
        public static AudioClip IntroClipSecondSoil;
        public static AudioClip SpawnClipSecretNoto;
        public static AudioClip SpawnClipSecretSoil;
        public static AudioClip BigPainClipNoto;
        public static AudioClip BigPainClipSoil;
        public static AudioClip LungeClipNoto;
        public static AudioClip LungeClipSoil;
        public static AudioClip ComboClipNoto;
        public static AudioClip ComboClipSoil;
        public static AudioClip DeathClipNoto;
        public static AudioClip DeathClipSoil;
        public static AudioClip KnockdownClipSpecialNoto;
        public static AudioClip KnockdownClipSpecialSoil;
        public static AudioClip KnockdownClipSpecialBrutalNoto;
        public static AudioClip KnockdownClipSpecialBrutalSoil;
        public static AudioClip[] SandedClipsNoto;
        public static AudioClip[] SandedClipsSoil;

        public static AudioClip AgonySpawnClipNoto;
        public static AudioClip AgonySpawnClipSoil;
        public static AudioClip TundraSpawnClipNoto;
        public static AudioClip TundraSpawnClipSoil;
        public static AudioClip AgonyKnockdownClipNoto;
        public static AudioClip AgonyKnockdownClipSoil;
        public static AudioClip TundraKnockdownClipNoto;
        public static AudioClip TundraKnockdownClipSoil;

        public static AudioClip[] SpawnClipsNoto;
        public static AudioClip[] SpawnClipsSoil;
        public static AudioClip[] EnrageClipsNoto;
        public static AudioClip[] EnrageClipsSoil;
        public static AudioClip[] KnockdownClipsNoto;
        public static AudioClip[] KnockdownClipsSoil;
        public static AudioClip[] RangedClipsNoto;
        public static AudioClip[] RangedClipsSoil;

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

        public static readonly string[] SandedSubs =
        {
            "TRY HEALIN' OFF ME NOW, DIPSHIT!",
            "ENJOY THE DRY SPELL, BITCH!",
            "AWWWW, SOMEONE FEELIN' THIRSTY!?"
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

        public static AudioClip UseSwordsmachineClip(AudioClip melClip, AudioClip notoClip, AudioClip soilClip)
        {
            if (UltraVoicePlugin.SwordsmachineVoiceActorField == null)
                return melClip;

            switch (UltraVoicePlugin.SwordsmachineVoiceActorField.Value)
            {
                case UltraVoicePlugin.SwordsmachineVoiceActor.Noto:
                    return notoClip;
                case UltraVoicePlugin.SwordsmachineVoiceActor.Soil:
                    return soilClip;
                default:
                    return melClip;
            }
        }

        public static AudioClip[] UseSwordsmachineClips(AudioClip[] melClips, AudioClip[] notoClips, AudioClip[] soilClips)
        {
            if (UltraVoicePlugin.SwordsmachineVoiceActorField == null)
                return melClips;

            switch (UltraVoicePlugin.SwordsmachineVoiceActorField.Value)
            {
                case UltraVoicePlugin.SwordsmachineVoiceActor.Noto:
                    return notoClips;
                case UltraVoicePlugin.SwordsmachineVoiceActor.Soil:
                    return soilClips;
                default:
                    return melClips;
            }
        }

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            IntroClip = UltraVoicePlugin.LoadClip("Swordsmachine.Mel.sm_SpawnSpecial.wav");
            IntroClipSecond = UltraVoicePlugin.LoadClip("Swordsmachine.Mel.sm_SpawnSpecial2.wav");
            BigPainClip = UltraVoicePlugin.LoadClip("Swordsmachine.Mel.sm_BigPain.wav");
            LungeClip = UltraVoicePlugin.LoadClip("Swordsmachine.Mel.sm_Lunge.wav");
            ComboClip = UltraVoicePlugin.LoadClip("Swordsmachine.Mel.sm_Combo.wav");
            DeathClip = UltraVoicePlugin.LoadClip("Swordsmachine.Mel.sm_Death.wav");
            KnockdownClipSpecial = UltraVoicePlugin.LoadClip("Swordsmachine.Mel.sm_KnockdownSpecial.wav");
            KnockdownClipSpecialBrutal = UltraVoicePlugin.LoadClip("Swordsmachine.Mel.sm_PieceOfShit.wav");

            SpawnClipSecret = UltraVoicePlugin.LoadClip("Swordsmachine.Mel.sm_SpawnSpecialSecret.wav");
            SandedClips = UltraVoicePlugin.LoadClips("Swordsmachine.Mel.sm_Sanded{0}.wav", 3);

            AgonySpawnClip = UltraVoicePlugin.LoadClip("Swordsmachine.Mel.sm_SpawnSpecialAgony.wav");
            TundraSpawnClip = UltraVoicePlugin.LoadClip("Swordsmachine.Mel.sm_SpawnSpecialTundra.wav");
            AgonyKnockdownClip = UltraVoicePlugin.LoadClip("Swordsmachine.Mel.sm_DownedAgony.wav");
            TundraKnockdownClip = UltraVoicePlugin.LoadClip("Swordsmachine.Mel.sm_DownedTundra.wav");

            SpawnClips = UltraVoicePlugin.LoadClips("Swordsmachine.Mel.sm_Spawn{0}.wav", 7);
            EnrageClips = UltraVoicePlugin.LoadClips("Swordsmachine.Mel.sm_Enrage{0}.wav", 4);
            KnockdownClips = UltraVoicePlugin.LoadClips("Swordsmachine.Mel.sm_Knockdown{0}.wav", 3);
            RangedClips = UltraVoicePlugin.LoadClips("Swordsmachine.Mel.sm_Ranged{0}.wav", 2);

            IntroClipNoto = UltraVoicePlugin.LoadClip("Swordsmachine.Noto.sm_SpawnSpecial.wav");
            IntroClipSoil = UltraVoicePlugin.LoadClip("Swordsmachine.Soil.sm_SpawnSpecial.wav");
            IntroClipSecondNoto = UltraVoicePlugin.LoadClip("Swordsmachine.Noto.sm_SpawnSpecial2.wav");
            IntroClipSecondSoil = UltraVoicePlugin.LoadClip("Swordsmachine.Soil.sm_SpawnSpecial2.wav");
            BigPainClipNoto = UltraVoicePlugin.LoadClip("Swordsmachine.Noto.sm_BigPain.wav");
            BigPainClipSoil = UltraVoicePlugin.LoadClip("Swordsmachine.Soil.sm_BigPain.wav");
            LungeClipNoto = UltraVoicePlugin.LoadClip("Swordsmachine.Noto.sm_Lunge.wav");
            LungeClipSoil = UltraVoicePlugin.LoadClip("Swordsmachine.Soil.sm_Lunge.wav");
            ComboClipNoto = UltraVoicePlugin.LoadClip("Swordsmachine.Noto.sm_Combo.wav");
            ComboClipSoil = UltraVoicePlugin.LoadClip("Swordsmachine.Soil.sm_Combo.wav");
            DeathClipNoto = UltraVoicePlugin.LoadClip("Swordsmachine.Noto.sm_Death.wav");
            DeathClipSoil = UltraVoicePlugin.LoadClip("Swordsmachine.Soil.sm_Death.wav");
            KnockdownClipSpecialNoto = UltraVoicePlugin.LoadClip("Swordsmachine.Noto.sm_KnockdownSpecial.wav");
            KnockdownClipSpecialSoil = UltraVoicePlugin.LoadClip("Swordsmachine.Soil.sm_KnockdownSpecial.wav");
            KnockdownClipSpecialBrutalNoto = UltraVoicePlugin.LoadClip("Swordsmachine.Noto.sm_PieceOfShit.wav");
            KnockdownClipSpecialBrutalSoil = UltraVoicePlugin.LoadClip("Swordsmachine.Soil.sm_PieceOfShit.wav");
            SpawnClipSecretNoto = UltraVoicePlugin.LoadClip("Swordsmachine.Mel.sm_SpawnSpecialSecret.wav");
            SpawnClipSecretSoil = UltraVoicePlugin.LoadClip("Swordsmachine.Mel.sm_SpawnSpecialSecret.wav");
            SandedClipsNoto = UltraVoicePlugin.LoadClips("Swordsmachine.Mel.sm_Sanded{0}.wav", 3);
            SandedClipsSoil = UltraVoicePlugin.LoadClips("Swordsmachine.Mel.sm_Sanded{0}.wav", 3);

            AgonySpawnClipNoto = UltraVoicePlugin.LoadClip("Swordsmachine.Noto.sm_SpawnSpecialAgony.wav");
            AgonySpawnClipSoil = UltraVoicePlugin.LoadClip("Swordsmachine.Soil.sm_SpawnSpecialAgony.wav");
            TundraSpawnClipNoto = UltraVoicePlugin.LoadClip("Swordsmachine.Noto.sm_SpawnSpecialTundra.wav");
            TundraSpawnClipSoil = UltraVoicePlugin.LoadClip("Swordsmachine.Soil.sm_SpawnSpecialTundra.wav");
            AgonyKnockdownClipNoto = UltraVoicePlugin.LoadClip("Swordsmachine.Noto.sm_DownedAgony.wav");
            AgonyKnockdownClipSoil = UltraVoicePlugin.LoadClip("Swordsmachine.Soil.sm_DownedAgony.wav");
            TundraKnockdownClipNoto = UltraVoicePlugin.LoadClip("Swordsmachine.Noto.sm_DownedTundra.wav");
            TundraKnockdownClipSoil = UltraVoicePlugin.LoadClip("Swordsmachine.Soil.sm_DownedTundra.wav");

            SpawnClipsNoto = UltraVoicePlugin.LoadClips("Swordsmachine.Noto.sm_Spawn{0}.wav", 7);
            SpawnClipsSoil = UltraVoicePlugin.LoadClips("Swordsmachine.Soil.sm_Spawn{0}.wav", 7);
            EnrageClipsNoto = UltraVoicePlugin.LoadClips("Swordsmachine.Noto.sm_Enrage{0}.wav", 4);
            EnrageClipsSoil = UltraVoicePlugin.LoadClips("Swordsmachine.Soil.sm_Enrage{0}.wav", 4);
            KnockdownClipsNoto = UltraVoicePlugin.LoadClips("Swordsmachine.Noto.sm_Knockdown{0}.wav", 3);
            KnockdownClipsSoil = UltraVoicePlugin.LoadClips("Swordsmachine.Soil.sm_Knockdown{0}.wav", 3);
            RangedClipsNoto = UltraVoicePlugin.LoadClips("Swordsmachine.Noto.sm_Ranged{0}.wav", 2);
            RangedClipsSoil = UltraVoicePlugin.LoadClips("Swordsmachine.Soil.sm_Ranged{0}.wav", 2);

            logger.LogInfo("Swordsmachine voice lines loaded successfully!");
        }

        public static void PlayRangedVoice(SwordsMachine sm)
        {
            if (!UltraVoicePlugin.SwordsmachineVoiceEnabled.Value)
                return;

            if (!VoiceManager.CheckCooldown(sm, 3f))
                return;

            if (VoiceManager.IsEnemyVoicePlaying(sm))
                return;

            if (VoiceManager.TooSoonAfterSpawn(sm, 2f))
                return;

            VoiceManager.PlayRandomVoice(sm, "Swordsmachine",
                UseSwordsmachineClips(RangedClips, RangedClipsNoto, RangedClipsSoil),
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

            if (!UltraVoicePlugin.SwordsmachineVoiceEnabled.Value)
                return;

            if (SwordsmachineCharacter.IsAgonyOrTundra(__instance))
                return;

            if (__instance.bossVersion)
            {
                if (SceneHelper.CurrentScene == "Level 0-3")
                    UltraVoicePlugin.Instance.StartCoroutine(PlayBossIntro(__instance));

                if (SceneHelper.CurrentScene == "Level 0-2") 
                    VoiceManager.CreateVoiceSource(__instance, "Swordsmachine",
                    SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.SpawnClipSecret, SwordsmachineCharacter.SpawnClipSecretNoto, SwordsmachineCharacter.SpawnClipSecretSoil),
                    "FINALLY CAUGHT UP WITH ME, HUH, PIPSQUEAK!?",
                    true
                );
            }

            VoiceManager.PlayRandomVoice(__instance, "Swordsmachine",
                SwordsmachineCharacter.UseSwordsmachineClips(SwordsmachineCharacter.SpawnClips, SwordsmachineCharacter.SpawnClipsNoto, SwordsmachineCharacter.SpawnClipsSoil),
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
                clip = SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.IntroClip, SwordsmachineCharacter.IntroClipNoto, SwordsmachineCharacter.IntroClipSoil);
                subtitle = "YOU WANT A FIGHT? LET'S FIGHT!";
            }
            else
            {
                clip = SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.IntroClipSecond, SwordsmachineCharacter.IntroClipSecondNoto, SwordsmachineCharacter.IntroClipSecondSoil);
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
            if (!UltraVoicePlugin.SwordsmachineVoiceEnabled.Value)
                return;

            if (!SwordsmachineCharacter.IsAgonyOrTundra(__instance))
                return;

            VoiceManager.enemySpawnTimes[__instance] = Time.time;

            if (SwordsmachineCharacter.IsAgony(__instance))
                VoiceManager.CreateVoiceSource(
                    __instance,
                    "AgonySpawn",
                    SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.AgonySpawnClip, SwordsmachineCharacter.AgonySpawnClipNoto, SwordsmachineCharacter.AgonySpawnClipSoil),
                    "JUMP 'EM!",
                    true,
                    SwordsmachineCharacter.AgonyColor
                );
            else if (SwordsmachineCharacter.IsTundra(__instance))
                VoiceManager.CreateVoiceSource(
                    __instance,
                    "TundraSpawn",
                    SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.TundraSpawnClip, SwordsmachineCharacter.TundraSpawnClipNoto, SwordsmachineCharacter.TundraSpawnClipSoil),
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
            if (!UltraVoicePlugin.SwordsmachineVoiceEnabled.Value)
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
                SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.EnrageClips[i], SwordsmachineCharacter.EnrageClipsNoto[i], SwordsmachineCharacter.EnrageClipsSoil[i]),
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
            if (!UltraVoicePlugin.SwordsmachineVoiceEnabled.Value)
                return;

            if (SwordsmachineCharacter.IsAgonyOrTundra(__instance))
                return;

            VoiceManager.CreateVoiceSource(
                __instance,
                "SwordsmachineBigPain",
                SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.BigPainClip, SwordsmachineCharacter.BigPainClipNoto, SwordsmachineCharacter.BigPainClipSoil),
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
                SwordsmachineCharacter.UseSwordsmachineClips(SwordsmachineCharacter.KnockdownClips, SwordsmachineCharacter.KnockdownClipsNoto, SwordsmachineCharacter.KnockdownClipsSoil),
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
                SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.KnockdownClipSpecial, SwordsmachineCharacter.KnockdownClipSpecialNoto, SwordsmachineCharacter.KnockdownClipSpecialSoil),
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
                SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.KnockdownClipSpecialBrutal, SwordsmachineCharacter.KnockdownClipSpecialBrutalNoto, SwordsmachineCharacter.KnockdownClipSpecialBrutalSoil),
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
            if (!UltraVoicePlugin.SwordsmachineVoiceEnabled.Value)
                return;

            if (!VoiceManager.CheckCooldown(__instance, 0.1f))
                return;

            VoiceManager.CreateVoiceSource(
                __instance,
                "SwordsmachineBigPain",
                SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.BigPainClip, SwordsmachineCharacter.BigPainClipNoto, SwordsmachineCharacter.BigPainClipSoil),
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
                    SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.AgonyKnockdownClip, SwordsmachineCharacter.AgonyKnockdownClipNoto, SwordsmachineCharacter.AgonyKnockdownClipSoil),
                    "DAMMIT!",
                    true,
                    SwordsmachineCharacter.AgonyColor
                );
            else if (SwordsmachineCharacter.IsTundra(sm))
                VoiceManager.CreateVoiceSource(
                    sm,
                    "TundraKnockdown",
                    SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.TundraKnockdownClip, SwordsmachineCharacter.TundraKnockdownClipNoto, SwordsmachineCharacter.TundraKnockdownClipSoil),
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
            if (!UltraVoicePlugin.SwordsmachineVoiceEnabled.Value)
                return;

            if (!VoiceManager.CheckCooldown(__instance, 3f))
                return;

            if (VoiceManager.IsEnemyVoicePlaying(__instance))
                return;

            VoiceManager.CreateVoiceSource(
                __instance,
                "SwordsmachineCombo",
                SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.ComboClip, SwordsmachineCharacter.ComboClipNoto, SwordsmachineCharacter.ComboClipSoil),
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
            if (!UltraVoicePlugin.SwordsmachineVoiceEnabled.Value)
                return;

            if (!VoiceManager.CheckCooldown(__instance, 3f))
                return;

            if (VoiceManager.IsEnemyVoicePlaying(__instance))
                return;

            VoiceManager.CreateVoiceSource(
                __instance,
                "SwordsmachineLunge",
                SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.LungeClip, SwordsmachineCharacter.LungeClipNoto, SwordsmachineCharacter.LungeClipSoil),
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
            if (!UltraVoicePlugin.SwordsmachineVoiceEnabled.Value)
                return;

            VoiceManager.CreateVoiceSource(
                __instance,
                "SwordsmachineDeath",
                SwordsmachineCharacter.UseSwordsmachineClip(SwordsmachineCharacter.DeathClip, SwordsmachineCharacter.DeathClipNoto, SwordsmachineCharacter.DeathClipSoil),
                null,
                true,
                subtitleColor: SwordsmachineCharacter.GetColorOverride(__instance),
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(EnemyIdentifier), nameof(EnemyIdentifier.Sandify))]
    class SwordsmachineSandedPatch
    {
        static void Prefix(EnemyIdentifier __instance, out bool __state)
        {
            __state = __instance.sandified;
        }

        static void Postfix(EnemyIdentifier __instance, bool __state)
        {
            if (!UltraVoicePlugin.SwordsmachineVoiceEnabled.Value)
                return;

            if (__state || !__instance.sandified)
                return;

            if (__instance.enemyType != EnemyType.Swordsmachine)
                return;

            if (!__instance.TryGetComponent<SwordsMachine>(out SwordsMachine sm))
                return;

            VoiceManager.PlayRandomVoice(
                sm,
                "SwordsmachineSanded",
                SwordsmachineCharacter.UseSwordsmachineClips(SwordsmachineCharacter.SandedClips, SwordsmachineCharacter.SandedClipsNoto, SwordsmachineCharacter.SandedClipsSoil),
                SwordsmachineCharacter.SandedSubs,
                true
            );
        }
    }
}
