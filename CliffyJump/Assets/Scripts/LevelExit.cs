using UnityEngine;
using UnityEngine.Rendering.UI;

public class LevelExit : MonoBehaviour
{
    public GameObject entrance;
	public NextScreen nextScreen;
	public GameObject player;

	private GameUI ui;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		ui = GameObject.Find("UI").GetComponent<GameUI>();
		if (ui == null)
		{
			Debug.LogError("LevelExit: UI component not found! Make sure there is an UI prefab object in the scene this Exit is in");
		}
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
			ui.OnScreenClear();
			nextScreen.LoadNextScreen();
        }
	}
}
