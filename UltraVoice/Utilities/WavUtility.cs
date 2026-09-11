using UnityEngine;
using System.IO;

namespace UltraVoice.Utilities
{
    public static class WavUtility
    {
        public static AudioClip ToAudioClip(byte[] fileBytes, string name = "AudioClip")
        {
            using (var memStream = new MemoryStream(fileBytes))
            using (var reader = new BinaryReader(memStream))
            {

                string riff = new string(reader.ReadChars(4));
                if (riff != "RIFF")
                    return null;

                reader.ReadInt32();

                string wave = new string(reader.ReadChars(4));
                if (wave != "WAVE")
                    return null;

                int channels = 0;
                int sampleRate = 0;
                int bytesPerSample = 0;

                while (memStream.Position < memStream.Length)
                {
                    string chunkId = new string(reader.ReadChars(4));
                    int chunkSize = reader.ReadInt32();

                    if (chunkId == "fmt ")
                    {
                        reader.ReadInt16();
                        channels = reader.ReadInt16();
                        sampleRate = reader.ReadInt32();
                        reader.ReadInt32();
                        reader.ReadInt16();
                        int bitsPerSample = reader.ReadInt16();
                        bytesPerSample = bitsPerSample / 8;

                        if (chunkSize > 16)
                            reader.ReadBytes(chunkSize - 16);
                    }
                    else if (chunkId == "data")
                    {

                        int sampleCount = chunkSize / bytesPerSample / channels;
                        float[] audioData = new float[sampleCount * channels];

                        for (int i = 0; i < sampleCount * channels; i++)
                        {
                            if (bytesPerSample == 2)
                            {
                                short sample = reader.ReadInt16();
                                audioData[i] = sample / 32768f;
                            }
                            else if (bytesPerSample == 1)
                            {
                                byte sample = reader.ReadByte();
                                audioData[i] = (sample - 128) / 128f;
                            }
                        }

                        AudioClip clip = AudioClip.Create(name, sampleCount, channels, sampleRate, false);
                        clip.SetData(audioData, 0);
                        return clip;
                    }
                    else
                    {

                        reader.ReadBytes(chunkSize);
                    }
                }
            }

            return null;
        }
    }
}