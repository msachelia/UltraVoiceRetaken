using HarmonyLib;
using System.Collections;
using UltraVoice.Utilities;
using UnityEngine;

namespace UltraVoice.Characters
{
    public class ShopTerminalCharacter
    {
        public static AudioClip[] BootUpClips;
        public static AudioClip[] BootUpPRankClips;
        public static AudioClip[] BootUpCheatsClips;
        public static AudioClip[] BootUpCybergrindClips;
        public static AudioClip BootUpCybergrindRespawnClip;
        public static AudioClip[] WaveCompleteClips;
        public static AudioClip[] CybergrindDeathClips;
        public static AudioClip[] PageEnemyClips;
        public static AudioClip[] PageWeaponClips;
        public static AudioClip[] PurchaseClips;
        public static AudioClip[] IdleClips;
        public static AudioClip BootUpSpecialClip;
        public static AudioClip BootUpSpecial05Clip;
        public static AudioClip BootUpSpecial14Clip;
        public static AudioClip BootUpSpecial24Clip;
        public static AudioClip BootUpSpecial44Clip;
        public static AudioClip BootUpSpecial74Clip;
        public static AudioClip BootUpSpecial81Clip;
        public static AudioClip BootUpSpecial82Clip;
        public static AudioClip BootUpSpecial83Clip;
        public static AudioClip BootUpSpecial84Clip;
        public static AudioClip BootUpSpecialP2Clip;
        public static AudioClip FlippedClip;

        public static AudioClip[] BootUpClipsGoober;
        public static AudioClip[] BootUpPRankClipsGoober;
        public static AudioClip[] BootUpCheatsClipsGoober;
        public static AudioClip[] BootUpCybergrindClipsGoober;
        public static AudioClip BootUpCybergrindRespawnClipGoober;
        public static AudioClip[] WaveCompleteClipsGoober;
        public static AudioClip[] CybergrindDeathClipsGoober;
        public static AudioClip[] PageEnemyClipsGoober;
        public static AudioClip[] PageWeaponClipsGoober;
        public static AudioClip[] PurchaseClipsGoober;
        public static AudioClip[] IdleClipsGoober;
        public static AudioClip BootUpSpecialClipGoober;
        public static AudioClip BootUpSpecial05ClipGoober;
        public static AudioClip BootUpSpecial14ClipGoober;
        public static AudioClip BootUpSpecial24ClipGoober;
        public static AudioClip BootUpSpecial44ClipGoober;
        public static AudioClip BootUpSpecial74ClipGoober;
        public static AudioClip BootUpSpecial81ClipGoober;
        public static AudioClip BootUpSpecial82ClipGoober;
        public static AudioClip BootUpSpecial83ClipGoober;
        public static AudioClip BootUpSpecial84ClipGoober;
        public static AudioClip BootUpSpecialP2ClipGoober;
        public static AudioClip FlippedClipGoober;

        public static readonly string[] BootUpSubs =
        {
            "Well, if it ain’t our favourite killing machine!",
            "Ay, glad to see you haven’t died yet!",
            "Nice to see a familiar face around here.",
            "Back for more carnage? I love it!",
            "He-hey! Look who it is!"
        };

        public static readonly string[] BootUpPRankSubs =
        {
            "That was AWESOME! Keep it up!",
            "Daaamn! Nice work up there, bud!",
            "That last one’s goin’ in the highlight reel for SURE!",
            "I’m tellin’ ya, the ratings are through the roof!"
        };

        public static readonly string[] BootUpCheatsSubs =
        {
            "Hey, uh, did you edit this footage? I don’t think I can use this...",
            "Uh, that’s not really how physics works, is it? Just checkin'.",
            "Creative... though I’m not sure the audience is gonna buy that one.",
            "Thanks for the- HEY! Did you splice this run together?"
        };

        public static readonly string[] BootUpCybergrindSubs =
        {
            "Sim’s up. Let’s see what you got!",
            "Sim’s ready. Show ‘em what you’re made of!",
            "Sim’s running! Go give ‘em hell!",
            "Sim’s primed. Go get a new high score!"
        };

