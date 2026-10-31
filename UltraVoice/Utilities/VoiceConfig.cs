using System;
using System.Collections.Generic;
using PluginConfig.API;
using PluginConfig.API.Fields;
using UltraVoice.Characters;
using UnityEngine;

namespace UltraVoice.Utilities
{
    public class VoiceConfig
    {
        public PluginConfigurator Config { get; }

        ConfigPanel Root => Config.rootPanel;

        public VoiceConfig()
        {
            Config = PluginConfigurator.Create(UltraVoicePlugin.PluginName, UltraVoicePlugin.PluginGuid);
            Config.icon = UpdateChecker.LoadIcon("UltraVoice.Images.notiffy_icon.png");

            UltraVoicePlugin.TogglesPanel = CreatePanel(Root, "toggles", "Enemy Line Toggles");
            UltraVoicePlugin.ActorPanel = CreatePanel(Root, "actors", "Voice Actors");
            UltraVoicePlugin.VolumePanel = CreatePanel(Root, "volumes", "Volume Levels");
            UltraVoicePlugin.SubtitleColorsPanel = CreatePanel(Root, "subtitlecolors", "Subtitle Colors");

            UltraVoicePlugin.VoiceCooldown = CreateSlider(Root, "cooldown", "Voice Cooldown", 0.25f, 0f, 0.5f, 2);

            UltraVoicePlugin.SubtitleLimit = new IntField(Root, "Max Allowed Subtitles", "subtitlelimit", 5, 1, 10);

            BuildToggles();
            BuildSubtitleColors();
            BuildVoiceActors();
            BuildVolumes();

            UltraVoicePlugin.MasterVoiceEnabled.postValueChangeEvent += _ => RefreshInteractable();
            UltraVoicePlugin.SubtitleColorsEnabled.postValueChangeEvent += _ => RefreshInteractable();
            UltraVoicePlugin.V1VoiceEnabled.postValueChangeEvent += _ => RefreshInteractable();
            UltraVoicePlugin.PowerVoiceActorField.postValueChangeEvent += _ => RefreshInteractable();

            RefreshInteractable();
        }

        static void RefreshInteractable()
        {
            bool master = UltraVoicePlugin.MasterVoiceEnabled.value;

            UltraVoicePlugin.VoiceCooldown.interactable = master;
            UltraVoicePlugin.SubtitleLimit.interactable = master;

            UltraVoicePlugin.EnemyTogglesPanel.interactable = master;
            UltraVoicePlugin.ActorPanel.interactable = master;
            UltraVoicePlugin.MasterVolumeField.interactable = master;
            UltraVoicePlugin.EnemyVolumePanel.interactable = master;

            UltraVoicePlugin.SubtitleColorsEnabled.interactable = master;
            UltraVoicePlugin.CharacterSubtitleColorsPanel.interactable = master && UltraVoicePlugin.SubtitleColorsEnabled.value;

            UltraVoicePlugin.V1LinesPanel.interactable = master && UltraVoicePlugin.V1VoiceEnabled.value;

            UltraVoicePlugin.PowerVoicePoolPanel.interactable = master
                && UltraVoicePlugin.PowerVoiceActorField.value == UltraVoicePlugin.PowerVoiceActor.Random;
        }

