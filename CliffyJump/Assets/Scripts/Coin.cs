using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioSource pickUpSound;

    private void OnTriggerEnter(Collider other)
    {
        pickUpSound.Play();
        GetComponentInChildren<ParticleSystem>().Play();
        transform.Find("Model").gameObject.SetActive(false);
    }
}
