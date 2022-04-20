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
    }
    public void Online()
    {
        online.LoadNextScene();
    }
    public void Exit()
    {
        Application.Quit();
    }

}
