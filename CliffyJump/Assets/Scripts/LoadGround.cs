using UnityEngine;

public class LoadGround : MonoBehaviour
{
    [SerializeField] private AnimationCurve animCurve;
    public GameObject[] columns;

    public float startHeight = -5.0f;
    public float endHeight = 2.0f;
    public float duration = 1.0f;

    float startTime;

    bool raiseGround = true;
    bool lowerGround = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (raiseGround && !lowerGround)
        {
            if (Time.time - startTime > duration)
            {
                // Make sure the height of the object is exactly endHeight
                Debug.Log("end height reached!");
                raiseGround = false;
                for (int i = 0; i < columns.Length; ++i)
                {
                    columns[i].transform.position = new Vector3(columns[i].transform.position.x, endHeight, columns[i].transform.position.z);
                }
            }
            else
            {
                float t = (Time.time - startTime) / duration;
                float newY = startHeight + animCurve.Evaluate(t) * (endHeight - startHeight);
                for (int i = 0; i < columns.Length; ++i)
                {
                    columns[i].transform.position = new Vector3(columns[i].transform.position.x, newY, columns[i].transform.position.z);
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Alpha0))
            {
                lowerGround = true;
                startTime = Time.time;
            }
            if (lowerGround)
            {
                float t = 1.0f - (Time.time - startTime) / duration;
                float newY = startHeight + animCurve.Evaluate(t) * (endHeight - startHeight);
                for (int i = 0; i < columns.Length; ++i)
                {
                    columns[i].transform.position = new Vector3(columns[i].transform.position.x, newY, columns[i].transform.position.z);
                }
            }
        }
    }
}
