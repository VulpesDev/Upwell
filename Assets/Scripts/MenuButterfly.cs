using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButterfly : MonoBehaviour
{
    Animator    animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Land() {
        animator.SetBool("Idle", true);
        StartCoroutine(RandomizeBehaviour());
    }

    IEnumerator RandomizeBehaviour() {
        yield return new WaitForSeconds(Random.Range(1.5f, 5.0f));
        animator.SetBool("Idle", false);
        yield return new WaitForSeconds(Random.Range(0.1f, 0.8f));
        animator.SetBool("Idle", true);
        StartCoroutine(RandomizeBehaviour());
    }
}
