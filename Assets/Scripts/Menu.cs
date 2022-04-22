using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private SceneController story,online;

    public void Story()
    {
        story.LoadNextScene();
        Server.Log("Story Game");
    }
    public void Online()
    {
        online.LoadNextScene();
        Server.Log("Online");
    }
    public void Exit()
    {
        Application.Quit();
        Server.Log("Exit");
    }

}
