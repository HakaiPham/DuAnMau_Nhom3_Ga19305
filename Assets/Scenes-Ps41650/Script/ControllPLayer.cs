using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.UI;
using TMPro;

public class ControllPLayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _speed;
    Animator _Animator;
    BoxCollider2D _Collider;
    Rigidbody2D _Rigidbody;
    [SerializeField] private float _JumpPower;
    [SerializeField] private GameObject _bullet;
    [SerializeField] Transform _locatedAttack;
     private float _reloadBullet;
    [SerializeField] private float _reloadBulletTime;
    [SerializeField] private float _speedClimp;
    [SerializeField] private TextMeshProUGUI _ScoreText;
    int score;
    [SerializeField] private GameObject _PanelGameover;
    [SerializeField] private GameObject _PanelMenu;
    [SerializeField] private Image _ReloadBulletFill;
    [SerializeField] private GameObject _PanelReloadBullet;
    void Start()
    {
        _Animator = GetComponent<Animator>();
        _Collider = GetComponent<BoxCollider2D>();
        _Rigidbody = GetComponent<Rigidbody2D>();
        _reloadBullet = 0;
        score = 0;
        _ScoreText.text = score.ToString("");
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var localScale = transform.localScale;
        if (horizontalInput >0||horizontalInput < 0)
        {
            _Animator.SetBool("isrun", true);
            localScale.x = Mathf.Abs(localScale.x) * Mathf.Sign(horizontalInput);
            transform.localScale = localScale;
            transform.localPosition += new Vector3(horizontalInput, 0, 0) * _speed * Time.deltaTime;
        }
        else if (horizontalInput == 0)
        {
            _Animator.SetBool("isrun", false);
        }
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            _Animator.SetBool("isjump", true);
            Jump();
        }
        else if (_Collider.IsTouchingLayers(LayerMask.GetMask("Ground")) && !Input.GetKeyDown(KeyCode.Space))
        {
            _Animator.SetBool("isjump", false);
        }
        _reloadBullet -= Time.deltaTime;
        //Debug.Log("Thoi gian reload bullet  ban dau con: " + _reloadBullet);
        if (_reloadBullet <= 0&&Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("Thoa man dieu kien");
            _Animator.SetBool("isattack", true);
            PlayerAtack();
            _ReloadBulletFill.fillAmount = 0; 
            StartCoroutine(ReloadBulletImage());
            _reloadBullet = _reloadBulletTime;
        }
        else
        {
            if (!Input.GetKeyDown(KeyCode.E)) { _Animator.SetBool("isattack", false); }
        }
        ClimbLadder();
    }
    IEnumerator ReloadBulletImage()
    {
        for(int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.1f);
            _ReloadBulletFill.fillAmount += 0.1f;
        }
    }
    public void Jump()
    {
        if(!_Collider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if (_Collider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            _Rigidbody.velocity = new Vector2(_Rigidbody.velocity.x, _JumpPower);
            //_Rigidbody.AddForce(new Vector2(0, _JumpPower));
            if(Input.GetKeyDown(KeyCode.E) ) 
            {
                _Animator.SetBool("isJumbOrAttack", true);
            }
            else
            {
                _Animator.SetBool("isJumbOrAttack", false);
            }
        }
    }
    public void PlayerAtack()
    {
            Invoke("CreateSkill", 0.4f);
    }
    public void CreateSkill()
    {
        var createBullet = Instantiate(_bullet, _locatedAttack.position, Quaternion.identity);
        var speedAttack = new Vector2(5f, 0);
        var localscale = transform.localScale;
        if (localscale.x < 0)
        {
           speedAttack = new Vector2(-5f, 0);
            createBullet.transform.localScale = new Vector3(-3, -3, -3);
        }
        createBullet.GetComponent<Rigidbody2D>().velocity = speedAttack;
        Destroy(createBullet, 5f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bug") 
            && _Collider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            _Rigidbody.velocity = Vector2.zero;
        }
        if (collision.gameObject.CompareTag("trap"))
        {
            Time.timeScale = 0;
            _PanelGameover.SetActive(true);
            _PanelMenu.SetActive(false);
            _PanelReloadBullet.SetActive(false);
        }
        if (collision.gameObject.CompareTag("jumpfeature"))
        {
            _Rigidbody.velocity = Vector2.up * 10;
        }
        if (collision.gameObject.CompareTag("monster"))
        {
            Time.timeScale = 0;
            _PanelGameover.SetActive(true);
            _PanelMenu.SetActive(false);
            _PanelReloadBullet.SetActive(false);
            Destroy(gameObject);
        }
    }
   public void ClimbLadder()
    {
        if (_Collider.IsTouchingLayers(LayerMask.GetMask("ladder")))
        {
            Debug.Log("Da va cham ladder");
            _Rigidbody.gravityScale = 0;
            if(Input.GetKey(KeyCode.UpArrow)) 
            {
                _Animator.SetBool("isclimb", true);
                _Rigidbody.velocity = Vector2.up * _speedClimp;
            }
            else if (!Input.GetKey(KeyCode.UpArrow))
            {
                //_Animator.SetBool("isclimb", true);
                _Rigidbody.velocity = Vector2.zero;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                _Animator.SetBool("isclimb", true);
                _Rigidbody.velocity = Vector2.down * _speedClimp;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ladder"))
        {
            _Animator.SetBool("isclimb", false);
            _Rigidbody.gravityScale = 1;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("item"))
        {
            score += 10;
            _ScoreText.text = score.ToString("");
        }
    }
}
