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

    private int currentLevel;

    public void SetLevel(int level) 
    {
        currentLevel = level;
        currentLevelMaxProgress = PlayerPrefs.GetInt("maxProgress" + level, 0);
	}

    public void OnScreenLoad(int screenIndex)
    {
        Progress(screenIndex * 10);
    }

    public void Progress(int progress) 
    {
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        realCoins = PlayerPrefs.GetInt("coins", 0);
        displayCoins = realCoins;
		UpdateCoinDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
