using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Screen : MonoBehaviour
{
    [SerializeField] private AnimationCurve animCurve;
    public GameObject[] objects;
    public GameObject[] obstacles;
    public GameObject player;

    public float startHeight = -5.0f;
    public float endHeight = 2.0f;
    public float duration = 1.0f;

    private List<GameObject> saws = new List<GameObject>();

    void Start()
    {
        Load();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha0))
        {
            StartCoroutine(LowerGround());
        }
    }

    public void Load() 
    {
        transform.gameObject.SetActive(true);

		List<GameObject> tmp = new List<GameObject>();
		foreach (Transform child in GetComponentsInChildren<Transform>())
			// Gets objects in the Obstacle (3) layer. Extra checks are to ensure the prefab's children aren't added
			if (child.gameObject.layer == 3 && child.parent != null && child.parent.gameObject.layer != 3)
				tmp.Add(child.gameObject);
			// Gets objects in the StopPoint (7) layer. 
			else if (child.gameObject.layer == 7 && child.parent != null && child.parent.gameObject.layer != 7)
				tmp.Add(child.gameObject);
			else if (child.gameObject.CompareTag("Saw"))
			{
				saws.Add(child.gameObject);
				child.gameObject.SetActive(false);
			}

		obstacles = tmp.ToArray();
		foreach (GameObject obstacle in obstacles)
			obstacle.SetActive(false);

		player.GetComponent<PlayerMove>().enabled = false;

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
		Debug.Log("Raise ground!");
		
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
        yield return new WaitForSeconds(0.75f);

        Debug.Log("Spawn obstacles!");

        foreach (GameObject saw in saws)
            saw.SetActive(true);

		foreach (GameObject obstacle in obstacles) 
        {
            obstacle.SetActive(true);
            StartCoroutine(obstacle.GetComponent<ObstacleManager>().LoadObstacle());
        }
        yield return null;
    }

    public IEnumerator DespawnObstacles() 
    {
		Debug.Log("Despawn obstacles!");

		foreach (GameObject obstacle in obstacles)
		{
			StartCoroutine(obstacle.GetComponent<ObstacleManager>().UnloadObstacle());
		}
		yield return null;
	}

    public IEnumerator EnablePlayer() 
    {
        yield return new WaitForSeconds(1.15f);
        player.GetComponent<PlayerMove>().enabled = true;
        yield return null;
    }
}
