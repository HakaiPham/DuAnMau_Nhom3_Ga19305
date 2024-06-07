using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _PanelHelp;
    [SerializeField] private GameObject _PanelAchivement;
    [SerializeField] private GameObject _OffHelp;
    [SerializeField] private GameObject _OffPlay;
    [SerializeField] private GameObject _MENU;
    Memory _memoryGame;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CloseButton()
    {
        _PanelHelp.SetActive(false);
    }
    public void CloseButton2()
    {
        _OffHelp.SetActive(true);
        _OffPlay.SetActive(true);
        _PanelAchivement.SetActive(false);
    }
    public void HelpButton()
    {
        _PanelHelp.SetActive(true);
    }
    public void AchivementButton()
    {
        _OffHelp.SetActive(false);
        _OffPlay.SetActive(false);
        _PanelAchivement.SetActive(true);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Scene1");
        //_player.SetActive(true);
        ControllPLayer.score = 0;
        Time.timeScale = 1;
    }
    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ControllPLayer.score = 0;

        Time.timeScale = 1;
    }
    public void GameHome()
    {
        SceneManager.LoadScene("UiGame");
        //_player.SetActive(false);
    }
    public void ButtonCloseGame()
    {
        _MENU.SetActive(false);
        Time.timeScale = 1;
    }
    public void OpenMenu()
    {
        _MENU.SetActive(true);
        Time.timeScale = 0;
    }
}
