using HarmonyLib;
using ThornClient.System;

namespace UltraVoice.Utilities
{
    [HarmonyPatch(typeof(ClickGUI), nameof(ClickGUI.SetTooltipText))]
    class ThornEmptyTooltipPatch
    {
        static bool Prefix(string text)
        {
            return !string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text.Trim());
        }
    }
}
