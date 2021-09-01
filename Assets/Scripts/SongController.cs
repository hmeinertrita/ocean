using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongController : MonoBehaviour
{
    public AudioClip[] songs;
    public AudioSource source;
    private string[][] songCommentary = new string[][] {
        new string[] {
            "FamilyMart (miss U!) - Slime Girls",
        },
        new string[] {
            "Meteor Showers - Louie Zhong",
        },
        new string[] {
            "Endless Summer Malaise!!! - Twinkle Park",
        },
    };
    private string[][] transitionCommentary = new string[][] {
        new string[] {},
        new string[] {},
        new string[] {}
    };
    private int index;
    private static SongController instance;
    public static int SongCount { get { return instance.songs.Length; }}
    void Start()
    {
        if (instance == null) {
            instance = this;
        }

        index = -1;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("samples: " + source.timeSamples + " / " + source.clip.samples);
        if (TransitionManager.Started) {
            if (source.clip != null && source.timeSamples >= source.clip.samples - 500) {
                StartTransition();
            }
            if (Input.GetButtonUp("Skip") && !TransitionManager.Transitioning) {
                TextPlayer.Clear();
                StartTransition();
            }
        }
    }
    private void StartTransition() {
        source.Stop();
        string[] lines = transitionCommentary[index];
        TransitionManager.StartTransition(TextPlayer.CalculateTime(lines));
        TextPlayer.AddLines(lines);
    }
    public static void PlayNext() {
        instance.index++;
        if (instance.index == instance.songs.Length) {
            instance.index = 0;
        }

        instance.source.clip = instance.songs[instance.index];
        instance.source.Play();
        Debug.Log(instance.songCommentary[instance.index][0]);
        TextPlayer.AddLines(instance.songCommentary[instance.index]);
    }

    public static void Wipe() {
        Debug.Log("wiping!");
        for (int i = 0; i < instance.songs.Length; i++) {
            instance.songCommentary[i] = new string[0];
            instance.transitionCommentary[i] = new string[0];
        }
    }
}
