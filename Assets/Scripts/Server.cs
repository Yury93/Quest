using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text.RegularExpressions;

public class Server : MonoBehaviour
{
    [SerializeField] string GameName = "�������� ����� ����";
    public static Server server;
    const string path = "http://cycjiukx.beget.tech/Index.php";

    public delegate void OnReturnServerMessage(string message);
    private static bool enableLoggin=true;
    private void Awake()
    {
        server = this;
    }
    void Start()
    {
        StartCoroutine(logCheck());
    }
    IEnumerator logCheck()
    {
        WWWForm form = new WWWForm();
        form.AddField("FunctionName", "isLogEnabled");
        form.AddField("Name", GameName);
        WWW www = new WWW(path, form);
        yield return www;
        if (www.error != null)
        {
            Debug.Log(www.error);
            yield break;
        }
        string answer = "";
        answer = www.text.Trim(' ', '\r', '\n');
        checkGetter(answer);
    }


    void checkGetter(string s)
    {
        if (s == "1") enableLoggin = true;
        if (s == "0") enableLoggin = false;
    }
    static void getter(string g)
    {

        Debug.Log("<color=yellow>Server answer: </color> " + g);
    }


    /// <summary>
    /// ������� ��� �� ������ �� �������� ����� ����
    /// </summary>
    /// <param name="GameName">��� �������</param>
    /// <param name="LogText">��� ���������</param>
    /// <param name="value">��������</param>
    /// <param name="getter">���� �������� ����� (�� �����������)</param>
    public static void Log(string LogText)
    {
        if (!enableLoggin)
        {
            Debug.Log("���� ���������");
            return;
        }
        server.StartCoroutine(server.logCor(server.GameName, LogText, getter));
    }
    IEnumerator logCor(string gameName, string logText, OnReturnServerMessage getter)
    {
        logText = System.DateTime.Now.ToString("dd.MM [HH:mm:ss]") + " " + logText;
        WWWForm form = new WWWForm();
        form.AddField("FunctionName", "addLog");
        form.AddField("Name", gameName);
        form.AddField("ParamName", logText);
        WWW www = new WWW(path, form);

        yield return www;
        if (www.error != null)
        {
            Debug.Log(www.error);
            yield break;
        }
        string answer = "";
        answer = www.text.Trim(' ', '\r', '\n');
        if (getter != null) getter(answer);
    }

    [ContextMenu("������� ���� � �������")]
    public void GetLogs()
    {
        server.StartCoroutine(server.getCor(server.GameName, getter));
    }
    
    IEnumerator getCor(string gameName, OnReturnServerMessage getter)
    {
        WWWForm form = new WWWForm();
        form.AddField("FunctionName", "getLog");
        form.AddField("Name", gameName);
        WWW www = new WWW(path, form);

        yield return www;
        if (www.error != null)
        {
            Debug.Log(www.error);
            yield break;
        }
        string answer = "";
        answer = www.text.Trim(' ', '\r', '\n');
        if (getter != null) getter(answer);
    }
    [ContextMenu("��������� ���� � �������")]
    public void ClearLogs()
    {
        server.StartCoroutine(server.clearCor(server.GameName, getter));
    }
    IEnumerator clearCor(string gameName, OnReturnServerMessage getter)
    {
        WWWForm form = new WWWForm();
        form.AddField("FunctionName", "clearLog");
        form.AddField("Name", gameName);
        WWW www = new WWW(path, form);

        yield return www;
        if (www.error != null)
        {
            Debug.Log(www.error);
            yield break;
        }
        string answer = "";
        answer = www.text.Trim(' ', '\r', '\n');
        if (getter != null) getter(answer);
    }
}
