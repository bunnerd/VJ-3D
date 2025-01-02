using System.Collections;
using UnityEngine;

public class BirdDecoration : Decoration
{
    public bool playSound = true;

    public AnimationCurve heightCurve;
    public AnimationCurve xzCurve;
    public float startHeight = 20f;
    public float endHeight = 0f;
    public float duration = 1f;

    Animator animator;
    bool hasFlown = false;

    public override void Init()
    {
        animator = GetComponent<Animator>();
        transform.Find("RotateThis/Model").localPosition = Vector3.zero;
    }

    public override IEnumerator Load()
    {
        gameObject.SetActive(true);
        if (playSound)
            GetComponent<AudioSource>().Play();

        // Land animation by code //
        animator.SetTrigger("flapWings");
        float startTime = Time.time;

        float startX = Random.Range(0, 4);
        float startZ = 4 - startX;

        while (Time.time - startTime <= duration)
        {
            float t = (Time.time - startTime) / duration;
            float y = startHeight + heightCurve.Evaluate(t) * (endHeight - startHeight);
            float x = startX + xzCurve.Evaluate(t) * (0 - startX);
            float z = startZ + xzCurve.Evaluate(t) * (0 - startZ);
            transform.Find("RotateThis").localPosition = new Vector3(x, y, z);
            yield return new WaitForEndOfFrame();
        }
        transform.Find("RotateThis").localPosition = new Vector3(0, endHeight, 0);

        animator.SetTrigger("stopWings");
        // End of land animation //

        hasFlown = false;
        transform.Find("RotateThis/Model").localPosition = Vector3.zero;
        StartCoroutine(IdleAnimationLoop());
        yield return null;
    }

    public override IEnumerator Unload()
    {
        if (!hasFlown)
        {
            if (playSound)
                GetComponent<AudioSource>().Play();
            hasFlown = true;
            animator.SetTrigger("fly");
            animator.SetTrigger("flapWings");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
            gameObject.SetActive(false);
            transform.Find("RotateThis/Model").localPosition = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            StartCoroutine(Unload());
    }

    private IEnumerator IdleAnimationLoop()
    {
        while (!hasFlown)
        {
            yield return new WaitForSeconds(Random.Range(1f, 2f));
            if (!hasFlown)
                animator.SetTrigger("eat");
        }
    }
}
