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

    bool movedOnce = false;
    bool moving = false;
    bool isActive = false;

    Vector3 initialPos;

	private void Awake()
	{
		initialPos = transform.position;
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
            return;

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

    public void ResetObstacle()
    {
        isActive = false;
        transform.position = initialPos;
    }

    public void Init() 
    {
		transform.position = initialPos;
        //Debug.Log("Initial transform: " + initialTransform + ", pos:" + initialTransform.position + ", transform: " + transform + ", pos:" + transform.position);
		startY = initialPos.y;
		endY = startY + 2.0f;

		movedOnce = false;
		moving = false;
		start = Time.time;
        isActive = true;
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
