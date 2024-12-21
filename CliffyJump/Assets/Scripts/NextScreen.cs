using UnityEngine;

public class NextScreen : MonoBehaviour
{
    public GameObject player;
    public GameObject[] screens = new GameObject[10];

    private int currentScreen = -1;

    public void LoadNextScreen() 
    {
        // No more screens, we have beaten the level
        if (++currentScreen == 10) 
        {
            return;
        }

        // More screens, load the next
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		Application.targetFrameRate = 60;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
