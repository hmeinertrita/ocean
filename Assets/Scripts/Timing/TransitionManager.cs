using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public float transitionTime;
    public Transition[] transitions;
    public static float Completion { get {
        return instance.timer / instance.actualTransitionTime;
    }}
    public static bool Transitioning { get {
        return instance.transitioning;
    }}
    public static int Index { get {
        return instance.index;
    }}
    public static bool Started { get {
        return instance.started;
    }}
    public static int NextIndex { get {
        if (instance.index + 1 == SongController.SongCount + 1) {
            return 1;
        }
        else {
            return instance.index + 1;
        }
    }}
    private static TransitionManager instance;
    private float timer; 
    private bool transitioning;
    private bool started;
    private int index;
    private float actualTransitionTime;

    void Start()
    {
        if (instance == null) {
            instance = this;
        }

        transitioning = false;
        started = false;
        actualTransitionTime = transitionTime;
        Resolution res = Screen.resolutions[Screen.resolutions.Length - 1];
        Screen.SetResolution(res.width, res.height, FullScreenMode.ExclusiveFullScreen);
    }

    // Update is called once per frame
    void Update()
    {
        if (!started) {
            if (Input.GetButtonUp("Jump")) {
                //TextPlayer.AddLines(new string [] {"(ok now don't press it again please)"});
                TextPlayer.FadeFromStart();
            }
            if (Input.GetButtonUp("Skip")) {
                TextPlayer.FadeFromStart();
            }
        }
        else if (transitioning) {
            timer += Time.deltaTime;
            if (timer > actualTransitionTime) {
                Debug.Log("ending transition");
                transitioning = false;
                timer = 0;
                index = NextIndex;
                foreach (Transition t in transitions) {
                    t.EndTransition();
                }
                SongController.PlayNext();
            }
        }
    }

    public static void StartTransition(float time) {
        if (!instance.transitioning) {
            Debug.Log("starting transition");
            instance.transitioning = true;
            instance.timer = 0;
            instance.actualTransitionTime = Mathf.Max(time + 3, instance.transitionTime);
            foreach (Transition t in instance.transitions) {
              t.StartTransition();
            }
        }
    }

    public static void Play() {
        instance.started = true;
        StartTransition(10);
    }
}
