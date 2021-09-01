using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncRain : AudioSyncFloat {
    public GameObject prefab;
    public int raindropCount;
    public float ceiling;
    public float floor;
    public float size;
    public float direction;
    public float stretch;
    public float squish;

    private Rain[] drops;
    void Start()
    {
        drops = new Rain[raindropCount];
        for (int i = 0; i < raindropCount; i++) {
            GameObject drop = Instantiate(prefab, transform);
            drops[i] = drop.GetComponent<Rain>();
            drops[i].ceiling = ceiling;
            drops[i].floor = floor;
            drops[i].size = size;

            float x = Random.Range(-1 * size, size);
            float z = Random.Range(-1 * size, size);
            float y = Random.Range(floor, ceiling);

            drop.transform.localPosition = new Vector3(x, y, z);
        }
    }
	public override void OnUpdate()
	{
		base.OnUpdate();

		if (m_isBeat) return;

        for (int i = 0; i < drops.Length; i++) {
            drops[i].ceiling = ceiling;
            drops[i].floor = floor;
            drops[i].size = size;
            drops[i].SquishStretchMove(squish * valuePercentage, stretch * valuePercentage, currentValue * direction);
        }
	}
}