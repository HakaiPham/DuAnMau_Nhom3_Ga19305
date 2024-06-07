using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _Right;
    [SerializeField] private float _Left;
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
    }

    // Update is called once per frame
    void Update()
    {
        MoveBlink();
    }
    public void MoveBlink()
    {
        var blinkPosition = transform.position;
        //Debug.Log("Vi tri hien tai: " + blinkPosition.x);
        if (blinkPosition.x >= _Right || blinkPosition.x <= _Left)
        {
           ismove = !ismove;//Đổi hướng di chuyển
            _currentTime = _WaitTime;//Đặt lại thời gian dừng
        }
        if (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
            _rb.velocity = Vector2.zero;//Dừng đối tượng trong thời gian chờ
            Debug.Log("Thoi gian chờ còn :" + _currentTime);
        }
        else
        {
            if(_currentTime < 0)
            {
                _currentTime = 0;   
            }
            _rb.velocity = ismove ? Vector2.right * _Speed : Vector2.left * _Speed;
            Debug.Log("Đang di chuyển: " + (ismove ? "phải" : "trái"));
        }
    }
    public void OffAnimation()
    {
        _animator.SetBool("isBlinkAttack", false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            _animator.SetBool("isBlinkAttack", true);
            Invoke("OffAnimation", 0.3f);
        }
    }
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Wall"))
    //    {
    //        _animator.SetBool("isBlinkAttack", false);
    //    }
    //}
}
