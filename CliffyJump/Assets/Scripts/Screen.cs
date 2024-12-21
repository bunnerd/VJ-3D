using System.Collections;
using UnityEngine;

public class Screen : MonoBehaviour
{
    [SerializeField] private AnimationCurve animCurve;
    public GameObject[] objects;

    public float startHeight = -5.0f;
    public float endHeight = 2.0f;
    public float duration = 1.0f;

    void Start()
    {
        Application.targetFrameRate = 60;
        StartCoroutine(RaiseGround());
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha0))
        {
            StartCoroutine(LowerGround());
        }
    }

    public IEnumerator RaiseGround()
    {
        float startTime = Time.time;
        while (Time.time - startTime <= duration)
        {
            float t = (Time.time - startTime) / duration;
            float newY = startHeight + animCurve.Evaluate(t) * (endHeight - startHeight);
            for (int i = 0; i < objects.Length; ++i)
            {
                objects[i].transform.position = new Vector3(objects[i].transform.position.x, newY, objects[i].transform.position.z);
            }
            yield return new WaitForEndOfFrame();
        }

        // Make sure the height of the object is exactly endHeight
        for (int i = 0; i < objects.Length; ++i)
        {
            objects[i].transform.position = new Vector3(objects[i].transform.position.x, endHeight, objects[i].transform.position.z);
        }
    }

    public IEnumerator LowerGround()
    {
        float startTime = Time.time;
        while (Time.time - startTime <= duration)
        {
            float t = 1.0f - (Time.time - startTime) / duration;
            float newY = startHeight + animCurve.Evaluate(t) * (endHeight - startHeight);
            for (int i = 0; i < objects.Length; ++i)
            {
                objects[i].transform.position = new Vector3(objects[i].transform.position.x, newY, objects[i].transform.position.z);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
