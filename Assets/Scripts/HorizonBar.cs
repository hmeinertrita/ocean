using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizonBar : MonoBehaviour
{
    public Transform topSphere;
    public Transform botSphere;
    public Transform cylinder;
    public float scale;

    // Update is called once per frame
    void Update()
    {
        cylinder.localScale = new Vector3(cylinder.localScale.x, scale, cylinder.localScale.z);
        topSphere.localPosition = new Vector3(0, scale, 0);
        botSphere.localPosition = new Vector3(0, scale * -1, 0);
    }
}
