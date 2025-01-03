using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenCheat : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			PlayerPrefs.SetInt("selectedScreen", 0);
			SceneManager.LoadScene("Level1");
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			PlayerPrefs.SetInt("selectedScreen", 0);
			SceneManager.LoadScene("Level2");
		}
	}
}
