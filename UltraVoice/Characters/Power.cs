using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UltraVoice.Utilities;
using UnityEngine;

namespace UltraVoice.Characters
{
    public class PowerCharacter
    {
        public static readonly string[] VoiceActors = { "Cazsu", "Goober", "Mel", "NotoFem", "NotoMasc" };

        static readonly Dictionary<string, AudioClip>[] AltClipSets = new Dictionary<string, AudioClip>[VoiceActors.Length];

        static readonly List<int> VoicePool = new List<int> { 0 };

        static readonly Dictionary<string, float> VoiceBaseVolumes = new Dictionary<string, float>
        {
            { "Cazsu", 2.1f },
            { "Goober", 2.1f },
            { "Mel", 2.1f },
            { "NotoFem", 2.1f },
            { "NotoMasc", 2.1f }
        };

        static readonly Dictionary<string, AudioClip> SwappedOriginals = new Dictionary<string, AudioClip>();

        public static int CurrentVoice = 0;

        public static readonly Dictionary<int, int> InstanceVoices = new Dictionary<int, int>();

        public static readonly Queue<int> PendingIntroVoices = new Queue<int>();

        public static readonly Queue<AudioSource> PendingIntroSources = new Queue<AudioSource>();

        public static readonly HashSet<int> ScriptedVoiceHistory = new HashSet<int>();

        static readonly string[] VanillaClipNames =
        {
            "pow_CheapShot1", "pow_CheapShot2", "pow_CheapShot3",
            "pow_Death1", "pow_Death2",
            "pow_Enrage1", "pow_Enrage2", "pow_Enrage3", "pow_Enrage4", "pow_Enrage5",
            "pow_Glaive1", "pow_Glaive2",
            "pow_GlaiveThrow1", "pow_GlaiveThrow2",
            "pow_Greatsword1", "pow_Greatsword2",
            "pow_Hurt1", "pow_Hurt2", "pow_Hurt3",
            "pow_HurtBig1", "pow_HurtBig2",
            "pow_Intro1", "pow_Intro2", "pow_Intro3", "pow_Intro4", "pow_Intro5",
            "pow_OverHere1", "pow_overHere2",
            "pow_Rapier1", "pow_Rapier2",
            "pow_ScreamContinuous",
            "pow_Spear1", "pow_Spear2",
            "pow_SpecialIntro1", "pow_SpecialIntro1_Short",
            "pow_SpecialIntro2", "pow_SpecialIntro2_Short",
            "pow_SpecialIntro3", "pow_SpecialIntro4",
            "pow_Taunt1", "pow_Taunt2", "pow_Taunt3", "pow_Taunt4"
        };

        public static void RollVoice()
        {
            CurrentVoice = VoicePool[Random.Range(0, VoicePool.Count)];
        }

        public static int RollExcluding(HashSet<int> used)
        {
            List<int> options = new List<int>();

            foreach (int voice in VoicePool)
                if (!used.Contains(voice))
                    options.Add(voice);

            if (options.Count == 0)
                return VoicePool[Random.Range(0, VoicePool.Count)];

            return options[Random.Range(0, options.Count)];
        }

        public static HashSet<int> ActiveVoices(int excludeInstanceId)
        {
            var used = new HashSet<int>();

            foreach (Power power in Object.FindObjectsOfType<Power>())
            {
                int id = power.GetInstanceID();

                if (id == excludeInstanceId)
                    continue;

                if (power.TryGetComponent<EnemyIdentifier>(out var eid) && eid.dead)
                    continue;

                if (InstanceVoices.TryGetValue(id, out int voice))
                    used.Add(voice);
            }

            return used;
        }

        public static void AssignVoice(int instanceId, int voice)
        {
            InstanceVoices[instanceId] = voice;
        }

        public static void ResetIntroRolls()
        {
            InstanceVoices.Clear();
            PendingIntroVoices.Clear();
            PendingIntroSources.Clear();
            ScriptedVoiceHistory.Clear();
        }

