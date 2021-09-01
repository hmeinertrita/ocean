using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeTransition : Transition
{
    public AudioSource source;
    public float[] volumes;

    public override void StartTransition()
    {
        source.volume = volumes[TransitionManager.NextIndex];
        Debug.Log("setting volume to: "+volumes[TransitionManager.NextIndex]);
    }

    public override void EndTransition()
    {
    }
}
