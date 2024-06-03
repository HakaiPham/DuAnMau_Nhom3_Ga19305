using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    // Start is called before the first frame update
    Animator _animator;
    Rigidbody2D _rb;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("monster"))
        {
            Destroy(collision.gameObject);
            _rb.velocity = Vector2.zero;
            _animator.SetBool("isskill", true);
            Invoke("DestroySkill", 1f);
        }
    }
    public void DestroySkill()
    {
        Destroy(gameObject);
    }
}