        public static void MoveIntroLineToPower(AudioSource introSource, Power power)
        {
            if (introSource == null || power == null || !introSource.isPlaying || introSource.volume <= 0f)
                return;

            GameObject obj = new GameObject("UltraVoice_PowerIntroLine");
            obj.transform.SetParent(power.transform);
            obj.transform.localPosition = Vector3.zero;

            AudioSource moved = obj.AddComponent<AudioSource>();
            moved.clip = introSource.clip;
            moved.outputAudioMixerGroup = introSource.outputAudioMixerGroup;
            moved.pitch = introSource.pitch;
            moved.volume = introSource.volume;
            moved.spatialBlend = introSource.spatialBlend;
            moved.rolloffMode = introSource.rolloffMode;
            moved.minDistance = introSource.minDistance;
            moved.maxDistance = introSource.maxDistance;
            moved.dopplerLevel = introSource.dopplerLevel;
            moved.priority = introSource.priority;
            moved.time = introSource.time;
            moved.Play();

            introSource.volume = 0f;

            Object.Destroy(obj, moved.clip.length + 1f);
        }

        public static void LoadVoiceLines(BepInEx.Logging.ManualLogSource logger)
        {
            var resourceNames = new HashSet<string>(Assembly.GetExecutingAssembly().GetManifestResourceNames());

            for (int i = 0; i < VoiceActors.Length; i++)
            {
                string actor = VoiceActors[i];
                var set = new Dictionary<string, AudioClip>();
                AltClipSets[i] = set;

                foreach (string vanillaName in VanillaClipNames)
                {
                    string resourcePath = $"UltraVoice.Resources.Power.{actor}.{vanillaName}{actor}.wav";

                    if (!resourceNames.Contains(resourcePath))
                        continue;

                    AudioClip clip = UltraVoicePlugin.LoadClip(resourcePath);

                    if (clip == null)
                        continue;

                    clip = VoiceManager.ToMono(clip);
                    clip.name = $"{vanillaName}{actor}";
                    set[vanillaName] = clip;
                }

                if (set.Count > 0)
                    VoicePool.Add(i + 1);
            }

            logger.LogInfo("Power voice lines loaded successfully!");
        }

        static readonly Dictionary<string, AudioClip> ScaledCache = new Dictionary<string, AudioClip>();
        static readonly Dictionary<string, float> ScaledGain = new Dictionary<string, float>();

        public static AudioClip GetVoiceClip(int voice, AudioClip vanilla)
        {
            if (voice <= 0 || voice > VoiceActors.Length || vanilla == null)
                return null;

            if (!AltClipSets[voice - 1].TryGetValue(vanilla.name, out AudioClip alt) || alt == null)
                return null;

            return WithVolume(alt, VoiceActors[voice - 1]);
        }

        static AudioClip WithVolume(AudioClip alt, string actor)
        {
            if (!VoiceBaseVolumes.TryGetValue(actor, out float baseVolume))
                baseVolume = 1f;

            float gain = (UltraVoicePlugin.PowerAltVolumeField != null
                ? UltraVoicePlugin.PowerAltVolumeField.value / 100f
                : 1f) * baseVolume;

            if (Mathf.Approximately(gain, 1f))
                return alt;

            if (ScaledCache.TryGetValue(alt.name, out AudioClip cached)
                && cached != null
                && Mathf.Approximately(ScaledGain[alt.name], gain))
                return cached;

            AudioClip scaled = VoiceManager.AmplifyClip(alt, gain);
            scaled.name = alt.name + "_scaled";

            ScaledCache[alt.name] = scaled;
            ScaledGain[alt.name] = gain;

            return scaled;
        }

        public static void NormalizeSharedClips(AudioClip[] clips)
        {
            if (clips == null)
                return;

            for (int i = 0; i < clips.Length; i++)
            {
                AudioClip clip = clips[i];

                if (clip == null)
                    continue;

                if (SwappedOriginals.TryGetValue(clip.name, out AudioClip original))
                {
                    clips[i] = original;
                    clip = original;
                }

                AudioClip alt = GetVoiceClip(CurrentVoice, clip);
                if (alt != null)
                {
                    SwappedOriginals[alt.name] = clip;
                    clips[i] = alt;
                }
            }
        }
    }

