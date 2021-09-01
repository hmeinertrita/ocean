using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mini "engine" for analyzing spectrum data
/// Feel free to get fancy in here for more accurate visualizations!
/// </summary>
public class AudioSpectrum : MonoBehaviour {

    // Unity fills this up for us
    private static float[] m_audioSpectrum;
    public static int m_spectrumLength = 128;

	private void Update()
    {
        // get the data
        AudioListener.GetSpectrumData(m_audioSpectrum, 0, FFTWindow.Hamming);

        // assign spectrum value
        // this "engine" focuses on the simplicity of other classes only..
        // ..needing to retrieve one value (spectrumValue)
        
    }

    public static float GetSpectrumValue(int index) {
        if (m_audioSpectrum != null && m_audioSpectrum.Length > 0  && m_audioSpectrum.Length > index && index >= 0)
        {
            return m_audioSpectrum[index] * 100;
        }
        return 0;
    }

    private void Start()
    {
        /// initialize buffer
        m_audioSpectrum = new float[m_spectrumLength];
    }

    

}