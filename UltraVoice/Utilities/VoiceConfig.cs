using System;
using System.Collections.Generic;
using ThornClient.Core;
using ThornClient.Core.ConfigurableElements;
using UltraVoice.Characters;
using UnityEngine;

namespace UltraVoice.Utilities
{
    public class VoiceConfig : Module
    {
        static Sprite icon;

        public override Sprite Icon => icon != null ? icon : icon = UpdateChecker.LoadIcon("UltraVoice.Images.thorn_icon.png");

        public VoiceConfig() : base(
            UltraVoicePlugin.PluginGuid,
            UltraVoicePlugin.PluginName,
            "Voice lines for ULTRAKILL enemies",
            ModuleCategory.Misc,
            KeyCode.None,
            KeyCode.None,
            false,
            false)
        {
            UltraVoicePlugin.TogglesPanel = CreateGroup("toggles", "Enemy Line Toggles", "", null);
            UltraVoicePlugin.ActorPanel = CreateGroup("actors", "Voice Actors", "", null);
            UltraVoicePlugin.VolumePanel = CreateGroup("volumes", "Volume Levels", "", null);
            UltraVoicePlugin.SubtitleColorsPanel = CreateGroup("subtitlecolors", "Subtitle Colors", "", null);

            UltraVoicePlugin.VoiceCooldown = CreateSetting("cooldown", "Voice Cooldown", "", 0.25f, null);
            UltraVoicePlugin.VoiceCooldown.Hints = new InterfaceHints
            {
                Range = new Tuple<float, float>(0f, 0.5f),
                Decimals = 2
            };

            UltraVoicePlugin.SubtitleLimit = CreateSetting("subtitlelimit", "Max Allowed Subtitles", "", 5, null);
            UltraVoicePlugin.SubtitleLimit.Hints = new InterfaceHints
            {
                Range = new Tuple<float, float>(1f, 10f)
            };

            BuildToggles();
            BuildSubtitleColors();
            BuildVoiceActors();
            BuildVolumes();
        }