    [HarmonyPatch(typeof(Power), "Start")]
    class PowerSpawnRollPatch
    {
        static void Postfix(Power __instance)
        {
            if (!UltraVoicePlugin.VoicesEnabled || !UltraVoicePlugin.PowerAltVoiceEnabled.value)
                return;

            int id = __instance.GetInstanceID();

            if (PowerCharacter.InstanceVoices.ContainsKey(id))
                return;

            if (PowerCharacter.PendingIntroVoices.Count > 0)
            {
                int pending = PowerCharacter.PendingIntroVoices.Dequeue();
                PowerCharacter.CurrentVoice = pending;
                PowerCharacter.AssignVoice(id, pending);

                if (PowerCharacter.PendingIntroSources.Count > 0)
                    PowerCharacter.MoveIntroLineToPower(PowerCharacter.PendingIntroSources.Dequeue(), __instance);

                return;
            }

            int voice = PowerCharacter.RollExcluding(PowerCharacter.ActiveVoices(id));
            PowerCharacter.CurrentVoice = voice;
            PowerCharacter.AssignVoice(id, voice);
        }
    }

    [HarmonyPatch(typeof(PowerVoiceController), "Intro")]
    class PowerIntroVoicePatch
    {
        static void Postfix(ref AudioClip __result)
        {
            if (!UltraVoicePlugin.VoicesEnabled || !UltraVoicePlugin.PowerAltVoiceEnabled.value)
                return;

            AudioClip alt = PowerCharacter.GetVoiceClip(PowerCharacter.CurrentVoice, __result);

            if (alt != null)
                __result = alt;
        }
    }

    [HarmonyPatch(typeof(Power), nameof(Power.PlaySound))]
    class PowerPlaySoundPatch
    {
        static void Prefix(Power __instance, ref AudioClip clip)
        {
            if (!UltraVoicePlugin.VoicesEnabled || !UltraVoicePlugin.PowerAltVoiceEnabled.value)
                return;

            if (clip == null)
                return;

            if (!PowerCharacter.InstanceVoices.TryGetValue(__instance.GetInstanceID(), out int voice))
                voice = PowerCharacter.CurrentVoice;

            AudioClip alt = PowerCharacter.GetVoiceClip(voice, clip);

            if (alt != null)
                clip = alt;
        }
    }

    [HarmonyPatch(typeof(PowerVoiceController), "FallScream")]
    class PowerFallScreamPatch
    {
        static void Postfix(ref AudioClip __result)
        {
            if (!UltraVoicePlugin.VoicesEnabled || !UltraVoicePlugin.PowerAltVoiceEnabled.value)
                return;

            AudioClip alt = PowerCharacter.GetVoiceClip(PowerCharacter.CurrentVoice, __result);

            if (alt != null)
                __result = alt;
        }
    }

    [HarmonyPatch(typeof(PowerIntro), "Activate")]
    class PowerScriptedIntroPatch
    {
        static void Prefix(PowerIntro __instance)
        {
            if (!UltraVoicePlugin.VoicesEnabled || !UltraVoicePlugin.PowerAltVoiceEnabled.value)
                return;

            HashSet<int> used = PowerCharacter.ActiveVoices(0);
            foreach (int queued in PowerCharacter.PendingIntroVoices)
                used.Add(queued);

            if (SceneHelper.CurrentScene == "Level 8-3")
                foreach (int past in PowerCharacter.ScriptedVoiceHistory)
                    used.Add(past);

            PowerCharacter.CurrentVoice = PowerCharacter.RollExcluding(used);
            used.Add(PowerCharacter.CurrentVoice);
            PowerCharacter.ScriptedVoiceHistory.Add(PowerCharacter.CurrentVoice);

            Power[] children = __instance.GetComponentsInChildren<Power>(true);
            if (children.Length > 0)
            {
                bool first = true;
                foreach (Power child in children)
                {
                    int voice = first ? PowerCharacter.CurrentVoice : PowerCharacter.RollExcluding(used);
                    first = false;
                    used.Add(voice);
                    PowerCharacter.ScriptedVoiceHistory.Add(voice);
                    PowerCharacter.AssignVoice(child.GetInstanceID(), voice);
                }
            }
            else
            {
                PowerCharacter.PendingIntroVoices.Enqueue(PowerCharacter.CurrentVoice);
            }

            AudioClip altIntro = PowerCharacter.GetVoiceClip(PowerCharacter.CurrentVoice, __instance.introOverride);
            if (altIntro != null)
                __instance.introOverride = altIntro;

            if (__instance.persistentData != null)
                PowerCharacter.NormalizeSharedClips(__instance.persistentData.RepeatedIntroClips);
        }

