using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _platformSpeed; // tốc độ di chuyển của ván tự chuyển dộng
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
            platformPosition.x += _platformSpeed * Time.deltaTime; // Nếu imove = true 
            // thì di chuyển sang phải
            Debug.Log("Di chuyen sang phai: " + platformPosition.x);
        }
        else
        {
            platformPosition.x -= _platformSpeed * Time.deltaTime;
            Debug.Log("Di chuyen sang trai: " + platformPosition.x); // ngược lại sang trái
        }
        if (platformPosition.x >= _Right) // Nếu mà vị trí hiện tại của tấm ván lớn hơn = bằng giới
            // hạn bên phải thì chuyển hướng sang trái
        {
            ismove = false;
            platformPosition.x = _Right; // Đảm bảo ko bị vượt  qua giới hạn bên phải
        }
        else if (platformPosition.x <= _Left)//ngược lại thì chuyển sang phải
        {
            ismove = true;
            platformPosition.x = _Left;
        }
        transform.localPosition = platformPosition; // vị trí của tấm ván sẽ được cập nhập liên tục.
    }
}
