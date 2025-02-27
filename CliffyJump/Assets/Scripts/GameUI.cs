using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI progressText;

    private int displayCoins;
    private int realCoins;

    private int currentLevelMaxProgress;
    private int currentProgress;

    public int currentLevel;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		SetLevel(currentLevel);
        realCoins = PlayerPrefs.GetInt("coins", 0);
        displayCoins = realCoins;
        UpdateCoinDisplay();
    }

	public void SetLevel(int level) 
    {
        currentLevelMaxProgress = PlayerPrefs.GetInt("maxProgress" + level, 0);
	}

    public void OnScreenLoad(int screenIndex)
    {
        Progress(screenIndex * 10);
    }

    public void Progress(int progress) 
    {
		currentLevelMaxProgress = PlayerPrefs.GetInt("maxProgress" + currentLevel, 0);
		currentProgress = progress;
        if (progress > currentLevelMaxProgress) 
        {
            currentLevelMaxProgress = progress;
            PlayerPrefs.SetInt("maxProgress" + currentLevel, progress);
        }
        UpdateProgressDisplay();
	}

    private void UpdateProgressDisplay()
    {
        progressText.SetText(currentProgress.ToString());

	}

    public void CollectCoin() 
    {
        ++displayCoins;
		UpdateCoinDisplay();
    }

    public void OnScreenClear() 
    {
        realCoins = displayCoins;
        PlayerPrefs.SetInt("coins", realCoins);
		UpdateCoinDisplay();
	}

    public void OnDeath() 
    {
        displayCoins = realCoins;
		UpdateCoinDisplay();
    }

    private void UpdateCoinDisplay() 
    {
        coinsText.SetText(displayCoins.ToString());
    }
}