        public static readonly string[] WaveCompleteSubs =
        {
            "V-1! V-1! V-1!",
            "Style points for that one!",
            "Yeaaah! That’s what I’m talkin’ about!",
            "They aren't even putting up a fight at this point!",
            "Clean sweep!",
            "What a performance!",
            "And the crows goes wild!",
            "Ladies and gentlemen, another PERFECT wave!"
        };

        public static readonly string[] CybergrindDeathSubs =
        {
            "Ooooooh! That one looked like it hurt!",
            "Aw, dammit! It was just startin’ to get interesting!",
            "Ay, it happens to the best of us.",
            "Hey, at least it was a flashy way to go!"
        };

        public static readonly string[] PageEnemySubs =
        {
            "Ah, lookin’ to strategize, eh?",
            "Read up! It’ll do ya some good!",
            "Studying the competition? Smart move."
        };

        public static readonly string[] PageWeaponSubs =
        {
            "You do cool stuff, I give you cool stuff! Simple as!",
            "Everything a top-tier machine could ever want. All for the right price.",
            "Quality hardware for a quality machine. Take your pick!",
            "I’ve got the good stuff right here. Take a look."
        };

        public static readonly string[] PurchaseSubs =
        {
            "Pleasure doin’ business!",
            "Money well-spent!",
            "Have fun with that!",
            "Now that’s an upgrade!",
            "Don't shoot your eye out, kid!"
        };

        public static readonly string[] IdleSubs =
        {
            "Hello? Hell to V1?",
            "You there, buddy?",
            "Enjoying the tunes?",
            "You just gonna stand there?",
            "I’m just a terminal, V1, I don’t have much to say if you’re not shootin’ things."
        };

        public static readonly Color SubtitleColor = new Color(0.992f, 0.839f, 0.216f);

        public static bool LastLevelWasPRank = false;

        public static bool LastLevelUsedCheats = false;

        public static bool BootUpPlayed = false;

        public static bool FlippedPlayed = false;

        public static bool CybergrindDeath = false;

        public static int ShopOpenedFrame = -10;

        public static float LastInteraction = -999f;

        public static float EndlessStartTime = -999f;

        const float IdleDelay = 20f;

        public static IEnumerator WaveWatcher()
        {
            int lastWave = 0;

            while (SceneHelper.CurrentScene == "Endless")
            {
                yield return new WaitForSeconds(0.25f);

                EndlessGrid grid = MonoSingleton<EndlessGrid>.Instance;

                if (grid == null)
                    continue;

                if (Time.time < EndlessStartTime + 5f)
                    continue;

                if (grid.currentWave <= lastWave)
                {
                    lastWave = grid.currentWave;
                    continue;
                }

                lastWave = grid.currentWave;

                if (!UltraVoicePlugin.ShopTerminalVoiceEnabled.value)
                    continue;

                NewMovement player = MonoSingleton<NewMovement>.Instance;

                if (player == null)
                    continue;

                VoiceManager.PlayRandomVoice(player, "ShopTerminal",
                    UseTerminalClips(WaveCompleteClips, WaveCompleteClipsGoober),
                    WaveCompleteSubs,
                    colorOverride: SubtitleColor,
                    spatialBlend: 0f
                );
            }
        }

        public static bool IsWeaponShop(ShopZone zone)
        {
            return zone != null && zone.GetComponentInChildren<ShopCategory>(true) != null;
        }

        public static void PlayPageLine(Component page, AudioClip[] clips, string[] subs)
        {
            if (!UltraVoicePlugin.ShopTerminalVoiceEnabled.value)
                return;

            if (Time.frameCount - ShopOpenedFrame <= 1)
                return;

            ShopZone zone = page.GetComponentInParent<ShopZone>();

            if (!IsWeaponShop(zone))
                return;

            VoiceManager.PlayRandomVoice(zone, "ShopTerminal", clips, subs, colorOverride: SubtitleColor);
        }

        public static AudioClip[] UseTerminalClips(AudioClip[] bouncyClips, AudioClip[] gooberClips)
        {
            return UsingGoober ? gooberClips : bouncyClips;
        }

