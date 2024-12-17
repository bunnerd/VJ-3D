using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static Saw;

public class ObstacleManager : MonoBehaviour
{
    public AnimationCurve animationCurve;

    public float startScale = 0f;
    public float endScale = 1.25f;
    public float duration = 0.4f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(LoadObstacle(startScale, endScale, duration));
        }
    }

    public IEnumerator LoadObstacle(float startScale, float endScale, float duration)
    {
        float startTime = Time.time;

        while (Time.time - startTime <= duration)
        {
            float t = (Time.time - startTime) / duration;
            float scale = startScale + animationCurve.Evaluate(t) * (endScale - startScale);
            transform.localScale = new Vector3(scale, scale, scale);
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = new Vector3(endScale, endScale, endScale);
    }
}
