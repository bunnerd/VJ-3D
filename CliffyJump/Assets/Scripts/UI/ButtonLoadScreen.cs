using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonLoadScreen : MonoBehaviour
{
    public int level, screen;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(LoadScreen);
    }

    public void LoadScreen()
    {
        PlayerPrefs.SetInt("selectedScreen", screen);
        SceneManager.LoadSceneAsync("Level" + level);
    }
}
