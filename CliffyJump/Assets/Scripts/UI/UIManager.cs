using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button exitGameButton;

    public TextMeshProUGUI sakuraProgress;
    public TextMeshProUGUI ruinsProgress;

    void Start()
    {
        exitGameButton.onClick.AddListener(ExitGame);
        sakuraProgress.text = PlayerPrefs.GetInt("maxProgress" + 0, 0).ToString() + "%";
        ruinsProgress.text = PlayerPrefs.GetInt("maxProgress" + 1, 0).ToString() + "%";
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
