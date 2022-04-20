using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSingle : MonoBehaviour
{
    [SerializeField] private Quest quest;
    void Start()
    {
        quest.OnLeftAnswer += Quest_OnLeftAnswer;
        quest.OnRightAnswer += Quest_OnRightAnswer;
    }

    private void Quest_OnRightAnswer()
    {
        print("������� ����������� ������");
    }

    private void Quest_OnLeftAnswer()
    {
        print("������� �� ����������� ������");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