        void BuildToggles()
        {
            SettingGroup toggles = UltraVoicePlugin.TogglesPanel;

            UltraVoicePlugin.MasterVoiceEnabled = CreateSetting("mastervoice", "Enable Voice Lines", "", true, toggles);

            SettingGroup enemies = CreateGroup("enemytoggles", "Enemy Toggles", "", toggles);
            UltraVoicePlugin.EnemyTogglesPanel = enemies;

            UltraVoicePlugin.CerberusVoiceEnabled = CreateSetting("cerberusvoice", "Enable Cerberus Voice Lines", "", true, enemies);
            UltraVoicePlugin.EarthmoverVoiceEnabled = CreateSetting("earthmovervoice", "Enable Earthmover Voice Lines", "", true, enemies);
            UltraVoicePlugin.FerrymanVoiceEnabled = CreateSetting("ferrymanvoice", "Enable Ferryman Voice Lines", "", true, enemies);
            UltraVoicePlugin.FilthVoiceEnabled = CreateSetting("filthvoice", "Enable Filth Voice Lines", "", false, enemies);
            UltraVoicePlugin.GeryonVoiceEnabled = CreateSetting("geryonvoice", "Enable Geryon Voice Lines", "", true, enemies);
            UltraVoicePlugin.GuttermanVoiceEnabled = CreateSetting("guttermanvoice", "Enable Gutterman Voice Lines", "", true, enemies);
            UltraVoicePlugin.GuttertankVoiceEnabled = CreateSetting("guttertankvoice", "Enable Guttertank Voice Lines", "", true, enemies);
            UltraVoicePlugin.MassVoiceEnabled = CreateSetting("massvoice", "Enable Hideous Mass Voice Lines", "", true, enemies);
            UltraVoicePlugin.IdolVoiceEnabled = CreateSetting("idolvoice", "Enable Idol Voice Lines", "", true, enemies);
            UltraVoicePlugin.InsurrectionistVoiceEnabled = CreateSetting("Insurrectionist", "Enable Insurrectionist Voice Lines", "", true, enemies);
            UltraVoicePlugin.LeviathanVoiceEnabled = CreateSetting("levivoice", "Enable Leviathan Voice Lines", "", true, enemies);
            UltraVoicePlugin.MauriceVoiceEnabled = CreateSetting("mauricevoice", "Enable Malicious Face Voice Lines", "", true, enemies);
            UltraVoicePlugin.MannequinVoiceEnabled = CreateSetting("mannequinvoice", "Enable Mannequin Voice Lines", "", false, enemies);
            UltraVoicePlugin.MindflayerVoiceEnabled = CreateSetting("mindflayervoice", "Enable Mindflayer Voice Lines", "", true, enemies);
            UltraVoicePlugin.MirrorReaperVoiceEnabled = CreateSetting("mirrorreapervoice", "Enable Mirror Reaper Voice Lines", "", true, enemies);
            UltraVoicePlugin.PowerAltVoiceEnabled = CreateSetting("poweraltvoice", "Enable Power Alt Voice Lines", "", true, enemies);
            UltraVoicePlugin.ProvidenceVoiceEnabled = CreateSetting("providencevoice", "Enable Providence Voice Lines", "", true, enemies);
            UltraVoicePlugin.SchismVoiceEnabled = CreateSetting("schismvoice", "Enable Schism Voice Lines", "", false, enemies);
            UltraVoicePlugin.SentryVoiceEnabled = CreateSetting("sentryvoice", "Enable Sentry Voice Lines", "", true, enemies);
            UltraVoicePlugin.ShopTerminalVoiceEnabled = CreateSetting("shopterminalvoice", "Enable Shop Terminal Voice Lines", "", true, enemies);
            UltraVoicePlugin.SoldierVoiceEnabled = CreateSetting("soldiervoice", "Enable Soldier Voice Lines", "", false, enemies);
            UltraVoicePlugin.StalkerVoiceEnabled = CreateSetting("stalkervoice", "Enable Stalker Voice Lines", "", false, enemies);
            UltraVoicePlugin.StrayVoiceEnabled = CreateSetting("strayvoice", "Enable Stray Voice Lines", "", false, enemies);
            UltraVoicePlugin.StreetcleanerVoiceEnabled = CreateSetting("streetcleanervoice", "Enable Streetcleaner Voice Lines", "", true, enemies);
            UltraVoicePlugin.SwordsmachineVoiceEnabled = CreateSetting("swordsmachinevoice", "Enable Swordsmachine Voice Lines", "", true, enemies);
            UltraVoicePlugin.V1VoiceEnabled = CreateSetting("v1voice", "Enable V1 Voice Lines", "", true, enemies);

            SettingGroup v1Lines = CreateGroup("v1lines", "V1 Line Categories", "", enemies);
            UltraVoicePlugin.V1LinesPanel = v1Lines;

            (string key, string name)[] v1Categories =
            {
                ("kill", "Kill Lines"),
                ("multikill", "Multikill Lines"),
                ("bosskill", "Boss Kill Lines"),
                ("headshot", "Headshot Lines"),
                ("airshot", "Airshot Lines"),
                ("environmental", "Environmental Kill Lines"),
                ("crushed", "Crushed Lines"),
                ("ricoshot", "Ricoshot Kill Lines"),
                ("levelstart", "Level Start Lines"),
                ("maxstyle", "Max Style Lines"),
                ("rank", "Level Rank Lines"),
                ("parry", "Parry Lines"),
                ("enrage", "Enrage Lines"),
                ("death", "Death Lines"),
                ("pickup", "Weapon Pickup Lines"),
                ("dualwield", "Dual Wield Lines"),
                ("purchase", "Purchase Lines"),
                ("skull", "Skull Pickup Lines"),
                ("checkpoint", "Checkpoint Lines"),
                ("respawn", "Respawn Lines"),
                ("secret", "Secret Orb Lines")
            };

            foreach (var (key, name) in v1Categories)
                UltraVoicePlugin.V1LineToggles[key] = CreateSetting($"v1line_{key}", name, "", true, v1Lines);

            UltraVoicePlugin.V2VoiceEnabled = CreateSetting("v2voice", "Enable V2 Voice Lines", "", true, enemies);
            UltraVoicePlugin.VirtueVoiceEnabled = CreateSetting("virtuevoice", "Enable Virtue Voice Lines", "", true, enemies);
            UltraVoicePlugin.WickedVoiceEnabled = CreateSetting("wickedvoice", "Enable Wicked Voice Lines", "", true, enemies);
        }

