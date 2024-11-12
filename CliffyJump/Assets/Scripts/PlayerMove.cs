using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController controller;

    public Orientation orientation = Orientation.Forward;
    public float speed;
    public enum Orientation 
    {
        Forward = 0, 
        Right = 1, 
        Backward = 2, 
        Left = 3
    }

    private Vector3[] moveVectors;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		controller = GetComponent<CharacterController>();

		moveVectors = new Vector3[]
		{
			new (0f, 0f, speed),
			new (speed, 0f, 0f),
			new (0f, 0f, -speed),
			new (-speed, 0f, 0f)
		};
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.D))
		{
			TurnRight();
		}
		else if (Input.GetKeyDown(KeyCode.A))
		{
			TurnLeft();
		}
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		controller.Move(moveVectors[(int)orientation] * Time.fixedDeltaTime);
	}

	private void TurnLeft() 
    {
		Debug.Log("Turning left");
		// Rotate model
		transform.Rotate(new Vector3(0f, 1f, 0f), 90f);

		switch (orientation)
		{
			case Orientation.Forward:
				orientation = Orientation.Left;
				break;
			case Orientation.Right:
				orientation = Orientation.Forward;
				break;
			case Orientation.Backward:
				orientation = Orientation.Right;
				break;
			case Orientation.Left:
				orientation = Orientation.Backward;
				break;
			default:
				Debug.LogError("PlayerMove: Trying to turn left with unknown orientation");
				break;
		}
	}

    private void TurnRight() 
    {
        Debug.Log("Turning right");
        // Rotate model
        transform.Rotate(new Vector3(0f, 1f, 0f), -90f);

        switch (orientation) 
        {
            case Orientation.Forward:
                orientation = Orientation.Right;
                break;
            case Orientation.Right:
                orientation = Orientation.Backward;
                break;
            case Orientation.Backward:
                orientation = Orientation.Left;
                break;
            case Orientation.Left:
                orientation = Orientation.Forward;
                break;
            default:
                Debug.LogError("PlayerMove: Trying to turn right with unknown orientation");
                break;
        }
    }
}
