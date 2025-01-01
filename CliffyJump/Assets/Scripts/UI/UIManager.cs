using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button exitGameButton;

    public TextMeshProUGUI sakuraProgressText;
    public TextMeshProUGUI ruinsProgressText;
    public TextMeshProUGUI coinsText;

    public ScreenButtonsManager sakuraButtons;
    public ScreenButtonsManager ruinsButtons;

    void Start()
    {
        exitGameButton.onClick.AddListener(ExitGame);
        sakuraProgressText.text = PlayerPrefs.GetInt("maxProgress" + 1, 0).ToString() + "%";
        ruinsProgressText.text = PlayerPrefs.GetInt("maxProgress" + 2, 0).ToString() + "%";
        coinsText.text = PlayerPrefs.GetInt("coins", 0).ToString();
    }

	private void Update()
	{
        // Adds progress
		if (Input.GetKeyDown(KeyCode.A)) 
        {
            PlayerPrefs.SetInt("maxProgress" + 1, 100);
            PlayerPrefs.SetInt("maxProgress" + 2, 100);
			sakuraProgressText.text = PlayerPrefs.GetInt("maxProgress" + 1, 0).ToString() + "%";
			ruinsProgressText.text = PlayerPrefs.GetInt("maxProgress" + 2, 0).ToString() + "%";
            sakuraButtons.UpdateButtons();
			ruinsButtons.UpdateButtons();
		}
        // Removes progress
        else if (Input.GetKeyDown(KeyCode.D))
        {
			PlayerPrefs.SetInt("maxProgress" + 1, 0);
			PlayerPrefs.SetInt("maxProgress" + 2, 0);
			sakuraProgressText.text = PlayerPrefs.GetInt("maxProgress" + 1, 0).ToString() + "%";
			ruinsProgressText.text = PlayerPrefs.GetInt("maxProgress" + 2, 0).ToString() + "%";
			sakuraButtons.UpdateButtons();
			ruinsButtons.UpdateButtons();
		}
	}

	private void ExitGame()
    {
        Application.Quit();
    }
}