        void BuildToggles()
        {
            ConfigPanel toggles = UltraVoicePlugin.TogglesPanel;

            UltraVoicePlugin.MasterVoiceEnabled = CreateSetting("mastervoice", "Enable Voice Lines", true, toggles);

            ConfigPanel enemies = CreatePanel(toggles, "enemytoggles", "Enemy Toggles");
            UltraVoicePlugin.EnemyTogglesPanel = enemies;

            UltraVoicePlugin.CerberusVoiceEnabled = CreateEnemyToggle("cerberusvoice", "Cerberus", true, enemies);
            UltraVoicePlugin.DeathcatcherVoiceEnabled = CreateEnemyToggle("deathcatchervoice", "Deathcatcher", true, enemies);
            UltraVoicePlugin.EarthmoverVoiceEnabled = CreateEnemyToggle("earthmovervoice", "Earthmover", true, enemies);
            UltraVoicePlugin.FerrymanVoiceEnabled = CreateEnemyToggle("ferrymanvoice", "Ferryman", true, enemies);
            UltraVoicePlugin.FilthVoiceEnabled = CreateEnemyToggle("filthvoice", "Filth", true, enemies);
            UltraVoicePlugin.GeryonVoiceEnabled = CreateEnemyToggle("geryonvoice", "Geryon", true, enemies);
            UltraVoicePlugin.GuttermanVoiceEnabled = CreateEnemyToggle("guttermanvoice", "Gutterman", true, enemies);
            UltraVoicePlugin.GuttertankVoiceEnabled = CreateEnemyToggle("guttertankvoice", "Guttertank", true, enemies);
            UltraVoicePlugin.MassVoiceEnabled = CreateEnemyToggle("massvoice", "Hideous Mass", true, enemies);
            UltraVoicePlugin.IdolVoiceEnabled = CreateEnemyToggle("idolvoice", "Idol", true, enemies);
            UltraVoicePlugin.InsurrectionistVoiceEnabled = CreateEnemyToggle("Insurrectionist", "Insurrectionist", true, enemies);
            UltraVoicePlugin.LeviathanVoiceEnabled = CreateEnemyToggle("levivoice", "Leviathan", true, enemies);
            UltraVoicePlugin.MauriceVoiceEnabled = CreateEnemyToggle("mauricevoice", "Malicious Face", true, enemies);
            UltraVoicePlugin.MannequinVoiceEnabled = CreateEnemyToggle("mannequinvoice", "Mannequin", true, enemies);
            UltraVoicePlugin.MindflayerVoiceEnabled = CreateEnemyToggle("mindflayervoice", "Mindflayer", true, enemies);
            UltraVoicePlugin.MirrorReaperVoiceEnabled = CreateEnemyToggle("mirrorreapervoice", "Mirror Reaper", true, enemies);
            UltraVoicePlugin.PowerAltVoiceEnabled = CreateEnemyToggle("poweraltvoice", "Power Alt", true, enemies);
            UltraVoicePlugin.ProvidenceVoiceEnabled = CreateEnemyToggle("providencevoice", "Providence", true, enemies);
            UltraVoicePlugin.SchismVoiceEnabled = CreateEnemyToggle("schismvoice", "Schism", true, enemies);
            UltraVoicePlugin.SentryVoiceEnabled = CreateEnemyToggle("sentryvoice", "Sentry", true, enemies);
            UltraVoicePlugin.ShopTerminalVoiceEnabled = CreateEnemyToggle("shopterminalvoice", "Shop Terminal", true, enemies);
            UltraVoicePlugin.SoldierVoiceEnabled = CreateEnemyToggle("soldiervoice", "Soldier", true, enemies);
            UltraVoicePlugin.StalkerVoiceEnabled = CreateEnemyToggle("stalkervoice", "Stalker", true, enemies);
            UltraVoicePlugin.StrayVoiceEnabled = CreateEnemyToggle("strayvoice", "Stray", true, enemies);
            UltraVoicePlugin.StreetcleanerVoiceEnabled = CreateEnemyToggle("streetcleanervoice", "Streetcleaner", true, enemies);
            UltraVoicePlugin.SwordsmachineVoiceEnabled = CreateEnemyToggle("swordsmachinevoice", "Swordsmachine", true, enemies);
            UltraVoicePlugin.V1VoiceEnabled = CreateEnemyToggle("v1voice", "V1", true, enemies);

            ConfigPanel v1Lines = CreatePanel(enemies, "v1lines", "V1 Line Categories");
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
                UltraVoicePlugin.V1LineToggles[key] = CreateSetting($"v1line_{key}", name, true, v1Lines);

            UltraVoicePlugin.V2VoiceEnabled = CreateEnemyToggle("v2voice", "V2", true, enemies);
            UltraVoicePlugin.VirtueVoiceEnabled = CreateEnemyToggle("virtuevoice", "Virtue", true, enemies);
            UltraVoicePlugin.WickedVoiceEnabled = CreateEnemyToggle("wickedvoice", "Wicked", true, enemies);
        }

