using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerSingle : MonoBehaviour
{
    [SerializeField] private Quest quest;
    [SerializeField] private float timer;
    private float startTimer;
    [SerializeField] private int score;
    [SerializeField] private Text scoreTxt, timerTxt;
    [SerializeField] private SceneController sceneController;
    [SerializeField] private Wallet betObj;
    [Header("Dinamically")] 
    [SerializeField] private int bet;
    private bool gameOver;
    void Start()
    {
        quest.OnLeftAnswer += Quest_OnLeftAnswer;
        quest.OnRightAnswer += Quest_OnRightAnswer;
        timer = 10;
        startTimer = timer;
        scoreTxt.text = "Gold: " + score.ToString();
        timerTxt.text = ((int)timer).ToString();
        gameOver = false;
    }

    private void Quest_OnRightAnswer()
    {
        timer = startTimer;
        score++;
        scoreTxt.text = "Gold: "+score.ToString();
        if (gameOver)
        {
            if (score >= bet)
            {
                betObj.WinCash(score);
            }
            else
            {
                betObj.LoseCash();
            }
            StartCoroutine(CorSceneLoad());
        }
    }

    private void Quest_OnLeftAnswer()
    {
        timer -= 5f;
        if (gameOver)
        {
            if (score >= bet)
            {
                betObj.WinCash(score);
            }
            else
            {
                betObj.LoseCash();

            }
            StartCoroutine(CorSceneLoad());
        }
    }
    void Update()
    {
        if(timer > 0 && quest.gameObject.activeSelf == true)
        {
            timer -= Time.deltaTime;
            timerTxt.text = ((int)timer).ToString();
        }
        else if(quest.gameObject.activeSelf == true)
        {
            
            timer = 0;
            timerTxt.text = "Time's up! Answer the last question!";
            gameOver = true;
        }
        
    }
    IEnumerator CorSceneLoad()
    {
        yield return new WaitForSeconds(0f);
        quest.gameObject.SetActive(false);
    }
    public void StartGame()
    {
        quest.gameObject.SetActive(true);
        timer = 10;
        gameOver = false;
        bet = betObj.Bet;
    }
}
