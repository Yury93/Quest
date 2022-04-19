using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    [SerializeField] private string textButton;
    public string TextButton => textButton;
    [SerializeField] private Quest quest;
    private void Start()
    {
        quest = FindObjectOfType<Quest>();
        textButton = gameObject.GetComponentInChildren<Text>().text;
    }
    public void GetTextButton()
    {
        quest.SetButtonAnswer(gameObject);
        textButton = gameObject.GetComponentInChildren<Text>().text;
    }
}
