using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
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
        public static AudioClip[] CrushedClips;
        public static AudioClip[] RicoshotClips;
        public static AudioClip[] LevelStartClips;
        public static AudioClip[] LevelStartBossClips;
        public static AudioClip[] DualWieldClips;
        public static AudioClip BosskillV2Clip;
        public static AudioClip BosskillEarthmoverClip;
        public static AudioClip V2EscapeClip;
        public static AudioClip V2CowardClip;

        public static AudioClip[] KillClipsCyberlord;
        public static AudioClip[] MultikillClipsCyberlord;
        public static AudioClip[] BosskillClipsCyberlord;
        public static AudioClip[] HeadshotClipsCyberlord;
        public static AudioClip[] AirshotClipsCyberlord;
        public static AudioClip[] EnvironmentalClipsCyberlord;
        public static AudioClip[] MaxStyleClipsCyberlord;
        public static AudioClip[] RankLowClipsCyberlord;
        public static AudioClip[] RankHighClipsCyberlord;
        public static AudioClip[] RankPClipsCyberlord;
        public static AudioClip[] ParryClipsCyberlord;
        public static AudioClip[] DeathClipsCyberlord;
        public static AudioClip[] EnrageClipsCyberlord;
        public static AudioClip[] NewWeaponClipsCyberlord;
        public static AudioClip[] PurchaseClipsCyberlord;
        public static AudioClip[] SkullPickupClipsCyberlord;
        public static AudioClip[] CheckpointClipsCyberlord;
        public static AudioClip[] RespawnClipsCyberlord;
        public static AudioClip[] SecretOrbClipsCyberlord;
        public static AudioClip[] CrushedClipsCyberlord;
        public static AudioClip[] RicoshotClipsCyberlord;
        public static AudioClip[] LevelStartClipsCyberlord;
        public static AudioClip[] LevelStartBossClipsCyberlord;
        public static AudioClip[] DualWieldClipsCyberlord;
        public static AudioClip BosskillV2ClipCyberlord;
        public static AudioClip BosskillEarthmoverClipCyberlord;
        public static AudioClip V2EscapeClipCyberlord;
        public static AudioClip V2CowardClipCyberlord;

        public static AudioClip[] KillClipsNoto;
        public static AudioClip[] MultikillClipsNoto;
        public static AudioClip[] BosskillClipsNoto;
        public static AudioClip[] HeadshotClipsNoto;
        public static AudioClip[] AirshotClipsNoto;
        public static AudioClip[] EnvironmentalClipsNoto;
        public static AudioClip[] MaxStyleClipsNoto;
        public static AudioClip[] RankLowClipsNoto;
        public static AudioClip[] RankHighClipsNoto;
        public static AudioClip[] RankPClipsNoto;
        public static AudioClip[] ParryClipsNoto;
        public static AudioClip[] DeathClipsNoto;
        public static AudioClip[] EnrageClipsNoto;
        public static AudioClip[] NewWeaponClipsNoto;
        public static AudioClip[] PurchaseClipsNoto;
        public static AudioClip[] SkullPickupClipsNoto;
        public static AudioClip[] CheckpointClipsNoto;
        public static AudioClip[] RespawnClipsNoto;
        public static AudioClip[] SecretOrbClipsNoto;
        public static AudioClip[] CrushedClipsNoto;
        public static AudioClip[] RicoshotClipsNoto;
        public static AudioClip[] LevelStartClipsNoto;
        public static AudioClip[] LevelStartBossClipsNoto;
        public static AudioClip[] DualWieldClipsNoto;
        public static AudioClip BosskillV2ClipNoto;
        public static AudioClip BosskillEarthmoverClipNoto;
        public static AudioClip V2EscapeClipNoto;
        public static AudioClip V2CowardClipNoto;

        public static AudioClip[] KillClipsHashCap;
        public static AudioClip[] MultikillClipsHashCap;
        public static AudioClip[] BosskillClipsHashCap;
        public static AudioClip[] HeadshotClipsHashCap;
        public static AudioClip[] AirshotClipsHashCap;
        public static AudioClip[] EnvironmentalClipsHashCap;
        public static AudioClip[] MaxStyleClipsHashCap;
        public static AudioClip[] RankLowClipsHashCap;
        public static AudioClip[] RankHighClipsHashCap;
        public static AudioClip[] RankPClipsHashCap;
        public static AudioClip[] ParryClipsHashCap;
        public static AudioClip[] DeathClipsHashCap;
        public static AudioClip[] EnrageClipsHashCap;
        public static AudioClip[] NewWeaponClipsHashCap;
        public static AudioClip[] PurchaseClipsHashCap;
        public static AudioClip[] SkullPickupClipsHashCap;
        public static AudioClip[] CheckpointClipsHashCap;
        public static AudioClip[] RespawnClipsHashCap;
        public static AudioClip[] SecretOrbClipsHashCap;
        public static AudioClip[] CrushedClipsHashCap;
        public static AudioClip[] RicoshotClipsHashCap;
        public static AudioClip[] LevelStartClipsHashCap;
        public static AudioClip[] LevelStartBossClipsHashCap;
        public static AudioClip[] DualWieldClipsHashCap;
        public static AudioClip BosskillV2ClipHashCap;
        public static AudioClip BosskillEarthmoverClipHashCap;
        public static AudioClip V2EscapeClipHashCap;
        public static AudioClip V2CowardClipHashCap;

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

        public static readonly string[] CrushedSubs =
        {
            "I’M NOT CLEANING THAT UP.",
            "THAT LOOKED LIKE IT HURT.",
            "FLATTENED. WHAT A SHAME.",
            "LOOK UP NEXT TIME."
        };

        public static readonly string[] RicoshotSubs =
        {
            "KEEP THE CHANGE.",
            "IT’S A KINDNESS.",
            "A COIN FOR YOUR TROUBLES.",
            "I DO NOT TAKE CASH."
        };

        public static readonly string[] LevelStartSubs =
        {
            "LET’S DO THIS.",
            "LET’S GET THIS DONE.",
            "THEY DON’T STAND A CHANCE.",
            "MOBILIZING.",
            "GET DEADLY.",
            "CLEAR TO ENGAGE.",
            "AND SO IT BEGINS."
        };

        public static readonly string[] DualWieldSubs =
        {
            "BRING IT ON.",
            "THE MORE THE MERRIER.",
            "HOW THE TABLES HAVE TURNED.",
            "THIS CHANGES THE ODDS."
        };

        public static readonly string[] LevelStartBossSubs =
        {
            "THEY SHALL FALL.",
            "THEY ARE ALREADY DEAD.",
            "THIS IS WHAT I WAS MADE FOR.",
            "THIS IS BUSINESS.",
            "I’LL TAKE DOWN ANYONE.",
            "MAKE IT INTERESTING.",
            "WHATEVER IT TAKES."
        };

        public static readonly Color SubtitleColor = new Color(0.290f, 0.451f, 0.784f);

        public static bool V2BossKillPlayed = false;

        public static float LastHeadshotKillTime = -999f;

        public static float LastAirshotKillTime = -999f;

        public static float LastEnvKillTime = -999f;

        public static float LastMauricedTime = -999f;

        public static float LastCrushedTime = -999f;

        public static float LastRicoshotTime = -999f;

        public static float MaxStyleLineEndTime = -999f;

        public static float V2KnockedOutTime = -999f;

        public static float EnvLineEndTime = -999f;

        public static float PurchaseLineEndTime = -999f;

        public static readonly HashSet<int> AlreadyPickedUp = new HashSet<int>();

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            KillClipsCyberlord = UltraVoicePlugin.LoadClips("assets/resources/v1/cyberlord/v1_kill{0}.wav", 9);
            MultikillClipsCyberlord = UltraVoicePlugin.LoadClips("assets/resources/v1/cyberlord/v1_multikill{0}.wav", 7);
            BosskillClipsCyberlord = UltraVoicePlugin.LoadClips("assets/resources/v1/cyberlord/v1_bosskill{0}.wav", 7);
            HeadshotClipsCyberlord = UltraVoicePlugin.LoadClips("assets/resources/v1/cyberlord/v1_headshot{0}.wav", 5);
            AirshotClipsCyberlord = UltraVoicePlugin.LoadClips("assets/resources/v1/cyberlord/v1_airshot{0}.wav", 3);
            EnvironmentalClipsCyberlord = UltraVoicePlugin.LoadClips("assets/resources/v1/cyberlord/v1_environmental{0}.wav", 6);
            MaxStyleClipsCyberlord = UltraVoicePlugin.LoadClips("assets/resources/v1/cyberlord/v1_maxstyle{0}.wav", 4);
            RankLowClipsCyberlord = UltraVoicePlugin.LoadClips("assets/resources/v1/cyberlord/v1_ranklow{0}.wav", 4);
            RankHighClipsCyberlord = UltraVoicePlugin.LoadClips("assets/resources/v1/cyberlord/v1_rankhigh{0}.wav", 4);
            RankPClipsCyberlord = UltraVoicePlugin.LoadClips("assets/resources/v1/cyberlord/v1_rankp{0}.wav", 4);
            ParryClipsCyberlord = UltraVoicePlugin.LoadClips("assets/resources/v1/cyberlord/v1_parry{0}.wav", 6);
            DeathClipsCyberlord = UltraVoicePlugin.LoadClips("assets/resources/v1/cyberlord/v1_death{0}.wav", 4);
            EnrageClipsCyberlord = UltraVoicePlugin.LoadClips("assets/resources/v1/cyberlord/v1_enrage{0}.wav", 5);
            NewWeaponClipsCyberlord = UltraVoicePlugin.LoadClips("assets/resources/v1/cyberlord/v1_newweapon{0}.wav", 6);
            PurchaseClipsCyberlord = UltraVoicePlugin.LoadClips("assets/resources/v1/cyberlord/v1_purchase{0}.wav", 3);
            SkullPickupClipsCyberlord = UltraVoicePlugin.LoadClips("assets/resources/v1/cyberlord/v1_skullpickup{0}.wav", 5);
            CheckpointClipsCyberlord = UltraVoicePlugin.LoadClips("assets/resources/v1/cyberlord/v1_checkpoint{0}.wav", 5);
            RespawnClipsCyberlord = UltraVoicePlugin.LoadClips("assets/resources/v1/cyberlord/v1_respawn{0}.wav", 5);
            SecretOrbClipsCyberlord = UltraVoicePlugin.LoadClips("assets/resources/v1/cyberlord/v1_secretorb{0}.wav", 5);
            CrushedClipsCyberlord = UltraVoicePlugin.LoadClips("assets/resources/v1/cyberlord/v1_crushed{0}.wav", 4);
            RicoshotClipsCyberlord = UltraVoicePlugin.LoadClips("assets/resources/v1/cyberlord/v1_ricoshot{0}.wav", 4);
            LevelStartClipsCyberlord = UltraVoicePlugin.LoadClips("assets/resources/v1/cyberlord/v1_levelstartnormal{0}.wav", 7);
            LevelStartBossClipsCyberlord = UltraVoicePlugin.LoadClips("assets/resources/v1/cyberlord/v1_levelstartboss{0}.wav", 7);
            DualWieldClipsCyberlord = UltraVoicePlugin.LoadClips("assets/resources/v1/cyberlord/v1_dualwield{0}.wav", 4);
            BosskillV2ClipCyberlord = UltraVoicePlugin.LoadClip("assets/resources/v1/cyberlord/v1_bosskillv2.wav");
            BosskillEarthmoverClipCyberlord = UltraVoicePlugin.LoadClip("assets/resources/v1/cyberlord/v1_bosskillearthmover.wav");
            V2EscapeClipCyberlord = UltraVoicePlugin.LoadClip("assets/resources/v1/cyberlord/v1_v2escape.wav");
            V2CowardClipCyberlord = UltraVoicePlugin.LoadClip("assets/resources/v1/cyberlord/v1_v2coward.wav");

            KillClipsNoto = UltraVoicePlugin.LoadClips("assets/resources/v1/noto/v1_kill{0}.wav", 9);
            MultikillClipsNoto = UltraVoicePlugin.LoadClips("assets/resources/v1/noto/v1_multikill{0}.wav", 7);
            BosskillClipsNoto = UltraVoicePlugin.LoadClips("assets/resources/v1/noto/v1_bosskill{0}.wav", 7);
            HeadshotClipsNoto = UltraVoicePlugin.LoadClips("assets/resources/v1/noto/v1_headshot{0}.wav", 5);
            AirshotClipsNoto = UltraVoicePlugin.LoadClips("assets/resources/v1/noto/v1_airshot{0}.wav", 3);
            EnvironmentalClipsNoto = UltraVoicePlugin.LoadClips("assets/resources/v1/noto/v1_environmental{0}.wav", 6);
            MaxStyleClipsNoto = UltraVoicePlugin.LoadClips("assets/resources/v1/noto/v1_maxstyle{0}.wav", 4);
            RankLowClipsNoto = UltraVoicePlugin.LoadClips("assets/resources/v1/noto/v1_ranklow{0}.wav", 4);
            RankHighClipsNoto = UltraVoicePlugin.LoadClips("assets/resources/v1/noto/v1_rankhigh{0}.wav", 4);
            RankPClipsNoto = UltraVoicePlugin.LoadClips("assets/resources/v1/noto/v1_rankp{0}.wav", 4);
            ParryClipsNoto = UltraVoicePlugin.LoadClips("assets/resources/v1/noto/v1_parry{0}.wav", 6);
            DeathClipsNoto = UltraVoicePlugin.LoadClips("assets/resources/v1/noto/v1_death{0}.wav", 4);
            EnrageClipsNoto = UltraVoicePlugin.LoadClips("assets/resources/v1/noto/v1_enrage{0}.wav", 5);
            NewWeaponClipsNoto = UltraVoicePlugin.LoadClips("assets/resources/v1/noto/v1_newweapon{0}.wav", 6);
            PurchaseClipsNoto = UltraVoicePlugin.LoadClips("assets/resources/v1/noto/v1_purchase{0}.wav", 3);
            SkullPickupClipsNoto = UltraVoicePlugin.LoadClips("assets/resources/v1/noto/v1_skullpickup{0}.wav", 5);
            CheckpointClipsNoto = UltraVoicePlugin.LoadClips("assets/resources/v1/noto/v1_checkpoint{0}.wav", 5);
            RespawnClipsNoto = UltraVoicePlugin.LoadClips("assets/resources/v1/noto/v1_respawn{0}.wav", 5);
            SecretOrbClipsNoto = UltraVoicePlugin.LoadClips("assets/resources/v1/noto/v1_secretorb{0}.wav", 5);
            CrushedClipsNoto = UltraVoicePlugin.LoadClips("assets/resources/v1/noto/v1_crushed{0}.wav", 4);
            RicoshotClipsNoto = UltraVoicePlugin.LoadClips("assets/resources/v1/noto/v1_ricoshot{0}.wav", 4);
            LevelStartClipsNoto = UltraVoicePlugin.LoadClips("assets/resources/v1/noto/v1_levelstartnormal{0}.wav", 7);
            LevelStartBossClipsNoto = UltraVoicePlugin.LoadClips("assets/resources/v1/noto/v1_levelstartboss{0}.wav", 7);
            DualWieldClipsNoto = UltraVoicePlugin.LoadClips("assets/resources/v1/noto/v1_dualwield{0}.wav", 4);
            BosskillV2ClipNoto = UltraVoicePlugin.LoadClip("assets/resources/v1/noto/v1_bosskillv2.wav");
            BosskillEarthmoverClipNoto = UltraVoicePlugin.LoadClip("assets/resources/v1/noto/v1_bosskillearthmover.wav");
            V2EscapeClipNoto = UltraVoicePlugin.LoadClip("assets/resources/v1/noto/v1_v2escape.wav");
            V2CowardClipNoto = UltraVoicePlugin.LoadClip("assets/resources/v1/noto/v1_v2coward.wav");

            KillClipsHashCap = UltraVoicePlugin.LoadClips("assets/resources/v1/hashcap/v1_kill{0}.wav", 9);
            MultikillClipsHashCap = UltraVoicePlugin.LoadClips("assets/resources/v1/hashcap/v1_multikill{0}.wav", 7);
            BosskillClipsHashCap = UltraVoicePlugin.LoadClips("assets/resources/v1/hashcap/v1_bosskill{0}.wav", 7);
            HeadshotClipsHashCap = UltraVoicePlugin.LoadClips("assets/resources/v1/hashcap/v1_headshot{0}.wav", 5);
            AirshotClipsHashCap = UltraVoicePlugin.LoadClips("assets/resources/v1/hashcap/v1_airshot{0}.wav", 3);
            EnvironmentalClipsHashCap = UltraVoicePlugin.LoadClips("assets/resources/v1/hashcap/v1_environmental{0}.wav", 6);
            MaxStyleClipsHashCap = UltraVoicePlugin.LoadClips("assets/resources/v1/hashcap/v1_maxstyle{0}.wav", 4);
            RankLowClipsHashCap = UltraVoicePlugin.LoadClips("assets/resources/v1/hashcap/v1_ranklow{0}.wav", 4);
            RankHighClipsHashCap = UltraVoicePlugin.LoadClips("assets/resources/v1/hashcap/v1_rankhigh{0}.wav", 4);
            RankPClipsHashCap = UltraVoicePlugin.LoadClips("assets/resources/v1/hashcap/v1_rankp{0}.wav", 4);
            ParryClipsHashCap = UltraVoicePlugin.LoadClips("assets/resources/v1/hashcap/v1_parry{0}.wav", 6);
            DeathClipsHashCap = UltraVoicePlugin.LoadClips("assets/resources/v1/hashcap/v1_death{0}.wav", 4);
            EnrageClipsHashCap = UltraVoicePlugin.LoadClips("assets/resources/v1/hashcap/v1_enrage{0}.wav", 5);
            NewWeaponClipsHashCap = UltraVoicePlugin.LoadClips("assets/resources/v1/hashcap/v1_newweapon{0}.wav", 6);
            PurchaseClipsHashCap = UltraVoicePlugin.LoadClips("assets/resources/v1/hashcap/v1_purchase{0}.wav", 3);
            SkullPickupClipsHashCap = UltraVoicePlugin.LoadClips("assets/resources/v1/hashcap/v1_skullpickup{0}.wav", 5);
            CheckpointClipsHashCap = UltraVoicePlugin.LoadClips("assets/resources/v1/hashcap/v1_checkpoint{0}.wav", 5);
            RespawnClipsHashCap = UltraVoicePlugin.LoadClips("assets/resources/v1/hashcap/v1_respawn{0}.wav", 5);
            SecretOrbClipsHashCap = UltraVoicePlugin.LoadClips("assets/resources/v1/hashcap/v1_secretorb{0}.wav", 5);
            CrushedClipsHashCap = UltraVoicePlugin.LoadClips("assets/resources/v1/hashcap/v1_crushed{0}.wav", 4);
            RicoshotClipsHashCap = UltraVoicePlugin.LoadClips("assets/resources/v1/hashcap/v1_ricoshot{0}.wav", 4);
            LevelStartClipsHashCap = UltraVoicePlugin.LoadClips("assets/resources/v1/hashcap/v1_levelstartnormal{0}.wav", 7);
            LevelStartBossClipsHashCap = UltraVoicePlugin.LoadClips("assets/resources/v1/hashcap/v1_levelstartboss{0}.wav", 7);
            DualWieldClipsHashCap = UltraVoicePlugin.LoadClips("assets/resources/v1/hashcap/v1_dualwield{0}.wav", 4);
            BosskillV2ClipHashCap = UltraVoicePlugin.LoadClip("assets/resources/v1/hashcap/v1_bosskillv2.wav");
            BosskillEarthmoverClipHashCap = UltraVoicePlugin.LoadClip("assets/resources/v1/hashcap/v1_bosskillearthmover.wav");
            V2EscapeClipHashCap = UltraVoicePlugin.LoadClip("assets/resources/v1/hashcap/v1_v2escape.wav");
            V2CowardClipHashCap = UltraVoicePlugin.LoadClip("assets/resources/v1/hashcap/v1_v2coward.wav");

            ApplyVoiceActor();

            logger.LogInfo("V1 voice lines loaded successfully!");
        }

        public static void ApplyVoiceActor()
        {
            UltraVoicePlugin.V1VoiceActor actor = UltraVoicePlugin.V1VoiceActorField != null
                ? UltraVoicePlugin.V1VoiceActorField.Value
                : UltraVoicePlugin.V1VoiceActor.HashCap;

            bool noto = actor == UltraVoicePlugin.V1VoiceActor.Noto;
            bool hashcap = actor == UltraVoicePlugin.V1VoiceActor.HashCap;

            KillClips = hashcap ? KillClipsHashCap : noto ? KillClipsNoto : KillClipsCyberlord;
            MultikillClips = hashcap ? MultikillClipsHashCap : noto ? MultikillClipsNoto : MultikillClipsCyberlord;
            BosskillClips = hashcap ? BosskillClipsHashCap : noto ? BosskillClipsNoto : BosskillClipsCyberlord;
            HeadshotClips = hashcap ? HeadshotClipsHashCap : noto ? HeadshotClipsNoto : HeadshotClipsCyberlord;
            AirshotClips = hashcap ? AirshotClipsHashCap : noto ? AirshotClipsNoto : AirshotClipsCyberlord;
            EnvironmentalClips = hashcap ? EnvironmentalClipsHashCap : noto ? EnvironmentalClipsNoto : EnvironmentalClipsCyberlord;
            MaxStyleClips = hashcap ? MaxStyleClipsHashCap : noto ? MaxStyleClipsNoto : MaxStyleClipsCyberlord;
            RankLowClips = hashcap ? RankLowClipsHashCap : noto ? RankLowClipsNoto : RankLowClipsCyberlord;
            RankHighClips = hashcap ? RankHighClipsHashCap : noto ? RankHighClipsNoto : RankHighClipsCyberlord;
            RankPClips = hashcap ? RankPClipsHashCap : noto ? RankPClipsNoto : RankPClipsCyberlord;
            ParryClips = hashcap ? ParryClipsHashCap : noto ? ParryClipsNoto : ParryClipsCyberlord;
            DeathClips = hashcap ? DeathClipsHashCap : noto ? DeathClipsNoto : DeathClipsCyberlord;
            EnrageClips = hashcap ? EnrageClipsHashCap : noto ? EnrageClipsNoto : EnrageClipsCyberlord;
            NewWeaponClips = hashcap ? NewWeaponClipsHashCap : noto ? NewWeaponClipsNoto : NewWeaponClipsCyberlord;
            PurchaseClips = hashcap ? PurchaseClipsHashCap : noto ? PurchaseClipsNoto : PurchaseClipsCyberlord;
            SkullPickupClips = hashcap ? SkullPickupClipsHashCap : noto ? SkullPickupClipsNoto : SkullPickupClipsCyberlord;
            CheckpointClips = hashcap ? CheckpointClipsHashCap : noto ? CheckpointClipsNoto : CheckpointClipsCyberlord;
            RespawnClips = hashcap ? RespawnClipsHashCap : noto ? RespawnClipsNoto : RespawnClipsCyberlord;
            SecretOrbClips = hashcap ? SecretOrbClipsHashCap : noto ? SecretOrbClipsNoto : SecretOrbClipsCyberlord;
            CrushedClips = hashcap ? CrushedClipsHashCap : noto ? CrushedClipsNoto : CrushedClipsCyberlord;
            RicoshotClips = hashcap ? RicoshotClipsHashCap : noto ? RicoshotClipsNoto : RicoshotClipsCyberlord;
            LevelStartClips = hashcap ? LevelStartClipsHashCap : noto ? LevelStartClipsNoto : LevelStartClipsCyberlord;
            LevelStartBossClips = hashcap ? LevelStartBossClipsHashCap : noto ? LevelStartBossClipsNoto : LevelStartBossClipsCyberlord;
            DualWieldClips = hashcap ? DualWieldClipsHashCap : noto ? DualWieldClipsNoto : DualWieldClipsCyberlord;
            BosskillV2Clip = hashcap ? BosskillV2ClipHashCap : noto ? BosskillV2ClipNoto : BosskillV2ClipCyberlord;
            BosskillEarthmoverClip = hashcap ? BosskillEarthmoverClipHashCap : noto ? BosskillEarthmoverClipNoto : BosskillEarthmoverClipCyberlord;
            V2EscapeClip = hashcap ? V2EscapeClipHashCap : noto ? V2EscapeClipNoto : V2EscapeClipCyberlord;
            V2CowardClip = hashcap ? V2CowardClipHashCap : noto ? V2CowardClipNoto : V2CowardClipCyberlord;
        }

        public static void PlayBosskillLine()
        {
            if (!UltraVoicePlugin.V1LineEnabled("bosskill"))
                return;

            UltraVoicePlugin.Instance.StartCoroutine(BosskillRoutine());
        }

        static IEnumerator BosskillRoutine()
        {
            NewMovement player = MonoSingleton<NewMovement>.Instance;

            if (!VoiceManager.CheckCooldown(player, 4f))
                yield break;

            yield return new WaitForSeconds(1f);

            while (Time.time < MaxStyleLineEndTime)
                yield return null;

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
            if (Random.Range(0f, 1f) >= 0.5f)
                yield break;

            if (!VoiceManager.CheckCooldown(player, 4f))
                yield break;

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

            bool crushed = Time.time - LastMauricedTime < 0.3f ||
                (SceneHelper.CurrentScene != "Level 5-2" && Time.time - LastCrushedTime < 0.3f);
            bool ricoshot = Time.time - LastRicoshotTime < 0.3f;
            bool airshot = Time.time - LastAirshotKillTime < 0.5f;
            bool headshot = Time.time - LastHeadshotKillTime < 0.3f;

            string category = crushed ? "crushed" : env ? "environmental" : ricoshot ? "ricoshot" : airshot ? "airshot" : headshot ? "headshot" : "kill";

            if (!UltraVoicePlugin.V1LineEnabled(category))
                yield break;

            AudioClip[] clips = crushed ? CrushedClips : env ? EnvironmentalClips : ricoshot ? RicoshotClips : airshot ? AirshotClips : headshot ? HeadshotClips : KillClips;
            string[] subs = crushed ? CrushedSubs : env ? EnvironmentalSubs : ricoshot ? RicoshotSubs : airshot ? AirshotSubs : headshot ? HeadshotSubs : KillSubs;

            if (clips == null || clips.Length == 0)
                yield break;

            int index = Random.Range(0, clips.Length);

            AudioSource src = VoiceManager.CreateVoiceSource(player, "V1", clips[index],
                index < subs.Length ? subs[index] : null,
                false,
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
            if (!UltraVoicePlugin.V1VoiceEnabled.Value)
                return;

            if (__state || __instance.puppet || !__instance.dead)
                return;

            if (__instance.hitter == "enemy")
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
            if (UltraVoicePlugin.V2VoiceEnabled.Value && v2DeathClip != null)
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
            if (__instance.totalRank == null || __state || !__instance.totalRank.gameObject.activeSelf)
                return;

            string rank = StripTags(__instance.totalRank.text);

            ShopTerminalCharacter.LastLevelWasPRank = rank == "P";
            ShopTerminalCharacter.LastLevelUsedCheats = rank == "_" || rank == "-";

            if (!UltraVoicePlugin.V1LineEnabled("rank"))
                return;

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

    [HarmonyPatch(typeof(V2), "KnockedOut")]
    class V1V2CowardPatch
    {
        static void Prefix(V2 __instance)
        {
            if (__instance == null || __instance.secondEncounter)
                return;

            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != V2Character.LimboClimaxSceneName)
                return;

            V1Character.V2KnockedOutTime = Time.time;
        }
    }

    [HarmonyPatch(typeof(WeaponPickUp), "Awake")]
    class V1V2ArmDropPatch
    {
        static void Postfix(WeaponPickUp __instance)
        {
            if (__instance == null || !__instance.arm)
                return;

            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != V2Character.LimboClimaxSceneName)
                return;

            if (Time.time - V1Character.V2KnockedOutTime > 30f)
                return;

            if (!UltraVoicePlugin.VoicesEnabled || UltraVoicePlugin.V1VoiceEnabled == null || !UltraVoicePlugin.V1VoiceEnabled.Value)
                return;

            if (V1Character.V2CowardClip == null)
                return;

            NewMovement player = MonoSingleton<NewMovement>.Instance;

            if (player == null)
                return;

            VoiceManager.PlayRandomVoiceDelayed(0.5f, player, "V1",
                new[] { V1Character.V2CowardClip },
                new[] { "HMH. COWARD." },
                interrupt: true,
                colorOverride: V1Character.SubtitleColor,
                spatialBlend: 0f
            );
        }
    }

    [HarmonyPatch(typeof(HudMessage), nameof(HudMessage.PlayMessage))]
    class V1V2EscapePatch
    {
        static bool Prefix(HudMessage __instance)
        {
            if (SceneHelper.CurrentScene != "Level 4-4")
                return true;

            if (__instance == null || __instance.message == null || !__instance.message.ToLowerInvariant().Contains("not getting away"))
                return true;

            if (!UltraVoicePlugin.VoicesEnabled || UltraVoicePlugin.V1VoiceEnabled == null || !UltraVoicePlugin.V1VoiceEnabled.Value)
                return true;

            if (V1Character.V2EscapeClip == null)
                return true;

            NewMovement player = MonoSingleton<NewMovement>.Instance;

            if (player == null)
                return true;

            Object.Destroy(__instance);

            VoiceManager.CreateVoiceSource(player, "V1",
                V1Character.V2EscapeClip,
                "YOU'RE NOT GETTING AWAY THIS TIME.",
                true,
                V1Character.SubtitleColor,
                spatialBlend: 0f
            );

            return false;
        }
    }

    [HarmonyPatch(typeof(StatsManager), nameof(StatsManager.StartTimer))]
    class V1LevelStartPatch
    {
        static readonly HashSet<string> BossLevels = new HashSet<string>
        {
            "Level 0-5",
            "Level 1-4",
            "Level 2-4",
            "Level 3-2",
            "Level 4-4",
            "Level 5-4",
            "Level 6-2",
            "Level 7-4",
            "Level 8-4",
            "Level P-1",
            "Level P-2"
        };

        static void Prefix(StatsManager __instance, out bool __state)
        {
            __state = __instance != null && __instance.timerOnOnce;
        }

        static void Postfix(StatsManager __instance, bool __state)
        {
            if (__instance == null || __state || !__instance.timerOnOnce)
                return;

            string scene = SceneHelper.CurrentScene;

            if (scene == "Endless" || scene == "uk_construct" || (scene != null && scene.EndsWith("-S")))
                return;

            if (!UltraVoicePlugin.V1LineEnabled("levelstart"))
                return;

            NewMovement player = MonoSingleton<NewMovement>.Instance;

            if (player == null)
                return;

            bool boss = scene != null && BossLevels.Contains(scene);

            VoiceManager.PlayRandomVoice(player, "V1",
                boss ? V1Character.LevelStartBossClips : V1Character.LevelStartClips,
                boss ? V1Character.LevelStartBossSubs : V1Character.LevelStartSubs,
                colorOverride: V1Character.SubtitleColor,
                spatialBlend: 0f
            );
        }
    }

    [HarmonyPatch(typeof(CheckPoint), nameof(CheckPoint.ActivateCheckPoint))]
    class V1CheckpointPatch
    {
        static void Postfix()
        {
            if (!UltraVoicePlugin.V1LineEnabled("checkpoint"))
                return;

            if (SceneHelper.CurrentScene == "uk_construct")
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

            if (V1Character.PurchaseClips == null || V1Character.PurchaseClips.Length == 0)
                return;

            int i = Random.Range(0, V1Character.PurchaseClips.Length);
            AudioClip clip = V1Character.PurchaseClips[i];

            if (clip == null)
                return;

            AudioSource src = VoiceManager.CreateVoiceSource(player, "V1", clip,
                i < V1Character.PurchaseSubs.Length ? V1Character.PurchaseSubs[i] : null,
                false,
                V1Character.SubtitleColor,
                spatialBlend: 0f
            );

            if (src != null)
                V1Character.PurchaseLineEndTime = Time.time + clip.length;
        }
    }

    [HarmonyPatch(typeof(DualWieldPickup), "PickedUp")]
    class V1DualWieldPatch
    {
        static void Postfix()
        {
            if (!UltraVoicePlugin.V1LineEnabled("dualwield"))
                return;

            if (Time.time < V1Character.MaxStyleLineEndTime)
                return;

            NewMovement player = MonoSingleton<NewMovement>.Instance;

            if (player == null)
                return;

            VoiceManager.PlayRandomVoice(player, "V1", V1Character.DualWieldClips, V1Character.DualWieldSubs,
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

            if (!VoiceManager.CheckCooldown(__instance, 10f))
                return;

            if (__instance == null || __instance.ranks == null)
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

            if (pointID == "ultrakill.splattered" || pointID == "ultrakill.compressed")
                V1Character.LastEnvKillTime = Time.time;

            if (pointID == "ultrakill.mauriced")
                V1Character.LastMauricedTime = Time.time;

            if (pointID == "ultrakill.ricoshot")
                V1Character.LastRicoshotTime = Time.time;

            if (pointID == "ultrakill.parry" || pointID == "ultrakill.chargeback")
            {
                if (pointID == "ultrakill.chargeback" && eid != null && eid.enemyType == EnemyType.Gutterman)
                    return;

                if (!UltraVoicePlugin.V1LineEnabled("parry"))
                    return;

                NewMovement player = MonoSingleton<NewMovement>.Instance;

                if (player == null)
                    return;

                if (!VoiceManager.CheckCooldown(player, 5))
                    return;

                if (Time.time < V1Character.MaxStyleLineEndTime)
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

                if (styleName.Contains("CRUSHED"))
                    V1Character.LastCrushedTime = Time.time;

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

            if (__instance.multikillCount < 4)
                return;

            if (Time.time - V1Character.LastEnvKillTime < 0.3f)
                return;

            if (Time.time < V1Character.MaxStyleLineEndTime)
                return;

            NewMovement player = MonoSingleton<NewMovement>.Instance;

            if (player == null)
                return;

            VoiceManager.PlayRandomVoice(player, "V1", V1Character.MultikillClips, V1Character.MultikillSubs,
                interrupt: false,
                colorOverride: V1Character.SubtitleColor,
                spatialBlend: 0f
            );
        }
    }
}
