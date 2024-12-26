using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioSource pickUpSound;

    private void OnTriggerEnter(Collider other)
    {
        pickUpSound.Play();
        GetComponentInChildren<ParticleSystem>().Play();
        transform.Find("Model").gameObject.SetActive(false);
        StartCoroutine(Disable(2f));
    }

    private IEnumerator Disable(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }
}
