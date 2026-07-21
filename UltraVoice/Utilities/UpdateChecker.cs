using Notiffy.API;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace UltraVoice.Utilities
{
    public class UpdateChecker
    {
        const string MetricsUrl = "https://thunderstore.io/api/v1/package-metrics/achelia/UltraVoice_Retaken/";
        const string PageUrl = "https://thunderstore.io/c/ultrakill/p/achelia/UltraVoice_Retaken/";
        const string ChangelogUrl = "https://thunderstore.io/c/ultrakill/p/achelia/UltraVoice_Retaken/changelog";
        const string OpenPageAction = "ultravoice_open_page";
        const string ViewChangelogAction = "ultravoice_view_changelog";
        const string MainMenuScene = "Main Menu";

        static uint notificationId;
        static string pendingVersion;
        static bool notificationShown;

        [Serializable]
        class PackageMetrics
        {
            public int downloads;
            public int rating_score;
            public string latest_version;
        }

        public static IEnumerator CheckForUpdates(BepInEx.Logging.ManualLogSource logger)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(MetricsUrl))
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    logger.LogWarning($"UltraVoice update check failed: {request.error}");
                    yield break;
                }

                PackageMetrics metrics = null;

                try
                {
                    metrics = JsonUtility.FromJson<PackageMetrics>(request.downloadHandler.text);
                }
                catch (Exception e)
                {
                    logger.LogWarning($"UltraVoice update check could not parse the response: {e.Message}");
                }

                if (metrics == null || string.IsNullOrEmpty(metrics.latest_version))
                    yield break;

                if (!Version.TryParse(metrics.latest_version, out Version latest)
                    || !Version.TryParse(UltraVoicePlugin.PluginVersion, out Version current))
                    yield break;

                if (latest <= current)
                    yield break;

                logger.LogInfo($"UltraVoice update available: {UltraVoicePlugin.PluginVersion} -> {metrics.latest_version}");

                pendingVersion = metrics.latest_version;
                TryShowNotification();
            }
        }

        public static void OnSceneLoaded()
        {
            TryShowNotification();
        }

        static void TryShowNotification()
        {
            if (notificationShown || pendingVersion == null)
                return;

            if (SceneHelper.CurrentScene != MainMenuScene
                && SceneManager.GetActiveScene().name != MainMenuScene)
                return;

            notificationShown = true;

            NotificationSystem.ActionInvoked += OnActionInvoked;

            notificationId = NotificationSystem.NotifySend(
                "Update available!",
                $"UltraVoice Retaken has been updated to {pendingVersion}",
                iconSprite: LoadIcon(),
                urgency: Urgency.Critical,
                appName: "UltraVoice Retaken",
                actions: new Dictionary<string, string>
                {
                    { OpenPageAction, "Thunderstore Page" },
                    { ViewChangelogAction, "View Changelog" }
                }
            );
        }

        static void OnActionInvoked(uint id, string action)
        {
            if (id != notificationId)
                return;

            if (action == OpenPageAction)
                Application.OpenURL(PageUrl);
            else if (action == ViewChangelogAction)
                Application.OpenURL(ChangelogUrl);
        }

        public static Sprite LoadIcon()
        {
            try
            {
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("UltraVoice.Resources.icon.png"))
                {
                    if (stream == null)
                        return null;

                    byte[] data = new byte[stream.Length];
                    stream.Read(data, 0, data.Length);

                    Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);

                    if (!ImageConversion.LoadImage(texture, data))
                        return null;

                    return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
