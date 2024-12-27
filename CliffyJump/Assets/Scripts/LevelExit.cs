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
            player.GetComponent<PlayerMove>().FullStop();
            player.GetComponent<PlayerMove>().Teleport(entrance.GetComponent<LevelEntrance>().GetStartPosition());
			nextScreen.LoadNextScreen();
        }
	}
}