        void BuildSubtitleColors()
        {
            SettingGroup colors = UltraVoicePlugin.SubtitleColorsPanel;

            UltraVoicePlugin.SubtitleColorsEnabled = CreateSetting("subtitlecolorsenabled", "Enable Subtitle Colors", "", true, colors);

            SettingGroup perCharacter = CreateGroup("charsubtitlecolors", "Character Subtitle Colors", "", colors);
            UltraVoicePlugin.CharacterSubtitleColorsPanel = perCharacter;

            (string key, string label)[] subtitleColorCharacters =
            {
                ("Cerberus", "Cerberus"),
                ("Ferryman", "Ferryman"),
                ("Geryon", "Geryon"),
                ("Gutterman", "Gutterman"),
                ("Guttertank", "Guttertank"),
                ("HideousMass", "Hideous Mass"),
                ("Idol", "Idol"),
                ("Insurrectionist", "Insurrectionist"),
                ("MaliciousFace", "Malicious Face"),
                ("Mannequin", "Mannequin"),
                ("Mindflayer", "Mindflayer"),
                ("MirrorReaper", "Mirror Reaper"),
                ("Power", "Power"),
                ("Providence", "Providence"),
                ("Sentry", "Sentry"),
                ("ShopTerminal", "Shop Terminal"),
                ("Streetcleaner", "Streetcleaner"),
                ("Swordsmachine", "Swordsmachine"),
                ("V1", "V1"),
                ("V2", "V2"),
                ("Virtue", "Virtue"),
                ("Wicked", "Wicked")
            };

            foreach (var (key, label) in subtitleColorCharacters)
                UltraVoicePlugin.SubtitleColorToggles[key] = CreateSetting($"subcolor_{key.ToLowerInvariant()}", label, "", true, perCharacter);
        }

