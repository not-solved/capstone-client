using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoadBombExp : MonoBehaviour {

    public GameObject bomb;
    public GameObject ExpCanvas;
    public GameObject GameMenu;
    private DateTime endTime;
    private bool isExplosed = false;
    public void SetTime() {
        endTime = DateTime.Now.AddSeconds(2);
        isExplosed = true;
    }

    void Update() {
        if(isExplosed) {
            if((endTime - DateTime.Now).TotalSeconds == 1)
                bomb.GetComponent<Player>().Collect();
            if((endTime - DateTime.Now).TotalSeconds <= 0) {
                Debug.Log("TimeOut");
                isExplosed = false;
                GameMenu.SetActive(true);
                ExpCanvas.SetActive(false);
            }
        }
    }
}
