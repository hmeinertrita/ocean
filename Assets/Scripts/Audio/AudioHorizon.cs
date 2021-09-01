using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHorizon : MonoBehaviour {

    public float[] spectrum;
    public float fallSpeed;
    public float radius;
    public GameObject prefab;
    private HorizonBar[] bars;

    private void Start()
	{
        spectrum = new float[AudioSpectrum.m_spectrumLength];
        bars = new HorizonBar[AudioSpectrum.m_spectrumLength * 2];
        for (int i = 0; i < AudioSpectrum.m_spectrumLength; i++) {
            float angle = (Mathf.PI / AudioSpectrum.m_spectrumLength) * i;
            float x = radius * Mathf.Sin(angle);
            float z = radius * Mathf.Cos(angle);
            bars[i] = Instantiate(prefab, new Vector3(x, 0, z), Quaternion.identity, transform).GetComponent<HorizonBar>();
            bars[bars.Length - 1 - i] = Instantiate(prefab, new Vector3(-x, 0, z), Quaternion.identity, transform).GetComponent<HorizonBar>();
        }
	}
    private void Update()
	{
        for (int i = 0; i < AudioSpectrum.m_spectrumLength; i++) {
            float value = AudioSpectrum.GetSpectrumValue(i);
            if (value > spectrum[i]) {
                spectrum[i] = value;
            }
            else {
                spectrum[i] -= fallSpeed * Time.deltaTime;
            }

            bars[i].scale = spectrum[i];
            bars[bars.Length - 1 - i].scale = spectrum[i];

            float angle = (Mathf.PI / AudioSpectrum.m_spectrumLength) * i;
            float x = radius * Mathf.Sin(angle);
            float z = radius * Mathf.Cos(angle);

            bars[i].transform.localPosition =  new Vector3(x, 0, z);
            bars[bars.Length - 1 - i].transform.localPosition =  new Vector3(-x, 0, z);
        }
	}
}