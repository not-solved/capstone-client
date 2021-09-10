using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GetCloserMessage : MonoBehaviour{
    public Text message;
    public GameObject messagePanel;
    public DateTime leftMessageTime;    

    public void ShowMessage(string msg) {
        message.text = msg;
        messagePanel.SetActive(true);
        leftMessageTime = DateTime.Now.AddSeconds(3);
    }

    void Update() {
        if((leftMessageTime - DateTime.Now).TotalSeconds <= 0) {
            messagePanel.SetActive(false);
        }    
    }
}
