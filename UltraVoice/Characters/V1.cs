using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using UltraVoice.Utilities;
using UnityEngine;

namespace UltraVoice.Characters
{
    public class V1Character
    {
        public static AudioClip[] KillClips;
        public static AudioClip[] MultikillClips;
        public static AudioClip[] BosskillClips;
        public static AudioClip[] HeadshotClips;
        public static AudioClip[] AirshotClips;
        public static AudioClip[] EnvironmentalClips;
        public static AudioClip[] MaxStyleClips;
        public static AudioClip[] RankLowClips;
        public static AudioClip[] RankHighClips;
        public static AudioClip[] RankPClips;
        public static AudioClip[] ParryClips;
        public static AudioClip[] DeathClips;
        public static AudioClip[] EnrageClips;
        public static AudioClip[] NewWeaponClips;
        public static AudioClip[] PurchaseClips;
        public static AudioClip[] SkullPickupClips;
        public static AudioClip[] CheckpointClips;
        public static AudioClip[] RespawnClips;
        public static AudioClip[] SecretOrbClips;
        public static AudioClip BosskillV2Clip;
        public static AudioClip BosskillEarthmoverClip;
        public static AudioClip BosskillGeryonClip;
        public static AudioClip EnrageV2Clip;

        public static readonly string[] KillSubs =
        {
            "ELIMINATED.",
            "EXECUTED.",
            "NEUTRALIZED.",
            "TERMINATED.",
            "PROBLEM SOLVED.",
            "NEXT.",
            "TARGET REMOVED.",
            "DISPOSED OF.",
            "NO LONGER RELEVANT."
        };

        public static readonly string[] MultikillSubs =
        {
            "COLLATERAL DAMAGE.",
            "CHAIN REACTION.",
            "SATISFYING.",
            "ANYONE ELSE?",
            "EFFICIENT.",
            "OPTIMAL.",
            "TOO EASY."
        };

        public static readonly string[] BosskillSubs =
        {
            "PRIORITY TARGET DOWN.",
            "THREAT ELIMINATED.",
            "INEVITABLE.",
            "UNFORTUNATE.",
            "YOU TRIED.",
            "EXPECTED OUTCOME.",
            "YOU WERE OUTMATCHED."
        };

        public static readonly string[] HeadshotSubs =
        {
            "CLEAN SHOT.",
            "PERFECT.",
            "PINPOINT ACCURACY.",
            "I DON’T MISS.",
            "CALCULATED."
        };

        public static readonly string[] AirshotSubs =
        {
            "BEAUTIFUL.",
            "IMPRESSIVE.",
            "THAT WAS ART."
        };

        public static readonly string[] EnvironmentalSubs =
        {
            "GOODBYE.",
            "LOOK OUT BELOW.",
            "I BARELY TOUCHED YOU.",
            "DOOMED TO FALL.",
            "YOU DID THIS TO YOURSELF.",
            "NATURAL CAUSES."
        };

        public static readonly string[] MaxStyleSubs =
        {
            "PEAK PERFORMANCE!",
            "I AM INVINCIBLE!",
            "THIS IS HAPPENING!",
            "MAXIMUM OUTPUT!"
        };

        public static readonly string[] RankLowSubs =
        {
            "INSUFFICIENT.",
            "IMPROVEMENT NECESSARY.",
            "THIS NEEDS WORK.",
            "SUBOPTIMAL."
        };

        public static readonly string[] RankHighSubs =
        {
            "ACCEPTABLE.",
            "AS INTENDED.",
            "SATISFACTORY.",
            "SOLID WORK."
        };

        public static readonly string[] RankPSubs =
        {
            "THIS IS HOW IT SHOULD BE.",
            "NOTHING WASTED. NOTHING LEFT.",
            "PERFECT EXECUTION.",
            "ABSOLUTE EFFICIENCY."
        };

