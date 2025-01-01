using UnityEngine;

public class Saw : MonoBehaviour
{
    Vector3 startPos;
    Vector3 endPos;

    Vector3 startToEnd;

    public float initialOffset;
    public float offset;
    public float duration;
    public float initialStopDuration;
    public float stopDuration;
    float start;

    bool going = false;
    bool stopped = true;
    public bool isActive = false;

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

        initialOffset = offset;
        initialStopDuration = stopDuration;

		if (rotation == Rotation.Counterclockwise)
			rotationSpeed *= -1;
	}

	public void ResetObstacle()
	{
        isActive = false;
		transform.position = startPos;
	}

    public void Init()
    {
		going = false;
        stopped = true;

		offset = initialOffset;
		stopDuration = initialStopDuration;
        transform.position = startPos;

		float aux = stopDuration;
		stopDuration = offset;
		offset = aux;

		start = Time.time;
		isActive = true;
	}

	void FixedUpdate()
    {
        if (!isActive)
            return;

		if (stopped)
		{
            if (Time.time - start > stopDuration)
            {
                start = Time.time;
                stopped = false;
                going = !going;

                if (offset != stopDuration)
                    stopDuration = offset;
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