        void BuildVoiceActors()
        {
            SettingGroup actors = UltraVoicePlugin.ActorPanel;

            UltraVoicePlugin.CerberusVoiceActorField = CreateActorSetting("cerbvoiceactor", "Cerberus Voice Actor", UltraVoicePlugin.CerberusVoiceActor.Mel, actors,
                (UltraVoicePlugin.CerberusVoiceActor.Mel, "Mel"),
                (UltraVoicePlugin.CerberusVoiceActor.Soil, "Buried Soil"),
                (UltraVoicePlugin.CerberusVoiceActor.Rotund, "Rotund"));

            UltraVoicePlugin.EarthmoverVoiceActorField = CreateActorSetting("earthmovervoiceactor", "Earthmover Voice Actor", UltraVoicePlugin.EarthmoverVoiceActor.Rotund, actors,
                (UltraVoicePlugin.EarthmoverVoiceActor.Rotund, "Rotund"),
                (UltraVoicePlugin.EarthmoverVoiceActor.Tondar, "TondarYZD"),
                (UltraVoicePlugin.EarthmoverVoiceActor.Megaslash, "Megaslash"));

            UltraVoicePlugin.FerrymanVoiceActorField = CreateActorSetting("ferryvoiceactor", "Ferryman Voice Actor", UltraVoicePlugin.FerrymanVoiceActor.Mel, actors,
                (UltraVoicePlugin.FerrymanVoiceActor.Mel, "Mel"),
                (UltraVoicePlugin.FerrymanVoiceActor.Soil, "Buried Soil"),
                (UltraVoicePlugin.FerrymanVoiceActor.Nicky, "Nicky"));

            UltraVoicePlugin.GeryonVoiceActorField = CreateActorSetting("geryonvoiceactor", "Geryon Voice Actor", UltraVoicePlugin.GeryonVoiceActor.TheRatmanValinMegaslash, actors,
                (UltraVoicePlugin.GeryonVoiceActor.TheRatmanValinMegaslash, "TheRatman, Valin, Megalash"),
                (UltraVoicePlugin.GeryonVoiceActor.Mel, "Mel"));

            UltraVoicePlugin.GuttermanVoiceActorField = CreateActorSetting("gmvoiceactor", "Gutterman Voice Actor", UltraVoicePlugin.GuttermanVoiceActor.Mel, actors,
                (UltraVoicePlugin.GuttermanVoiceActor.Mel, "Mel"),
                (UltraVoicePlugin.GuttermanVoiceActor.Lemen, "Lemen"));

            UltraVoicePlugin.GuttertankVoiceActorField = CreateActorSetting("gtvoiceactor", "Guttertank Voice Actor", UltraVoicePlugin.GuttertankVoiceActor.Mel, actors,
                (UltraVoicePlugin.GuttertankVoiceActor.Mel, "Mel"),
                (UltraVoicePlugin.GuttertankVoiceActor.Virchew, "Virchew"));

            UltraVoicePlugin.IdolVoiceActorField = CreateActorSetting("idolvoiceactor", "Idol Voice Actor", UltraVoicePlugin.IdolVoiceActor.Voinvi, actors,
                (UltraVoicePlugin.IdolVoiceActor.Voinvi, "Voinvi"),
                (UltraVoicePlugin.IdolVoiceActor.Virchew, "Virchew"));

            UltraVoicePlugin.MirrorReaperVoiceActorField = CreateActorSetting("mrvoiceactor", "Mirror Reaper Voice Actor", UltraVoicePlugin.MirrorReaperVoiceActor.Noto, actors,
                (UltraVoicePlugin.MirrorReaperVoiceActor.Noto, "Noto"),
                (UltraVoicePlugin.MirrorReaperVoiceActor.Rotund, "Rotund"));

            UltraVoicePlugin.PowerVoiceActorField = CreateActorSetting("powervoiceactor", "Power Voice Actor", UltraVoicePlugin.PowerVoiceActor.Random, actors,
                (UltraVoicePlugin.PowerVoiceActor.Random, "Random"),
                (UltraVoicePlugin.PowerVoiceActor.Cazsu, "Cazsu (Masc)"),
                (UltraVoicePlugin.PowerVoiceActor.Goober, "Goober (Masc)"),
                (UltraVoicePlugin.PowerVoiceActor.NotoMasc, "Noto (Masc)"),
                (UltraVoicePlugin.PowerVoiceActor.Vanilla, "Kennady (Fem)"),
                (UltraVoicePlugin.PowerVoiceActor.Mel, "Mel (Fem)"),
                (UltraVoicePlugin.PowerVoiceActor.NotoFem, "Noto (Fem)"));

            SettingGroup powerPool = CreateGroup("powervoicepool", "Power Voice Pool", "", actors);
            UltraVoicePlugin.PowerVoicePoolPanel = powerPool;

            foreach (string actor in UltraVoicePlugin.PowerVoiceOrder)
                UltraVoicePlugin.PowerVoiceToggles[actor] = CreateSetting($"powervoice_{actor.ToLowerInvariant()}", UltraVoicePlugin.PowerVoiceNames[actor], "", true, powerPool);

            UltraVoicePlugin.SentryVoiceActorField = CreateActorSetting("svoiceactor", "Sentry Voice Actor", UltraVoicePlugin.SentryVoiceActor.Goober, actors,
                (UltraVoicePlugin.SentryVoiceActor.Noto, "Noto"),
                (UltraVoicePlugin.SentryVoiceActor.Goober, "Goober"));

            UltraVoicePlugin.ShopTerminalVoiceActorField = CreateActorSetting("shopterminalvoiceactor", "Shop Terminal Voice Actor", UltraVoicePlugin.ShopTerminalVoiceActor.Bouncy, actors,
                (UltraVoicePlugin.ShopTerminalVoiceActor.Bouncy, "Bouncy"),
                (UltraVoicePlugin.ShopTerminalVoiceActor.Goober, "Goober"));

            UltraVoicePlugin.SwordsmachineVoiceActorField = CreateActorSetting("smvoiceactor", "Swordsmachine Voice Actor", UltraVoicePlugin.SwordsmachineVoiceActor.Mel, actors,
                (UltraVoicePlugin.SwordsmachineVoiceActor.Mel, "Mel"),
                (UltraVoicePlugin.SwordsmachineVoiceActor.Noto, "Noto"),
                (UltraVoicePlugin.SwordsmachineVoiceActor.Soil, "Buried Soil"));

            UltraVoicePlugin.V1VoiceActorField = CreateActorSetting("v1voiceactor", "V1 Voice Actor", UltraVoicePlugin.V1VoiceActor.HashCap, actors,
                (UltraVoicePlugin.V1VoiceActor.Cyberlord, "Cyberlord (Masc)"),
                (UltraVoicePlugin.V1VoiceActor.Noto, "Noto (Fem)"),
                (UltraVoicePlugin.V1VoiceActor.HashCap, "HashCap (Masc)"));

            UltraVoicePlugin.V1VoiceActorField.OnValueChanged += _ => V1Character.ApplyVoiceActor();

            UltraVoicePlugin.V2VoiceActorField = CreateActorSetting("v2voiceactor", "V2 Voice Actor", UltraVoicePlugin.V2VoiceActor.Ruby, actors,
                (UltraVoicePlugin.V2VoiceActor.Ruby, "Ruby (Fem)"),
                (UltraVoicePlugin.V2VoiceActor.Cazsu, "Cazsu (Masc)"));

            UltraVoicePlugin.VirtueVoiceActorField = CreateActorSetting("virtuevoiceactor", "Virtue Voice Actor", UltraVoicePlugin.VirtueVoiceActor.Noto, actors,
                (UltraVoicePlugin.VirtueVoiceActor.Noto, "Noto"),
                (UltraVoicePlugin.VirtueVoiceActor.Virchew, "Virchew"));
        }

