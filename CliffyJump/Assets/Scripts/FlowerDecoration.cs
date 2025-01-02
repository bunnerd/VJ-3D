using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class FlowerDecoration : Decoration
{
    ParticleSystem petalParticles;
    public AudioSource sound;

    public AnimationCurve animationCurve;

    public float startScale = 0f;
    public float endScale = 1.25f;
    public float duration = 0.4f;

    private void Start()
    {
        petalParticles = GetComponentInChildren<ParticleSystem>();
    }

    public override void Init()
    {

    }

    public override IEnumerator Load()
    {
        gameObject.SetActive(true);
        transform.Find("flowersNoPetals").gameObject.SetActive(false);
        transform.Find("flowers").gameObject.SetActive(true);
        float startTime = Time.time;

        while (Time.time - startTime <= duration)
        {
            float t = (Time.time - startTime) / duration;
            float scale = startScale + animationCurve.Evaluate(t) * (endScale - startScale);
            transform.localScale = new Vector3(scale, scale, scale);
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = new Vector3(endScale, endScale, endScale);
    }

    public override IEnumerator Unload()
    {
        float startTime = Time.time;

        while (Time.time - startTime <= duration)
        {
            float t = (Time.time - startTime) / duration;
            float scale = startScale + animationCurve.Evaluate(1 - t) * (endScale - startScale);
            transform.localScale = new Vector3(scale, scale, scale);
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = new Vector3(startScale, startScale, startScale);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            petalParticles.Play();
            sound.Play();
            transform.Find("flowersNoPetals").gameObject.SetActive(true);
            transform.Find("flowers").gameObject.SetActive(false);
        }
    }
}
