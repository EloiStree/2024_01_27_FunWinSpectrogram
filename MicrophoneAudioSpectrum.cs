using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneAudioSpectrum : AbstractAudioSpectrum
{
    public AudioSource m_audioSource;


    public override void GetSpectrumRawData(ref float[] raw)
    {
        Microphone.Start(Microphone.devices[0], true, 1, 44000);
        m_audioSource.GetSpectrumData(raw, 0, FFTWindow.BlackmanHarris);
    }
}
