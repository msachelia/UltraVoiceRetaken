using System.Collections.Generic;
using UnityEngine;

namespace UltraVoice.Utilities
{
    public class VoiceManager
    {
        private static float lastVoiceTime = -999f;
        private static Dictionary<int, float> enemyVoiceCooldown = new Dictionary<int, float>();
        public static Dictionary<UnityEngine.Component, float> spawnVoiceEndTimes = new Dictionary<UnityEngine.Component, float>();
        public static Dictionary<UnityEngine.Component, float> enemySpawnTimes = new Dictionary<UnityEngine.Component, float>();

        public static UnityEngine.Color GetEnemyTypeColor(EnemyType enemyType)
        {
            return enemyType switch
            {
                EnemyType.V2 => new UnityEngine.Color(0.878f, 0.302f, 0.282f),
                EnemyType.Swordsmachine => new UnityEngine.Color(0.91f, 0.6f, 0.05f),
                EnemyType.Streetcleaner => new UnityEngine.Color(0.82f, 0.30f, 0.09f),
                EnemyType.Cerberus => new UnityEngine.Color(0.65f, 0.65f, 0.65f),
                EnemyType.Mindflayer => new UnityEngine.Color(0.26f, 0.89f, 0.74f),
                EnemyType.Virtue => new UnityEngine.Color(0.4f, 0.75f, 0.94f),
                EnemyType.Gutterman => new UnityEngine.Color(0.91f, 0.73f, 0.51f),
                EnemyType.Ferryman => new UnityEngine.Color(0f, 0.66f, 0.77f),
                EnemyType.Guttertank => new UnityEngine.Color(0.8f, 0.07f, 0.07f),
                EnemyType.Mannequin => new UnityEngine.Color(0.91f, 0.91f, 0.91f),
                EnemyType.Providence => new UnityEngine.Color(0.9f, 0.79f, 0.86f),
                EnemyType.Turret => new UnityEngine.Color(0.51f, 0.68f, 0.1f),
                EnemyType.MaliciousFace => new UnityEngine.Color(0.76f, 0.66f, 0.56f),
                EnemyType.MirrorReaper => new UnityEngine.Color(0.9f, 0.85f, 0.76f),
                EnemyType.Sisyphus => new UnityEngine.Color(0.79f, 0.58f, 0.49f),
                EnemyType.HideousMass => new UnityEngine.Color(0.67f, 0.57f, 0.57f),
                EnemyType.Idol => new UnityEngine.Color(0.62f, 0.87f, 0.92f),
                _ => UnityEngine.Color.white
            };
        }

        public static UnityEngine.Color? GetSubtitleColor(UnityEngine.Component enemy, UnityEngine.Color? colorOverride)
        {
            if (colorOverride.HasValue)
                return colorOverride.Value;

            if (enemy.TryGetComponent<EnemyIdentifier>(out var eid))
            {
                return GetEnemyTypeColor(eid.enemyType);
            }

            return null;
        }

        public static bool CheckCooldown(UnityEngine.Component enemy, float cooldown)
        {
            int id = enemy.GetInstanceID();

            if (enemyVoiceCooldown.TryGetValue(id, out float last))
            {
                if (Time.time - last < cooldown)
                    return false;
            }

            enemyVoiceCooldown[id] = Time.time;
            return true;
        }

        public static bool CheckGlobalCooldown()
        {
            if (Time.time - lastVoiceTime < UltraVoicePlugin.VoiceCooldown.value)
                return false;

            lastVoiceTime = Time.time;
            return true;
        }

        public static bool IsSpawnVoicePlaying(UnityEngine.Component enemy)
        {
            if (!spawnVoiceEndTimes.TryGetValue(enemy, out float endTime))
                return false;

            return Time.time < endTime;
        }

        public static bool IsEnemyVoicePlaying(UnityEngine.Component enemy)
        {
            foreach (Transform child in enemy.transform)
            {
                if (child.name.StartsWith("UltraVoice_"))
                    return true;
            }
            return false;
        }

        public static bool TooSoonAfterSpawn(UnityEngine.Component enemy, float delay)
        {
            if (!enemySpawnTimes.TryGetValue(enemy, out float spawnTime))
                return false;

            return Time.time - spawnTime < delay;
        }

        public static AudioClip ToMono(AudioClip clip)
        {
            if (clip == null || clip.channels == 1)
                return clip;

            int channels = clip.channels;
            float[] samples = new float[clip.samples * channels];
            clip.GetData(samples, 0);

            float[] mono = new float[clip.samples];
            for (int i = 0; i < mono.Length; i++)
            {
                float sum = 0f;
                for (int c = 0; c < channels; c++)
                    sum += samples[i * channels + c];
                mono[i] = sum / channels;
            }

            AudioClip result = AudioClip.Create(clip.name, clip.samples, 1, clip.frequency, false);
            result.SetData(mono, 0);

            return result;
        }

        public static AudioClip AmplifyClip(AudioClip clip, float gain)
        {
            float[] samples = new float[clip.samples * clip.channels];
            clip.GetData(samples, 0);

            for (int i = 0; i < samples.Length; i++)
                samples[i] = Mathf.Clamp(samples[i] * gain, -1f, 1f);

            AudioClip amplified = AudioClip.Create(clip.name + "_amp", clip.samples, clip.channels, clip.frequency, false);
            amplified.SetData(samples, 0);

            return amplified;
        }

