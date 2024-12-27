using UnityEngine;
using System.Collections;

public class CoinLoader : MonoBehaviour
{
	public AnimationCurve animationCurve;

	public float startScale = 0f;
	public float endScale = 1.25f;
	public float duration = 0.4f;

	public bool invertX = false;
	public bool invertZ = false;

	private float scaleX;
	private float scaleZ;

	private void Start()
	{
		scaleX = invertX ? -1.0f : 1.0f;
		scaleZ = invertZ ? -1.0f : 1.0f;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.O))
		{
			StartCoroutine(Load());
		}
	}

	public IEnumerator Load()
	{
		float startTime = Time.time;
		float freq = GetComponentInChildren<RotateObject>().frequency;
		GetComponentInChildren<RotateObject>().frequency /= 3.0f;

		while (Time.time - startTime <= duration)
		{
			float t = (Time.time - startTime) / duration;
			float scale = startScale + animationCurve.Evaluate(t) * (endScale - startScale);
			transform.localScale = new Vector3(scaleX * scale, scale, scaleZ * scale);
			yield return new WaitForEndOfFrame();
		}
		transform.localScale = new Vector3(scaleX * endScale, endScale, scaleZ * endScale);
		GetComponentInChildren<RotateObject>().frequency = freq;
	}

	public IEnumerator Unload()
	{
		float startTime = Time.time;

		while (Time.time - startTime <= duration)
		{
			float t = (Time.time - startTime) / duration;
			float scale = startScale + animationCurve.Evaluate(1 - t) * (endScale - startScale);
			transform.localScale = new Vector3(scaleX * scale, scale, scaleZ * scale);
			yield return new WaitForEndOfFrame();
		}
		transform.localScale = new Vector3(scaleX * startScale, startScale, scaleZ * startScale);
	}
}
