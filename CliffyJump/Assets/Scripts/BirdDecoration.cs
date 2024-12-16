using System.Collections;
using UnityEngine;

public class BirdDecoration : MonoBehaviour
{
    Animator animator;
    bool hasFlown = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(IdleAnimationLoop());
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Fly());
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

    private IEnumerator Fly()
    {
        hasFlown = true;
        animator.SetTrigger("fly");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        gameObject.SetActive(false);
    }
}
