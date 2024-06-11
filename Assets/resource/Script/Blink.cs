using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _Right = 7.35f;
    [SerializeField] private float _Left = -7.14f;
    [SerializeField] private float _Speed;
    Animator _animator;
    Rigidbody2D _rb;
    bool ismove;
    float _currentTime;// THời gian hiện tại
    [SerializeField] private float _WaitTime;// thời gian chờ đợi
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _currentTime = _WaitTime; //Khởi tạo thời gian dừng
        ismove = false;
        StartCoroutine(MoveBlinkCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        //MoveBlink();
    }
    private IEnumerator MoveBlinkCoroutine()
    {
        while (true)
        {
            var blinkPosition = transform.position;

            // Kiểm tra giới hạn và đổi hướng nếu cần
            if (blinkPosition.x >= _Right  || blinkPosition.x <= _Left)
            {
                ismove = !ismove; // Đổi hướng di chuyển
                Debug.Log("Đổi hướng: " + (ismove ? "phải" : "trái"));
                yield return new WaitForSeconds(_WaitTime); // Đợi trước khi đổi hướng
            }

            // Di chuyển đối tượng
            _rb.velocity = ismove ? Vector2.right * _Speed : Vector2.left * _Speed;
            Debug.Log("Đang di chuyển: " + (ismove ? "phải" : "trái") + " với tốc độ: " + _rb.velocity);

            yield return null; // Đợi đến khung hình tiếp theo
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            _animator.SetBool("Blink", true);
            Invoke("OffAnimation", 0.3f);
        }
    }
    public void OffAnimation()
    {
        _animator.SetBool("Blink", false);
    }
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Wall"))
    //    {
    //        _animator.SetBool("isBlinkAttack", false);
    //    }
    //}
}
