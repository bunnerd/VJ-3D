using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float speed;
    public float gravity;

    private bool isGrounded = false;
    private CharacterController c;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        c = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		speed -= gravity * Time.fixedDeltaTime;
		CollisionFlags flags = c.Move(new Vector3(0f, speed * Time.fixedDeltaTime, 0f));

		if (!isGrounded)
        {
            if ((flags & CollisionFlags.CollidedBelow) != 0) 
            {
                isGrounded = true;
				speed = 0f;
			}
        }
        else 
        {
            if (speed < 0f)
                speed = 0f;

			if ((flags & CollisionFlags.CollidedBelow) == 0)
			{
				isGrounded = false;
			}
        }
    }

    public bool Grounded() 
    {
        return isGrounded;
    }

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
        if (!isGrounded) 
        {
			Debug.Log("Hit " + hit.gameObject.name);
			isGrounded = true;
        }
	}   
}
