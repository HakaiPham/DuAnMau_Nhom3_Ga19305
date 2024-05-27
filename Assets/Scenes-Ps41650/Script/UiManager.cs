using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _PanelHelp;
    [SerializeField] private GameObject _PanelAchivement;
    void Start()
    {
        _PanelAchivement.SetActive(false);
        _PanelHelp.SetActive(false);
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
        _PanelAchivement.SetActive(false);
    }
    public void HelpButton()
    {
        _PanelHelp.SetActive(true);
    }
    public void AchivementButton()
    {
        _PanelAchivement.SetActive(true);
    }
}
