using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberPlayer : MonoBehaviour
{
    private void Start()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.AddListPlayer(gameObject);
    }
}
