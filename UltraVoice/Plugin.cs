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
    separately under VOICE_ACTING_LICENSE.md and are not covered by the GPL.
*/

using BepInEx;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ThornClient.Core;
using ThornClient.Core.ConfigurableElements;
using ThornClient.Managers;
using UltraVoice.Characters;
using UltraVoice.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UltraVoice
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency("com.github.end-4.thornClient")]
    [BepInDependency("com.github.end-4.notiffy")]
    public class UltraVoicePlugin : BaseUnityPlugin
    {
        public const string PluginGuid = "com.achelia.ultravoiceretaken";
        public const string PluginName = "UltraVoice Retaken";
        public const string PluginVersion = "2.0.0";

        public static UltraVoicePlugin Instance;
        public static VoiceManager VoiceManager;
        private Harmony harmony;

        public static VoiceConfig ConfigModule;

        public static Setting<bool> CerberusVoiceEnabled;
        public static Setting<bool> SwordsmachineVoiceEnabled;
        public static Setting<bool> V2VoiceEnabled;
        public static Setting<bool> StreetcleanerVoiceEnabled;
        public static Setting<bool> MindflayerVoiceEnabled;
        public static Setting<bool> VirtueVoiceEnabled;
        public static Setting<bool> FerrymanVoiceEnabled;
        public static Setting<bool> GuttermanVoiceEnabled;
        public static Setting<bool> MannequinVoiceEnabled;
        public static Setting<bool> GuttertankVoiceEnabled;
        public static Setting<bool> ProvidenceVoiceEnabled;
        public static Setting<bool> SentryVoiceEnabled;
        public static Setting<bool> MauriceVoiceEnabled;
        public static Setting<bool> EarthmoverVoiceEnabled;
        public static Setting<bool> MirrorReaperVoiceEnabled;
        public static Setting<bool> GeryonVoiceEnabled;
        public static Setting<bool> LeviathanVoiceEnabled;
        public static Setting<bool> InsurrectionistVoiceEnabled;
        public static Setting<bool> FilthVoiceEnabled;
        public static Setting<bool> StrayVoiceEnabled;
        public static Setting<bool> SchismVoiceEnabled;
        public static Setting<bool> SoldierVoiceEnabled;
        public static Setting<bool> StalkerVoiceEnabled;
        public static Setting<bool> MassVoiceEnabled;
        public static Setting<bool> IdolVoiceEnabled;
        public static Setting<bool> WickedVoiceEnabled;
        public static Setting<bool> V1VoiceEnabled;
        public static Setting<bool> ShopTerminalVoiceEnabled;
        public static Setting<bool> PowerAltVoiceEnabled;
        public static Setting<bool> DeathcatcherVoiceEnabled;
        public static Setting<bool> MasterVoiceEnabled;
        public static Setting<bool> SubtitleColorsEnabled;
        public static Setting<float> VoiceCooldown;
        public static Setting<int> SubtitleLimit;

        public static Setting<SwordsmachineVoiceActor> SwordsmachineVoiceActorField;
        public static Setting<SentryVoiceActor> SentryVoiceActorField;
        public static Setting<GuttertankVoiceActor> GuttertankVoiceActorField;
        public static Setting<GuttermanVoiceActor> GuttermanVoiceActorField;
        public static Setting<CerberusVoiceActor> CerberusVoiceActorField;
        public static Setting<FerrymanVoiceActor> FerrymanVoiceActorField;
        public static Setting<IdolVoiceActor> IdolVoiceActorField;
        public static Setting<VirtueVoiceActor> VirtueVoiceActorField;
        public static Setting<MirrorReaperVoiceActor> MirrorReaperVoiceActorField;
        public static Setting<ShopTerminalVoiceActor> ShopTerminalVoiceActorField;
        public static Setting<GeryonVoiceActor> GeryonVoiceActorField;
        public static Setting<EarthmoverVoiceActor> EarthmoverVoiceActorField;
        public static Setting<PowerVoiceActor> PowerVoiceActorField;
        public static SettingGroup PowerVoicePoolPanel;
        public static Dictionary<string, Setting<bool>> PowerVoiceToggles = new Dictionary<string, Setting<bool>>();

        public static readonly string[] PowerVoiceOrder = { "Cazsu", "Goober", "NotoMasc", "Vanilla", "Mel", "NotoFem" };

        public static readonly Dictionary<string, string> PowerVoiceNames = new Dictionary<string, string>
        {
            { "Cazsu", "Cazsu (Masc)" },
            { "Goober", "Goober (Masc)" },
            { "NotoMasc", "Noto (Masc)" },
            { "Vanilla", "Kennady (Fem)" },
            { "Mel", "Mel (Fem)" },
            { "NotoFem", "Noto (Fem)" }
        };
        public static Setting<V1VoiceActor> V1VoiceActorField;
        public static Setting<V2VoiceActor> V2VoiceActorField;
        public static SettingGroup TogglesPanel;
        public static SettingGroup EnemyTogglesPanel;
        public static SettingGroup ActorPanel;
        public static SettingGroup VolumePanel;
        public static SettingGroup EnemyVolumePanel;
        public static SettingGroup V1LinesPanel;
        public static SettingGroup SubtitleColorsPanel;
        public static SettingGroup CharacterSubtitleColorsPanel;
        public static readonly Dictionary<string, Setting<bool>> SubtitleColorToggles = new Dictionary<string, Setting<bool>>();

        public static Dictionary<string, Setting<bool>> V1LineToggles = new Dictionary<string, Setting<bool>>();

        public static bool V1LineEnabled(string key)
        {
            if (V1VoiceEnabled == null || !V1VoiceEnabled.Value)
                return false;

            return !V1LineToggles.TryGetValue(key, out Setting<bool> field) || field.Value;
        }

        public static bool VoicesEnabled => MasterVoiceEnabled == null || MasterVoiceEnabled.Value;

        public static bool SubtitleColorAllowed(string voiceName)
        {
            if (SubtitleColorsEnabled != null && !SubtitleColorsEnabled.Value)
                return false;

            if (string.IsNullOrEmpty(voiceName))
                return true;

            if (voiceName.StartsWith("Agony") || voiceName.StartsWith("Tundra"))
                voiceName = "Swordsmachine";

            foreach (var pair in SubtitleColorToggles)
            {
                if (voiceName.StartsWith(pair.Key))
                    return pair.Value.Value;
            }

            return true;
        }

        public static Setting<float> MasterVolumeField;
        public static Setting<float> PowerAltVolumeField;
        public static Dictionary<string, Setting<float>> EnemyVolumeFields = new Dictionary<string, Setting<float>>();

        public enum SwordsmachineVoiceActor
        {
            Mel,
            Noto,
            Soil
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
            Soil,
            Nicky
        }

        public enum V1VoiceActor
        {
            Cyberlord,
            Noto,
            HashCap
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

        public enum ShopTerminalVoiceActor
        {
            Bouncy,
            Goober
        }

        public enum EarthmoverVoiceActor
        {
            Rotund,
            Tondar,
            Megaslash
        }

        public enum GeryonVoiceActor
        {
            TheRatmanValinMegaslash,
            Mel
        }

        public enum PowerVoiceActor
        {
            Random,
            Cazsu,
            Goober,
            NotoMasc,
            Vanilla,
            Mel,
            NotoFem
        }

        void Awake()
        {
            Instance = this;
            VoiceManager = new VoiceManager();

            ConfigModule = new VoiceConfig();
            RegisterConfig();
            ConfigManager.LoadConfig(ConfigModule);

            LoadAssets();

            harmony = new Harmony(PluginGuid);
            harmony.PatchAll();

            SceneManager.sceneLoaded += OnSceneLoaded;

            StartCoroutine(UpdateChecker.CheckForUpdates(Logger));

            Logger.LogInfo("UltraVoice Retaken loaded successfully!");
        }

        void OnDestroy()
        {
            harmony.UnpatchSelf();
        }

        static void RegisterConfig()
        {
            if (ConfigModule == null || ModuleManager.Items == null)
                return;

            ModuleManager.Items.RemoveAll(module =>
                module != null && module.GUID == PluginGuid && !ReferenceEquals(module, ConfigModule));

            foreach (ThornClient.Core.Module module in ModuleManager.Items)
            {
                if (ReferenceEquals(module, ConfigModule))
                    return;
            }

            ModuleManager.Items.Add(ConfigModule);
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            RegisterConfig();

            ResetEnemyStates();

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

            ShopTerminalCharacter.BootUpPlayed = false;
            ShopTerminalCharacter.FlippedPlayed = false;

            if (SceneHelper.CurrentScene != "Endless")
                ShopTerminalCharacter.CybergrindDeath = false;
            else
                StartCoroutine(ShopTerminalCharacter.WaveWatcher());

            EarthmoverCharacter.IntroPlayed = false;
            EarthmoverCharacter.DeathPlayed = false;
            EarthmoverCharacter.lastIdolTime = -999f;

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
            DeathcatcherCharacter.LoadVoiceLines(Logger);
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
            ShopTerminalCharacter.LoadVoiceLines(Logger);
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
            float master = MasterVolumeField != null ? MasterVolumeField.Value / 100f : 1f;

            if (string.IsNullOrEmpty(voiceName))
                return master;

            if (voiceName.StartsWith("Agony") || voiceName.StartsWith("Tundra"))
                voiceName = "Swordsmachine";

            float character = 1f;

            foreach (var pair in EnemyVolumeFields)
            {
                if (voiceName.StartsWith(pair.Key))
                {
                    character = pair.Value.Value / 100f;
                    break;
                }
            }

            return master * character;
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