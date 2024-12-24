using UnityEngine;

public class Hammer : MonoBehaviour
{
    public enum Direction 
    {
        Clockwise,
        CounterClockwise
    };

    public float rotationSpeed;
    public Direction direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (direction == Direction.CounterClockwise)
            rotationSpeed *= -1.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0.0f, rotationSpeed * Time.fixedDeltaTime, 0.0f);
    }
}
