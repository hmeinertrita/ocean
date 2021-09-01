using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTransition : Transition
{
    public Color[] skyColors;
    public Color[] seaColors;
    public ColorController controller;
    private Gradient skyGradient;
    private Gradient seaGradient;
    private GradientColorKey[] skyColorKey;
    private GradientColorKey[] seaColorKey;
    private GradientAlphaKey[] alphaKey;
    void Start()
    {
        skyGradient = new Gradient();
        seaGradient = new Gradient();

        skyColorKey = new GradientColorKey[2];
        skyColorKey[0].color = skyColors[0];
        skyColorKey[0].time = 0.0f;
        skyColorKey[1].color = skyColors[1];
        skyColorKey[1].time = 1.0f;

        seaColorKey = new GradientColorKey[2];
        seaColorKey[0].color = seaColors[0];
        seaColorKey[0].time = 0.0f;
        seaColorKey[1].color = seaColors[1];
        seaColorKey[1].time = 1.0f;

        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;

        skyGradient.SetKeys(skyColorKey, alphaKey);
        seaGradient.SetKeys(seaColorKey, alphaKey);

        controller.sky = skyColors[0];
        controller.sea = seaColors[0];
        Outline[] outlines = GameObject.FindObjectsOfType<Outline>();
        foreach (Outline o in outlines) {
            o.OutlineColor = skyColors[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (TransitionManager.Transitioning) {
            controller.sky = skyGradient.Evaluate(TransitionManager.Completion);
            controller.sea = seaGradient.Evaluate(TransitionManager.Completion);
            
            Outline[] outlines = GameObject.FindObjectsOfType<Outline>();
            foreach (Outline o in outlines) {
                o.OutlineColor = skyGradient.Evaluate(TransitionManager.Completion);
            }
        }
    }

    public override void StartTransition()
    {
        
    }
    public override void EndTransition()
    {
        skyColorKey = new GradientColorKey[2];
        skyColorKey[0].color = skyColors[TransitionManager.Index];
        skyColorKey[0].time = 0.0f;
        skyColorKey[1].color = skyColors[TransitionManager.NextIndex];
        skyColorKey[1].time = 1.0f;

        seaColorKey = new GradientColorKey[2];
        seaColorKey[0].color = seaColors[TransitionManager.Index];
        seaColorKey[0].time = 0.0f;
        seaColorKey[1].color = seaColors[TransitionManager.NextIndex];
        seaColorKey[1].time = 1.0f;

        skyGradient.SetKeys(skyColorKey, alphaKey);
        seaGradient.SetKeys(seaColorKey, alphaKey);

        controller.sky = skyColors[TransitionManager.Index];
        controller.sea = seaColors[TransitionManager.Index];

        Outline[] outlines = GameObject.FindObjectsOfType<Outline>();
        foreach (Outline o in outlines) {
            o.OutlineColor = skyColors[TransitionManager.Index];
        }
    }
}
