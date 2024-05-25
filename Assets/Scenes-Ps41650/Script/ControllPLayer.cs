using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllPlayer: MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _speed;
    Animator _Animator;
    BoxCollider2D _Collider;
    Rigidbody2D _Rigidbody;
    [SerializeField] private float _JumpPower;
    void Start()
    {
        _Animator = GetComponent<Animator>();
        _Collider = GetComponent<BoxCollider2D>();
        _Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var localScale = transform.localScale;
        if (horizontalInput >0||horizontalInput < 0)
        {
            _Animator.SetBool("isrun", true);
            localScale.x = Mathf.Sign(horizontalInput);
            transform.localScale = localScale;
            transform.localPosition += new Vector3(horizontalInput, 0, 0) * _speed * Time.deltaTime;
        }
        else if (horizontalInput == 0)
        {
            _Animator.SetBool("isrun", false);
        }
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            Jump();
        }
        else if(!Input.GetKeyDown(KeyCode.Space))
        {
            _Animator.SetBool("isjump", false);
        }
    }
    public void Jump()
    {
        if(!_Collider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if (_Collider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            _Animator.SetBool("isjump", true);
            _Rigidbody.velocity = new Vector2(_Rigidbody.velocity.x, _JumpPower);
            //_Rigidbody.AddForce(new Vector2(0, _JumpPower));
        }
    }
}
