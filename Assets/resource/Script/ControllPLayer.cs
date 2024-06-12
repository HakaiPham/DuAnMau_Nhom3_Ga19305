using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.UI;
using TMPro;

public class ControllPLayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _speed;// Tốc độ di chuyển của nhân vật
    Animator _Animator;
    BoxCollider2D _Collider;//Collider của nhân vật
    Rigidbody2D _Rigidbody;
    [SerializeField] private float _JumpPower;//Lực nhảy của nhân vật
    [SerializeField] private GameObject _bullet;// Đạn
    [SerializeField] Transform _locatedAttack;// Vị trí bắn ra viên đạn
     private float _reloadBullet;//reload đạn
    [SerializeField] private float _reloadBulletTime;// thời gian reload đạn
    [SerializeField] private float _speedClimp;// Tốc độ leo thang
    [SerializeField] private TextMeshProUGUI _ScoreText;//Score text
    public static int score=0;// score
    [SerializeField] private GameObject _PanelGameover;//Panel của gameOver
    [SerializeField] private GameObject _PanelMenu;//Panel của Menu
    [SerializeField] private Image _ReloadBulletFill;//Hình ảnh reload bullet
    [SerializeField] private GameObject _PanelReloadBullet;// Panel của reload bullet
    //public static ControllPLayer instance;
    AudioSource _audioSource;//Audio Source
    [SerializeField] AudioClip _ClipGameOver;//Sound gameover
    [SerializeField] AudioClip _ClipJump;//Sound Jump
    [SerializeField] AudioClip _ClipBowAttack;//Sound Bow Attack
    [SerializeField] AudioClip _ClipCollectItem;//Sond Collect item
    //Memory _memoryGame;
    void Start()
    {
        _Animator = GetComponent<Animator>();
        _Collider = GetComponent<BoxCollider2D>();
        _Rigidbody = GetComponent<Rigidbody2D>();
        _reloadBullet = 0;
        _ScoreText.text = score.ToString("");
        //_memoryGame = FindObjectOfType<Memory>();
        _audioSource = FindObjectOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");//Tạo nút bấm cho nhân vật
        var localScale = transform.localScale;//xoay chiều của player
        if (horizontalInput >0||horizontalInput < 0)
        {
            _Animator.SetBool("isrun", true);
            localScale.x = Mathf.Abs(localScale.x) * Mathf.Sign(horizontalInput);
            // dòng code giúp cho nhân vật giữ kích thước của scale hiện tại
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
            _audioSource.PlayOneShot(_ClipJump);
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
            //_Animator.SetBool("isattack", true);
            _audioSource.PlayOneShot(_ClipBowAttack);
            _Animator.SetTrigger("isattack 0");
            PlayerAtack();
            _ReloadBulletFill.fillAmount = 0; // Khi bắn hoạt ảnh của player sẽ xuống 0
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
            yield return new WaitForSeconds(0.1f);//Tạm dừng 0.1f
            _ReloadBulletFill.fillAmount += 0.1f;//Mỗi 0.1f thì +0.1f hồi phục reload đạn
        }
    }
    public void Jump()
    {
        if(!_Collider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if (_Collider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            _Rigidbody.velocity = new Vector2(_Rigidbody.velocity.x, _JumpPower);
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
            _Rigidbody.velocity = Vector2.zero;// Khi mà đụng vào giới hạn các bức tường
            // người chơi không thể nhảy
        }
        if (collision.gameObject.CompareTag("trap"))//bẫy
        {
            Time.timeScale = 0;//Khi đụng bẫy, game sẽ dừng lại
            _audioSource.PlayOneShot(_ClipGameOver);// Phat âm thanh của game over
            _PanelGameover.SetActive(true);// Xuất hiện màn hình gameOver
            _PanelMenu.SetActive(false);//Ẩn Nút menu game
            _PanelReloadBullet.SetActive(false);// Ẩn hoạt ảnh reload đạn
            StartCoroutine(WaitStopMusic());// Sau khi phát nhạt gameover xong, sẽ dừng
            //lại âm thanh game
        }
        if (collision.gameObject.CompareTag("jumpfeature"))//Đệm nhảy tự động
        {
            _Rigidbody.velocity = Vector2.up * 10;//Lực nhảy 
        }
        if (collision.gameObject.CompareTag("monster"))//Quái vật
        {
            Time.timeScale = 0;//Khi đụng quái, game sẽ dừng lại
            _audioSource.PlayOneShot(_ClipGameOver);// phát âm thanh gameOver
            _PanelGameover.SetActive(true);//Xuất hiện màn hình gameOver
            _PanelMenu.SetActive(false);//Ẩn Nút menu game
            _PanelReloadBullet.SetActive(false);// Ẩn hoạt ảnh reload đạn
            StartCoroutine(WaitStopMusic());// Sau khi phát nhạt gameover xong, sẽ dừng
            //lại âm thanh game
        }
        if (collision.gameObject.CompareTag("Blink"))//Cục gai
        {
            Time.timeScale = 0;
            _audioSource.PlayOneShot(_ClipGameOver);
            _PanelGameover.SetActive(true);
            _PanelMenu.SetActive(false);
            _PanelReloadBullet.SetActive(false);
            StartCoroutine(WaitStopMusic());
        }

    }
   public void ClimbLadder()//Code leo thang
    {
        if (_Collider.IsTouchingLayers(LayerMask.GetMask("ladder"))) // Nhân vật va chạm với 1 cái 
            // layer tên là ladder
        {
            Debug.Log("Da va cham ladder");
            _Rigidbody.gravityScale = 0;//Trọng lực của nhân vật =0
            if(Input.GetKey(KeyCode.UpArrow)) // Khi nhấn phím mũi tên 
            {
                _Animator.SetBool("isclimb", true);
                _Rigidbody.velocity = Vector2.up * _speedClimp;//Tốc độ di chuyển khi leo thang
            }
            else if (!Input.GetKey(KeyCode.UpArrow))
            {
                //_Animator.SetBool("isclimb", true);
                _Rigidbody.velocity = Vector2.zero;// Khi người chơi dừng lại
            }
            if (Input.GetKey(KeyCode.DownArrow))//khi người chơi leo xuống
            {
                _Animator.SetBool("isclimb", true);
                _Rigidbody.velocity = Vector2.down * _speedClimp;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision) // Khi người chơi va chạm và
        // rời khỏi Collider của thang sẽ chạy dòng code bên trong
    {
        if (collision.gameObject.CompareTag("ladder"))
        {
            _Animator.SetBool("isclimb", false);
            _Rigidbody.gravityScale = 1;// trả về lại trọng lực ban đầu của nhân vật
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("item"))// Ăn item
        {
            _audioSource.PlayOneShot(_ClipCollectItem);
            score +=10;
            _ScoreText.text = score.ToString("");
        }
        if (collision.gameObject.CompareTag("Fire")) // Fire(bẫy)
        {
            Time.timeScale = 0;
            _audioSource.PlayOneShot(_ClipGameOver);
            _PanelGameover.SetActive(true);
            _PanelMenu.SetActive(false);
            _PanelReloadBullet.SetActive(false);
            StartCoroutine(WaitStopMusic());
        }

    }
    IEnumerator WaitStopMusic()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        _audioSource.Stop();
    }
    public static int GetScore()//Phương thức dùng để tham chiếu qua các script khác
    {
        return score;
    }
}
