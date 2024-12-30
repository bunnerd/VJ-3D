using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public AnimationCurve animCurve;

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.position;
        float startTime = Time.time;
        float t, magnitudeMultiplier;
        while (Time.time - startTime < duration)
        {
            t = (Time.time - startTime) / duration;
            magnitudeMultiplier = animCurve.Evaluate(t);

            float x = originalPos.x + Random.Range(-1f, 1f) * magnitude * magnitudeMultiplier;
            float y = originalPos.y + Random.Range(-1f, 1f) * magnitude * magnitudeMultiplier;
            float z = originalPos.z + Random.Range(-1f, 1f) * magnitude * magnitudeMultiplier;
            transform.position = new Vector3(x, y, z);
            yield return null;
        }

        transform.position = originalPos;
    }
}
