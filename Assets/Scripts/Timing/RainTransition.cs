using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainTransition : Transition
{
    public float[] restValues;
    public float[] beatValues;
    public float[] directionValues;

    public int[] indexValues;
    public float[] biasValues;
    public AudioSyncRain controller;
    // Update is called once per frame
    void Update()
    {
        if (TransitionManager.Transitioning) {
            controller.restValue = Mathf.Lerp(
                restValues[TransitionManager.Index],
                restValues[TransitionManager.NextIndex], 
                TransitionManager.Completion
            );
            controller.beatValue = Mathf.Lerp(
                beatValues[TransitionManager.Index],
                beatValues[TransitionManager.NextIndex], 
                TransitionManager.Completion
            );
            controller.direction = Mathf.Lerp(
                directionValues[TransitionManager.Index],
                directionValues[TransitionManager.NextIndex], 
                TransitionManager.Completion
            );
        }
    }

    public override void StartTransition()
    {
        
    }
    public override void EndTransition()
    {
        controller.restValue = restValues[TransitionManager.Index];
        controller.beatValue = beatValues[TransitionManager.Index];
        controller.direction = directionValues[TransitionManager.Index];
        controller.bias = biasValues[TransitionManager.Index];
        controller.spectrumIndex = indexValues[TransitionManager.Index];
    }
}
