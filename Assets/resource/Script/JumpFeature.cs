using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFeature : MonoBehaviour
{
    Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _animator.SetBool("isjumpfeature", true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _animator.SetBool("isjumpfeature", false);
        }
    }
}
