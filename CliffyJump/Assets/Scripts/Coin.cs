using System.Collections;
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
        //pickUpSound.Play();
        //GetComponentInChildren<ParticleSystem>().Play();
        //transform.Find("Model").gameObject.SetActive(false);
        //StartCoroutine(Disable(2f));
    }

    //private IEnumerator Disable(float seconds)
    //{
    //    yield return new WaitForSeconds(seconds);
    //    gameObject.SetActive(false);
    //>>> bda959ef68850917a2851f191e7ed1810a3ff78c
    //}
}
