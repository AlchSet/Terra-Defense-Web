using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTextChanger : MonoBehaviour {

    [TextArea]
    public string[] upgradeMsg;

    int index = 0;


    Text text;
    private void Awake()
    {
        text = GetComponent<Text>();
    }


    public void UpgradeText()
    {
        text.text = upgradeMsg[index];
        index = (index + 1) % upgradeMsg.Length;
    }
}
