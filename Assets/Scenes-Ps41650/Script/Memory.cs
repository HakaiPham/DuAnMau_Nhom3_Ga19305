using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Memory : MonoBehaviour
{
    // Start is called before the first frame update
    ControllPLayer _player;
    Data _Datagame;
    [SerializeField] private TextMeshProUGUI _BestScore;
    [SerializeField] private TextMeshProUGUI _CurrentScore;
    void Start()
    {
        _player = FindObjectOfType<ControllPLayer>();
    }

    // Update is called once per frame
    void Update()
    {
        //ReadData
        //ShowData
        //Update Write new Data
        if (ControllPLayer.score > 0)
        {
            ReadDataFromFile();
            ShowData();
            WriteDataToFile();
        }
    }
    public void ReadDataFromFile()
    {
        _Datagame = DataManager.ReadData();
        if(_Datagame == null)
        {
            _Datagame = new Data() { score = 0 };

        }
    }
    public void ShowData()
    {
        var score = ControllPLayer.GetScore();
        //Score tu file
        var scoreFromFile = _Datagame.score;
        //Lay diem cao nhat
        var maxScore = Mathf.Max(score,scoreFromFile);
        _BestScore.text = "Best Score: "+maxScore;
        _CurrentScore.text = "Score: "+score;
        //Save Du lieu vao fikle
        //Lay diem lon nhat
        _Datagame.score = maxScore;
    }
    public void WriteDataToFile()
    {
        DataManager.SaveData(_Datagame);
    } 
}
