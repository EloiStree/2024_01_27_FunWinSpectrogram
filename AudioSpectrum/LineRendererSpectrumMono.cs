using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererSpectrumMono : MonoBehaviour
{
    public AbstractAudioSpectrum m_spectrum;
    public AnimationCurve m_curve;
    public LineRenderer m_lineRenderer;
    public float m_lenght = 5;
    public float m_height = 2;
    void Update()
    {
        var bands = m_spectrum.Levels;

        if (m_lineRenderer.positionCount != bands.Length)
            m_lineRenderer.positionCount = bands.Length;
        m_curve = new AnimationCurve();

        for (var i = 0; i < bands.Length; i++)
        {
            m_curve.AddKey(1.0f / bands.Length * i, bands[i]);
        }

        m_lineRenderer.widthCurve = m_curve;


    }
}
