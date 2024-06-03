using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _platformSpeed;
    [SerializeField] private float _Right = 2.61f;
    [SerializeField] private float _Left = -0.55f;
    bool ismove = true;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var platformPosition = transform.localPosition;
        if (ismove == true)
        {
            platformPosition.x += _platformSpeed * Time.deltaTime;
            Debug.Log("Di chuyen sang phai: " + platformPosition.x);
        }
        else
        {
            platformPosition.x -= _platformSpeed * Time.deltaTime;
            Debug.Log("Di chuyen sang trai: " + platformPosition.x);
        }
        if (platformPosition.x >= _Right)
        {
            ismove = false;
            platformPosition.x = _Right; // Đảm bảo ko bị vượt  qua giới hạn bên phải
        }
        else if (platformPosition.x <= _Left)
        {
            ismove = true;
            platformPosition.x = _Left;
        }
        transform.localPosition = platformPosition;
    }
}
