using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ObstacleManager : MonoBehaviour
{
    public AnimationCurve animationCurve;

    public float startScale = 0f;
    public float endScale = 1.25f;
    public float duration = 0.4f;

    public bool invertX = false;
    public bool invertZ = false;

    public bool isDartBlock = false;
    public bool isSpikeTrap = false;
    public bool isSaw = false;
    public bool isHammer = false;

    private float scaleX;
    private float scaleZ;

	private void Start()
	{
        scaleX = invertX ? -1.0f : 1.0f;
        scaleZ = invertZ ? -1.0f : 1.0f;
	}

    public IEnumerator LoadObstacle()
    {
		float startTime = Time.time;

        while (Time.time - startTime <= duration)
        {
            float t = (Time.time - startTime) / duration;
            float scale = startScale + animationCurve.Evaluate(t) * (endScale - startScale);
            transform.localScale = new Vector3(scaleX * scale, scale, scaleZ * scale);
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = new Vector3(scaleX * endScale, endScale, scaleZ * endScale);

		if (isDartBlock)
			GetComponent<DartBlock>().Init();
		else if (isSaw)
			GetComponentInParent<Saw>().Init();
        else if (isSpikeTrap)
            GetComponent<SpikeTrap>().Init();
		else if (isHammer)
			GetComponent<Hammer>().Init();
	}

	public IEnumerator UnloadObstacle()
	{
		float startTime = Time.time;

		while (Time.time - startTime <= duration)
		{
			float t = (Time.time - startTime) / duration;
			float scale = startScale + animationCurve.Evaluate(1-t) * (endScale - startScale);
			transform.localScale = new Vector3(scaleX * scale, scale, scaleZ * scale);
			yield return new WaitForEndOfFrame();
		}
		transform.localScale = new Vector3(scaleX * startScale, startScale, scaleZ * startScale);

        if (isDartBlock)
            GetComponent<DartBlock>().ResetObstacle();
        else if (isSaw)
            GetComponentInParent<Saw>().ResetObstacle();
        else if (isSpikeTrap)
            GetComponent<SpikeTrap>().ResetObstacle();
        else if (isHammer)
			GetComponent<Hammer>().ResetObstacle();
	}
}
