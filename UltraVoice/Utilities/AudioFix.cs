using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace UltraVoice.Utilities
{
    [HarmonyPatch(typeof(VirtualAudioManager), "UpdateSources")]
    class AudioFix
    {
        static void Prefix(VirtualAudioManager __instance)
        {
            List<VirtualAudioFilter> sources = __instance.m_Sources;
            bool removedAny = false;

            for (int i = sources.Count - 1; i >= 0; i--)
            {
                VirtualAudioFilter filter = sources[i];

                if (filter == null)
                    continue;

                AudioSource source = filter.source;

                if (source == null)
                    continue;

                try
                {
                    source.IsPaused();
                }
                catch (System.NullReferenceException)
                {
                    filter.trackedIndex = -1;
                    Object.Destroy(filter);
                    sources.RemoveAt(i);
                    removedAny = true;
                }
            }

            if (!removedAny)
                return;

            for (int i = 0; i < sources.Count; i++)
            {
                if (sources[i] != null)
                    sources[i].trackedIndex = i;
            }
        }
    }
}