        void BuildSubtitleColors()
        {
            ConfigPanel colors = UltraVoicePlugin.SubtitleColorsPanel;

            UltraVoicePlugin.SubtitleColorsEnabled = CreateSetting("subtitlecolorsenabled", "Enable Subtitle Colors", true, colors);

            ConfigPanel perCharacter = CreatePanel(colors, "charsubtitlecolors", "Character Subtitle Colors");
            UltraVoicePlugin.CharacterSubtitleColorsPanel = perCharacter;

            (string key, string label)[] subtitleColorCharacters =
            {
                ("Cerberus", "Cerberus"),
                ("Deathcatcher", "Deathcatcher"),
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
                ("Stalker", "Stalker"),
                ("ShopTerminal", "Shop Terminal"),
                ("Streetcleaner", "Streetcleaner"),
                ("Swordsmachine", "Swordsmachine"),
                ("V1", "V1"),
                ("V2", "V2"),
                ("Virtue", "Virtue"),
                ("Wicked", "Wicked")
            };

            foreach (var (key, label) in subtitleColorCharacters)
                UltraVoicePlugin.SubtitleColorToggles[key] = CreateSetting($"subcolor_{key.ToLowerInvariant()}", label, true, perCharacter);
        }

        void BuildVoiceActors()
        {
            ConfigPanel actors = UltraVoicePlugin.ActorPanel;

            UltraVoicePlugin.CerberusVoiceActorField = CreateActorSetting("cerbvoiceactor", "Cerberus", UltraVoicePlugin.CerberusVoiceActor.Mel, actors,
                (UltraVoicePlugin.CerberusVoiceActor.Mel, "Mel"),
                (UltraVoicePlugin.CerberusVoiceActor.Soil, "Buried Soil"),
                (UltraVoicePlugin.CerberusVoiceActor.Rotund, "Rotund"));

            UltraVoicePlugin.EarthmoverVoiceActorField = CreateActorSetting("earthmovervoiceactor", "Earthmover", UltraVoicePlugin.EarthmoverVoiceActor.Rotund, actors,
                (UltraVoicePlugin.EarthmoverVoiceActor.Rotund, "Rotund"),
                (UltraVoicePlugin.EarthmoverVoiceActor.Tondar, "TondarYZD"),
                (UltraVoicePlugin.EarthmoverVoiceActor.Megaslash, "Megaslash"));

            UltraVoicePlugin.FerrymanVoiceActorField = CreateActorSetting("ferryvoiceactor", "Ferryman", UltraVoicePlugin.FerrymanVoiceActor.Mel, actors,
                (UltraVoicePlugin.FerrymanVoiceActor.Mel, "Mel"),
                (UltraVoicePlugin.FerrymanVoiceActor.Soil, "Buried Soil"),
                (UltraVoicePlugin.FerrymanVoiceActor.Nicky, "Nicky"));

            UltraVoicePlugin.GeryonVoiceActorField = CreateActorSetting("geryonvoiceactor", "Geryon", UltraVoicePlugin.GeryonVoiceActor.TheRatmanValinMegaslash, actors,
                (UltraVoicePlugin.GeryonVoiceActor.TheRatmanValinMegaslash, "TheRatman, Valin, Megalash"),
                (UltraVoicePlugin.GeryonVoiceActor.Mel, "Mel"));

            UltraVoicePlugin.GuttermanVoiceActorField = CreateActorSetting("gmvoiceactor", "Gutterman", UltraVoicePlugin.GuttermanVoiceActor.Mel, actors,
                (UltraVoicePlugin.GuttermanVoiceActor.Mel, "Mel"),
                (UltraVoicePlugin.GuttermanVoiceActor.Lemen, "Lemen"));

            UltraVoicePlugin.GuttertankVoiceActorField = CreateActorSetting("gtvoiceactor", "Guttertank", UltraVoicePlugin.GuttertankVoiceActor.Mel, actors,
                (UltraVoicePlugin.GuttertankVoiceActor.Mel, "Mel"),
                (UltraVoicePlugin.GuttertankVoiceActor.Virchew, "Virchew"));

            UltraVoicePlugin.IdolVoiceActorField = CreateActorSetting("idolvoiceactor", "Idol", UltraVoicePlugin.IdolVoiceActor.Voinvi, actors,
                (UltraVoicePlugin.IdolVoiceActor.Voinvi, "Voinvi"),
                (UltraVoicePlugin.IdolVoiceActor.Virchew, "Virchew"));

            UltraVoicePlugin.MirrorReaperVoiceActorField = CreateActorSetting("mrvoiceactor", "Mirror Reaper", UltraVoicePlugin.MirrorReaperVoiceActor.Noto, actors,
                (UltraVoicePlugin.MirrorReaperVoiceActor.Noto, "Noto"),
                (UltraVoicePlugin.MirrorReaperVoiceActor.Rotund, "Rotund"));

            UltraVoicePlugin.PowerVoiceActorField = CreateActorSetting("powervoiceactor", "Power", UltraVoicePlugin.PowerVoiceActor.Random, actors,
                (UltraVoicePlugin.PowerVoiceActor.Random, "Random"),
                (UltraVoicePlugin.PowerVoiceActor.Cazsu, "Cazsu (Masc)"),
                (UltraVoicePlugin.PowerVoiceActor.Goober, "Goober (Masc)"),
                (UltraVoicePlugin.PowerVoiceActor.NotoMasc, "Noto (Masc)"),
                (UltraVoicePlugin.PowerVoiceActor.Vanilla, "Kennady (Fem)"),
                (UltraVoicePlugin.PowerVoiceActor.Mel, "Mel (Fem)"),
                (UltraVoicePlugin.PowerVoiceActor.NotoFem, "Noto (Fem)"));

            ConfigPanel powerPool = CreatePanel(actors, "powervoicepool", "Power Voice Pool");
            UltraVoicePlugin.PowerVoicePoolPanel = powerPool;

            foreach (string actor in UltraVoicePlugin.PowerVoiceOrder)
                UltraVoicePlugin.PowerVoiceToggles[actor] = CreateSetting($"powervoice_{actor.ToLowerInvariant()}", UltraVoicePlugin.PowerVoiceNames[actor], true, powerPool);

            UltraVoicePlugin.SentryVoiceActorField = CreateActorSetting("svoiceactor", "Sentry", UltraVoicePlugin.SentryVoiceActor.Goober, actors,
                (UltraVoicePlugin.SentryVoiceActor.Noto, "Noto"),
                (UltraVoicePlugin.SentryVoiceActor.Goober, "Goober"));

            UltraVoicePlugin.ShopTerminalVoiceActorField = CreateActorSetting("shopterminalvoiceactor", "Shop Terminal", UltraVoicePlugin.ShopTerminalVoiceActor.Bouncy, actors,
                (UltraVoicePlugin.ShopTerminalVoiceActor.Bouncy, "Bouncy"),
                (UltraVoicePlugin.ShopTerminalVoiceActor.Goober, "Goober"));

            UltraVoicePlugin.SwordsmachineVoiceActorField = CreateActorSetting("smvoiceactor", "Swordsmachine", UltraVoicePlugin.SwordsmachineVoiceActor.Mel, actors,
                (UltraVoicePlugin.SwordsmachineVoiceActor.Mel, "Mel"),
                (UltraVoicePlugin.SwordsmachineVoiceActor.Noto, "Noto"),
                (UltraVoicePlugin.SwordsmachineVoiceActor.Soil, "Buried Soil"));

            UltraVoicePlugin.V1VoiceActorField = CreateActorSetting("v1voiceactor", "V1", UltraVoicePlugin.V1VoiceActor.HashCap, actors,
                (UltraVoicePlugin.V1VoiceActor.Cyberlord, "Cyberlord (Masc)"),
                (UltraVoicePlugin.V1VoiceActor.Noto, "Noto (Fem)"),
                (UltraVoicePlugin.V1VoiceActor.HashCap, "HashCap (Masc)"));

            UltraVoicePlugin.V1VoiceActorField.postValueChangeEvent += _ => V1Character.ApplyVoiceActor();

            UltraVoicePlugin.V2VoiceActorField = CreateActorSetting("v2voiceactor", "V2", UltraVoicePlugin.V2VoiceActor.Ruby, actors,
                (UltraVoicePlugin.V2VoiceActor.Ruby, "Ruby (Fem)"),
                (UltraVoicePlugin.V2VoiceActor.Cazsu, "Cazsu (Masc)"));

            UltraVoicePlugin.VirtueVoiceActorField = CreateActorSetting("virtuevoiceactor", "Virtue", UltraVoicePlugin.VirtueVoiceActor.Noto, actors,
                (UltraVoicePlugin.VirtueVoiceActor.Noto, "Noto"),
                (UltraVoicePlugin.VirtueVoiceActor.Virchew, "Virchew"));
        }

