using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColorController : MonoBehaviour
{
    public static Color Sky { get { return instance.sky; }}
    public static Color Sea { get { return instance.sea; }}
    public static ColorController instance;
    public string[] tags = {
        "C_SKY",
        "C_SEA",
    };
    public Color sky;
    public Color sea;
    public TextMeshProUGUI text;
    private Dictionary<string, Color> colors;
    void Start()
    {
        colors = new Dictionary<string, Color>();
        colors.Add("C_SKY", sky);
        colors.Add("C_SEA", sea);
        if (instance == null) {
            instance = this;
        }

        text.outlineColor = sea;
        text.faceColor = sky;
    }

    // Update is called once per frame
    void Update()
    {
        colors["C_SKY"] = sky;
        colors["C_SEA"] = sea;

        foreach (string tag in tags) {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject o in objects) {
                o.GetComponent<Renderer>().material.SetColor("_Color", colors[tag]);
            }
        }

        text.outlineColor = TransitionManager.Started ? sea : new Color(0.8f,0.8f,0.8f,1);
        text.faceColor = sky;
    }
}