        public static AudioClip UseTerminalClip(AudioClip bouncyClip, AudioClip gooberClip)
        {
            return UsingGoober ? gooberClip : bouncyClip;
        }

        public static bool UsingGoober =>
            UltraVoicePlugin.ShopTerminalVoiceActorField != null
            && UltraVoicePlugin.ShopTerminalVoiceActorField.value == UltraVoicePlugin.ShopTerminalVoiceActor.Goober;

        public static IEnumerator PurchaseRoutine(ShopZone zone)
        {
            yield return null;

            while (Time.time < V1Character.PurchaseLineEndTime)
                yield return null;

            if (!UltraVoicePlugin.ShopTerminalVoiceEnabled.value)
                yield break;

            if (zone == null)
                yield break;

            VoiceManager.PlayRandomVoice(zone, "ShopTerminal", UseTerminalClips(PurchaseClips, PurchaseClipsGoober), PurchaseSubs, colorOverride: SubtitleColor);
        }

        public static IEnumerator IdleWatcher(ShopZone zone)
        {
            LastInteraction = Time.time;

            while (zone != null && zone.inUse)
            {
                yield return new WaitForSeconds(0.5f);

                if (Time.time - LastInteraction < IdleDelay)
                    continue;

                LastInteraction = Time.time;

                if (!UltraVoicePlugin.ShopTerminalVoiceEnabled.value)
                    continue;

                VoiceManager.PlayRandomVoice(zone, "ShopTerminal", UseTerminalClips(IdleClips, IdleClipsGoober), IdleSubs, colorOverride: SubtitleColor);
            }
        }

        public static IEnumerator CybergrindDeathWatcher()
        {
            TimeController timeController = MonoSingleton<TimeController>.Instance;

            while (timeController != null && timeController.timeScale > 0f)
                yield return null;

            yield return new WaitForSecondsRealtime(1f);

            if (!UltraVoicePlugin.ShopTerminalVoiceEnabled.value)
                yield break;

            NewMovement player = MonoSingleton<NewMovement>.Instance;

            if (player == null)
                yield break;

            VoiceManager.PlayRandomVoice(player, "ShopTerminal",
                UseTerminalClips(CybergrindDeathClips, CybergrindDeathClipsGoober),
                CybergrindDeathSubs,
                colorOverride: SubtitleColor,
                spatialBlend: 0f,
                ignoreTimeFreeze: true
            );
        }

