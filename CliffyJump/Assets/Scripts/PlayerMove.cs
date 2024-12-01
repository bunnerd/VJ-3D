using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngineInternal;

public class PlayerMove : MonoBehaviour
{
    CharacterController controller;

    public Orientation orientation = Orientation.Forward;
    public float speed;
	public readonly float size = 1f;

	public LayerMask turnLayer;
	public LayerMask obstacleLayer;
	public LayerMask stopLayer;

	private bool collidedWithTrigger = false;

	public bool stopped = false;
	private bool collidedWithStopPoint = false;

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
		collidedWithTrigger = false;
		collidedWithStopPoint = false;
		stopped = false;

		moveVectors = new Vector3[]
		{
			new (0f, 0f, 1f),
			new (1f, 0f, 0f),
			new (0f, 0f, -1f),
			new (-1f, 0f, 0f)
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
		else if (Input.GetKeyDown(KeyCode.P)) 
		{
			Debug.Log(transform.position - new Vector3(0.0f, transform.lossyScale.y / 2, 0.0f));
		}
	}

	// Update is called once per frame
	void FixedUpdate()
	{

		Vector3 movement = speed * Time.fixedDeltaTime * moveVectors[(int)orientation];
		Vector3 nextPosition = transform.position + movement;

		CheckCollisionWithStopPoint(nextPosition);
		if (stopped)
			return;

		CheckCollisionWithObstacle(nextPosition);
		CheckCollisionWithTurnPoint(nextPosition, ref movement);

		controller.Move(movement);
	}

	public void TurnLeft() 
    {
		// Rotate model
		transform.Rotate(new Vector3(0f, 1f, 0f), -90f);

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

    public void TurnRight() 
    {
        // Rotate model
        transform.Rotate(new Vector3(0f, 1f, 0f), 90f);

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

	private void CheckCollisionWithTurnPoint(Vector3 nextPosition, ref Vector3 movement) 
	{
		Collider[] collisions = Physics.OverlapSphere(nextPosition, size, turnLayer);

		if (collisions.Length > 1)
		{
			Debug.LogError("More than one obstacle collision?");
		}

		bool collided = collisions.Length > 0;

		if (!collidedWithTrigger && collided)
		{
			collidedWithTrigger = true;
			foreach (Collider hit in collisions)
			{
				TurnPoint turnPoint = hit.gameObject.GetComponent<TurnPoint>();
				if (turnPoint.turns[turnPoint.currentTurn] == TurnPoint.Turn.None)
				{
					if (++turnPoint.currentTurn >= turnPoint.turns.Length)
					{
						turnPoint.currentTurn = 0;
					}
					continue;
				}

				Vector3 playerToCollider = turnPoint.centerPos - transform.position;
				movement = new Vector3(playerToCollider.x, 0.0f, playerToCollider.z);
				switch (turnPoint.turns[turnPoint.currentTurn])
				{
					case TurnPoint.Turn.Left:
						{
							TurnLeft();
							if (++turnPoint.currentTurn >= turnPoint.turns.Length)
							{
								turnPoint.currentTurn = 0;
							}
							break;
						}
					case TurnPoint.Turn.Right:
						{
							TurnRight();
							if (++turnPoint.currentTurn >= turnPoint.turns.Length)
							{
								turnPoint.currentTurn = 0;
							}
							break;
						}
					default:
						break;

				}
			}
		}
		else if (collidedWithTrigger && !collided)
		{
			collidedWithTrigger = false;
		}
	}

	private void CheckCollisionWithObstacle(Vector3 nextPosition) 
	{
		Collider[] collisions = Physics.OverlapSphere(nextPosition, size/2, obstacleLayer);
		if (collisions.Length > 0) 
		{
			Debug.Log("Pos: " + transform.position + ", nextPos: " + nextPosition + " ObsPos: " + collisions[0].transform.position);
			Die();
		}
	}

	private void Die() 
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	private void CheckCollisionWithStopPoint(Vector3 nextPosition) 
	{
		if (stopped)
			return;

		Collider[] collisions = Physics.OverlapSphere(nextPosition, size/2, stopLayer);
		if (collisions.Length > 0 && !collidedWithStopPoint)
		{
			Debug.Log("Stop point! Pos: " + transform.position + ", sphere to " + collisions[0].gameObject.transform.position + " (" + collisions[0].gameObject.transform.localPosition + ")");

			stopped = true;
			collidedWithStopPoint = true;
			Vector3 movement = collisions[0].gameObject.transform.position - transform.position;
			Debug.Log(movement);
			controller.Move(new Vector3(movement.x, 0.0f, movement.z));
			GetComponent<Animator>().enabled = false;
		}
		else if (collisions.Length == 0 && collidedWithStopPoint) 
		{
			collidedWithStopPoint = false;
		}
	}
}
