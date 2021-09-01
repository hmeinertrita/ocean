using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPlayer : MonoBehaviour
{
    public TextMeshProUGUI text;
    public CanvasGroup canvas;
    public float timePerCharacter;
    public float fadeTime;

    private Queue<string> queue;
    private float timer;
    private int state;
    private bool paused;
    private static TextPlayer instance;
    void Start()
    {
        queue = new Queue<string>();
        queue.Enqueue("mouse to look around\nspacebar to skip to next song\n\npress spacebar to begin");
        timer = 0;
        state = 1;
        paused = true;
        if (instance == null) {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (queue.Count > 0) {
            string line = queue.Peek();
            text.text = line;
            if (!paused) {
                timer += Time.deltaTime;
            }

            if (state == 0) {
                if (timer > fadeTime) {
                    state = 1;
                    timer = 0;
                }
                else {
                    canvas.alpha = timer / fadeTime;
                }
            }
            if (state == 1) {
                if (timer > timePerCharacter * line.Length) {
                    state = 2;
                    timer = 0;
                }
                else {
                    canvas.alpha = 1;
                }
            }
            if (state == 2) {
                if (timer > fadeTime) {
                    state = 0;
                    timer = 0;
                    canvas.alpha = 0;
                    text.text = "";
                    queue.Dequeue();

                    if (!TransitionManager.Started && queue.Count == 0) {
                        TransitionManager.Play();
                    }
                }
                else {
                    canvas.alpha = 1 - (timer / fadeTime);
                }
            }
        }
        else {
            canvas.alpha = 0;
            text.text = "";
        }
    }

    public static void AddLines(IEnumerable<string> lines) {
        foreach(string line in lines) {
            Debug.Log(line);
            instance.queue.Enqueue(line);
        }
    }

    public static void Clear() {
        instance.state = 0;
        instance.timer = 0;
        instance.canvas.alpha = 0;
        instance.text.text = "";
        instance.queue.Clear();
    }

    public static void FadeFromStart() {
        instance.state = 2;
        instance.paused = false;
    }

    public static float CalculateTime(IEnumerable<string> lines) {
        float time = 0;
        foreach(string line in lines) {
            time += (instance.fadeTime * 2) + (instance.timePerCharacter * line.Length);
        }
        return time;
    }
}