        static void Postfix(PowerIntro __instance)
        {
            if (!UltraVoicePlugin.VoicesEnabled || !UltraVoicePlugin.PowerAltVoiceEnabled.value)
                return;

            if (!__instance.TryGetComponent<AudioSource>(out AudioSource introSource) || !introSource.isPlaying)
                return;

            if (__instance.GetComponentsInChildren<Power>(true).Length > 0)
                UltraVoicePlugin.Instance.StartCoroutine(HandOffToChild(__instance.gameObject, introSource));
            else
                PowerCharacter.PendingIntroSources.Enqueue(introSource);
        }

        static IEnumerator HandOffToChild(GameObject introObject, AudioSource introSource)
        {
            for (float waited = 0f; waited < 6f; waited += Time.deltaTime)
            {
                if (introObject == null || introSource == null || !introSource.isPlaying || introSource.volume <= 0f)
                    yield break;

                Power power = introObject.GetComponentInChildren<Power>();
                if (power != null)
                {
                    PowerCharacter.MoveIntroLineToPower(introSource, power);
                    yield break;
                }

                yield return null;
            }
        }
    }

    [HarmonyPatch(typeof(SubtitleController), nameof(SubtitleController.DisplaySubtitle),
        new[] { typeof(string), typeof(AudioSource), typeof(bool) })]
    public class PowerSubtitlePatch
    {
        private static readonly Color PowerYellow = new Color(0.855f, 0.776f, 0.384f);

        private static readonly string[] PowerLines = new[]
        {
            "Be afraid, machine.",
            "Here shall be your grave.",
            "It is over, machine!",
            "Surrender or perish!",
            "Lay down and die!",
            "Bastard!",
            "You piece of SHIT!",
            "Just DIE already!",
            "Why won't you die!?",
            "God DAMN it!",
            "This lowly thing could never have bested him!",
            "An inconvenience at best.",
            "This is a waste of my time!",
            "Just another worthless object.",
            "PAY ATTENTION!",
            "Wait your TURN!",
            "WRONG TARGET!",
            "Rapier!",
            "Greatsword!",
            "Spear!",
            "Over here!",
            "Glaive!",
            "Take THIS!",
            "HALT!",
            "Where is Gabriel and what have you done to him?",
            "Enough!",
            "Your insolence must be punished.",
            "There is no escape from Gabriel's children.",
            "WHERE IS HE!?"
        };

        public static void Postfix(string caption)
        {
            if (!UltraVoicePlugin.PowerSubtitleColorEnabled.value)
                return;

            bool isPowerLine = PowerLines.Contains(caption);
            if (!isPowerLine)
                return;

            if (caption == "Enough!" && SceneHelper.CurrentScene != "Level 8-3")
                return;

            var controller = MonoSingleton<SubtitleController>.Instance;

            var container = controller.container;

            var lastChild = container.GetChild(container.childCount - 1);
            var subtitleComp = lastChild.GetComponent<Subtitle>();

            var text = subtitleComp.GetComponentInChildren<TMP_Text>();
            text.color = PowerYellow;
        }
    }
}