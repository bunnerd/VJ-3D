using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("coin");
        GetComponentInChildren<ParticleSystem>().Play();
        transform.Find("Model").gameObject.SetActive(false);
    }
}
