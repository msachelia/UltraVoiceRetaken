/*
    UltraVoice Retaken - Adds custom voice lines to ULTRAKILL enemies
    Copyright (C) 2026 msachelia, mel (mof_33)

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.

    The voice recordings distributed with this program are licensed
    separately under VOICE-ACTING-LICENSE.md and are not covered by the GPL.
*/

using BepInEx;
using HarmonyLib;
using PluginConfig.API;
using PluginConfig.API.Fields;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UltraVoice.Characters;
using UltraVoice.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UltraVoice
{
    [BepInPlugin("com.achelia.ultravoiceretaken", "UltraVoice Retaken", PluginVersion)]
    [BepInDependency("com.eternalUnion.pluginConfigurator")]
    [BepInDependency("com.github.end-4.notiffy")]
    public class UltraVoicePlugin : BaseUnityPlugin
    {
        public const string PluginVersion = "1.4.3";

        public static UltraVoicePlugin Instance;
        public static VoiceManager VoiceManager;
        private Harmony harmony;

        private PluginConfigurator config;

        public static BoolField CerberusVoiceEnabled;
        public static BoolField SwordsmachineVoiceEnabled;
        public static BoolField V2VoiceEnabled;
        public static BoolField StreetcleanerVoiceEnabled;
        public static BoolField MindflayerVoiceEnabled;
        public static BoolField VirtueVoiceEnabled;
        public static BoolField FerrymanVoiceEnabled;
        public static BoolField GuttermanVoiceEnabled;
        public static BoolField MannequinVoiceEnabled;
        public static BoolField GuttertankVoiceEnabled;
        public static BoolField ProvidenceVoiceEnabled;
        public static BoolField SentryVoiceEnabled;
        public static BoolField MauriceVoiceEnabled;
        public static BoolField EarthmoverVoiceEnabled;
        public static BoolField MirrorReaperVoiceEnabled;
        public static BoolField GeryonVoiceEnabled;
        public static BoolField LeviathanVoiceEnabled;
        public static BoolField InsurrectionistVoiceEnabled;
        public static BoolField FilthVoiceEnabled;
        public static BoolField StrayVoiceEnabled;
        public static BoolField SchismVoiceEnabled;
        public static BoolField SoldierVoiceEnabled;
        public static BoolField StalkerVoiceEnabled;
        public static BoolField MassVoiceEnabled;
        public static BoolField IdolVoiceEnabled;
        public static BoolField WickedVoiceEnabled;
        public static BoolField V1VoiceEnabled;
        public static BoolField PowerAltVoiceEnabled;
        public static BoolField MasterVoiceEnabled;
        public static BoolField PowerSubtitleColorEnabled;
        public static FloatField VoiceCooldown;
        public static IntField SubtitleLimit;

        public static EnumField<SwordsmachineVoiceActor> SwordsmachineVoiceActorField;
        public static EnumField<SentryVoiceActor> SentryVoiceActorField;
        public static EnumField<GuttertankVoiceActor> GuttertankVoiceActorField;
        public static EnumField<GuttermanVoiceActor> GuttermanVoiceActorField;
        public static EnumField<CerberusVoiceActor> CerberusVoiceActorField;
        public static EnumField<FerrymanVoiceActor> FerrymanVoiceActorField;
        public static EnumField<IdolVoiceActor> IdolVoiceActorField;
        public static EnumField<VirtueVoiceActor> VirtueVoiceActorField;
        public static EnumField<MirrorReaperVoiceActor> MirrorReaperVoiceActorField;
        public static EnumField<V2VoiceActor> V2VoiceActorField;
        public static ConfigPanel TogglesPanel;
        public static ConfigPanel EnemyTogglesPanel;
        public static ConfigPanel ActorPanel;
        public static ConfigPanel VolumePanel;
        public static ConfigPanel EnemyVolumePanel;
        public static ConfigPanel V1LinesPanel;

        public static Dictionary<string, BoolField> V1LineToggles = new Dictionary<string, BoolField>();

        public static bool V1LineEnabled(string key)
        {
            if (V1VoiceEnabled == null || !V1VoiceEnabled.value)
                return false;

            return !V1LineToggles.TryGetValue(key, out BoolField field) || field.value;
        }

        public static bool VoicesEnabled => MasterVoiceEnabled == null || MasterVoiceEnabled.value;

        public static FloatSliderField MasterVolumeField;
        public static FloatSliderField PowerAltVolumeField;
        public static Dictionary<string, FloatSliderField> EnemyVolumeFields = new Dictionary<string, FloatSliderField>();

        public enum SwordsmachineVoiceActor
        {
            Mel,
            Noto
        }

        public enum SentryVoiceActor
        {
            Noto,
            Goober
        }

        public enum GuttertankVoiceActor
        {
            Mel,
            Virchew
        }

        public enum GuttermanVoiceActor
        {
            Mel,
            Lemen
        }

        public enum CerberusVoiceActor
        {
            Mel,
            Soil,
            Rotund
        }

        public enum FerrymanVoiceActor
        {
            Mel,
            Soil
        }

        public enum V2VoiceActor
        {
            Ruby,
            Cazsu
        }

        public enum IdolVoiceActor
        {
            Voinvi,
            Virchew
        }

        public enum VirtueVoiceActor
        {
            Noto,
            Virchew
        }

        public enum MirrorReaperVoiceActor
        {
            Noto,
            Rotund
        }

        void Awake()
        {
            Instance = this;
            VoiceManager = new VoiceManager();

            config = PluginConfigurator.Create("UltraVoice Retaken", "com.achelia.ultravoiceretaken");
            config.icon = UpdateChecker.LoadIcon();

            TogglesPanel = new ConfigPanel(config.rootPanel, "Enemy Line Toggles", "toggles");
            ActorPanel = new ConfigPanel(config.rootPanel, "Voice Actors", "actors");
            VolumePanel = new ConfigPanel(config.rootPanel, "Volume Levels", "volumes");

            MasterVoiceEnabled = new BoolField(
                TogglesPanel,
                "Enable Voice Lines",
                "mastervoice",
                true
            );

            EnemyTogglesPanel = new ConfigPanel(TogglesPanel, "Enemy Toggles", "enemytoggles");

            MasterVoiceEnabled.postValueChangeEvent += (bool enabled) => EnemyTogglesPanel.interactable = enabled;
            EnemyTogglesPanel.interactable = MasterVoiceEnabled.value;

            CerberusVoiceEnabled = new BoolField(
                EnemyTogglesPanel,
                "Enable Cerberus Voice Lines",
                "cerberusvoice",
                true
            );

            EarthmoverVoiceEnabled = new BoolField(
                EnemyTogglesPanel,
                "Enable Earthmover Voice Lines",
                "earthmovervoice",
                true
            );

            FerrymanVoiceEnabled = new BoolField(
                EnemyTogglesPanel,
                "Enable Ferryman Voice Lines",
                "ferrymanvoice",
                true
            );

            FilthVoiceEnabled = new BoolField(
                EnemyTogglesPanel,
                "Enable Filth Voice Lines",
                "filthvoice",
                false
            );

            GeryonVoiceEnabled = new BoolField(
                EnemyTogglesPanel,
                "Enable Geryon Voice Lines",
                "geryonvoice",
                true
            );

            GuttermanVoiceEnabled = new BoolField(
                EnemyTogglesPanel,
                "Enable Gutterman Voice Lines",
                "guttermanvoice",
                true
            );

            GuttertankVoiceEnabled = new BoolField(
                EnemyTogglesPanel,
                "Enable Guttertank Voice Lines",
                "guttertankvoice",
                true
            );

            MassVoiceEnabled = new BoolField(
                EnemyTogglesPanel,
                "Enable Hideous Mass Voice Lines",
                "massvoice",
                true
            );

            IdolVoiceEnabled = new BoolField(
                EnemyTogglesPanel,
                "Enable Idol Voice Lines",
                "idolvoice",
                true
            );

            InsurrectionistVoiceEnabled = new BoolField(
                EnemyTogglesPanel,
                "Enable Insurrectionist Voice Lines",
                "Insurrectionist",
                true
            );

            LeviathanVoiceEnabled = new BoolField(
                EnemyTogglesPanel,
                "Enable Leviathan Voice Lines",
                "levivoice",
                true
            );

            MauriceVoiceEnabled = new BoolField(
                EnemyTogglesPanel,
                "Enable Malicious Face Voice Lines",
                "mauricevoice",
                true
            );

            MannequinVoiceEnabled = new BoolField(
                EnemyTogglesPanel,
                "Enable Mannequin Voice Lines",
                "mannequinvoice",
                true
            );

            MindflayerVoiceEnabled = new BoolField(
                EnemyTogglesPanel,
                "Enable Mindflayer Voice Lines",
                "mindflayervoice",
                true
            );

            MirrorReaperVoiceEnabled = new BoolField(
                EnemyTogglesPanel,
                "Enable Mirror Reaper Voice Lines",
                "mirrorreapervoice",
                true
            );

            PowerAltVoiceEnabled = new BoolField(
                EnemyTogglesPanel,
                "Enable Power Alt Voice Lines",
                "poweraltvoice",
                true
            );

            ProvidenceVoiceEnabled = new BoolField(
                EnemyTogglesPanel,
                "Enable Providence Voice Lines",
                "providencevoice",
                true
            );

            SchismVoiceEnabled = new BoolField(
                EnemyTogglesPanel,
                "Enable Schism Voice Lines",
                "schismvoice",
                false
            );

            SentryVoiceEnabled = new BoolField(
                EnemyTogglesPanel,
                "Enable Sentry Voice Lines",
                "sentryvoice",
                true
            );

            SoldierVoiceEnabled = new BoolField(
                EnemyTogglesPanel,
                "Enable Soldier Voice Lines",
                "soldiervoice",
                false
            );

            StalkerVoiceEnabled = new BoolField(
                EnemyTogglesPanel,
                "Enable Stalker Voice Lines",
                "stalkervoice",
                false
            );

            StrayVoiceEnabled = new BoolField(
                EnemyTogglesPanel,
                "Enable Stray Voice Lines",
                "strayvoice",
                false
            );

            StreetcleanerVoiceEnabled = new BoolField(
                EnemyTogglesPanel,
                "Enable Streetcleaner Voice Lines",
                "streetcleanervoice",
                true
            );

            SwordsmachineVoiceEnabled = new BoolField(
                EnemyTogglesPanel,
                "Enable Swordsmachine Voice Lines",
                "swordsmachinevoice",
                true
            );

            V1VoiceEnabled = new BoolField(
                EnemyTogglesPanel,
                "Enable V1 Voice Lines",
                "v1voice",
                true
            );

            V1LinesPanel = new ConfigPanel(EnemyTogglesPanel, "V1 Line Categories", "v1lines");

            V1VoiceEnabled.postValueChangeEvent += (bool enabled) => V1LinesPanel.interactable = enabled;
            V1LinesPanel.interactable = V1VoiceEnabled.value;

            (string key, string name)[] v1Categories =
            {
                ("kill", "Kill Lines"),
                ("multikill", "Multikill Lines"),
                ("bosskill", "Boss Kill Lines"),
                ("headshot", "Headshot Lines"),
                ("airshot", "Airshot Lines"),
                ("environmental", "Environmental Kill Lines"),
                ("maxstyle", "Max Style Lines"),
                ("rank", "Level Rank Lines"),
                ("parry", "Parry Lines"),
                ("enrage", "Enrage Lines"),
                ("death", "Death Lines"),
                ("pickup", "Weapon Pickup Lines"),
                ("purchase", "Purchase Lines"),
                ("skull", "Skull Pickup Lines"),
                ("checkpoint", "Checkpoint Lines"),
                ("respawn", "Respawn Lines"),
                ("secret", "Secret Orb Lines")
            };

            foreach (var (key, name) in v1Categories)
                V1LineToggles[key] = new BoolField(V1LinesPanel, name, $"v1line_{key}", true);

            V2VoiceEnabled = new BoolField(
                EnemyTogglesPanel,
                "Enable V2 Voice Lines",
                "v2voice",
                true
            );

            VirtueVoiceEnabled = new BoolField(
                EnemyTogglesPanel,
                "Enable Virtue Voice Lines",
                "virtuevoice",
                true
            );

            WickedVoiceEnabled = new BoolField(
                EnemyTogglesPanel,
                "Enable Wicked Voice Lines",
                "wickedvoice",
                true
            );

            PowerSubtitleColorEnabled = new BoolField(
                config.rootPanel,
                "Enable Power Subtitle Color",
                "powersubcolor",
                true
            );

            VoiceCooldown = new FloatField(
                config.rootPanel,
                "Voice Cooldown",
                "cooldown",
                0.25f,
                0f,
                0.5f
            );

            SubtitleLimit = new IntField(
                config.rootPanel,
                "Max Allowed Subtitles",
                "subtitlelimit",
                5,
                1,
                10
            );

            UltraVoicePlugin.CerberusVoiceActorField = new EnumField<CerberusVoiceActor>(
                ActorPanel,
                "Cerberus Voice Actor",
                "cerbvoiceactor",
                CerberusVoiceActor.Mel
            );

            UltraVoicePlugin.FerrymanVoiceActorField = new EnumField<FerrymanVoiceActor>(
                ActorPanel,
                "Ferryman Voice Actor",
                "ferryvoiceactor",
                FerrymanVoiceActor.Mel
            );

            UltraVoicePlugin.GuttermanVoiceActorField = new EnumField<GuttermanVoiceActor>(
                ActorPanel,
                "Gutterman Voice Actor",
                "gmvoiceactor",
                GuttermanVoiceActor.Mel
            );

            UltraVoicePlugin.GuttertankVoiceActorField = new EnumField<GuttertankVoiceActor>(
                ActorPanel,
                "Guttertank Voice Actor",
                "gtvoiceactor",
                GuttertankVoiceActor.Mel
            );

            UltraVoicePlugin.IdolVoiceActorField = new EnumField<IdolVoiceActor>(
                ActorPanel,
                "Idol Voice Actor",
                "idolvoiceactor",
                IdolVoiceActor.Voinvi
            );

            UltraVoicePlugin.MirrorReaperVoiceActorField = new EnumField<MirrorReaperVoiceActor>(
                ActorPanel,
                "Mirror Reaper Voice Actor",
                "mrvoiceactor",
                MirrorReaperVoiceActor.Noto
            );

            UltraVoicePlugin.SentryVoiceActorField = new EnumField<SentryVoiceActor>(
                ActorPanel,
                "Sentry Voice Actor",
                "svoiceactor",
                SentryVoiceActor.Goober
            );

            UltraVoicePlugin.SwordsmachineVoiceActorField = new EnumField<SwordsmachineVoiceActor>(
                ActorPanel,
                "Swordsmachine Voice Actor",
                "smvoiceactor",
                SwordsmachineVoiceActor.Mel
            );

            UltraVoicePlugin.V2VoiceActorField = new EnumField<V2VoiceActor>(
                ActorPanel,
                "V2 Voice Actor",
                "v2voiceactor",
                V2VoiceActor.Ruby
            );

            UltraVoicePlugin.VirtueVoiceActorField = new EnumField<VirtueVoiceActor>(
                ActorPanel,
                "Virtue Voice Actor",
                "virtuevoiceactor",
                VirtueVoiceActor.Noto
            );

            UltraVoicePlugin.SwordsmachineVoiceActorField.SetEnumDisplayName(SwordsmachineVoiceActor.Mel, "Mel");
            UltraVoicePlugin.SwordsmachineVoiceActorField.SetEnumDisplayName(SwordsmachineVoiceActor.Noto, "Noto");

            UltraVoicePlugin.SentryVoiceActorField.SetEnumDisplayName(SentryVoiceActor.Goober, "Goober");
            UltraVoicePlugin.SentryVoiceActorField.SetEnumDisplayName(SentryVoiceActor.Noto, "Noto");

            UltraVoicePlugin.GuttertankVoiceActorField.SetEnumDisplayName(GuttertankVoiceActor.Mel, "Mel");
            UltraVoicePlugin.GuttertankVoiceActorField.SetEnumDisplayName(GuttertankVoiceActor.Virchew, "Virchew");

            UltraVoicePlugin.GuttermanVoiceActorField.SetEnumDisplayName(GuttermanVoiceActor.Mel, "Mel");
            UltraVoicePlugin.GuttermanVoiceActorField.SetEnumDisplayName(GuttermanVoiceActor.Lemen, "Lemen");

            UltraVoicePlugin.CerberusVoiceActorField.SetEnumDisplayName(CerberusVoiceActor.Mel, "Mel");
            UltraVoicePlugin.CerberusVoiceActorField.SetEnumDisplayName(CerberusVoiceActor.Soil, "Soil");
            UltraVoicePlugin.CerberusVoiceActorField.SetEnumDisplayName(CerberusVoiceActor.Rotund, "Rotund");

            UltraVoicePlugin.FerrymanVoiceActorField.SetEnumDisplayName(FerrymanVoiceActor.Mel, "Mel");
            UltraVoicePlugin.FerrymanVoiceActorField.SetEnumDisplayName(FerrymanVoiceActor.Soil, "Soil");

            UltraVoicePlugin.V2VoiceActorField.SetEnumDisplayName(V2VoiceActor.Ruby, "Ruby");
            UltraVoicePlugin.V2VoiceActorField.SetEnumDisplayName(V2VoiceActor.Cazsu, "Cazsu");

            UltraVoicePlugin.IdolVoiceActorField.SetEnumDisplayName(IdolVoiceActor.Voinvi, "Voinvi");
            UltraVoicePlugin.IdolVoiceActorField.SetEnumDisplayName(IdolVoiceActor.Virchew, "Virchew");

            UltraVoicePlugin.VirtueVoiceActorField.SetEnumDisplayName(VirtueVoiceActor.Noto, "Noto");
            UltraVoicePlugin.VirtueVoiceActorField.SetEnumDisplayName(VirtueVoiceActor.Virchew, "Virchew");

            UltraVoicePlugin.MirrorReaperVoiceActorField.SetEnumDisplayName(MirrorReaperVoiceActor.Noto, "Noto");
            UltraVoicePlugin.MirrorReaperVoiceActorField.SetEnumDisplayName(MirrorReaperVoiceActor.Rotund, "Rotund");

            MasterVolumeField = new FloatSliderField(
                VolumePanel,
                "Master Voice Volume",
                "mastervolume",
                new Tuple<float, float>(0f, 200f),
                100f,
                0
            );

            EnemyVolumePanel = new ConfigPanel(VolumePanel, "Enemy Volumes", "enemyvolumes");

            (string key, string displayName, BoolField toggle)[] volumeEnemies =
            {
                ("Cerberus", "Cerberus", CerberusVoiceEnabled),
                ("Earthmover", "Earthmover", EarthmoverVoiceEnabled),
                ("Ferryman", "Ferryman", FerrymanVoiceEnabled),
                ("Filth", "Filth", FilthVoiceEnabled),
                ("Geryon", "Geryon", GeryonVoiceEnabled),
                ("Gutterman", "Gutterman", GuttermanVoiceEnabled),
                ("Guttertank", "Guttertank", GuttertankVoiceEnabled),
                ("HideousMass", "Hideous Mass", MassVoiceEnabled),
                ("Idol", "Idol", IdolVoiceEnabled),
                ("Insurrectionist", "Insurrectionist", InsurrectionistVoiceEnabled),
                ("Leviathan", "Leviathan", LeviathanVoiceEnabled),
                ("MaliciousFace", "Malicious Face", MauriceVoiceEnabled),
                ("Mannequin", "Mannequin", MannequinVoiceEnabled),
                ("Mindflayer", "Mindflayer", MindflayerVoiceEnabled),
                ("MirrorReaper", "Mirror Reaper", MirrorReaperVoiceEnabled),
                ("PowerAlt", "Power Alt", PowerAltVoiceEnabled),
                ("Providence", "Providence", ProvidenceVoiceEnabled),
                ("Schism", "Schism", SchismVoiceEnabled),
                ("Sentry", "Sentry", SentryVoiceEnabled),
                ("Soldier", "Soldier", SoldierVoiceEnabled),
                ("Stalker", "Stalker", StalkerVoiceEnabled),
                ("Stray", "Stray", StrayVoiceEnabled),
                ("Streetcleaner", "Streetcleaner", StreetcleanerVoiceEnabled),
                ("Swordsmachine", "Swordsmachine", SwordsmachineVoiceEnabled),
                ("V1", "V1", V1VoiceEnabled),
                ("V2", "V2", V2VoiceEnabled),
                ("Virtue", "Virtue", VirtueVoiceEnabled),
                ("Wicked", "Wicked", WickedVoiceEnabled)
            };

            foreach (var (key, displayName, toggle) in volumeEnemies)
            {
                FloatSliderField slider = new FloatSliderField(
                    EnemyVolumePanel,
                    $"{displayName} Volume",
                    $"volume_{key.ToLowerInvariant()}",
                    new Tuple<float, float>(0f, 200f),
                    100f,
                    0
                );

                EnemyVolumeFields[key] = slider;

                slider.hidden = !toggle.value;
                toggle.postValueChangeEvent += (bool enabled) => slider.hidden = !enabled;
            }

            PowerAltVolumeField = EnemyVolumeFields["PowerAlt"];

            LoadAssets();

            harmony = new Harmony("com.achelia.ultravoiceretaken");
            harmony.PatchAll();

            SceneManager.sceneLoaded += OnSceneLoaded;

            StartCoroutine(UpdateChecker.CheckForUpdates(Logger));

            Logger.LogInfo("UltraVoice Retaken loaded successfully!");
        }

        void OnDestroy()
        {
            harmony.UnpatchSelf();
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            ResetEnemyStates();

            if (scene.name == "4f8ecffaa98c2614f89922daf31fa22d")
                IntroTagline.ApplyToScene();

            UpdateChecker.OnSceneLoaded();
        }

        void ResetEnemyStates()
        {
            SwordsmachineCharacter.FirstFightDone = false;
            SwordsmachineCharacter.FirstFightLinePlayed = false;

            V2Character.V2IntroTime = -999f;
            V2Character.V2CutsceneVoicePlayed = false;
            V2Character.V2SecondVoiceRestartPlayed = false;
            V2Character.V2DeathPlayed = false;
            V1Character.V2BossKillPlayed = false;
            V1Character.AlreadyPickedUp.Clear();

            FerrymanCharacter.FerrymanCoinTossed = false;
            FerrymanCharacter.FerrymanPhaseChangePlayed = false;

            GuttertankCharacter.GuttertankSpawnInMirror = false;

            GeryonCharacter.EnrageLinePlayed = false;
            GeryonCharacter.CanRestartFight = false;

            PowerCharacter.RollVoice();
            PowerCharacter.ResetIntroRolls();
        }

        void LoadAssets()
        {
            CerberusCharacter.LoadVoiceLines(Logger);
            EarthmoverCharacter.LoadVoiceLines(Logger);
            FerrymanCharacter.LoadVoiceLines(Logger);
            FilthCharacter.LoadVoiceLines(Logger);
            GeryonCharacter.LoadVoiceLines(Logger);
            GuttermanCharacter.LoadVoiceLines(Logger);
            GuttertankCharacter.LoadVoiceLines(Logger);
            HideousMassCharacter.LoadVoiceLines(Logger);
            IdolCharacter.LoadVoiceLines(Logger);
            InsurrectionistCharacter.LoadVoiceLines(Logger);
            LeviathanCharacter.LoadVoiceLines(Logger);
            MaliciousFaceCharacter.LoadVoiceLines(Logger);
            MannequinCharacter.LoadVoiceLines(Logger);
            MindflayerCharacter.LoadVoiceLines(Logger);
            MirrorReaperCharacter.LoadVoiceLines(Logger);
            PowerCharacter.LoadVoiceLines(Logger);
            ProvidenceCharacter.LoadVoiceLines(Logger);
            SchismCharacter.LoadVoiceLines(Logger);
            SentryCharacter.LoadVoiceLines(Logger);
            SoldierCharacter.LoadVoiceLines(Logger);
            StalkerCharacter.LoadVoiceLines(Logger);
            StrayCharacter.LoadVoiceLines(Logger);
            StreetcleanerCharacter.LoadVoiceLines(Logger);
            SwordsmachineCharacter.LoadVoiceLines(Logger);
            V1Character.LoadVoiceLines(Logger);
            V2Character.LoadVoiceLines(Logger);
            VirtueCharacter.LoadVoiceLines(Logger);
            WickedCharacter.LoadVoiceLines(Logger);
        }

        public static float GetVoiceVolume(string voiceName)
        {
            float master = MasterVolumeField != null ? MasterVolumeField.value / 100f : 1f;

            if (string.IsNullOrEmpty(voiceName))
                return master;

            if (voiceName.StartsWith("Agony") || voiceName.StartsWith("Tundra"))
                voiceName = "Swordsmachine";

            float character = 1f;

            foreach (var pair in EnemyVolumeFields)
            {
                if (voiceName.StartsWith(pair.Key))
                {
                    character = pair.Value.value / 100f;
                    break;
                }
            }

            return master * character * GetAltVoiceBaseVolume(voiceName);
        }

        static float GetAltVoiceBaseVolume(string voiceName)
        {
            if (voiceName.StartsWith("Gutterman") && GuttermanVoiceActorField != null && GuttermanVoiceActorField.value == GuttermanVoiceActor.Lemen)
                return 0.6f;

            if (voiceName.StartsWith("Ferryman") && FerrymanVoiceActorField != null && FerrymanVoiceActorField.value == FerrymanVoiceActor.Soil)
                return 0.6f;

            if (voiceName.StartsWith("Cerberus") && CerberusVoiceActorField != null && CerberusVoiceActorField.value == CerberusVoiceActor.Soil)
                return 0.6f;

            if (voiceName.StartsWith("Idol") && IdolVoiceActorField != null && IdolVoiceActorField.value == IdolVoiceActor.Virchew)
                return 2.5f;

            if (voiceName == "Geryon")
                return 1.6f;

            if (voiceName == "V1")
                return 1.7f;

            return 1f;
        }

        public static AudioClip[] LoadClips(string format, int count)
        {
            AudioClip[] clips = new AudioClip[count];

            for (int i = 0; i < count; i++)
                clips[i] = LoadClip(string.Format(format, i + 1));

            return clips;
        }

        public static AudioClip LoadClip(string resourcePath)
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();

                if (!resourcePath.StartsWith("UltraVoice.Resources."))
                    resourcePath = $"UltraVoice.Resources.{resourcePath}";

                using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
                {
                    if (stream == null)
                    {
                        Instance.Logger.LogWarning($"UltraVoice missing clip: {resourcePath}");
                        return null;
                    }

                    byte[] audioData = new byte[stream.Length];
                    stream.Read(audioData, 0, audioData.Length);

                    AudioClip clip = WavUtility.ToAudioClip(audioData);

                    if (clip == null)
                    {
                        Instance.Logger.LogWarning($"UltraVoice failed to convert clip: {resourcePath}");
                        return null;
                    }

                    return clip;
                }
            }
            catch (System.Exception e)
            {
                Instance.Logger.LogError($"UltraVoice error loading clip {resourcePath}: {e}");
                return null;
            }
        }

    }
}