using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class NextScreen : MonoBehaviour
{
    public GameObject player;
    public GameObject[] screens = new GameObject[10];
	public GameObject entrance;
	public int startScreen = 0;
	public int nextLevel = -1;

	private GameUI ui;

    private int loadedScreen = -1;
	public bool testing = false;

    public void LoadNextScreen() 
    {
		if (loadedScreen == 9) 
		{
			// No more screens!
			if (nextLevel == -1)
			{
                // No more levels!
                SceneManager.LoadScene("Menu");
            }
			else
			{
                PlayerPrefs.SetInt("selectedScreen", 0);
                SceneManager.LoadScene("Level" + nextLevel);
            }
        }
		else
			StartCoroutine(LoadScreen(loadedScreen + 1));
	}

	// Probably unused
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
		ui.OnScreenLoad(i);
		player.GetComponent<PlayerMove>().Teleport(entrance.GetComponent<LevelEntrance>().GetStartPosition());
		yield return new WaitForSeconds(1.15f);
	}

	public void ReloadCurrentScreen() 
	{
		StartCoroutine(LoadScreen(loadedScreen));
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		ui = GameObject.Find("UI").GetComponent<GameUI>();
		if (ui == null)
		{
			Debug.LogError("NextScreen: UI component not found! Make sure there is an UI prefab object in the scene this NextScreen is in");
		}

		if (testing)
			StartCoroutine(LoadScreen(startScreen));
		else
			StartCoroutine(LoadScreen(PlayerPrefs.GetInt("selectedScreen", startScreen)));
	}
}
