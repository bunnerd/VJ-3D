using UnityEngine;
using UnityEngine.UI;

public class ScreenButtonsManager : MonoBehaviour
{
    public int levelNumber;

    private void Start()
    {
        int lastClearedScreen = PlayerPrefs.GetInt("maxProgress" + levelNumber, 0)/10 + 1;
        Button[] buttons = GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = i < lastClearedScreen;
        }
    }
}