        void BuildVolumes()
        {
            ConfigPanel volumes = UltraVoicePlugin.VolumePanel;

            UltraVoicePlugin.MasterVolumeField = CreateSlider(volumes, "mastervolume", "Master Voice Volume", 100f, 0f, 200f, 0);

            ConfigPanel enemyVolumes = CreatePanel(volumes, "enemyvolumes", "Enemy Volumes");
            UltraVoicePlugin.EnemyVolumePanel = enemyVolumes;

            (string key, string displayName, BoolField toggle)[] volumeEnemies =
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
                FloatSliderField slider = CreateSlider(enemyVolumes, $"volume_{key.ToLowerInvariant()}", displayName, 100f, 0f, 200f, 0);

                UltraVoicePlugin.EnemyVolumeFields[key] = slider;

                slider.hidden = !toggle.value;
                toggle.postValueChangeEvent += enabled => slider.hidden = !enabled;
            }

            UltraVoicePlugin.PowerAltVolumeField = UltraVoicePlugin.EnemyVolumeFields["PowerAlt"];
        }

        static ConfigPanel CreatePanel(ConfigPanel parent, string guid, string name)
        {
            return new ConfigPanel(parent, name, guid);
        }

        static BoolField CreateSetting(string guid, string name, bool defaultValue, ConfigPanel panel)
        {
            return new BoolField(panel, name, guid, defaultValue);
        }

        static BoolField CreateEnemyToggle(string guid, string name, bool defaultValue, ConfigPanel panel)
        {
            return new BoolField(panel, name, guid, defaultValue);
        }

        static FloatSliderField CreateSlider(ConfigPanel panel, string guid, string name, float defaultValue, float min, float max, int decimals)
        {
            return new FloatSliderField(panel, name, guid, new Tuple<float, float>(min, max), defaultValue, decimals);
        }

        static EnumField<T> CreateActorSetting<T>(string guid, string name, T defaultValue, ConfigPanel panel, params (T actor, string label)[] options) where T : struct, Enum
        {
            EnumField<T> field = new EnumField<T>(panel, name, guid, defaultValue);

            foreach (var (actor, label) in options)
                field.SetEnumDisplayName(actor, label);

            return field;
        }
    }
}