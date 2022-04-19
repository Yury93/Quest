using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Quest : MonoBehaviour
{
    [Serializable]public class Question
    {
        [TextArea(2, 5)]
        public string InputQuest;
        [TextArea(2, 4)]
        public List<string> InputAnswer = new List<string>(3);
    }
    [SerializeField] private List<Question> questions;
    [SerializeField] private Text quests;
    [SerializeField] private List <Text> answers = new List<Text>(3);

    private int rnd;
    private string rightAnswer;
    private GameObject buttonAnswer;
    [SerializeField] private GameManager gameManager;

    public event Action OnRightAnswer,OnLeftAnswer;
    private void Awake()
    {
        QuestionGenerate();
    }
    public void OnClickPlay()
    {
            ClickAnswer();
            QuestionGenerate();
    }

    private void QuestionGenerate()
    {
        if (questions.Count != 0)
        {
            rnd = UnityEngine.Random.Range(0, questions.Count);
            quests.text = questions[rnd].InputQuest;

            rightAnswer = questions[rnd].InputAnswer[0];

            for (int i = 0; i < answers.Count; i++)
            {
                var r = UnityEngine.Random.Range(0, questions[rnd].InputAnswer.Count);
                answers[i].text = questions[rnd].InputAnswer[r];
                questions[rnd].InputAnswer.RemoveAt(r);
            }
            questions.RemoveAt(rnd);
        }
        else { return; }
    }

    public void ClickAnswer()
    {
       var text = buttonAnswer.GetComponent<AnswerButton>().TextButton;
      
        if (rightAnswer == text)
        {
            OnRightAnswer?.Invoke();
            Debug.Log("Правильный ответ");
        }
        else
        {
            OnLeftAnswer?.Invoke();
            Debug.Log("Не правильный ответ");
        }
    }
    public void SetButtonAnswer(GameObject go)
    {
        buttonAnswer = go;
    }
}
