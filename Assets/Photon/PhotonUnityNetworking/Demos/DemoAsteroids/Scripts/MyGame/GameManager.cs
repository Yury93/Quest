using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks,IPunObservable
{
    [SerializeField] private GameObject prefabPlayer, ImageBg;

    [SerializeField] private Text scoreTxtPlayer1, scoreTxtPlayer2, timerTxt;
    [SerializeField] private float timerPl;
    private float startTimerPl1, startTimerPl2;
    [SerializeField] private int scorePl1, scorePl2;
    [SerializeField] private bool player1Go;
    public bool Player1Go => player1Go;
    [SerializeField] private Quest quest;

    private PhotonView view;
    public PhotonView View => view;
    private int countPl;

    [SerializeField] private Text textNextPlayer;
    [SerializeField] private List<GameObject> players;
    private void Start()
    {
        StartCoroutine(CorConnected());
        view = GetComponent<PhotonView>();
       
        PhotonNetwork.Instantiate(prefabPlayer.name, transform.position, Quaternion.identity);

        int i = PhotonNetwork.LocalPlayer.ActorNumber;
        ExitGames.Client.Photon.Hashtable h = PhotonNetwork.LocalPlayer.CustomProperties;
        h["OnConnected"] = 1;

        PhotonNetwork.LocalPlayer.SetCustomProperties(h);
        if (view.IsMine)
        {
            player1Go = true;
        }
        else
        {
            player1Go = false;
        }
        view.RPC("RPC_Switch", RpcTarget.All);
        startTimerPl1 = timerPl;
        startTimerPl2 = timerPl;

        scoreTxtPlayer1.text = "The opponent's goals: " + scorePl1.ToString();
        //scoreTxtPlayer2.text = "Score: " + scorePl2.ToString();


        quest.OnRightAnswer += Quest_OnRightAnswer;
        quest.OnLeftAnswer += Quest_OnLeftAnswer;
    }

    IEnumerator CorConnected()
    {
        while (true)
        {
            var i = 0;

            yield return new WaitForSeconds(1);
            foreach (var item in PhotonNetwork.PlayerList)
            {
                if (item.CustomProperties["OnConnected"] != null)
                {
                    if ((int)item.CustomProperties["OnConnected"] == 1)
                    {
                        i++;
                    }
                }
            }
            if (i == 2)
            {
                countPl = 2;
                yield break;
            }
        }
    }

    [PunRPC]
    public void RPC_Right()
    {
        
        if (player1Go )
        {
            scorePl1 += 1;
            scoreTxtPlayer1.text = "The opponent's goals: " + scorePl1.ToString();
            timerPl = startTimerPl1;
            player1Go = false;
            return;
        }
        else if (!player1Go )
        {
            scorePl2 += 1;
            scoreTxtPlayer2.text = "The opponent's goals: " + scorePl2.ToString();
            timerPl = startTimerPl2;
            player1Go = true;
            return;
        }
    }
    [PunRPC]
    public void RPC_Left()
    {
        if (player1Go )
        {
            scoreTxtPlayer1.text = "The opponent's goals: " + scorePl1.ToString();
            startTimerPl1 = timerPl;
            player1Go = false;
            return;

        }
        else if (!player1Go )
        {
            scoreTxtPlayer2.text = "The opponent's goals: " + scorePl2.ToString();
            startTimerPl2 = timerPl;
            player1Go = true;
            return;

        }
        else if (timerPl < 0)
        {
            print("Кто то проиграл");
            return;
        }
    }
    [PunRPC]
    public void RPC_Switch()
    {
        //player1Go = !player1Go;
      
        if (view.IsMine)
        {
            if (player1Go)
            {
                ImageBg.SetActive(false);
            }
            else
            {
                ImageBg.SetActive(true);
            }
        }
        else
        {
            if (!player1Go)
            {
                ImageBg.SetActive(true);
            }
            else
            {
                ImageBg.SetActive(false);
            }
        }
        textNextPlayer.text = "The next player to go!";
    }
    private void Quest_OnRightAnswer()
    {
        view.RPC("RPC_Right", RpcTarget.All);
        view.RPC("RPC_Switch", RpcTarget.All);
    }
    private void Quest_OnLeftAnswer()
    {
        view.RPC("RPC_Left", RpcTarget.All);
        view.RPC("RPC_Switch", RpcTarget.All);
    }
    [PunRPC]
    private void RPC_Update()
    {
        if (countPl > 1)
        {
            if (player1Go && timerPl > 0)
            {
                timerPl -= Time.deltaTime;
                timerTxt.text = ((int)timerPl).ToString();
            }

            if (!player1Go && timerPl > 0)
            {
                timerPl -= Time.deltaTime;
                timerTxt.text = ((int)timerPl).ToString();
            }
        
        }
    }
    
    private void Update()
    {
        view.RPC("RPC_Update", RpcTarget.All);
       
    }
    #region PunAPI
    public override void OnConnected()
    {
        base.OnConnected();
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        SceneManager.LoadScene("DemoAsteroids-LobbyScene");
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        SceneManager.LoadScene("DemoAsteroids-LobbyScene");
    }
    public void LoadSetPlayerGo(bool flag)
    {
        ImageBg.SetActive(flag);
    }
    #endregion
    public void AddListPlayer(GameObject playerGo)
    {
        players.Add(playerGo);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting && PhotonNetwork.IsMasterClient)
        {
            stream.SendNext(timerPl);
        }
        else if(stream.IsReading)
        {
            timerPl = (float)stream.ReceiveNext();
        }
    }
}