using HarmonyLib;
using System;
using System.Collections;
using UltraVoice.Utilities;
using UnityEngine;

namespace UltraVoice.Characters
{
    public class InsurrectionistCharacter
    {
        public static AudioClip[] SpawnClips;
        public static AudioClip[] AttackClips;
        public static AudioClip[] AttackSpecialClips;
        public static AudioClip[] StunClips;
        public static AudioClip[] FrustratedClips;
        public static AudioClip[] DeathClips;

        public static AudioClip AngrySpawnClip;
        public static AudioClip RudeSpawnClip;
        public static AudioClip AngryKnockdownClip;
        public static AudioClip RudeKnockdownClip;

        public static readonly string[] SpawnSubs =
        {
            "YOU OR ME!",
            "YOU SHALL FALL!",
            "MY DEATH OR YOURS!",
            "PROVE YOURSELF!"
        };

        public static readonly string[] FrustratedSubs =
        {
            "YOU WILL PAY!",
            "I AM NOT DONE!",
            "STILL STANDING!",
        };

        public static readonly string[] AttackSpecialSubs =
        {
            "YOU CANNOT ESCAPE!",
            "THIS WILL HURT!",
            "BE GONE!",
        };

        public static Color InsurrectionistColor => VoiceManager.GetEnemyTypeColor(EnemyType.Sisyphus);
        public static Color AngryColor = new Color(0.81f, 0.14f, 0.16f);
        public static Color RudeColor = new Color(0f, 0.26f, 0.43f);

        public static bool IsAngryOrRude(Sisyphus sisyphus)
        {
            Transform t = sisyphus.transform;

            while (t != null)
            {
                if (t.name.Contains("10B - Chapel Roof"))
                    return true;

                t = t.parent;
            }

            return false;
        }

        public static bool IsAngry(Sisyphus sisyphus)
        {
            return IsAngryOrRude(sisyphus) && sisyphus.gameObject.name.Contains("Angry");
        }

        public static bool IsRude(Sisyphus sisyphus)
        {
            return IsAngryOrRude(sisyphus) && sisyphus.gameObject.name.Contains("Rude");
        }

        public static Color? GetColorOverride(Sisyphus insur)
        {
            if (IsAngry(insur))
                return AngryColor;

            if (IsRude(insur))
                return RudeColor;

            return InsurrectionistColor;
        }

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            SpawnClips = UltraVoicePlugin.LoadClips("Insurrectionist.insur_Spawn{0}.wav", 4);
            AttackClips = UltraVoicePlugin.LoadClips("Insurrectionist.insur_Attack{0}.wav", 4);
            AttackSpecialClips = UltraVoicePlugin.LoadClips("Insurrectionist.insur_AttackSpecial{0}.wav", 3);
            StunClips = UltraVoicePlugin.LoadClips("Insurrectionist.insur_Stun{0}.wav", 2);
            FrustratedClips = UltraVoicePlugin.LoadClips("Insurrectionist.insur_Frustration{0}.wav", 3);
            DeathClips = UltraVoicePlugin.LoadClips("Insurrectionist.insur_Death{0}.wav", 2);

            AngrySpawnClip = UltraVoicePlugin.LoadClip("Insurrectionist.insur_SpawnSpecialAngry.wav");
            RudeSpawnClip = UltraVoicePlugin.LoadClip("Insurrectionist.insur_SpawnSpecialRude.wav");
            AngryKnockdownClip = UltraVoicePlugin.LoadClip("Insurrectionist.insur_DownedAngry.wav");
            RudeKnockdownClip = UltraVoicePlugin.LoadClip("Insurrectionist.insur_DownedRude.wav");

            logger.LogInfo("Insurrectionist voice lines loaded successfully!");
        }

