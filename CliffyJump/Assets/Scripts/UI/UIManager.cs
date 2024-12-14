using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button exitGameButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        exitGameButton.onClick.AddListener(ExitGame);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
