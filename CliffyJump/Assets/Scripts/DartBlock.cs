using UnityEngine;

public class DartBlock : MonoBehaviour
{
	public GameObject dartPrefab;
	public float offset = 0.0f;
	public float cooldown;
	public float dartSpeed;

	private float start;
	[SerializeField] private GameObject[] darts;
	private int dartAmount;
	private int currentDart = 0;
	private Vector3 dartSpeedVec;

	bool shotOnce = false;
	bool init = false;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (!init)
			return;

		if (!shotOnce)
		{
			if (Time.time - start >= offset)
			{
				shotOnce = true;
				Shoot();
			}
		}
		else if (Time.time - start >= cooldown)
		{
			Shoot();
		}
	}

	public void Init()
	{
		Debug.Log("Init");

		shotOnce = false;
		currentDart = 0;
		start = Time.time;

		// Idea:
		// dartsPerSecond = 1.0f / cooldown;
		// timeToExitScreen = 50.0f / dartSpeed;
		// dartAmount = ceil(dartsPerSecond * timeToExitScreen)

		// Simplified operation:
		dartAmount = Mathf.CeilToInt(50.0f / (cooldown * dartSpeed)) + 3;
		darts = new GameObject[dartAmount];

		for (int i = 0; i < dartAmount; ++i)
		{
			GameObject dartInstance = Instantiate(dartPrefab);

			// Set parent
			dartInstance.transform.parent = transform;
			dartInstance.transform.position = transform.position;
			dartInstance.transform.Rotate(0.0f, 0.0f, 90.0f);
			darts[i] = dartInstance;
		}

		if (transform.rotation.y == 0.0f)
			dartSpeedVec = new Vector3(dartSpeed, 0.0f, 0.0f);
		else if (transform.rotation.y == 180.0f)
			dartSpeedVec = new Vector3(-dartSpeed, 0.0f, 0.0f);
		else if (transform.rotation.y == 90.0f)
			dartSpeedVec = new Vector3(0.0f, 0.0f, -dartSpeed);
		else if (transform.rotation.y == 270.0f)
			dartSpeedVec = new Vector3(0.0f, 0.0f, dartSpeed);

		init = true;
	}

	void Shoot()
	{
		start = Time.time;

		// Get component and stuff
		darts[currentDart].GetComponent<Dart>().Shoot(dartSpeedVec);

		if (++currentDart == dartAmount)
			currentDart = 0;
	}
}
