using UnityEngine;
using UnityEngine.Rendering.UI;

public class LevelExit : MonoBehaviour
{
    public GameObject entrance;
	public NextScreen nextScreen;
	public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.CompareTag("Player")) 
        {
            Debug.Log("Trigger enter!!!!!!");

            player.GetComponent<PlayerMove>().enabled = false;
            Debug.Log("Player pos pre TP: " + player.transform.position);
            player.GetComponent<PlayerMove>().Teleport(entrance.GetComponent<LevelEntrance>().GetStartPosition());
			Debug.Log("Player pos post TP: " + player.transform.position);
			StartCoroutine(nextScreen.LoadNextScreen());
        }
	}
}
