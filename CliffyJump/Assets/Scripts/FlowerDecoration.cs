using UnityEngine;
using UnityEngine.Rendering;

public class FlowerDecoration : MonoBehaviour
{
    ParticleSystem petalParticles;
    public AudioSource sound;

    private void Start()
    {
        petalParticles = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        petalParticles.Play();
        sound.Play();
        transform.Find("flowersNoPetals").gameObject.SetActive(true);
        transform.Find("flowers").gameObject.SetActive(false);
    }
}
