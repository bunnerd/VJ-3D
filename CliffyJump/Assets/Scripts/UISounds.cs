using UnityEngine;
using UnityEngine.UI;

public class UISounds : MonoBehaviour
{
    public AudioSource audioSource;
    void Start()
    {
        Button b = gameObject.GetComponent<Button>();
        b.onClick.AddListener(PlayButtonSound);
    }

    private void PlayButtonSound()
    {
        audioSource.Play();
    }
}
