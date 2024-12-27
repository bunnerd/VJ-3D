using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NextScreen : MonoBehaviour
{
    public GameObject player;
    public GameObject[] screens = new GameObject[10];

    private int loadedScreen = -1;

    public void LoadNextScreen() 
    {
		if (loadedScreen == 9) 
		{
			// No more screens!
		}
		else
			StartCoroutine(LoadScreen(loadedScreen + 1));
	}

	public void LoadPrevScreen()
	{
		if (loadedScreen == 0)
		{
			// No more screens!
		}
		else
			StartCoroutine(LoadScreen(loadedScreen - 1));
	}

	// Loads the ith screen. Starts at 0
    public IEnumerator LoadScreen(int i) 
    {
		if (loadedScreen >= 0)
		{
			screens[loadedScreen].GetComponent<Screen>().Unload();
			yield return new WaitForSeconds(1.15f);
		}

		screens[i].GetComponent<Screen>().Load();
		loadedScreen = i;
		yield return new WaitForSeconds(1.15f);
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		Application.targetFrameRate = 60;
        StartCoroutine(LoadScreen(0));
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.RightArrow)) 
		{
			player.GetComponent<PlayerMove>().FullStop();
			LoadNextScreen();
		}
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			player.GetComponent<PlayerMove>().FullStop();
			LoadPrevScreen();
		}
	}
}