        public static readonly string[] ParrySubs =
        {
            "RETURN TO SENDER.",
            "RIGHT BACK AT YOU.",
            "PREDICTABLE.",
            "NOT TODAY.",
            "NOPE.",
            "BACK TO YOU."
        };

        public static readonly string[] EnrageSubs =
        {
            "SOMEONE’S ANGRY.",
            "AGGRESSION DETECTED.",
            "THERE IT IS.",
            "THIS WILL BE ENTERTAINING.",
            "NOW WE’RE TALKING."
        };

        public static readonly string[] NewWeaponSubs =
        {
            "MINE NOW.",
            "FINDERS KEEPERS.",
            "THANK YOU.",
            "THIS WILL DO.",
            "INTERESTING.",
            "UPGRADE ACCEPTED."
        };

        public static readonly string[] PurchaseSubs =
        {
            "NEW TOOL ACQUIRED.",
            "EQUIPMENT OBTAINED.",
            "TRANSACTION COMPLETE."
        };

        public static readonly string[] SkullPickupSubs =
        {
            "ITEM ACQUIRED.",
            "THIS WILL BE USEFUL.",
            "I WILL CARRY THIS.",
            "KEY ITEM SECURED.",
            "REQUIRED OBJECT OBTAINED."
        };

        public static readonly string[] CheckpointSubs =
        {
            "MARKING POSITION.",
            "MEMORY RECORDED.",
            "WRITING TO MEMORY.",
            "SAVE STATE ESTABLISHED.",
            "STATE SAVED."
        };

        public static readonly string[] RespawnSubs =
        {
            "BACK ONLINE.",
            "BACK TO WORK.",
            "RESUMING OPERATION.",
            "RELOADING FROM CHECKPOINT.",
            "WHERE WAS I?"
        };

        public static readonly string[] SecretOrbSubs =
        {
            "ANOMALY DOCUMENTED.",
            "IRREGULARITY DETECTED.",
            "YOU WERE HIDING THIS?",
            "HIDDEN OBJECT FOUND.",
            "HIDDEN THINGS SHOULD STAY HIDDEN."
        };

        public static readonly Color SubtitleColor = new Color(0.184f, 0.310f, 0.561f);

        public static bool V2BossKillPlayed = false;

        public static float LastHeadshotKillTime = -999f;

        public static float LastAirshotKillTime = -999f;

        public static float LastEnvKillTime = -999f;

        public static float MaxStyleLineEndTime = -999f;

        public static float EnvLineEndTime = -999f;

        public static readonly HashSet<int> AlreadyPickedUp = new HashSet<int>();

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            KillClips = UltraVoicePlugin.LoadClips("V1.v1_Kill{0}.wav", 9);
            MultikillClips = UltraVoicePlugin.LoadClips("V1.v1_Multikill{0}.wav", 7);
            BosskillClips = UltraVoicePlugin.LoadClips("V1.v1_Bosskill{0}.wav", 7);
            HeadshotClips = UltraVoicePlugin.LoadClips("V1.v1_Headshot{0}.wav", 5);
            AirshotClips = UltraVoicePlugin.LoadClips("V1.v1_Airshot{0}.wav", 3);
            EnvironmentalClips = UltraVoicePlugin.LoadClips("V1.v1_Environmental{0}.wav", 6);
            MaxStyleClips = UltraVoicePlugin.LoadClips("V1.v1_MaxStyle{0}.wav", 4);
            RankLowClips = UltraVoicePlugin.LoadClips("V1.v1_RankLow{0}.wav", 4);
            RankHighClips = UltraVoicePlugin.LoadClips("V1.v1_RankHigh{0}.wav", 4);
            RankPClips = UltraVoicePlugin.LoadClips("V1.v1_RankP{0}.wav", 4);
            ParryClips = UltraVoicePlugin.LoadClips("V1.v1_Parry{0}.wav", 6);
            DeathClips = UltraVoicePlugin.LoadClips("V1.v1_Death{0}.wav", 4);
            EnrageClips = UltraVoicePlugin.LoadClips("V1.v1_Enrage{0}.wav", 5);
            NewWeaponClips = UltraVoicePlugin.LoadClips("V1.v1_NewWeapon{0}.wav", 6);
            PurchaseClips = UltraVoicePlugin.LoadClips("V1.v1_Purchase{0}.wav", 3);
            SkullPickupClips = UltraVoicePlugin.LoadClips("V1.v1_SkullPickup{0}.wav", 5);
            CheckpointClips = UltraVoicePlugin.LoadClips("V1.v1_Checkpoint{0}.wav", 5);
            RespawnClips = UltraVoicePlugin.LoadClips("V1.v1_Respawn{0}.wav", 5);
            SecretOrbClips = UltraVoicePlugin.LoadClips("V1.v1_SecretOrb{0}.wav", 5);
            BosskillV2Clip = UltraVoicePlugin.LoadClip("V1.v1_BosskillV2.wav");
            BosskillEarthmoverClip = UltraVoicePlugin.LoadClip("V1.v1_BosskillEarthmover.wav");
            BosskillGeryonClip = UltraVoicePlugin.LoadClip("V1.v1_BosskillGeryon.wav");
            EnrageV2Clip = UltraVoicePlugin.LoadClip("V1.v1_EnrageV2.wav");