        public static void PlayAttackVoice(Sisyphus insur)
        {
            if (UnityEngine.Random.Range(0f, 1f) < 0.75f)
                VoiceManager.PlayRandomVoice(insur, "Insurrectionist",
                    AttackClips,
                    null,
                    randomPitch: true
                );
            else
                VoiceManager.PlayRandomVoice(insur, "Insurrectionist",
                    AttackSpecialClips,
                    AttackSpecialSubs,
                    colorOverride: GetColorOverride(insur),
                    randomPitch: true
                );
        }
    }

    [HarmonyPatch(typeof(Sisyphus), nameof(Sisyphus.Start))]
    class InsurrectionistSpawnPatch
    {
        static void Postfix(Sisyphus __instance)
        {
            if (!UltraVoicePlugin.InsurrectionistVoiceEnabled.value) return;

            if (!InsurrectionistCharacter.IsAngryOrRude(__instance))
                VoiceManager.PlayRandomVoice(__instance, "Insurrectionist",
                    InsurrectionistCharacter.SpawnClips,
                    InsurrectionistCharacter.SpawnSubs,
                    true,
                    colorOverride: InsurrectionistCharacter.GetColorOverride(__instance),
                    randomPitch: true
                );
            else if (InsurrectionistCharacter.IsAngry(__instance))
                VoiceManager.CreateVoiceSource(__instance, "Insurrectionist",
                    InsurrectionistCharacter.AngrySpawnClip,
                    "YOU... ARE CORNERED!",
                    true,
                    subtitleColor: InsurrectionistCharacter.GetColorOverride(__instance),
                    randomPitch: true
                );
            else if (InsurrectionistCharacter.IsRude(__instance))
                VoiceManager.CreateVoiceSource(__instance, "Insurrectionist",
                    InsurrectionistCharacter.RudeSpawnClip,
                    "YOU... ARE SURROUNDED!",
                    true,
                    subtitleColor: InsurrectionistCharacter.GetColorOverride(__instance),
                    randomPitch: true
                );
        }
    }

    [HarmonyPatch(typeof(Sisyphus), "Jump", new Type[] { typeof(bool) })]
    class InsurrectionistJumpPatch
    {
        static void Postfix(Sisyphus __instance)
        {
            if (!UltraVoicePlugin.InsurrectionistVoiceEnabled.value) return;

            VoiceManager.PlayRandomVoice(__instance, "Insurrectionist",
                InsurrectionistCharacter.AttackClips,
                null,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(Sisyphus), "Jump", new Type[] { typeof(Vector3), typeof(bool) })]
    class InsurrectionistJumpPatch2
    {
        static void Postfix(Sisyphus __instance)
        {
            if (!UltraVoicePlugin.InsurrectionistVoiceEnabled.value) return;

            VoiceManager.PlayRandomVoice(__instance, "Insurrectionist",
                InsurrectionistCharacter.AttackClips,
                null,
                randomPitch: true
            );
        }
    }

    [HarmonyPatch(typeof(Sisyphus), nameof(Sisyphus.AirStabAttack))]
    class InsurrectionistAirStabPatch
    {
        static void Postfix(Sisyphus __instance)
        {
            if (!UltraVoicePlugin.InsurrectionistVoiceEnabled.value) return;

            InsurrectionistCharacter.PlayAttackVoice(__instance);
        }
    }

    [HarmonyPatch(typeof(Sisyphus), nameof(Sisyphus.StabAttack))]
    class InsurrectionistGroundStabPatch
    {
        static void Postfix(Sisyphus __instance)
        {
            if (!UltraVoicePlugin.InsurrectionistVoiceEnabled.value) return;

            InsurrectionistCharacter.PlayAttackVoice(__instance);
        }
    }

    [HarmonyPatch(typeof(Sisyphus), nameof(Sisyphus.HorizontalSwingAttack))]
    class InsurrectionistHorizontalSwingPatch
    {
        static void Postfix(Sisyphus __instance)
        {
            if (!UltraVoicePlugin.InsurrectionistVoiceEnabled.value) return;

            InsurrectionistCharacter.PlayAttackVoice(__instance);
        }
    }

    [HarmonyPatch(typeof(Sisyphus), nameof(Sisyphus.OverheadSlamAttack))]
    class InsurrectionistOverheadSlamPatch
    {
        static void Postfix(Sisyphus __instance)
        {
            if (!UltraVoicePlugin.InsurrectionistVoiceEnabled.value) return;

            InsurrectionistCharacter.PlayAttackVoice(__instance);
        }
    }

    [HarmonyPatch(typeof(Animator), nameof(Animator.SetTrigger), typeof(int))]
    class InsurrectionistStompPatch
    {
        static void Prefix(Animator __instance, int id)
        {
            if (!UltraVoicePlugin.InsurrectionistVoiceEnabled.value) return;

            if (!__instance.gameObject.TryGetComponent<Sisyphus>(out var sisyphus))
                return;

            if (id != Sisyphus.s_Stomp) return;

            if (!VoiceManager.CheckCooldown(__instance, 2f))
                return;

            InsurrectionistCharacter.PlayAttackVoice(sisyphus);
        }
    }

    [HarmonyPatch(typeof(Sisyphus), nameof(Sisyphus.Knockdown))]
    class InsurrectionistKnockdowmPatch
    {
        static void Postfix(Sisyphus __instance)
        {
            if (!UltraVoicePlugin.InsurrectionistVoiceEnabled.value) return;

            if (!VoiceManager.CheckCooldown(__instance, 4f))
                return;

            VoiceManager.PlayRandomVoice(__instance, "Insurrectionist",
                InsurrectionistCharacter.StunClips,
                null,
                true,
                randomPitch: true
            );

            if (InsurrectionistCharacter.IsAngryOrRude(__instance))
                UltraVoicePlugin.Instance.StartCoroutine(PlayDowned(__instance));
            else
                VoiceManager.PlayRandomVoiceDelayed(1f, __instance, "Insurrectionist",
                    InsurrectionistCharacter.FrustratedClips,
                    InsurrectionistCharacter.FrustratedSubs,
                    interrupt: true,
                    colorOverride: InsurrectionistCharacter.GetColorOverride(__instance)
                );

            static IEnumerator PlayDowned(Sisyphus sisyphus)
            {
                yield return new WaitForSeconds(1f);

                if (sisyphus == null)
                    yield break;

                if (InsurrectionistCharacter.IsAngry(sisyphus))
                    VoiceManager.CreateVoiceSource(sisyphus, "Insurrectionist",
                        InsurrectionistCharacter.AngryKnockdownClip,
                        "BROTHER... HELP!",
                        true,
                        subtitleColor: InsurrectionistCharacter.GetColorOverride(sisyphus)
                    );
                else if (InsurrectionistCharacter.IsRude(sisyphus))
                    VoiceManager.CreateVoiceSource(sisyphus, "Insurrectionist",
                        InsurrectionistCharacter.RudeKnockdownClip,
                        "BROTHER... PLEASE!",
                        true,
                        subtitleColor: InsurrectionistCharacter.GetColorOverride(sisyphus)
                    );
            }
        }
    }

    [HarmonyPatch(typeof(Sisyphus), nameof(Sisyphus.Death))]
    class InsurrectionistDeathPatch
    {
        static void Postfix(Sisyphus __instance)
        {
            if (!UltraVoicePlugin.InsurrectionistVoiceEnabled.value) return;

            VoiceManager.PlayRandomVoice(__instance, "Insurrectionist",
                InsurrectionistCharacter.DeathClips,
                null,
                true,
                randomPitch: true
            );
        }
    }
}
