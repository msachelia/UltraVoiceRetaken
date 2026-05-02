using UnityEngine;

namespace UltraVoice.Utilities
{
    public static class WavUtility
    {
        public static AudioClip ToAudioClip(byte[] fileBytes, string name = "AudioClip")
        {
            int headerOffset = 12;
            while (headerOffset < fileBytes.Length)
            {
                if (fileBytes[headerOffset] == 'f' && fileBytes[headerOffset + 1] == 'm' &&
                    fileBytes[headerOffset + 2] == 't' && fileBytes[headerOffset + 3] == ' ')
                {
                    break;
                }
                headerOffset += 2;
            }

            int channels = fileBytes[headerOffset + 8];
            int sampleRate = System.BitConverter.ToInt32(fileBytes, headerOffset + 12);

            int dataOffset = 36;
            while (dataOffset < fileBytes.Length)
            {
                if (fileBytes[dataOffset] == 'd' && fileBytes[dataOffset + 1] == 'a' &&
                    fileBytes[dataOffset + 2] == 't' && fileBytes[dataOffset + 3] == 'a')
                {
                    dataOffset += 8;
                    break;
                }
                dataOffset += 2;
            }

            int audioDataLength = fileBytes.Length - dataOffset;
            float[] audioData = new float[audioDataLength / 2];

            for (int i = 0; i < audioData.Length; i++)
            {
                short sample = System.BitConverter.ToInt16(fileBytes, dataOffset + i * 2);
                audioData[i] = sample / 32768f;
            }

            AudioClip clip = AudioClip.Create(name, audioData.Length / channels, channels, sampleRate, false);
            clip.SetData(audioData, 0);
            return clip;
        }
    }
}