        void BuildVolumes()
        {
            SettingGroup volumes = UltraVoicePlugin.VolumePanel;

            UltraVoicePlugin.MasterVolumeField = CreateSetting("mastervolume", "Master Voice Volume", "", 100f, volumes);
            UltraVoicePlugin.MasterVolumeField.Hints = new InterfaceHints
            {
                Range = new Tuple<float, float>(0f, 200f),
                Decimals = 0
            };

            SettingGroup enemyVolumes = CreateGroup("enemyvolumes", "Enemy Volumes", "", volumes);
            UltraVoicePlugin.EnemyVolumePanel = enemyVolumes;

            (string key, string displayName, Setting<bool> toggle)[] volumeEnemies =
            {
                ("Cerberus", "Cerberus", UltraVoicePlugin.CerberusVoiceEnabled),
                ("Earthmover", "Earthmover", UltraVoicePlugin.EarthmoverVoiceEnabled),
                ("Ferryman", "Ferryman", UltraVoicePlugin.FerrymanVoiceEnabled),
                ("Filth", "Filth", UltraVoicePlugin.FilthVoiceEnabled),
                ("Geryon", "Geryon", UltraVoicePlugin.GeryonVoiceEnabled),
                ("Gutterman", "Gutterman", UltraVoicePlugin.GuttermanVoiceEnabled),
                ("Guttertank", "Guttertank", UltraVoicePlugin.GuttertankVoiceEnabled),
                ("HideousMass", "Hideous Mass", UltraVoicePlugin.MassVoiceEnabled),
                ("Idol", "Idol", UltraVoicePlugin.IdolVoiceEnabled),
                ("Insurrectionist", "Insurrectionist", UltraVoicePlugin.InsurrectionistVoiceEnabled),
                ("Leviathan", "Leviathan", UltraVoicePlugin.LeviathanVoiceEnabled),
                ("MaliciousFace", "Malicious Face", UltraVoicePlugin.MauriceVoiceEnabled),
                ("Mannequin", "Mannequin", UltraVoicePlugin.MannequinVoiceEnabled),
                ("Mindflayer", "Mindflayer", UltraVoicePlugin.MindflayerVoiceEnabled),
                ("MirrorReaper", "Mirror Reaper", UltraVoicePlugin.MirrorReaperVoiceEnabled),
                ("PowerAlt", "Power Alt", UltraVoicePlugin.PowerAltVoiceEnabled),
                ("Providence", "Providence", UltraVoicePlugin.ProvidenceVoiceEnabled),
                ("Schism", "Schism", UltraVoicePlugin.SchismVoiceEnabled),
                ("Sentry", "Sentry", UltraVoicePlugin.SentryVoiceEnabled),
                ("ShopTerminal", "Shop Terminal", UltraVoicePlugin.ShopTerminalVoiceEnabled),
                ("Soldier", "Soldier", UltraVoicePlugin.SoldierVoiceEnabled),
                ("Stalker", "Stalker", UltraVoicePlugin.StalkerVoiceEnabled),
                ("Stray", "Stray", UltraVoicePlugin.StrayVoiceEnabled),
                ("Streetcleaner", "Streetcleaner", UltraVoicePlugin.StreetcleanerVoiceEnabled),
                ("Swordsmachine", "Swordsmachine", UltraVoicePlugin.SwordsmachineVoiceEnabled),
                ("V1", "V1", UltraVoicePlugin.V1VoiceEnabled),
                ("V2", "V2", UltraVoicePlugin.V2VoiceEnabled),
                ("Virtue", "Virtue", UltraVoicePlugin.VirtueVoiceEnabled),
                ("Wicked", "Wicked", UltraVoicePlugin.WickedVoiceEnabled)
            };

            foreach (var (key, displayName, toggle) in volumeEnemies)
            {
                Setting<float> slider = CreateSetting($"volume_{key.ToLowerInvariant()}", $"{displayName} Volume", "", 100f, enemyVolumes);

                UltraVoicePlugin.EnemyVolumeFields[key] = slider;

                slider.Hints = BuildVolumeHints(!toggle.Value);
                toggle.OnValueChanged += enabled => slider.Hints = BuildVolumeHints(!enabled);
            }

            UltraVoicePlugin.PowerAltVolumeField = UltraVoicePlugin.EnemyVolumeFields["PowerAlt"];
        }

        static InterfaceHints BuildVolumeHints(bool hidden)
        {
            return new InterfaceHints
            {
                Range = new Tuple<float, float>(0f, 200f),
                Decimals = 0,
                Hidden = hidden
            };
        }

        Setting<T> CreateActorSetting<T>(string guid, string name, T defaultValue, SettingGroup group, params (T actor, string label)[] options) where T : struct
        {
            Setting<T> setting = CreateSetting(guid, name, "", defaultValue, group);

            Dictionary<string, string> substitutions = new Dictionary<string, string>();

            foreach (var (actor, label) in options)
                substitutions[actor.ToString()] = label;

            setting.Hints = new InterfaceHints
            {
                EnumSubstitutions = substitutions,
                EnumPreferButtonGroup = true
            };

            return setting;
        }
    }
}
