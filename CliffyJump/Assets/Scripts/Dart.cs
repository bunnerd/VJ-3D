using Unity.VisualScripting;
using UnityEngine;

public class Dart : MonoBehaviour
{
    Vector3 startPos;
    Vector3 speed;
	Vector3 finalSpeed;
    float accelerationTime = 0.5f;
	Vector3 acceleration;

    public bool active = false;

    public void Shoot(Vector3 finalSpeed)
    {
        active = true;
        startPos = transform.position;
        this.finalSpeed = finalSpeed;
        acceleration = finalSpeed / accelerationTime;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!active)
            return;

        if (Mathf.Abs(speed.x) < Mathf.Abs(finalSpeed.x) || Mathf.Abs(speed.z) < Mathf.Abs(finalSpeed.z)) 
        {
            speed += acceleration * Time.fixedDeltaTime;
        }

        transform.position += speed * Time.fixedDeltaTime;

        if (Vector3.Distance(startPos, transform.position) > 50.0f) 
        {
            transform.position = startPos;
            active = false;
        }
    }
}
