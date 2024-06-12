using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _speedMonster;
    [SerializeField] private float _Right;
    [SerializeField] private float _Left;
    bool isTurn;
    void Start()
    {
        isTurn = true;
    }

    // Update is called once per frame
    void Update()
    {
        MoveMonster();
    }
    public void MoveMonster()
    {
        var move = transform.position;
        if(move.x >= _Right) // giới hạn bên phải, đạt giới hạn sẽ tự động xoay sang trái
        {
            isTurn = false;
        }
        else if(move.x <= _Left) // ngược lại 
        {
            isTurn = true;
        }
        var monster = Vector2.right;//quái sẽ di chuyển sang phải đầu tiên 
        // vì isturn = true
        if(isTurn == false)
        {
            monster = Vector2.left;
        }
        transform.Translate(monster*_speedMonster*Time.deltaTime);//code di chuyển quái
        var localScale = transform.localScale;
        if(isTurn == true && localScale.x<0||isTurn == false && localScale.x > 0)
        {
            localScale.x *= -1;//Tự động xoay mặt khi quái quay sang trái hoặc phải
            transform.localScale = localScale; // nhận lại giá trị các giá trị scale đã thay đổi
        }
    }
}
