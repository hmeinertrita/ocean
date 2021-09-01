using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncFloat : AudioSyncer {
    public float beatValue;
	public float restValue;
    protected float currentValue {get; private set;}

    protected float valuePercentage { get {
        float min = Mathf.Min(restValue, beatValue);
        float max = Mathf.Max(restValue, beatValue);
        return (currentValue - min) / (max - min);
    }}
	private IEnumerator MoveToValue(float _target)
	{
		float _curr = currentValue;
		float _initial = _curr;
		float _timer = 0;

		while (_curr != _target)
		{
			_curr = Mathf.Lerp(_initial, _target, _timer / timeToBeat);
			_timer += Time.deltaTime;

			currentValue = _curr;

			yield return null;
		}

		m_isBeat = false;
	}

	public override void OnUpdate()
	{
		base.OnUpdate();

		if (m_isBeat) return;

		currentValue = Mathf.Lerp(currentValue, restValue, restSmoothTime * Time.deltaTime);
	}

	public override void OnBeat()
	{
		base.OnBeat();

		StopCoroutine("MoveToValue");
		StartCoroutine("MoveToValue", beatValue);
	}
}