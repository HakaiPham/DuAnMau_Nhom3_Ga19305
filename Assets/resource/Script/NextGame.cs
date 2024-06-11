using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextGame : MonoBehaviour
{
    [SerializeField] BoxCollider2D _boxCollider2D;
    private void Update()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("item");
        Debug.Log("Coin: "+coins.Length);
        if (_boxCollider2D.IsTouchingLayers(LayerMask.GetMask("GateNextGame"))
            &&coins.Length==0){
            SceneManager.LoadScene("Scene 3");
        }
    }
}
