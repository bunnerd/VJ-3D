using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Space relativeTo = Space.Self;
    public Vector3 rotationAxis = Vector3.up;
    public float frequency = 1.0f;

    // Update is called once per frame
    void Update()
    {
        float angle = (360.0f * Time.deltaTime) / frequency;
        transform.Rotate(rotationAxis, angle, relativeTo);
    }
}
