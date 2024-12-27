using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI coinsText;

    private int displayCoins;
    private int realCoins;

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