            logger.LogInfo("V1 voice lines loaded successfully!");
        }

        public static void PlayBosskillLine()
        {
            if (!UltraVoicePlugin.V1LineEnabled("bosskill"))
                return;

            UltraVoicePlugin.Instance.StartCoroutine(BosskillRoutine());
        }

        static IEnumerator BosskillRoutine()
        {
            yield return new WaitForSeconds(1f);

            while (Time.time < MaxStyleLineEndTime)
                yield return null;

            NewMovement player = MonoSingleton<NewMovement>.Instance;

            if (player == null)
                yield break;

            VoiceManager.PlayRandomVoice(player, "V1",
                BosskillClips,
                BosskillSubs,
                interrupt: true,
                colorOverride: SubtitleColor,
                spatialBlend: 0f
            );
        }

        public static IEnumerator PickupLineRoutine(NewMovement player, AudioClip[] clips, string[] subs)
        {
            for (float waited = 0f; waited < 8f; waited += Time.deltaTime)
            {
                if (player == null)
                    yield break;

                if (Time.time >= MaxStyleLineEndTime && !VoiceManager.IsEnemyVoicePlaying(player))
                    break;

                yield return null;
            }

            if (player == null)
                yield break;

            VoiceManager.PlayRandomVoice(player, "V1", clips, subs,
                colorOverride: SubtitleColor,
                spatialBlend: 0f
            );
        }

