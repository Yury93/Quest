using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Quest : MonoBehaviour
{
    [Serializable]public class Question 
    {
        [TextArea(2,5)]
        public string InputQuest;
        [TextArea(2, 4)]
        public string[] InputAnswer = new string[3];
    }
    [SerializeField] private List<Question> question;
    [SerializeField] private Text quests;
    [SerializeField] private Text[] answers = new Text[3];

    private int rnd;

    private void Start()
    {
        OnClickPlay();
    }
    public void OnClickPlay()
    {
        QuestionGenerate();
    }

    private void QuestionGenerate()
    {
        rnd = UnityEngine.Random.Range(0, question.Count);
        quests.text = question[rnd].InputQuest;
        question.RemoveAt(rnd);
        for (int i = 0; i < answers.Length; i++)
        {
            answers[i].text = question[rnd].InputAnswer[i];
        }
    }

    public void ClickAnswer()
    {
       
    }
}
