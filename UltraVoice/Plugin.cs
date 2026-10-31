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
using PluginConfig.API;
using PluginConfig.API.Fields;
using UltraVoice.Characters;
using UltraVoice.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UltraVoice
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency("com.eternalUnion.pluginConfigurator")]
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
        public static BoolField ShopTerminalVoiceEnabled;
        public static BoolField PowerAltVoiceEnabled;
        public static BoolField DeathcatcherVoiceEnabled;
        public static BoolField MasterVoiceEnabled;
        public static BoolField SubtitleColorsEnabled;
        public static FloatSliderField VoiceCooldown;
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
        public static EnumField<ShopTerminalVoiceActor> ShopTerminalVoiceActorField;
        public static EnumField<GeryonVoiceActor> GeryonVoiceActorField;
        public static EnumField<EarthmoverVoiceActor> EarthmoverVoiceActorField;
        public static EnumField<PowerVoiceActor> PowerVoiceActorField;
        public static ConfigPanel PowerVoicePoolPanel;
        public static Dictionary<string, BoolField> PowerVoiceToggles = new Dictionary<string, BoolField>();

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
        public static EnumField<V1VoiceActor> V1VoiceActorField;
        public static EnumField<V2VoiceActor> V2VoiceActorField;
        public static ConfigPanel TogglesPanel;
        public static ConfigPanel EnemyTogglesPanel;
        public static ConfigPanel ActorPanel;
        public static ConfigPanel VolumePanel;
        public static ConfigPanel EnemyVolumePanel;
        public static ConfigPanel V1LinesPanel;
        public static ConfigPanel SubtitleColorsPanel;
        public static ConfigPanel CharacterSubtitleColorsPanel;
        public static readonly Dictionary<string, BoolField> SubtitleColorToggles = new Dictionary<string, BoolField>();

        public static Dictionary<string, BoolField> V1LineToggles = new Dictionary<string, BoolField>();

        public static bool V1LineEnabled(string key)
        {
            if (V1VoiceEnabled == null || !V1VoiceEnabled.value)
                return false;

            return !V1LineToggles.TryGetValue(key, out BoolField field) || field.value;
        }

        public static bool VoicesEnabled => MasterVoiceEnabled == null || MasterVoiceEnabled.value;

        public static bool SubtitleColorAllowed(string voiceName)
        {
            if (SubtitleColorsEnabled != null && !SubtitleColorsEnabled.value)
                return false;

            if (string.IsNullOrEmpty(voiceName))
                return true;

            if (voiceName.StartsWith("Agony") || voiceName.StartsWith("Tundra"))
                voiceName = "Swordsmachine";

            foreach (var pair in SubtitleColorToggles)
            {
                if (voiceName.StartsWith(pair.Key))
                    return pair.Value.value;
            }

            return true;
        }

        public static FloatSliderField MasterVolumeField;
        public static FloatSliderField PowerAltVolumeField;
        public static Dictionary<string, FloatSliderField> EnemyVolumeFields = new Dictionary<string, FloatSliderField>();

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

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
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
            WickedCharacter.LoadVoiceLines(Logger);
            StalkerCharacter.LoadVoiceLines(Logger);
            StrayCharacter.LoadVoiceLines(Logger);
            StreetcleanerCharacter.LoadVoiceLines(Logger);
            SwordsmachineCharacter.LoadVoiceLines(Logger);
            V1Character.LoadVoiceLines(Logger);
            V2Character.LoadVoiceLines(Logger);
            VirtueCharacter.LoadVoiceLines(Logger);
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