        public static void PlayScriptedLine(ShopZone zone, AudioClip clip, string firstSub, params (float bouncyDelay, float gooberDelay, string text)[] followUps)
        {
            UltraVoicePlugin.Instance.StartCoroutine(Routine());

            IEnumerator Routine()
            {
                var src = VoiceManager.CreateVoiceSource(zone, "ShopTerminal", clip, firstSub,
                    subtitleColor: SubtitleColor
                );

                if (src == null)
                    yield break;

                foreach (var (bouncyDelay, gooberDelay, text) in followUps)
                {
                    yield return new WaitForSeconds(UsingGoober ? gooberDelay : bouncyDelay);

                    if (!src)
                        yield break;

                    VoiceManager.ShowSubtitle(text, src, SubtitleColor);
                }
            }
        }

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            BootUpClips = UltraVoicePlugin.LoadClips("ShopTerminal.Bouncy.shop_BootUpGeneric{0}.wav", 5);
            BootUpClipsGoober = UltraVoicePlugin.LoadClips("ShopTerminal.Goober.shop_BootUpGeneric{0}.wav", 5);
            BootUpPRankClips = UltraVoicePlugin.LoadClips("ShopTerminal.Bouncy.shop_BootUpPRank{0}.wav", 4);
            BootUpPRankClipsGoober = UltraVoicePlugin.LoadClips("ShopTerminal.Goober.shop_BootUpPRank{0}.wav", 4);
            BootUpCheatsClips = UltraVoicePlugin.LoadClips("ShopTerminal.Bouncy.shop_BootUpCheats{0}.wav", 4);
            BootUpCheatsClipsGoober = UltraVoicePlugin.LoadClips("ShopTerminal.Goober.shop_BootUpCheats{0}.wav", 4);
            BootUpCybergrindClips = UltraVoicePlugin.LoadClips("ShopTerminal.Bouncy.shop_BootUpCybergrind{0}.wav", 4);
            BootUpCybergrindClipsGoober = UltraVoicePlugin.LoadClips("ShopTerminal.Goober.shop_BootUpCybergrind{0}.wav", 4);
            BootUpCybergrindRespawnClip = UltraVoicePlugin.LoadClip("ShopTerminal.Bouncy.shop_BootUpCybergrindRespawn.wav");
            BootUpCybergrindRespawnClipGoober = UltraVoicePlugin.LoadClip("ShopTerminal.Goober.shop_BootUpCybergrindRespawn.wav");
            WaveCompleteClips = UltraVoicePlugin.LoadClips("ShopTerminal.Goober.shop_WaveComplete{0}.wav", 8);
            WaveCompleteClipsGoober = UltraVoicePlugin.LoadClips("ShopTerminal.Goober.shop_WaveComplete{0}.wav", 8);
            CybergrindDeathClips = UltraVoicePlugin.LoadClips("ShopTerminal.Bouncy.shop_CybergrindDeath{0}.wav", 4);
            CybergrindDeathClipsGoober = UltraVoicePlugin.LoadClips("ShopTerminal.Goober.shop_CybergrindDeath{0}.wav", 4);
            PageEnemyClips = UltraVoicePlugin.LoadClips("ShopTerminal.Bouncy.shop_PageEnemy{0}.wav", 3);
            PageEnemyClipsGoober = UltraVoicePlugin.LoadClips("ShopTerminal.Goober.shop_PageEnemy{0}.wav", 3);
            PageWeaponClips = UltraVoicePlugin.LoadClips("ShopTerminal.Bouncy.shop_PageWeapon{0}.wav", 4);
            PageWeaponClipsGoober = UltraVoicePlugin.LoadClips("ShopTerminal.Goober.shop_PageWeapon{0}.wav", 4);
            PurchaseClips = UltraVoicePlugin.LoadClips("ShopTerminal.Bouncy.shop_Purchase{0}.wav", 5);
            PurchaseClipsGoober = UltraVoicePlugin.LoadClips("ShopTerminal.Goober.shop_Purchase{0}.wav", 5);
            IdleClips = UltraVoicePlugin.LoadClips("ShopTerminal.Bouncy.shop_Idle{0}.wav", 5);
            IdleClipsGoober = UltraVoicePlugin.LoadClips("ShopTerminal.Goober.shop_Idle{0}.wav", 5);
            BootUpSpecialClip = UltraVoicePlugin.LoadClip("ShopTerminal.Bouncy.shop_BootUpSpecial0-2.wav");
            BootUpSpecialClipGoober = UltraVoicePlugin.LoadClip("ShopTerminal.Goober.shop_BootUpSpecial0-2.wav");
            BootUpSpecial05Clip = UltraVoicePlugin.LoadClip("ShopTerminal.Bouncy.shop_BootUpSpecial0-5.wav");
            BootUpSpecial05ClipGoober = UltraVoicePlugin.LoadClip("ShopTerminal.Goober.shop_BootUpSpecial0-5.wav");
            BootUpSpecial14Clip = UltraVoicePlugin.LoadClip("ShopTerminal.Bouncy.shop_BootUpSpecial1-4.wav");
            BootUpSpecial14ClipGoober = UltraVoicePlugin.LoadClip("ShopTerminal.Goober.shop_BootUpSpecial1-4.wav");
            BootUpSpecial24Clip = UltraVoicePlugin.LoadClip("ShopTerminal.Bouncy.shop_BootUpSpecial2-4.wav");
            BootUpSpecial24ClipGoober = UltraVoicePlugin.LoadClip("ShopTerminal.Goober.shop_BootUpSpecial2-4.wav");
            BootUpSpecial44Clip = UltraVoicePlugin.LoadClip("ShopTerminal.Bouncy.shop_BootUpSpecial4-4.wav");
            BootUpSpecial44ClipGoober = UltraVoicePlugin.LoadClip("ShopTerminal.Goober.shop_BootUpSpecial4-4.wav");
            BootUpSpecial74Clip = UltraVoicePlugin.LoadClip("ShopTerminal.Bouncy.shop_BootUpSpecial7-4.wav");
            BootUpSpecial74ClipGoober = UltraVoicePlugin.LoadClip("ShopTerminal.Goober.shop_BootUpSpecial7-4.wav");
            BootUpSpecial81Clip = UltraVoicePlugin.LoadClip("ShopTerminal.Bouncy.shop_BootUpSpecial8-1.wav");
            BootUpSpecial81ClipGoober = UltraVoicePlugin.LoadClip("ShopTerminal.Goober.shop_BootUpSpecial8-1.wav");
            BootUpSpecial82Clip = UltraVoicePlugin.LoadClip("ShopTerminal.Bouncy.shop_BootUpSpecial8-2.wav");
            BootUpSpecial82ClipGoober = UltraVoicePlugin.LoadClip("ShopTerminal.Goober.shop_BootUpSpecial8-2.wav");
            BootUpSpecial83Clip = UltraVoicePlugin.LoadClip("ShopTerminal.Bouncy.shop_BootUpSpecial8-3.wav");
            BootUpSpecial83ClipGoober = UltraVoicePlugin.LoadClip("ShopTerminal.Goober.shop_BootUpSpecial8-3.wav");
            BootUpSpecial84Clip = UltraVoicePlugin.LoadClip("ShopTerminal.Bouncy.shop_BootUpSpecial8-4.wav");
            BootUpSpecial84ClipGoober = UltraVoicePlugin.LoadClip("ShopTerminal.Goober.shop_BootUpSpecial8-4.wav");
            BootUpSpecialP2Clip = UltraVoicePlugin.LoadClip("ShopTerminal.Bouncy.shop_BootUpSpecialP-2.wav");
            BootUpSpecialP2ClipGoober = UltraVoicePlugin.LoadClip("ShopTerminal.Goober.shop_BootUpSpecialP-2.wav");
            FlippedClip = UltraVoicePlugin.LoadClip("ShopTerminal.Bouncy.shop_Flipped.wav");
            FlippedClipGoober = UltraVoicePlugin.LoadClip("ShopTerminal.Goober.shop_Flipped.wav");

