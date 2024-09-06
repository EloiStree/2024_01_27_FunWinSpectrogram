using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitySoundAudioSpectrum : AbstractAudioSpectrum
{
    public override void GetSpectrumRawData(ref float [] rawSpectrum)
    {
        AudioListener.GetSpectrumData(rawSpectrum, 0, FFTWindow.BlackmanHarris);
    }
}
