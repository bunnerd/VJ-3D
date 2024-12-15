using UnityEngine;
using UnityEngine.Rendering;

public class FlowerDecoration : MonoBehaviour
{
    ParticleSystem petalParticles;

    private void Start()
    {
        petalParticles = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        petalParticles.Play();
    }
}