            logger.LogInfo("Shop Terminal voice lines loaded successfully!");
        }
    }

    public class ShopTerminalEnemyPageWatcher : MonoBehaviour
    {
        void OnEnable()
        {
            ShopTerminalCharacter.PlayPageLine(this,
                ShopTerminalCharacter.UseTerminalClips(ShopTerminalCharacter.PageEnemyClips, ShopTerminalCharacter.PageEnemyClipsGoober),
                ShopTerminalCharacter.PageEnemySubs
            );
        }
    }

    [HarmonyPatch(typeof(ShopZone), nameof(ShopZone.TurnOn))]
    class ShopTerminalScreenOnPatch
    {
        static void Prefix(ShopZone __instance, out bool __state)
        {
            __state = __instance.inUse;
            ShopTerminalCharacter.ShopOpenedFrame = Time.frameCount;
        }

        static void Postfix(ShopZone __instance, bool __state)
        {
            if (__state || !__instance.inUse)
                return;

            if (!ShopTerminalCharacter.IsWeaponShop(__instance))
                return;

            UltraVoicePlugin.Instance.StartCoroutine(ShopTerminalCharacter.IdleWatcher(__instance));
        }
    }

    [HarmonyPatch(typeof(ShopButton), "OnPointerClick")]
    class ShopTerminalInteractionPatch
    {
        static void Postfix()
        {
            ShopTerminalCharacter.LastInteraction = Time.time;
        }
    }

    [HarmonyPatch(typeof(VariationInfo), nameof(VariationInfo.WeaponBought))]
    class ShopTerminalPurchasePatch
    {
        static void Postfix(VariationInfo __instance)
        {
            ShopZone zone = __instance.GetComponentInParent<ShopZone>();

            if (zone == null)
                return;

            UltraVoicePlugin.Instance.StartCoroutine(ShopTerminalCharacter.PurchaseRoutine(zone));
        }
    }

    [HarmonyPatch(typeof(ShopMother), nameof(ShopMother.TurnOn))]
    class ShopTerminalMenuOnPatch
    {
        static void Prefix()
        {
            ShopTerminalCharacter.ShopOpenedFrame = Time.frameCount;
        }
    }

    [HarmonyPatch(typeof(EnemyInfoPage), "Start")]
    class ShopTerminalEnemyPagePatch
    {
        static void Postfix(EnemyInfoPage __instance)
        {
            if (__instance.GetComponent<ShopTerminalEnemyPageWatcher>() == null)
                __instance.gameObject.AddComponent<ShopTerminalEnemyPageWatcher>();
        }
    }

    [HarmonyPatch(typeof(ShopGearChecker), "OnEnable")]
    class ShopTerminalWeaponPagePatch
    {
        static void Postfix(ShopGearChecker __instance)
        {
            ShopTerminalCharacter.PlayPageLine(__instance,
                ShopTerminalCharacter.UseTerminalClips(ShopTerminalCharacter.PageWeaponClips, ShopTerminalCharacter.PageWeaponClipsGoober),
                ShopTerminalCharacter.PageWeaponSubs
            );
        }
    }

    [HarmonyPatch(typeof(FinalCyberRank), nameof(FinalCyberRank.GameOver))]
    class ShopTerminalCybergrindDeathPatch
    {
        static void Postfix()
        {
            if (ShopTerminalCharacter.CybergrindDeath)
                return;

            ShopTerminalCharacter.CybergrindDeath = true;

            UltraVoicePlugin.Instance.StartCoroutine(ShopTerminalCharacter.CybergrindDeathWatcher());
        }
    }

    [HarmonyPatch(typeof(ShopZone), "Start")]
    class ShopTerminalFlippedPatch
    {
        const float TriggerDistance = 15f;

        static void Postfix(ShopZone __instance)
        {
            if (SceneHelper.CurrentScene != "Level 5-3")
                return;

            if (__instance.transform.root.name != "Rotated")
                return;

            UltraVoicePlugin.Instance.StartCoroutine(Routine(__instance));
        }

        static IEnumerator Routine(ShopZone zone)
        {
            while (true)
            {
                yield return new WaitForSeconds(0.25f);

                if (zone == null || ShopTerminalCharacter.FlippedPlayed)
                    yield break;

                if (!UltraVoicePlugin.ShopTerminalVoiceEnabled.value)
                    continue;

                NewMovement player = MonoSingleton<NewMovement>.Instance;

                if (player == null)
                    continue;

                if (Vector3.Distance(zone.transform.position, player.transform.position) > TriggerDistance)
                    continue;

                ShopTerminalCharacter.FlippedPlayed = true;

                VoiceManager.CreateVoiceSource(
                    zone,
                    "ShopTerminal",
                    ShopTerminalCharacter.UseTerminalClip(ShopTerminalCharacter.FlippedClip, ShopTerminalCharacter.FlippedClipGoober),
                    "Ayyy, you mind helpin’ me up ‘ere?",
                    subtitleColor: ShopTerminalCharacter.SubtitleColor
                );

                yield break;
            }
        }
    }

    [HarmonyPatch(typeof(ShopZone), nameof(ShopZone.TurnOn))]
    class ShopTerminalBootUpPatch
    {
        static void Postfix(ShopZone __instance)
        {
            if (!UltraVoicePlugin.ShopTerminalVoiceEnabled.value)
                return;

            if (ShopTerminalCharacter.BootUpPlayed || !__instance.inUse)
                return;

            if (!ShopTerminalCharacter.IsWeaponShop(__instance))
                return;

            ShopTerminalCharacter.BootUpPlayed = true;

            UltraVoicePlugin.Instance.StartCoroutine(Routine(__instance));
        }

        static IEnumerator Routine(ShopZone zone)
        {
            yield return new WaitForSeconds(0.5f);

            if (zone == null)
                yield break;

            if (SceneHelper.CurrentScene == "Level 0-2")
            {
                ShopTerminalCharacter.PlayScriptedLine(zone, ShopTerminalCharacter.UseTerminalClip(ShopTerminalCharacter.BootUpSpecialClip, ShopTerminalCharacter.BootUpSpecialClipGoober),
                    "Eyyy, you new around here?",
                    (2f, 1.57f, "Nice to meet ya... V1, right?"),
                    (3.25f, 2.55f, "I’ll go with that.")
                );

                yield break;
            }

            if (SceneHelper.CurrentScene == "Level 0-5")
            {
                ShopTerminalCharacter.PlayScriptedLine(zone, ShopTerminalCharacter.UseTerminalClip(ShopTerminalCharacter.BootUpSpecial05Clip, ShopTerminalCharacter.BootUpSpecial05ClipGoober),
                    "Big guys up ahead.",
                    (1.25f, 0.87f, "Try not to get squashed.")
                );

                yield break;
            }

            if (SceneHelper.CurrentScene == "Level 1-4")
            {
                ShopTerminalCharacter.PlayScriptedLine(zone, ShopTerminalCharacter.UseTerminalClip(ShopTerminalCharacter.BootUpSpecial14Clip, ShopTerminalCharacter.BootUpSpecial14ClipGoober),
                    "Ay, just a heads-up,",
                    (1.45f, 1.15f, "another machine passed through here."),
                    (2f, 1.58f, "Looked a lot like you."),
                    (1.4f, 1.11f, "Might wanna keep an eye out for ‘er.")
                );

                yield break;
            }

            if (SceneHelper.CurrentScene == "Level 2-4")
            {
                ShopTerminalCharacter.PlayScriptedLine(zone, ShopTerminalCharacter.UseTerminalClip(ShopTerminalCharacter.BootUpSpecial24Clip, ShopTerminalCharacter.BootUpSpecial24ClipGoober),
                    "Tell me when you get to the big guy, alright?",
                    (2.2f, 1.74f, "I wanna know what he looks like up close!")
                );

                yield break;
            }

            if (SceneHelper.CurrentScene == "Level 7-4")
            {
                VoiceManager.CreateVoiceSource(
                    zone,
                    "ShopTerminal",
                    ShopTerminalCharacter.UseTerminalClip(ShopTerminalCharacter.BootUpSpecial74Clip, ShopTerminalCharacter.BootUpSpecial74ClipGoober),
                    "Sum’n tells me you got this one.",
                    subtitleColor: ShopTerminalCharacter.SubtitleColor
                );

                yield break;
            }

            if (SceneHelper.CurrentScene == "Level 8-1")
            {
                ShopTerminalCharacter.PlayScriptedLine(zone, ShopTerminalCharacter.UseTerminalClip(ShopTerminalCharacter.BootUpSpecial81Clip, ShopTerminalCharacter.BootUpSpecial81ClipGoober),
                    "Y-y’know what they say, kid!",
                    (1.6f, 1.97f, "S-seein’ is believin’!")
                );

                yield break;
            }

            if (SceneHelper.CurrentScene == "Level 8-2")
            {
                ShopTerminalCharacter.PlayScriptedLine(zone, ShopTerminalCharacter.UseTerminalClip(ShopTerminalCharacter.BootUpSpecial82Clip, ShopTerminalCharacter.BootUpSpecial82ClipGoober),
                    "Watch ya back out there, pal.",
                    (2.15f, 2.11f, "A-and I mean it this time!")
                );

                yield break;
            }

            if (SceneHelper.CurrentScene == "Level 8-3")
            {
                VoiceManager.CreateVoiceSource(
                    zone,
                    "ShopTerminal",
                    ShopTerminalCharacter.UseTerminalClip(ShopTerminalCharacter.BootUpSpecial83Clip, ShopTerminalCharacter.BootUpSpecial83ClipGoober),
                    "Make it stop... Make it stop!",
                    subtitleColor: ShopTerminalCharacter.SubtitleColor
                );

                yield break;
            }

            if (SceneHelper.CurrentScene == "Level 8-4")
            {
                ShopTerminalCharacter.PlayScriptedLine(zone, ShopTerminalCharacter.UseTerminalClip(ShopTerminalCharacter.BootUpSpecial84Clip, ShopTerminalCharacter.BootUpSpecial84ClipGoober),
                    "The other guys have all gone nuts from this place.",
                    (3f, 2.02f, "I’onno why.")
                );

                yield break;
            }

            if (SceneHelper.CurrentScene == "Level P-2")
            {
                VoiceManager.CreateVoiceSource(
                    zone,
                    "ShopTerminal",
                    ShopTerminalCharacter.UseTerminalClip(ShopTerminalCharacter.BootUpSpecialP2Clip, ShopTerminalCharacter.BootUpSpecialP2ClipGoober),
                    "HAVE FUN.",
                    subtitleColor: new Color(1f, 0f, 0f)
                );

                yield break;
            }

            if (SceneHelper.CurrentScene == "Level 4-4")
            {
                ShopTerminalCharacter.PlayScriptedLine(zone, ShopTerminalCharacter.UseTerminalClip(ShopTerminalCharacter.BootUpSpecial44Clip, ShopTerminalCharacter.BootUpSpecial44ClipGoober),
                    "Guess who passed through here earlier!",
                    (2.05f, 1.76f, "No, seriously, guess!")
                );

                yield break;
            }

            bool cybergrind = SceneHelper.CurrentScene == "Endless";

            if (cybergrind && ShopTerminalCharacter.CybergrindDeath)
            {
                ShopTerminalCharacter.CybergrindDeath = false;

                VoiceManager.CreateVoiceSource(
                    zone,
                    "ShopTerminal",
                    ShopTerminalCharacter.UseTerminalClip(ShopTerminalCharacter.BootUpCybergrindRespawnClip, ShopTerminalCharacter.BootUpCybergrindRespawnClipGoober),
                    "Back for more, huh? ‘Course you are!",
                    subtitleColor: ShopTerminalCharacter.SubtitleColor
                );

                yield break;
            }

            bool cheats = ShopTerminalCharacter.LastLevelUsedCheats;
            bool prank = ShopTerminalCharacter.LastLevelWasPRank;

            AudioClip[] clips = cybergrind ? ShopTerminalCharacter.UseTerminalClips(ShopTerminalCharacter.BootUpCybergrindClips, ShopTerminalCharacter.BootUpCybergrindClipsGoober)
                : cheats ? ShopTerminalCharacter.UseTerminalClips(ShopTerminalCharacter.BootUpCheatsClips, ShopTerminalCharacter.BootUpCheatsClipsGoober)
                : prank ? ShopTerminalCharacter.UseTerminalClips(ShopTerminalCharacter.BootUpPRankClips, ShopTerminalCharacter.BootUpPRankClipsGoober)
                : ShopTerminalCharacter.UseTerminalClips(ShopTerminalCharacter.BootUpClips, ShopTerminalCharacter.BootUpClipsGoober);

            string[] subs = cybergrind ? ShopTerminalCharacter.BootUpCybergrindSubs
                : cheats ? ShopTerminalCharacter.BootUpCheatsSubs
                : prank ? ShopTerminalCharacter.BootUpPRankSubs
                : ShopTerminalCharacter.BootUpSubs;

            if (clips == null || clips.Length == 0)
                yield break;

            int i = Random.Range(0, clips.Length);

            VoiceManager.CreateVoiceSource(
                zone,
                "ShopTerminal",
                clips[i],
                i < subs.Length ? subs[i] : null,
                subtitleColor: ShopTerminalCharacter.SubtitleColor
            );
        }
    }

    [HarmonyPatch(typeof(EndlessGrid), "Start")]
    public static class EndlessGridStartPatch
    {
        public static void Postfix()
        {
            ShopTerminalCharacter.EndlessStartTime = Time.time;
        }
    }
}