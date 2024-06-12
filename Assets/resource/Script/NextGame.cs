using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextGame : MonoBehaviour
{
    [SerializeField] BoxCollider2D _boxCollider2D; //Lấy Collider của player
    [SerializeField]private GameObject _PaneWinGame;
    AudioSource _audioSource;
    [SerializeField] AudioClip _ClipWinGame;
    bool _ismusicPlay = false;
    private void Start()
    {
        _audioSource = FindObjectOfType<AudioSource>();
    }
    private void Update()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("item");
        // tạo 1 mảng chứa các đối tượng có tag tên là item
        Debug.Log("Coin: "+coins.Length);
        if (_boxCollider2D.IsTouchingLayers(LayerMask.GetMask("GateNextGame"))
            &&coins.Length==0)
            // Khi mà player va chạm với cổng dịch chuyển hoặc cúp và số lượng coin trên bản đồ 
            // phải bằng 0 
        {
            Debug.Log("Tên scene hiện tại là: " + SceneManager.GetActiveScene().name);
            if(SceneManager.GetActiveScene().name == "Scene1") //Nếu scene hiện tại là scene 1(map1)
            {
                SceneManager.LoadScene("Scene 3");// chuyển sang scene 3(map2)
            }
            if (SceneManager.GetActiveScene().name == "Scene 3") // Nếu scene hiện tại là scene 3
            {
                SceneManager.LoadScene("scene2_new"); // chuyển sang scene 2(map3)
            }
            if (SceneManager.GetActiveScene().name == "scene2_new"&&_ismusicPlay==false)
                // Nếu scene hiện tại là scene2_new và diều kiện là false
            {
                _audioSource.PlayOneShot(_ClipWinGame);//Chạy âm thanh game
                _PaneWinGame.SetActive(true);
                Time.timeScale = 0;
                StartCoroutine(WaitStopMusic());
                _ismusicPlay = true;//Điều kiện chỉ phát âm thanh win game 1 lần
            }
        }
    }
    IEnumerator WaitStopMusic()
    {
        yield return new WaitForSecondsRealtime(3f); //Sau 3s các âm thanh game sẽ dừng lại
        _audioSource.Stop();
    }
}
