using HarmonyLib;
using System.IO;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UltraVoice.Utilities
{
    public static class IntroScreen
    {
        static Sprite whiteSprite;
        static Sprite redSprite;

        static Sprite LoadSprite(string resourcePath)
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath))
            {
                if (stream == null)
                    return null;

                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);

                Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);

                if (!texture.LoadImage(data))
                    return null;

                texture.filterMode = FilterMode.Point;

                return Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
        }

        public static void Apply(Image white, Image red)
        {
            if (whiteSprite == null)
                whiteSprite = LoadSprite("UltraVoice.Images.intro_white.png");

            if (redSprite == null)
                redSprite = LoadSprite("UltraVoice.Images.intro_red.png");

            SetSprite(white, whiteSprite);
            SetSprite(red, redSprite);
        }

        static void SetSprite(Image image, Sprite sprite)
        {
            if (image == null || sprite == null)
                return;

            image.sprite = sprite;
            image.type = Image.Type.Simple;
            image.preserveAspect = true;

            RectTransform rect = image.rectTransform;
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
        }
    }

    [HarmonyPatch(typeof(IntroViolenceScreen), "Start")]
    class IntroScreenPatch
    {
        static void Postfix(IntroViolenceScreen __instance)
        {
            IntroScreen.Apply(__instance.GetComponent<Image>(), __instance.red);
        }
    }

    public static class IntroTagline
    {
        public static void Apply(TMP_Text txt)
        {
            if (txt == null || string.IsNullOrEmpty(txt.text))
                return;

            string replaced = txt.text
                .Replace("MANKIND IS DEAD", "SCRIPT IS READ")
                .Replace("BLOOD IS FUEL", "ACTING IS FUEL")
                .Replace("HELL IS FULL", "BOOTH IS FULL");

            if (replaced != txt.text)
                txt.text = replaced;
        }

        public static void ApplyToScene()
        {
            foreach (TMP_Text txt in Object.FindObjectsOfType<TMP_Text>(true))
                Apply(txt);
        }
    }

    [HarmonyPatch(typeof(IntroText), "Start")]
    class IntroTaglinePatch
    {
        static void Prefix(IntroText __instance)
        {
            if (__instance.TryGetComponent<TMP_Text>(out TMP_Text txt))
                IntroTagline.Apply(txt);
        }
    }
}
