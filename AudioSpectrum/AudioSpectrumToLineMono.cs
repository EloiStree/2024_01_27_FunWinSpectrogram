using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSpectrumToLineMono : MonoBehaviour
{

    public AbstractAudioSpectrum m_spectrum;
    public AnimationCurve m_curve;

   
    void Update()
    {
        
        m_curve = new AnimationCurve();

        var bands = m_spectrum.Levels;
        for (var i = 0; i < bands.Length; i++)
        {
            m_curve.AddKey(1.0f / bands.Length * i, bands[i]);
        }
    }
}
