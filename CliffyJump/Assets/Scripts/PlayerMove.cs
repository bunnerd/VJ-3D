using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SceneManagement;
using UnityEngineInternal;

public class PlayerMove : MonoBehaviour
{
    CharacterController controller;
	public LevelEntrance entrance;

    public Orientation orientation = Orientation.Forward;
    public float speed;
	public readonly float size = 1f;

	public LayerMask turnLayer;
	public LayerMask obstacleLayer;
	public LayerMask stopLayer;
	public LayerMask jumpLayer;
	public LayerMask progressLayer;

	public AudioSource deathSound;
	private Gravity gravity;

	public NextScreen nextScreen;

	public CameraShake cameraShake;

	public bool godmode = false;

	private bool collidedWithTrigger = false;

	private bool fullStopped = false;
	public bool stopped = false;
	private bool collidedWithStopPoint = false;
	private bool collidedWithJumpPoint = false;
	private bool collidedWithProgressPoint = false;
	private bool teleporting = false;
	private bool dead = false;
    public enum Orientation 
    {
        Forward = 0, 
        Right = 1, 
        Backward = 2, 
        Left = 3
    }

    private Vector3[] moveVectors;

	private GameUI ui;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		controller = GetComponent<CharacterController>();
		collidedWithTrigger = false;
		collidedWithStopPoint = false;
		collidedWithJumpPoint = false;
		stopped = false;
		godmode = false;

		moveVectors = entrance.moveVectors;

		gravity = GetComponent<Gravity>();

		ui = GameObject.Find("UI").GetComponent<GameUI>();
		if (ui == null)
		{
			Debug.LogError("PlayerMove: UI component not found! Make sure there is an UI prefab object in the scene this Player is in");
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.G)) 
		{
			godmode = !godmode;
		}
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (fullStopped)
			return;

		Vector3 movement = speed * Time.fixedDeltaTime * moveVectors[(int)orientation];
		Vector3 nextPosition = transform.position + movement;

		CheckCollisionWithStopPoint(nextPosition);
		if (stopped)
			return;

		CheckCollisionWithObstacle(nextPosition);
		CheckCollisionWithTurnPoint(nextPosition, ref movement);
		CheckCollisionWithJumpPoint(nextPosition);
		CheckCollisionWithProgressPoint(nextPosition);

		if (!teleporting)
			controller.Move(movement);
	}

	public void Teleport(Vector3 position) 
	{
		teleporting = true;
		StartCoroutine(TeleportHelper(position));
	}

	private IEnumerator TeleportHelper(Vector3 position) 
	{
		int counter = 0;
		GameObject activeTrail = FirstActiveChild(transform.Find("Trail"));
        activeTrail.GetComponent<TrailRenderer>().enabled = false;

		while (++counter < 3) 
		{
			transform.position = position;
			yield return new WaitForEndOfFrame();
		}
		teleporting = false;

        activeTrail.GetComponent<TrailRenderer>().Clear();
        activeTrail.GetComponent<TrailRenderer>().enabled = true;
	}

	public void FullStop() 
	{
		fullStopped = true;
		transform.gameObject.GetComponent<Gravity>().fullStopped = true;
		transform.gameObject.GetComponent<Jump>().fullStopped = true;
	}

	public void EnableMove()
	{
		dead = false;
		fullStopped = false;
		transform.rotation = Quaternion.identity;
		orientation = Orientation.Forward;
		transform.gameObject.GetComponent<Gravity>().fullStopped = false;
		transform.gameObject.GetComponent<Jump>().fullStopped = false;
		transform.Find("Model").gameObject.SetActive(true);
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
		if (!gravity.Grounded())
			return;

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
		if (godmode)
			return;

		Collider[] collisions = Physics.OverlapSphere(nextPosition, size/2, obstacleLayer);
		if (collisions.Length > 0 && !dead) 
		{
			StartCoroutine(Die());
		}
	}

	private IEnumerator Die() 
	{
		dead = true;
		FullStop();
		deathSound.Play();
		transform.Find("Model").gameObject.SetActive(false);
		transform.Find("DeathParticles").gameObject.GetComponent<ParticleSystem>().Play();
		StartCoroutine(cameraShake.Shake(0.4f, 0.2f));
		yield return new WaitForSeconds(1.5f);

		// Erase coin progress in this screen here
		ui.OnDeath();

		// Reload the current screen
		nextScreen.ReloadCurrentScreen();
		//SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	private void CheckCollisionWithStopPoint(Vector3 nextPosition) 
	{
		if (stopped)
			return;

		Collider[] collisions = Physics.OverlapSphere(nextPosition, size/2, stopLayer);
		if (collisions.Length > 0 && !collidedWithStopPoint)
		{
			stopped = true;
			collidedWithStopPoint = true;
			Vector3 movement = collisions[0].gameObject.transform.position - transform.position;
			controller.Move(new Vector3(movement.x, 0.0f, movement.z));
			GetComponent<Animator>().SetTrigger("stop");
		}
		else if (collisions.Length == 0 && collidedWithStopPoint) 
		{
			collidedWithStopPoint = false;
		}
	}

	private void CheckCollisionWithJumpPoint(Vector3 nextPosition) 
	{
		if (!godmode)
			return;

		Collider[] collisions = Physics.OverlapSphere(nextPosition, size / 2, jumpLayer);
		if (collisions.Length > 0 && !collidedWithJumpPoint)
		{
			collidedWithJumpPoint = true;
			GetComponent<Jump>().DoJump();
		}
		else if (collisions.Length == 0 && collidedWithJumpPoint)
		{
			collidedWithJumpPoint = false;
		}
	}

	private void CheckCollisionWithProgressPoint(Vector3 nextPosition)
	{
		Collider[] collisions = Physics.OverlapSphere(nextPosition, size / 2, progressLayer);
		if (collisions.Length > 0 && !collidedWithProgressPoint)
		{
			collidedWithProgressPoint = true;
			ui.Progress(collisions[0].gameObject.GetComponent<Progress>().GetProgress());
		}
		else if (collisions.Length == 0 && collidedWithProgressPoint)
		{
			collidedWithProgressPoint = false;
		}
	}

	private GameObject FirstActiveChild(Transform objTransform)
	{
        foreach (Transform child in objTransform)
        {
            if (child.gameObject.activeSelf)
            {
                return child.gameObject;
            }
        }
		return null;
    }
}
