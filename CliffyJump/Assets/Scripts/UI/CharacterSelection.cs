using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public Button rightArrow;
    public Button leftArrow;
    public Button okButton;
    public GameObject charactersParent;
    public GameObject charactersScreen;
    public GameObject mainMenuScreen;

    private int charIndex = 0;
    private int numCharacters;

    private float startTime;
    public float duration;
    private int direction;
    private bool changingPos = false;
    private float lastX;
    public AnimationCurve movementCurve;

    void Start()
    {
        rightArrow.onClick.AddListener(RightArrowPressed);
        leftArrow.onClick.AddListener(LeftArrowPressed);
        okButton.onClick.AddListener(SaveSelectedCharacter);
        numCharacters = charactersParent.transform.childCount;

        lastX = charactersParent.GetComponent<RectTransform>().localPosition.x;
    }

    private void Update()
    {
        if (changingPos)
        {
            float t = (Time.time - startTime) / duration;
            //float newX = Mathf.SmoothStep(lastX, lastX + direction * charactersParent.GetComponent<RectTransform>().sizeDelta.x, t);
            float newX = lastX + direction * movementCurve.Evaluate(t) * charactersParent.GetComponent<RectTransform>().sizeDelta.x;
            charactersParent.GetComponent<RectTransform>().localPosition = new Vector3(newX, 0, 0);
            if (t >= 1)
            {
                //charactersParent.GetComponent<RectTransform>().localPosition = new Vector3 (lastX + direction * charactersParent.GetComponent<RectTransform>().sizeDelta.x, 0, 0);
                lastX = charactersParent.GetComponent<RectTransform>().localPosition.x;
                changingPos = false;
            }
        }
    }

    private void RightArrowPressed()
    {
        if (charIndex < numCharacters-1)
        {
            direction = -1;
            changingPos = true;
            startTime = Time.time;
            charIndex++;
        }
    }
    private void LeftArrowPressed()
    {
        if (charIndex > 0)
        {
            direction = 1;
            changingPos = true;
            startTime = Time.time;
            charIndex--;
        }
    }

    private void SaveSelectedCharacter()
    {
        PlayerPrefs.SetInt("character", charIndex);
        charactersScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
    }
}
