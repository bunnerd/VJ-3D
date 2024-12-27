using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
    [SerializeField] private AnimationCurve animCurve;
    public GameObject[] objects;
    public GameObject[] obstacles;
    public GameObject[] coins;
    public GameObject player;

    public float startHeight = -5.0f;
    public float endHeight = 2.0f;
    public float duration = 1.0f;

    private List<GameObject> saws = new List<GameObject>();

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha0))
        {
            StartCoroutine(LowerGround());
        }
    }

	private void Start()
	{
        // Get the coins
		List<GameObject> tmp = new List<GameObject>();
		foreach (Transform child in GetComponentsInChildren<Transform>())
		{
            if (child.gameObject.CompareTag("Coin"))
            {
                child.gameObject.SetActive(false);
				tmp.Add(child.gameObject);
			}
		}
        coins = tmp.ToArray();
	}

	public void Load() 
    {
        transform.gameObject.SetActive(true);
		saws = new List<GameObject>();

		List<GameObject> tmp = new List<GameObject>();
        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            // Gets objects in the Obstacle (3) layer. Extra checks are to ensure the prefab's children aren't added
            if (child.gameObject.layer == 3 && child.parent != null && child.parent.gameObject.layer != 3)
                tmp.Add(child.gameObject);
            // Gets objects in the StopPoint (7) layer. Same checks here
            else if (child.gameObject.layer == 7 && child.parent != null && child.parent.gameObject.layer != 7)
                tmp.Add(child.gameObject);
            else if (child.gameObject.CompareTag("Saw"))
            {
                saws.Add(child.gameObject);
                child.gameObject.SetActive(false);
            }
        }

		obstacles = tmp.ToArray();
		foreach (GameObject obstacle in obstacles)
			obstacle.SetActive(false);

        player.GetComponent<PlayerMove>().FullStop();

		StartCoroutine(RaiseGround());
		StartCoroutine(SpawnObstacles());
		StartCoroutine(EnablePlayer());
	}

    public void Unload() 
    {
		StartCoroutine(DespawnObstacles());
		StartCoroutine(LowerGround());
	}

    public IEnumerator RaiseGround()
    {		
        float startTime = Time.time;
        while (Time.time - startTime <= duration)
        {
            float t = (Time.time - startTime) / duration;
            float newY = startHeight + animCurve.Evaluate(t) * (endHeight - startHeight);
            for (int i = 0; i < objects.Length; ++i)
            {
                objects[i].transform.position = new Vector3(objects[i].transform.position.x, newY, objects[i].transform.position.z);
            }
            yield return new WaitForEndOfFrame();
        }

        // Make sure the height of the object is exactly endHeight
        for (int i = 0; i < objects.Length; ++i)
        {
            objects[i].transform.position = new Vector3(objects[i].transform.position.x, endHeight, objects[i].transform.position.z);
        }
    }

    public IEnumerator LowerGround()
    {
        // Waits until DespawnObstacles() has finished
        yield return new WaitForSeconds(0.4f);

        float startTime = Time.time;
        while (Time.time - startTime <= duration)
        {
            float t = 1.0f - (Time.time - startTime) / duration;
            float newY = startHeight + animCurve.Evaluate(t) * (endHeight - startHeight);
            for (int i = 0; i < objects.Length; ++i)
            {
                objects[i].transform.position = new Vector3(objects[i].transform.position.x, newY, objects[i].transform.position.z);
            }
            yield return new WaitForEndOfFrame();
        }

		transform.gameObject.SetActive(false);
	}

    public IEnumerator SpawnObstacles() 
    {
        // Waits until RaiseGround() has finished
        yield return new WaitForSeconds(0.75f);

        foreach (GameObject coin in coins) 
        {
			coin.SetActive(true);
            coin.GetComponent<Coin>().Init();
			StartCoroutine(coin.GetComponent<CoinLoader>().Load());
		}

        // Special case for the saw obstacle
        foreach (GameObject saw in saws)
            saw.SetActive(true);

        // Load animation for all obstacles (and stop points)
		foreach (GameObject obstacle in obstacles) 
        {
            obstacle.SetActive(true);
            StartCoroutine(obstacle.GetComponent<ObstacleManager>().LoadObstacle());
        }
        yield return null;
    }

    public IEnumerator DespawnObstacles() 
    {
        foreach (GameObject coin in coins) 
        {
			StartCoroutine(coin.GetComponent<CoinLoader>().Unload());
		}

		foreach (GameObject obstacle in obstacles)
		{
			StartCoroutine(obstacle.GetComponent<ObstacleManager>().UnloadObstacle());
		}
		yield return null;
	}

    public IEnumerator EnablePlayer() 
    {
        yield return new WaitForSeconds(1.15f);
		player.GetComponent<PlayerMove>().EnableMove();
        yield return null;
    }
}
