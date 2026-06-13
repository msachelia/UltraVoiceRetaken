using HarmonyLib;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UltraVoice.Patches
{
    [HarmonyPatch(typeof(SubtitleController), nameof(SubtitleController.DisplaySubtitle),
        new[] { typeof(string), typeof(AudioSource), typeof(bool) })]
    public static class SubtitleLimitPatch
    {
        public static void Prefix(SubtitleController __instance)
        {
            int max = UltraVoicePlugin.SubtitleLimit.value;
            Transform container = __instance.container;
            if (container == null) return;

            int activeCount = 0;
            for (int i = 0; i < container.childCount; i++)
            {
                var sub = container.GetChild(i).GetComponent<Subtitle>();
                if (sub != null && sub.gameObject.activeInHierarchy) activeCount++;
            }

            while (activeCount >= max)
            {
                Subtitle oldest = null;
                int lowestSibling = int.MaxValue;
                for (int i = 0; i < container.childCount; i++)
                {
                    var child = container.GetChild(i);
                    var sub = child.GetComponent<Subtitle>();
                    if (sub == null || !child.gameObject.activeInHierarchy) continue;
                    int idx = child.GetSiblingIndex();
                    if (idx < lowestSibling)
                    {
                        lowestSibling = idx;
                        oldest = sub;
                    }
                }

                if (oldest == null) break;

                Transform parent = oldest.transform.parent;
                Object.Destroy(oldest.gameObject);
                activeCount--;
                if (parent != null) ForceRebuildContainerLayout(parent);
            }
        }

        public static void Postfix(string caption, AudioSource audioSource, bool ignoreSetting, SubtitleController __instance)
        {
            if (audioSource == null) return;
            Transform container = __instance.container;
            if (container == null) return;

            Subtitle created = null;
            for (int i = container.childCount - 1; i >= 0; i--)
            {
                var sub = container.GetChild(i).GetComponent<Subtitle>();
                if (sub != null)
                {
                    created = sub;
                    break;
                }
            }

            if (created == null) return;

            if (created.GetComponent<SubtitleAudioWatcher>() == null)
            {
                var watcher = created.gameObject.AddComponent<SubtitleAudioWatcher>();
                watcher.source = audioSource;
                watcher.controller = __instance;
            }
        }

        public static void ForceRebuildContainerLayout(Transform container)
        {
            var rect = container as RectTransform ?? container.GetComponent<RectTransform>();
            if (rect != null) LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
        }
    }

    public class SubtitleAudioWatcher : MonoBehaviour
    {
        public AudioSource source;
        public SubtitleController controller;

        private Subtitle subtitle;
        private CanvasGroup group;
        private float fadeOutSpeed;
        private bool triggered;
        private Coroutine waitCoroutine;

        private const float WaitBeforeFade = 2f;
        private const float FallbackFadeDuration = 0.6f;

        private void Awake()
        {
            subtitle = GetComponent<Subtitle>();
            // minimal: assume subtitle exists because watcher is attached to it; still guard group lookup
            group = subtitle.group ?? subtitle.GetComponent<CanvasGroup>();
            fadeOutSpeed = subtitle.fadeOutSpeed;
        }

        private void OnDestroy()
        {
            if (waitCoroutine != null) StopCoroutine(waitCoroutine);
        }

        private void Update()
        {
            if (triggered) return;

            if (source == null || !source.isPlaying)
            {
                triggered = true;

                int active = CountActiveSubtitles();
                int max = UltraVoicePlugin.SubtitleLimit.value;
                if (active > max)
                {
                    Transform parent = subtitle.transform.parent;
                    Object.Destroy(subtitle.gameObject);
                    SubtitleLimitPatch.ForceRebuildContainerLayout(parent);
                    Destroy(this);
                    return;
                }

                waitCoroutine = StartCoroutine(WaitThenFade());
            }
        }

        private IEnumerator WaitThenFade()
        {
            float elapsed = 0f;
            while (elapsed < WaitBeforeFade)
            {
                int active = CountActiveSubtitles();
                int max = UltraVoicePlugin.SubtitleLimit.value;
                if (active > max)
                {
                    Transform parent = subtitle.transform.parent;
                    Object.Destroy(subtitle.gameObject);
                    SubtitleLimitPatch.ForceRebuildContainerLayout(parent);
                    Destroy(this);
                    yield break;
                }

                if (source != null && source.isPlaying)
                {
                    triggered = false;
                    yield break;
                }

                elapsed += Time.deltaTime;
                yield return null;
            }

            if (subtitle == null)
            {
                Destroy(this);
                yield break;
            }

            yield return StartCoroutine(FadeOutAndDestroy());
        }

        private int CountActiveSubtitles()
        {
            var container = controller.container;
            if (container != null)
            {
                int c = 0;
                for (int i = 0; i < container.childCount; i++)
                {
                    var s = container.GetChild(i).GetComponent<Subtitle>();
                    if (s != null && s.gameObject.activeInHierarchy) c++;
                }
                return c;
            }

            var all = Object.FindObjectsOfType<Subtitle>();
            int activeCount = 0;
            foreach (var s in all) if (s != null && s.gameObject.activeInHierarchy) activeCount++;
            return activeCount;
        }

        private IEnumerator FadeOutAndDestroy()
        {
            if (group == null)
            {
                Transform parent = subtitle.transform.parent;
                Object.Destroy(subtitle.gameObject);
                SubtitleLimitPatch.ForceRebuildContainerLayout(parent);
                Destroy(this);
                yield break;
            }

            float startAlpha = Mathf.Clamp01(group.alpha);
            float duration = FallbackFadeDuration;
            if (fadeOutSpeed > 0.00001f) duration = Mathf.Max(0.01f, startAlpha / fadeOutSpeed);

            if (duration <= 0.0001f)
            {
                Transform p = subtitle.transform.parent;
                Object.Destroy(subtitle.gameObject);
                SubtitleLimitPatch.ForceRebuildContainerLayout(p);
                Destroy(this);
                yield break;
            }

            float t = 0f;
            while (t < duration)
            {
                t += Time.deltaTime;
                group.alpha = Mathf.Lerp(startAlpha, 0f, t / duration);
                yield return null;
            }

            group.alpha = 0f;
            controller.NotifyHoldEnd(subtitle);
            Transform parentAfter = subtitle.transform.parent;
            Object.Destroy(subtitle.gameObject);
            SubtitleLimitPatch.ForceRebuildContainerLayout(parentAfter);
            Destroy(this);
        }
    }
}