        public static IEnumerator KillLineRoutine(NewMovement player, bool environmental)
        {
            yield return new WaitForSeconds(0.1f);

            if (player == null)
                yield break;

            if (Time.time < MaxStyleLineEndTime)
                yield break;

            bool env = environmental || Time.time - LastEnvKillTime < 0.3f;

            if (env && Time.time < EnvLineEndTime)
                yield break;

            StyleCalculator scalc = MonoSingleton<StyleCalculator>.Instance;

            if (!env && scalc != null && scalc.multikillCount >= 2)
                yield break;

            bool airshot = Time.time - LastAirshotKillTime < 0.5f;
            bool headshot = Time.time - LastHeadshotKillTime < 0.3f;

            string category = env ? "environmental" : airshot ? "airshot" : headshot ? "headshot" : "kill";

            if (!UltraVoicePlugin.V1LineEnabled(category))
                yield break;

            AudioClip[] clips = env ? EnvironmentalClips : airshot ? AirshotClips : headshot ? HeadshotClips : KillClips;
            string[] subs = env ? EnvironmentalSubs : airshot ? AirshotSubs : headshot ? HeadshotSubs : KillSubs;

            if (clips == null || clips.Length == 0)
                yield break;

            int index = Random.Range(0, clips.Length);

            AudioSource src = VoiceManager.CreateVoiceSource(player, "V1", clips[index],
                index < subs.Length ? subs[index] : null,
                env,
                SubtitleColor,
                spatialBlend: 0f
            );

            if (env && src != null && clips[index] != null)
                EnvLineEndTime = Time.time + clips[index].length;
        }
    }

    [HarmonyPatch(typeof(EnemyIdentifier), nameof(EnemyIdentifier.Death), new[] { typeof(bool) })]
    class V1KillPatch
    {
        static void Prefix(EnemyIdentifier __instance, out bool __state)
        {
            __state = __instance.dead;
        }

        static void Postfix(EnemyIdentifier __instance, bool __state)
        {
            if (!UltraVoicePlugin.V1VoiceEnabled.value)
                return;

            if (__state || __instance.puppet || !__instance.dead)
                return;

            NewMovement player = MonoSingleton<NewMovement>.Instance;

            if (player == null)
                return;

            if (__instance.TryGetComponent<BossHealthBar>(out _))
            {
                if (__instance.GetComponent<V2>() != null || __instance.GetComponent<Gabriel>() != null || __instance.GetComponent<GabrielSecond>() != null)
                    return;

                if (__instance.GetComponent<MinosPrime>() != null || __instance.GetComponent<SisyphusPrime>() != null)
                    return;

                if (__instance.GetComponent<FleshPrison>() != null)
                    return;

                if (__instance.GetComponent<Geryon>() != null)
                    return;

                V1Character.PlayBosskillLine();
                return;
            }

            bool environmental = __instance.hitter == "deathzone" || __instance.hitter == "terminalvelocity";

            if (environmental)
                V1Character.LastEnvKillTime = Time.time;

            UltraVoicePlugin.Instance.StartCoroutine(V1Character.KillLineRoutine(player, environmental));
        }
    }

    [HarmonyPatch(typeof(MinosPrime), nameof(MinosPrime.OutroEnd))]
    class V1MinosPrimeOutroPatch
    {
        static void Prefix(MinosPrime __instance, out bool __state)
        {
            __state = __instance.gameObject.activeInHierarchy;
        }

        static void Postfix(bool __state)
        {
            if (__state)
                V1Character.PlayBosskillLine();
        }
    }

    [HarmonyPatch(typeof(SisyphusPrime), nameof(SisyphusPrime.OutroEnd))]
    class V1SisyphusPrimeOutroPatch
    {
        static void Prefix(SisyphusPrime __instance, out bool __state)
        {
            __state = __instance.gameObject.activeInHierarchy;
        }

        static void Postfix(bool __state)
        {
            if (__state)
                V1Character.PlayBosskillLine();
        }
    }

    [HarmonyPatch(typeof(GameObject), "SetActive")]
    class V1BosskillV2Patch
    {
        static void Postfix(GameObject __instance, bool value)
        {
            if (__instance.name != "v2_GreenArm")
                return;

            if (!UltraVoicePlugin.V1LineEnabled("bosskill"))
                return;

            if (V1Character.V2BossKillPlayed)
                return;

            Transform t = __instance.transform;
            bool found = false;

            while (t != null)
            {
                if (t.name.Contains("8 Stuff(Clone)(Clone)"))
                {
                    found = true;
                    break;
                }
                t = t.parent;
            }

            if (!found)
                return;

            V1Character.V2BossKillPlayed = true;

            NewMovement player = MonoSingleton<NewMovement>.Instance;

            if (player == null)
                return;

            float delay = 2.5f;

            AudioClip v2DeathClip = V2Character.UseV2Clip(V2Character.DeathClip, V2Character.DeathClipCazsu);
            if (UltraVoicePlugin.V2VoiceEnabled.value && v2DeathClip != null)
                delay = v2DeathClip.length + 0.4f;

            UltraVoicePlugin.Instance.StartCoroutine(Routine());

            IEnumerator Routine()
            {
                yield return new WaitForSeconds(delay);

                while (Time.time < V1Character.MaxStyleLineEndTime)
                    yield return null;

                if (player == null)
                    yield break;

                VoiceManager.CreateVoiceSource(player, "V1",
                    V1Character.BosskillV2Clip,
                    "IT WAS YOU OR ME, SISTER.",
                    true,
                    V1Character.SubtitleColor,
                    spatialBlend: 0f
                );
            }
        }
    }

    [HarmonyPatch(typeof(StyleCalculator), nameof(StyleCalculator.HitCalculator))]
    class V1HeadshotPatch
    {
        static void Postfix(string hitLimb, bool dead)
        {
            if (dead && hitLimb == "head")
                V1Character.LastHeadshotKillTime = Time.time;
        }
    }

    [HarmonyPatch(typeof(FinalRank), "Appear")]
    class V1FinalRankPatch
    {
        static void Prefix(FinalRank __instance, out bool __state)
        {
            __state = __instance.totalRank != null && __instance.totalRank.gameObject.activeSelf;
        }

        static void Postfix(FinalRank __instance, bool __state)
        {
            if (!UltraVoicePlugin.V1LineEnabled("rank"))
                return;

            if (__instance.totalRank == null || __state || !__instance.totalRank.gameObject.activeSelf)
                return;

            string rank = StripTags(__instance.totalRank.text);

            AudioClip[] clips;
            string[] subs;

            switch (rank)
            {
                case "D":
                case "C":
                case "B":
                    clips = V1Character.RankLowClips;
                    subs = V1Character.RankLowSubs;
                    break;
                case "A":
                case "S":
                    clips = V1Character.RankHighClips;
                    subs = V1Character.RankHighSubs;
                    break;
                case "P":
                    clips = V1Character.RankPClips;
                    subs = V1Character.RankPSubs;
                    break;
                default:
                    return;
            }

            NewMovement player = MonoSingleton<NewMovement>.Instance;

            if (player == null)
                return;

            VoiceManager.PlayRandomVoice(player, "V1", clips, subs,
                interrupt: true,
                colorOverride: V1Character.SubtitleColor,
                spatialBlend: 0f
            );
        }

        static string StripTags(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";

            var sb = new System.Text.StringBuilder();
            bool inTag = false;

            foreach (char c in text)
            {
                if (c == '<')
                    inTag = true;
                else if (c == '>')
                    inTag = false;
                else if (!inTag)
                    sb.Append(c);
            }

            return sb.ToString().Trim();
        }
    }

    [HarmonyPatch(typeof(WeaponPickUp), "GotActivated")]
    class V1WeaponPickupPatch
    {
        static void Postfix(WeaponPickUp __instance)
        {
            if (!UltraVoicePlugin.V1LineEnabled("pickup"))
                return;

            if (__instance.pPref == "rev0" || (__instance.weapon != null && __instance.weapon.name.Contains("Revolver Pierce")))
                return;

            NewMovement player = MonoSingleton<NewMovement>.Instance;

            if (player == null)
                return;

            UltraVoicePlugin.Instance.StartCoroutine(V1Character.PickupLineRoutine(player,
                V1Character.NewWeaponClips, V1Character.NewWeaponSubs));
        }
    }

    [HarmonyPatch(typeof(AltPickUp), "GotActivated")]
    class V1AltPickupPatch
    {
        static void Postfix()
        {
            if (!UltraVoicePlugin.V1LineEnabled("pickup"))
                return;

            NewMovement player = MonoSingleton<NewMovement>.Instance;

            if (player == null)
                return;

            UltraVoicePlugin.Instance.StartCoroutine(V1Character.PickupLineRoutine(player,
                V1Character.NewWeaponClips, V1Character.NewWeaponSubs));
        }
    }

    [HarmonyPatch(typeof(GameObject), "SetActive")]
    class V1BosskillGeryonPatch
    {
        static void Postfix(GameObject __instance, bool value)
        {
            if (__instance == null || __instance.name != "Geryon_Rig")
                return;

            if (!UltraVoicePlugin.V1LineEnabled("bosskill"))
                return;

            if (__instance.transform.parent == null || __instance.transform.parent.name != "Theatre (1)")
                return;

            NewMovement player = MonoSingleton<NewMovement>.Instance;

            if (player == null)
                return;

            float delay = 1f;

            if (UltraVoicePlugin.GeryonVoiceEnabled.value && GeryonCharacter.DeathClip != null)
                delay = 0.2f + GeryonCharacter.DeathClip.length + 0.3f;

            UltraVoicePlugin.Instance.StartCoroutine(Routine());

            IEnumerator Routine()
            {
                yield return new WaitForSeconds(delay);

                if (player == null)
                    yield break;

                VoiceManager.CreateVoiceSource(player, "V1",
                    V1Character.BosskillGeryonClip,
                    "THANK YOU ALL FOR BEING SUCH A WONDERFUL CROWD.",
                    true,
                    V1Character.SubtitleColor,
                    spatialBlend: 0f
                );
            }
        }
    }

    [HarmonyPatch(typeof(CheckPoint), nameof(CheckPoint.ActivateCheckPoint))]
    class V1CheckpointPatch
    {
        static void Postfix()
        {
            if (!UltraVoicePlugin.V1LineEnabled("checkpoint"))
                return;

            if (Time.time < V1Character.MaxStyleLineEndTime)
                return;

            NewMovement player = MonoSingleton<NewMovement>.Instance;

            if (player == null)
                return;

            VoiceManager.PlayRandomVoice(player, "V1", V1Character.CheckpointClips, V1Character.CheckpointSubs,
                colorOverride: V1Character.SubtitleColor,
                spatialBlend: 0f
            );
        }
    }

    [HarmonyPatch(typeof(CheckPoint), nameof(CheckPoint.OnRespawn))]
    class V1CheckpointRespawnPatch
    {
        static void Postfix()
        {
            if (!UltraVoicePlugin.V1LineEnabled("respawn"))
                return;

            NewMovement player = MonoSingleton<NewMovement>.Instance;

            if (player == null)
                return;

            VoiceManager.PlayRandomVoiceDelayed(0.75f, player, "V1", V1Character.RespawnClips, V1Character.RespawnSubs,
                interrupt: true,
                colorOverride: V1Character.SubtitleColor,
                spatialBlend: 0f
            );
        }
    }

    [HarmonyPatch(typeof(Bonus), "OnTriggerEnter")]
    class V1SecretOrbPatch
    {
        static void Prefix(Bonus __instance, Collider other, out bool __state)
        {
            __state = !__instance.activated && !__instance.ghost && other.gameObject.CompareTag("Player");
        }

        static void Postfix(Bonus __instance, bool __state)
        {
            if (!__state || !__instance.activated)
                return;

            if (!UltraVoicePlugin.V1LineEnabled("secret"))
                return;

            if (Time.time < V1Character.MaxStyleLineEndTime)
                return;

            NewMovement player = MonoSingleton<NewMovement>.Instance;

            if (player == null)
                return;

            VoiceManager.PlayRandomVoice(player, "V1", V1Character.SecretOrbClips, V1Character.SecretOrbSubs,
                colorOverride: V1Character.SubtitleColor,
                spatialBlend: 0f
            );
        }
    }

    [HarmonyPatch(typeof(VariationInfo), nameof(VariationInfo.WeaponBought))]
    class V1PurchasePatch
    {
        static void Postfix()
        {
            if (!UltraVoicePlugin.V1LineEnabled("purchase"))
                return;

            if (Time.time < V1Character.MaxStyleLineEndTime)
                return;

            NewMovement player = MonoSingleton<NewMovement>.Instance;

            if (player == null)
                return;

            VoiceManager.PlayRandomVoice(player, "V1", V1Character.PurchaseClips, V1Character.PurchaseSubs,
                colorOverride: V1Character.SubtitleColor,
                spatialBlend: 0f
            );
        }
    }

    [HarmonyPatch(typeof(ItemIdentifier), "PickUp")]
    class V1SkullPickupPatch
    {
        static void Postfix(ItemIdentifier __instance)
        {
            if (!UltraVoicePlugin.V1LineEnabled("skull"))
                return;

            if (__instance.itemType != ItemType.SkullBlue && __instance.itemType != ItemType.SkullRed)
                return;

            if (SceneHelper.CurrentScene == "Level 0-S")
                return;

            if (!V1Character.AlreadyPickedUp.Add(__instance.GetInstanceID()))
                return;

            if (Time.time < V1Character.MaxStyleLineEndTime)
                return;

            NewMovement player = MonoSingleton<NewMovement>.Instance;

            if (player == null)
                return;

            VoiceManager.PlayRandomVoice(player, "V1", V1Character.SkullPickupClips, V1Character.SkullPickupSubs,
                colorOverride: V1Character.SubtitleColor,
                spatialBlend: 0f
            );
        }
    }

    [HarmonyPatch(typeof(DeathSequence), "OnEnable")]
    class V1DeathPatch
    {
        static void Postfix()
        {
            if (!UltraVoicePlugin.V1LineEnabled("death"))
                return;

            NewMovement player = MonoSingleton<NewMovement>.Instance;

            if (player == null)
                return;

            VoiceManager.PlayRandomVoice(player, "V1", V1Character.DeathClips, null,
                interrupt: true,
                spatialBlend: 0f
            );
        }
    }

    [HarmonyPatch(typeof(NewMovement), nameof(NewMovement.Respawn))]
    class V1RespawnPatch
    {
        static void Postfix(NewMovement __instance)
        {
            VoiceManager.InterruptVoices(__instance);
        }
    }

    [HarmonyPatch(typeof(StyleHUD), "AscendRank")]
    class V1MaxStylePatch
    {
        static void Prefix(StyleHUD __instance, out int __state)
        {
            __state = __instance.rankIndex;
        }

        static void Postfix(StyleHUD __instance, int __state)
        {
            if (!UltraVoicePlugin.V1LineEnabled("maxstyle"))
                return;

            int topRank = __instance.ranks.Count - 1;

            if (__state >= topRank || __instance.rankIndex < topRank)
                return;

            if (Time.time < V1Character.MaxStyleLineEndTime)
                return;

            NewMovement player = MonoSingleton<NewMovement>.Instance;

            if (player == null)
                return;

            if (V1Character.MaxStyleClips == null || V1Character.MaxStyleClips.Length == 0)
                return;

            int i = Random.Range(0, V1Character.MaxStyleClips.Length);
            AudioClip clip = V1Character.MaxStyleClips[i];

            if (clip == null)
                return;

            AudioSource src = VoiceManager.CreateVoiceSource(player, "V1", clip,
                i < V1Character.MaxStyleSubs.Length ? V1Character.MaxStyleSubs[i] : null,
                true,
                V1Character.SubtitleColor,
                spatialBlend: 0f
            );

            if (src != null)
                V1Character.MaxStyleLineEndTime = Time.time + clip.length;
        }
    }

    [HarmonyPatch(typeof(StyleHUD), nameof(StyleHUD.AddPoints))]
    class V1StylePatch
    {
        static void Postfix(string pointID, EnemyIdentifier eid)
        {
            if (pointID == "ultrakill.instakill")
                V1Character.LastHeadshotKillTime = Time.time;

            if (pointID == "ultrakill.airshot" || pointID == "ultrakill.fireworks" || pointID == "ultrakill.airslam")
                V1Character.LastAirshotKillTime = Time.time;

            if (pointID == "ultrakill.splattered" || pointID == "ultrakill.mauriced" || pointID == "ultrakill.compressed")
                V1Character.LastEnvKillTime = Time.time;

            if (pointID == "ultrakill.parry" || pointID == "ultrakill.chargeback")
            {
                if (pointID == "ultrakill.chargeback" && eid != null && eid.enemyType == EnemyType.Gutterman)
                    return;

                if (!UltraVoicePlugin.V1LineEnabled("parry"))
                    return;

                if (Time.time < V1Character.MaxStyleLineEndTime)
                    return;

                NewMovement player = MonoSingleton<NewMovement>.Instance;

                if (player == null)
                    return;

                VoiceManager.PlayRandomVoice(player, "V1", V1Character.ParryClips, V1Character.ParrySubs,
                    colorOverride: V1Character.SubtitleColor,
                    spatialBlend: 0f
                );
            }

            if (pointID != null && pointID.Length > 0)
            {
                StyleHUD shud = MonoSingleton<StyleHUD>.Instance;
                string styleName = shud != null ? shud.GetLocalizedName(pointID).ToUpperInvariant() : "";

                if (styleName.Contains("HITTING YOURSELF"))
                {
                    if (!UltraVoicePlugin.V1LineEnabled("enrage"))
                        return;

                    NewMovement enragePlayer = MonoSingleton<NewMovement>.Instance;

                    if (enragePlayer == null)
                        return;

                    VoiceManager.CreateVoiceSource(enragePlayer, "V1",
                        V1Character.EnrageV2Clip,
                        "STOP HITTING YOURSELF.",
                        true,
                        V1Character.SubtitleColor,
                        spatialBlend: 0f
                    );

                    return;
                }

                if (styleName.Contains("RAISON"))
                {
                    if (!UltraVoicePlugin.V1LineEnabled("bosskill"))
                        return;

                    NewMovement raisonPlayer = MonoSingleton<NewMovement>.Instance;

                    if (raisonPlayer == null)
                        return;

                    VoiceManager.PlayRandomVoiceDelayed(1f, raisonPlayer, "V1",
                        new[] { V1Character.BosskillEarthmoverClip },
                        new[] { "PURPOSE: FULFILLED." },
                        interrupt: true,
                        colorOverride: V1Character.SubtitleColor,
                        spatialBlend: 0f
                    );

                    return;
                }
            }

            if (pointID == "ultrakill.enraged")
            {
                if (!UltraVoicePlugin.V1LineEnabled("enrage"))
                    return;

                if (Time.time < V1Character.MaxStyleLineEndTime)
                    return;

                NewMovement player = MonoSingleton<NewMovement>.Instance;

                if (player == null)
                    return;

                VoiceManager.PlayRandomVoiceDelayed(0.5f, player, "V1", V1Character.EnrageClips, V1Character.EnrageSubs,
                    colorOverride: V1Character.SubtitleColor,
                    spatialBlend: 0f
                );
            }
        }
    }

    [HarmonyPatch(typeof(StyleCalculator), nameof(StyleCalculator.AddToMultiKill))]
    class V1MultikillPatch
    {
        static void Postfix(StyleCalculator __instance)
        {
            if (!UltraVoicePlugin.V1LineEnabled("multikill"))
                return;

            if (__instance.multikillCount != 2)
                return;

            if (Time.time - V1Character.LastEnvKillTime < 0.3f)
                return;

            if (Time.time < V1Character.MaxStyleLineEndTime)
                return;

            NewMovement player = MonoSingleton<NewMovement>.Instance;

            if (player == null)
                return;

            VoiceManager.PlayRandomVoice(player, "V1", V1Character.MultikillClips, V1Character.MultikillSubs,
                interrupt: true,
                colorOverride: V1Character.SubtitleColor,
                spatialBlend: 0f
            );
        }
    }
}
