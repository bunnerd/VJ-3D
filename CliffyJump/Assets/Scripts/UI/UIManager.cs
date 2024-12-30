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

    void Start()
    {
        exitGameButton.onClick.AddListener(ExitGame);
        sakuraProgressText.text = PlayerPrefs.GetInt("maxProgress" + 1, 0).ToString() + "%";
        ruinsProgressText.text = PlayerPrefs.GetInt("maxProgress" + 2, 0).ToString() + "%";
        coinsText.text = PlayerPrefs.GetInt("coins", 0).ToString();
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
