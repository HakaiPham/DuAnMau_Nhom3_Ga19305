using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoveLeft : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _speedMonster;
    [SerializeField] private float _Right;
    [SerializeField] private float _Left;
    bool isTurn;
    void Start()
    {
        isTurn = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveMonster();
    }
    public void MoveMonster()
    {
        var move = transform.position;
        if (move.x >= _Right)
        {
            isTurn = false;
        }
        else if (move.x <= _Left)
        {
            isTurn = true;
        }
        var monster = Vector2.right;
        if (isTurn == false)
        {
            monster = Vector2.left;
        }
        transform.Translate(monster * _speedMonster * Time.deltaTime);
        var localScale = transform.localScale;
        if (isTurn == true && localScale.x < 0 || isTurn == false && localScale.x > 0)
        {
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}
