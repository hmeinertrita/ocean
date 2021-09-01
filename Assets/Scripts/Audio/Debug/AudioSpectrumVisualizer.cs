using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSpectrumVisualizer : MonoBehaviour {

    public GameObject prefab;
    public RectTransform biasBar;
    public int arrLength;

    public float bias;
    public int index;
    public int meanWindowLength = 1; 
    public float valueFactor;

    private RectTransform[] bars;
    private Queue<float>[] values;
    private float barWidth;
    private void Start()
	{
        barWidth = prefab.GetComponent<RectTransform>().sizeDelta.x;
        bars = new RectTransform[arrLength];
        values = new Queue<float>[arrLength];
		for (int i = 0; i < bars.Length; i++) {
            GameObject bar = Instantiate(prefab, transform);
            RectTransform barTransform = bar.GetComponent<RectTransform>();
            barTransform.anchoredPosition = new Vector2(barWidth * i, 0);
            bars[i] = barTransform;
            values[i] = new Queue<float>();
        }
        biasBar.sizeDelta = new Vector2(barWidth, biasBar.sizeDelta.y);
	}

    private void Update() {
        for (int i = 0; i < bars.Length; i++) {
            float newValue = AudioSpectrum.GetSpectrumValue(i) * valueFactor;
            values[i].Enqueue(newValue);
            while (values[i].Count > meanWindowLength) {
                values[i].Dequeue();
            }

            float mean = 0;
            foreach (float value in values[i]) {
                mean += value;
            }
            mean = mean / meanWindowLength;
            
            bars[i].sizeDelta = new Vector2(bars[i].sizeDelta.x, mean);

            Image image = bars[i].GetComponent<Image>();
            if (i == index) {
                if (newValue >= bias * valueFactor) {
                    image.color = Color.green;
                }
                else {
                    image.color = Color.red;
                }
            }
            else {
                image.color = Color.white;
            }
        }

        biasBar.anchoredPosition = new Vector2(index * barWidth, bias * valueFactor);
    }
}