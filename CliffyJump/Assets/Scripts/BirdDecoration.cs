using System.Collections;
using UnityEngine;

public class BirdDecoration : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            animator.SetTrigger("fly");
            
        }
        else if (Input.GetKey(KeyCode.E))
        {
            animator.SetTrigger("eat");
        }
    }
}
