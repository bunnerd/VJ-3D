using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NextScreen : MonoBehaviour
{
    public GameObject player;
    public GameObject[] screens = new GameObject[10];
	public GameObject entrance;

	private GameUI ui;

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
		StartCoroutine(LoadScreen(PlayerPrefs.GetInt("selectedScreen", 0)));
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
