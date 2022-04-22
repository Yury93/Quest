using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Wallet : MonoBehaviour
{
    [Header("Set In Inspector")]
    /// <summary>
    /// Общее количество денег
    /// </summary>
    [SerializeField] private int walletMoney;
    /// <summary>
    /// Ставка
    /// </summary>
    [SerializeField] private int bet;
    public int Bet => bet;
    /// <summary>
    /// Текст общего колечества денег
    /// </summary>
    [SerializeField] private Text walletMoneyTxt;
    /// <summary>
    /// Текст ставки
    /// </summary>
    [SerializeField] private Text betTxt;
    [Header("Set In Dinamycally")]
    /// <summary>
    /// Количество денег на старте
    /// </summary>
    [SerializeField] private int walletMoneyStart;
    [SerializeField] private Button startGameButton;

    private void Start()
    {
        walletMoneyStart = walletMoney;
        bet = 1 ;
        betTxt.text = "BET  " + bet;

        walletMoneyTxt.text = "Gold: " + walletMoney.ToString();
    }

    public void MinusBet()
    {
        if (bet > 0)
        {
            bet -= 1;
            walletMoney += 1;
        }
        if(bet == 0)
        {
            startGameButton.interactable = false;
        }
        betTxt.text = "BET  " + bet;
        walletMoneyTxt.text = "Gold: " + walletMoney.ToString();
    }
    public void PlusBet()
    {
        if (bet < walletMoney || walletMoney > 0)
        {
            bet += 1;
            walletMoney -= 1;
        }
        if (bet > 0)
        {
            startGameButton.interactable = true;
        }
        betTxt.text = "BET  " + bet;
        walletMoneyTxt.text = "Gold: " + walletMoney.ToString();
    }
    public void MaxBet()
    {
        if (walletMoney > 0)
        {
            bet += walletMoney;
            walletMoney = 0;
        }
        
        betTxt.text = "BET  " + bet;
        walletMoneyTxt.text = "Gold: " +  walletMoney.ToString();
    }
    public void WinCash(int winMoney)
    {
        //AudioManager.Instance.WinAudio();
        walletMoney += winMoney + bet * 2;
        betTxt.text = "BET  " + bet;
        walletMoneyTxt.text = "Gold: " + walletMoney.ToString();
    }
    public void LoseCash()
    {
        //AudioManager.Instance.LoseAudio();
        if (walletMoney > 0)
        {
            walletMoney -= bet;
            bet = 1;
        }
        else if (walletMoney <= 0)
        {
            walletMoney = 0;
            bet = 0;
        }
        print("dmkank");
        betTxt.text = "BET  " + bet;
        walletMoneyTxt.text = "Gold: " + walletMoney.ToString();
        RegisterMoneyNull();
    }
    public void RegisterMoneyNull()
    {
        if (walletMoney <= 0)
        {
            walletMoney = 0;
            betTxt.text = "BET  " + bet;
            walletMoneyTxt.text = "Gold: " + walletMoney.ToString();
            StartCoroutine(CorUpdateWallet());
        }
    }
    IEnumerator CorUpdateWallet()
    {
        yield return new WaitForSeconds(20f);
        walletMoney = walletMoneyStart;
        betTxt.text = "BET  " + bet;
        walletMoneyTxt.text = "Gold: "+ walletMoney.ToString();
    }
}
