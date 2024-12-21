using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
	[SerializeField] private AnimationCurve curve;

    public float offset = 0.0f;
    private float start;
    public float duration;
    public float cooldown;

    private float startY;
    private float endY;

    bool first = true;
    bool movedOnce = false;
    bool moving = false;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (first) 
        {
            first = false;
			Init();
		}

        if (!movedOnce)
        {
            if (Time.time - start >= offset)
            {
                movedOnce = true;
                StartMovement();
            }
        }
        else if (moving)
        {
            UpdateMovement();
        }
        else 
        {
            if (Time.time - start >= cooldown) 
            {
                StartMovement();
            }
        }
    }

    void Init() 
    {
		startY = transform.position.y;
		endY = startY + 2.0f;

		movedOnce = false;
		moving = false;
		start = Time.time;
	}

    void StartMovement() 
    {
        start = Time.time;
        moving = true;
    }

    void UpdateMovement() 
    {
        if (Time.time - start > duration) 
        {
            StopMovement();
            return;
        }

		float t = (Time.time - start) / duration;
        float curveY = curve.Evaluate(t);

        float posX = transform.position.x;
        float posZ = transform.position.z;
        transform.position = new Vector3(posX, startY + curveY * (endY - startY), posZ);
	}

    void StopMovement() 
    {
        start = Time.time;
        moving = false;
    }
}
