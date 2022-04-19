using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugTxt : SingletonBase<DebugTxt>
{
    public Text text1;
    
    public void DebugText(string text)
    {
        text1.text = text;
    }
}
