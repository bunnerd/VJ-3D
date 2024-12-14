using UnityEngine;

public class Saw : MonoBehaviour
{
    Vector3 startPos;
    Vector3 endPos;

    Vector3 startToEnd;

    public float offset;
    public float duration;
    public float stopDuration;
    float start;

    bool going = true;
    bool stopped = false;

	public float rotationSpeed;
    public enum Rotation 
    {
        Clockwise,
        Counterclockwise
    };
    public Rotation rotation;

	void Start()
    {
        startPos = transform.position;
        endPos = transform.GetChild(0).position;
        startToEnd = endPos - startPos;
        going = true;

        if (rotation == Rotation.Counterclockwise)
            rotationSpeed *= -1;

        start = Time.time;
    }

    void FixedUpdate()
    {
		if (stopped)
		{
            if (Time.time - start > stopDuration)
            {
                start = Time.time;
                stopped = false;
                going = !going;
            }
            else 
            {
                return;
            }
		}
		float t = (Time.time - start) / duration;

		float st;
		if (going)
			st = Mathf.SmoothStep(0, 1, t);
		else
			st = Mathf.SmoothStep(0, 1, 1 - t);

		if (t >= 1.0f) 
        {
            start = Time.time;
            stopped = true;
        }

        float direction = going ? 1.0f : -1.0f;

		transform.position = startPos + startToEnd * st;
        transform.Rotate(0.0f, 0.0f, direction * rotationSpeed * Time.fixedDeltaTime);
    }
}
