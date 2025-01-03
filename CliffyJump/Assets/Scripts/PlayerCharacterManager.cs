using UnityEngine;

public class PlayerCharacterManager : MonoBehaviour
{
    public GameObject[] models;
    public GameObject[] trails;
    public Color[] deathParticlesColors;

    void Start()
    {
        int charIndex = PlayerPrefs.GetInt("character", 0);

        GetComponent<Animator>().SetInteger("charIndex", charIndex);

        // Disable all the other models and trails
        foreach (Transform model in transform.Find("Model"))
        {
            if (model.gameObject != models[charIndex])
            {
                model.gameObject.SetActive(false);
            }
        }
        foreach (Transform trail in transform.Find("Trail"))
        {
            if (trail.gameObject != trails[charIndex])
            {
                trail.gameObject.SetActive(false);
            }
        }

        // Enable the selected model and its corresponding trail
        models[charIndex].SetActive(true);
        trails[charIndex].SetActive(true);

        // Set the death particles' color to match the model
        var main = transform.Find("DeathParticles").GetComponent<ParticleSystem>().main;
        main.startColor = deathParticlesColors[charIndex];
    }
}
