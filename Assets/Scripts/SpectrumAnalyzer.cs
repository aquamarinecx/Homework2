using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumAnalyzer : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    public static float[] samples = new float[512];
    float[] freqBandHighest = new float[8];
    public static float[] audioBands = new float[8];
    public static float[] freqBand = new float[8];
    float[] _bufferdecrease = new float[8];

    void CreateAudioBands()
    {
        for (int g = 0; g < 8; ++g)
        {
            if (freqBand[g] > audioBands[g])
            {
                freqBand[g] = audioBands[g];
                _bufferdecrease[g] = 0.005f;
            }
            if (freqBand[g] < audioBands[g])
            {
                audioBands[g] -= freqBand[g];
                _bufferdecrease[g] = 1.2f;
            }
        }
    }
    void MakeFrequencyBands()
    {
        int count = 0;

        // Iterate through the 8 ns.
        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i + 1);

            // Adding the remaining two samples into the last bin.
            if (i == 7)
            {
                sampleCount += 2;
            }

            // Go through the number of samples for each bin, add the data to the average
            for (int j = 0; j < sampleCount; j++)
            {
                average += samples[count];
                count++;
            }

            // Divide to create the average, and scale it appropriately.
            average /= count;
            freqBand[i] = (i + 1) * 100 * average;
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
    }

}