using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncRipple : AudioSyncFloat
{
    public Transform inner;
    public Transform outer;

    private float progress;
    private float scaleFactor;
    // Start is called before the first frame update
    void Start()
    {
        progress = 0.05f;
        scaleFactor = Random.Range(0.75f, 1.5f);
    }

    // Update is called once per frame
    public override void OnUpdate(){
		base.OnUpdate();

		if (m_isBeat) return;

		progress += currentValue * Time.deltaTime;

        if (progress >= 1.3) {
            Destroy(gameObject);
        }
        else {
            float innerScale = (progress * progress) * scaleFactor;
            float outerScale = Mathf.Sqrt(progress) * scaleFactor;
            
            inner.localScale = new Vector3(innerScale, inner.localScale.y, innerScale);
            outer.localScale = new Vector3(outerScale, outer.localScale.y, outerScale);
        }
	}
}
