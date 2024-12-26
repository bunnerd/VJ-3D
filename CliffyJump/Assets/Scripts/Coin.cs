using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioSource pickUpSound;
    private bool collected = false;

    // Initialize the coin, making it collectable again
    public void Init() 
    {
        collected = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collected)
            return;

        if (other.CompareTag("Player")) 
        {
			collected = true;
			pickUpSound.Play();
			GetComponentInChildren<ParticleSystem>().Play();
			transform.Find("Model").gameObject.SetActive(false);
		}
    }
}
