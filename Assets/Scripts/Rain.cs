using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour {
    public float ceiling;
    public float floor;
    public float size;
    public float offset;
    public GameObject ripplePrefab;

    private Vector3 baseSize;
    private bool spawnedRipple;

    private void Start()
	{
        baseSize = transform.localScale;
        spawnedRipple = false;
	}

	private void Update()
	{
        
        if (!spawnedRipple && transform.localPosition.y < floor) {
            Instantiate(ripplePrefab, new Vector3(transform.localPosition.x, floor, transform.localPosition.z), Quaternion.identity);
            spawnedRipple = true;
        }
        if (transform.localPosition.y > ceiling + offset) {
            float diff = transform.localPosition.y - (ceiling + offset);
            float x = Random.Range(-1 * size, size);
            float z = Random.Range(-1 * size, size);
            Instantiate(ripplePrefab, new Vector3(x, floor, z), Quaternion.identity);
            transform.localPosition = new Vector3(x, (floor - offset) + diff, z);
        }
        if (transform.localPosition.y < floor - offset) {
            float diff = (floor - offset) - transform.localPosition.y;
            float x = Random.Range(-1 * size, size);
            float z = Random.Range(-1 * size, size);
            transform.localPosition = new Vector3(x, (ceiling + offset) - diff, z);
            spawnedRipple = false;
        }
	}

    public void SquishStretchMove(float squish, float stretch, float translation) {
        Vector3 newScale = new Vector3(
            baseSize.x * (1 - squish), 
            baseSize.y * (1 + stretch), 
            baseSize.z * (1 - squish)
        );
        newScale = newScale.sqrMagnitude > (Vector3.one * 2).sqrMagnitude ? Vector3.one * 2 : newScale;
        
        transform.localScale = newScale;
        transform.localPosition = transform.localPosition + (new Vector3(0, translation, 0) * Time.deltaTime);
    }
}