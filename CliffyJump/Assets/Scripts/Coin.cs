using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioSource pickUpSound;

    private GameUI ui;
    private bool collected = false;

    // Initialize the coin, making it collectable again
    public void Init() 
    {
        collected = false;
		transform.Find("Model").gameObject.SetActive(true);
	}

	private void Start()
	{
        Init();
		ui = GameObject.Find("UI").GetComponent<GameUI>();
        if (ui == null) 
        {
            Debug.LogError("Coin: UI component not found! Make sure there is an UI prefab object in the scene this coin is in");
        }
	}

	private void OnTriggerEnter(Collider other)
    {
        if (collected)
            return;

        if (other.CompareTag("Player")) 
        {
            Collect();
		}
    }

    private void Collect() 
    {
		collected = true;
		pickUpSound.Play();
		GetComponentInChildren<ParticleSystem>().Play();
		transform.Find("Model").gameObject.SetActive(false);
        ui.CollectCoin();
	}

    public bool Collected() { return collected; }
}
