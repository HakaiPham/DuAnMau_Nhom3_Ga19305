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
        if (collision.gameObject.CompareTag("monster")) // khi viên đạn va chạm với quái
        {
            Destroy(collision.gameObject); //tiêu diệt quái
            _rb.velocity = Vector2.zero;// viên đạn sẽ dừng lại
            _animator.SetBool("isskill", true);// chạy aniamtion viên đạn bị phá hũy
            Invoke("DestroySkill", 1f);// sau khi đợi animation chạy xong sẽ tiến hành phá hũy viên đạn
        }
    }
    public void DestroySkill()
    {
        Destroy(gameObject);
    }
}
