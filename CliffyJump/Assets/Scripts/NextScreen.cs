using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NextScreen : MonoBehaviour
{
    public GameObject player;
    public GameObject[] screens = new GameObject[10];

    private int currentScreen = 0;

    public IEnumerator LoadNextScreen() 
    {
        if (currentScreen >= 0) 
        {
            screens[currentScreen].GetComponent<Screen>().Unload();
            yield return new WaitForSeconds(1.15f);
        }

        // No more screens, we have beaten the level
        if (++currentScreen >= 10) 
        {
            yield return null;
        }

		// More screens, load the next
		screens[currentScreen].GetComponent<Screen>().Load();
        yield return new WaitForSeconds(1.15f);
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
