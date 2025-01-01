using UnityEngine;

public class Hammer : MonoBehaviour
{
    public enum Direction 
    {
        Clockwise,
        CounterClockwise
    };

    float initialYRotation;
    bool isActive = false;
    public float rotationSpeed;
    public Direction direction;

	private void Awake()
	{
        initialYRotation = transform.rotation.eulerAngles.y;
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        if (direction == Direction.CounterClockwise)
            rotationSpeed *= -1.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isActive)
            return;

        transform.Rotate(0.0f, rotationSpeed * Time.fixedDeltaTime, 0.0f);
    }

    public void Init()
    {
        isActive = true;
    }

    public void ResetObstacle() 
    {
        isActive = false;
        transform.rotation = Quaternion.Euler(0.0f, initialYRotation, 0.0f);
    }
}
