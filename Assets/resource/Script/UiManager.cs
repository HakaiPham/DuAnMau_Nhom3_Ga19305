using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _PanelHelp;//chức năng của help
    [SerializeField] private GameObject _PanelAchivement;//chức năng của achivement
    [SerializeField] private GameObject _OffHelp;// nút Help
    [SerializeField] private GameObject _OffPlay;// nít play
    [SerializeField] private GameObject _MENU;
    [SerializeField] private GameObject _PanelLevel;// bảng level
    AudioSource _AudioSource;
    Memory _memoryGame;
    void Start()
    {
        _AudioSource = FindObjectOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CloseButton()
    {
        _PanelHelp.SetActive(false); //Nút close của help
    }
    public void CloseButton2()
    {
        _OffHelp.SetActive(true); //hiện lại nút Help
        _OffPlay.SetActive(true); // Hiện lại nút play
        _PanelAchivement.SetActive(false); // nút close của achivement
    }
    public void HelpButton()
    {
        _PanelHelp.SetActive(true);//hiện bảng chức năng của help
    }
    public void AchivementButton()
    {
        _OffHelp.SetActive(false);//tắt nút help
        _OffPlay.SetActive(false);//tắt nút play
        _PanelAchivement.SetActive(true);//bật bảng chức năng của achivement
    }
    public void PlayGame()
    {
        _OffHelp.SetActive(false);
        _PanelLevel.SetActive(true);
        //_player.SetActive(true);
    }
    public void Level1()
    {
        SceneManager.LoadScene("Scene1");
        ControllPLayer.score = 0;
        Time.timeScale = 1;
    }
    public void Level2() // phương thức này có tác dụng vào map game
    {
        SceneManager.LoadScene("Scene 3");
        ControllPLayer.score = 0;//reset score về = 0
        Time.timeScale = 1;
    }
    public void level3()
    {
        SceneManager.LoadScene("scene2_new");
        ControllPLayer.score = 0;
        Time.timeScale = 1;
    }
    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // nút replay game
        ControllPLayer.score = 0;

        Time.timeScale = 1;
    }
    public void GameHome() // nút có chức năng quay lại giao diện chính
    {
        SceneManager.LoadScene("UiGame");
        //_player.SetActive(false);
    }
    public void ButtonCloseGame() //Nút thoát khoải bảng menu có trong game
    {
        _MENU.SetActive(false);
        Time.timeScale = 1;
    }
    public void OpenMenu() // nút mở bảng menu trong game
    {
        _MENU.SetActive(true);
        _AudioSource.Stop();
        Time.timeScale = 0;
    }
}
