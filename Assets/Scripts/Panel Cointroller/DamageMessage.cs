using UnityEngine;
using UnityEngine.UI;
using System;

public class DamageMessage : MonoBehaviour
{
    public Text message;
    public GameObject canvas;
    public GameObject damagedPanel;
    public DateTime leftMessageTime;    

    public void ShowDamagedMessage(int LifeCount) {
        if(LifeCount > 0) {
            message.text = "Your Life : " + LifeCount.ToString();
            damagedPanel.SetActive(true);
            leftMessageTime = DateTime.Now.AddSeconds(5);
        }
        else {
            message.text = "GAME OVER";
            damagedPanel.SetActive(true);
            leftMessageTime = DateTime.Now.AddSeconds(10);
        }
    }

    public void ShowDeadMessage() {
    }

    void Update() {
        if((leftMessageTime - DateTime.Now).TotalSeconds <= 0) {
            damagedPanel.SetActive(false);
        }    
    }
}
