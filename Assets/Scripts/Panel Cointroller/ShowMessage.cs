using UnityEngine;
using UnityEngine.UI;
using System;

public class ShowMessage : MonoBehaviour {

    public Text message;
    public GameObject messagePanel;
    public DateTime leftMessageTime;    

    public void injectCompleteMessage() {
        message.text = "Bomb inject complete";
        messagePanel.SetActive(true);
        leftMessageTime = DateTime.Now.AddSeconds(2);
    }
    public void injectFailMessage() {
        message.text = "Bomb is nearby you. Inject failed";
        messagePanel.SetActive(true);
        leftMessageTime = DateTime.Now.AddSeconds(2);
    }
    public void detectedMessage(int bombs) {
        if(bombs <= 0) {
            message.text = "No Bomb Detected";
        }
        else {
            message.text = bombs.ToString() + " Bombs Detected";
        }
        messagePanel.SetActive(true);
        leftMessageTime = DateTime.Now.AddSeconds(3);
    }

    public void ObjectTouchMessage(string msg) {
        message.text = msg;
        messagePanel.SetActive(true);
        leftMessageTime = DateTime.Now.AddSeconds(3);
    }

    public void eliminateMessage() {
        message.text = "Eliminate complete";
        messagePanel.SetActive(true);
        leftMessageTime = DateTime.Now.AddSeconds(2);
    }

    void Update() {
        if((leftMessageTime - DateTime.Now).TotalSeconds <= 0) {
            messagePanel.SetActive(false);
        }    
    }
}