        public static void ApplyVolume(AudioSource src, float gain)
        {
            if (gain > 1f && src.clip != null)
            {
                src.clip = AmplifyClip(src.clip, gain);
                src.volume = 1f;
                UnityEngine.Object.Destroy(src.clip, src.clip.length + 2f);
            }
            else
            {
                src.volume = gain;
            }
        }

        public static AudioSource CreateVoiceSource(
            UnityEngine.Component enemy,
            string name,
            AudioClip clip,
            string subtitle = null,
            bool shouldInterrupt = false,
            UnityEngine.Color? subtitleColor = null,
            bool randomPitch = false,
            float spatialBlend = 1f,
            float volumeMult = 1f,
            bool parentToEnemy = true,
            float maxDistance = 200f
        )
        {
            if (!UltraVoicePlugin.VoicesEnabled)
                return null;

            if (enemy == null || clip == null)
                return null;

            if (enemy.gameObject.name.Contains("Big Johninator"))
                return null;

            if (enemy.gameObject.TryGetComponent<EnemyIdentifier>(out var eid) && eid.puppet)
                return null;

            bool ignoreGlobalCooldown = name == "V1";

            if (!shouldInterrupt)
            {
                if (!ignoreGlobalCooldown && !CheckGlobalCooldown())
                    return null;

                if (IsEnemyVoicePlaying(enemy))
                    return null;
            }
            else
            {
                InterruptVoices(enemy);
            }

            if (!ignoreGlobalCooldown)
                lastVoiceTime = Time.time;

            GameObject obj = new GameObject($"UltraVoice_{name}");

            if (parentToEnemy)
            {
                obj.transform.SetParent(enemy.transform);
                obj.transform.localPosition = UnityEngine.Vector3.zero;
            }
            else
            {
                obj.transform.position = enemy.transform.position;
            }

            var src = obj.AddComponent<AudioSource>();

            src.clip = clip;
            src.spatialBlend = spatialBlend;

            ApplyVolume(src, Mathf.Min(volumeMult, 1f) * UltraVoicePlugin.GetVoiceVolume(name));
            src.minDistance = 50f;
            src.maxDistance = maxDistance;
            src.dopplerLevel = 0.25f;

            if (randomPitch)
                src.pitch = UnityEngine.Random.Range(0.95f, 1.05f);

            var mixer = MonoSingleton<AudioMixerController>.Instance;
            if (mixer != null)
                src.outputAudioMixerGroup = mixer.allGroup;

            src.Play();

            var finalColor = GetSubtitleColor(enemy, subtitleColor);

            if (!string.IsNullOrEmpty(subtitle))
                ShowSubtitle(subtitle, src, finalColor);

            UnityEngine.Object.Destroy(obj, clip.length + 1f);

            return src;
        }

        public static void PlayRandomVoice(
            UnityEngine.Component enemy,
            string enemyName,
            AudioClip[] clips,
            string[] subtitles,
            bool interrupt = false,
            bool randomPitch = false,
            float volumeMult = 1f,
            UnityEngine.Color? colorOverride = null,
            float spatialBlend = 1f
        )
        {
            if (clips == null || clips.Length == 0)
                return;

            int index = UnityEngine.Random.Range(0, clips.Length);
            string subtitle = null;

            if (subtitles != null && index < subtitles.Length)
                subtitle = subtitles[index];

            CreateVoiceSource(enemy, enemyName, clips[index], subtitle, interrupt, colorOverride, randomPitch: randomPitch, spatialBlend: spatialBlend, volumeMult: volumeMult);
        }

        public static void PlayRandomVoiceDelayed(
            float delay,
            UnityEngine.Component enemy,
            string enemyName,
            AudioClip[] clips,
            string[] subtitles,
            bool interrupt = false,
            bool randomPitch = false,
            float volumeMult = 1f,
            UnityEngine.Color? colorOverride = null,
            float spatialBlend = 1f
        )
        {
            UltraVoicePlugin.Instance.StartCoroutine(Routine());

            System.Collections.IEnumerator Routine()
            {
                yield return new WaitForSeconds(delay);

                if (enemy == null)
                    yield break;

                PlayRandomVoice(enemy, enemyName, clips, subtitles, interrupt, randomPitch, volumeMult, colorOverride, spatialBlend);
            }
        }

        public static void ShowSubtitle(string text, AudioSource src, UnityEngine.Color? color = null)
        {
            if (!string.IsNullOrEmpty(text))
            {
                if (color.HasValue)
                {
                    string coloredText = $"<color=#{ColorUtility.ToHtmlStringRGB(color.Value)}>{text}</color>";
                    MonoSingleton<SubtitleController>.Instance.DisplaySubtitle(coloredText, src);
                }
                else
                {
                    MonoSingleton<SubtitleController>.Instance.DisplaySubtitle(text, src);
                }
            }
        }

        public static void InterruptVoices(UnityEngine.Component enemy)
        {
            foreach (Transform child in enemy.transform)
            {
                if (child.name.StartsWith("UltraVoice_"))
                    UnityEngine.Object.Destroy(child.gameObject);
            }
        }
    }
}