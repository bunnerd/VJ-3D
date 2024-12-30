using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StopMenu: MonoBehaviour
{
    public GameObject player;
    public GameObject countdownText;
    public int countdownSecs = 3;
    public GameObject stopMenu;

    public void Show()
    {
        stopMenu.SetActive(true);
        gameObject.SetActive(true);
        player.GetComponent<Jump>().fullStopped = true;
        Time.timeScale = 0f;
    }
    
    public void Hide()
    {
        stopMenu.SetActive(false);
        StartCoroutine(Countdown());
    }

    public void LoadMainMenu()
    {
        player.GetComponent<Jump>().fullStopped = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync("Menu");
    }

    private IEnumerator Countdown()
    {
        // Countdown
        countdownText.SetActive(true);
        TextMeshProUGUI textComponent = countdownText.GetComponent<TextMeshProUGUI>();
        for (int i = 0; i < countdownSecs; ++i)
        {
            textComponent.text = (countdownSecs - i).ToString();
            yield return new WaitForSecondsRealtime(1.0f);
        }
        countdownText.SetActive(false);

        // Restart
        gameObject.SetActive(false);
        player.GetComponent<Jump>().fullStopped = false;
        Time.timeScale = 1.0f;
    }
}
