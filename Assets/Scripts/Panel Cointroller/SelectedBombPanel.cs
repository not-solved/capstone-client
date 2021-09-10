using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectedBombPanel : MonoBehaviour {

    public Text BombName;
    public Text LeftTime;
    public GameObject panel;

    private BombState BombInfo;
    private DateTime EndTime;

    public void SetBombData(GameObject Bomb) {
        BombInfo = Bomb.GetComponent<BombState>();
        BombName.text = BombInfo.getName();
        EndTime = BombInfo.expTime();
        panel.SetActive(true);
    }

    public BombState GetBombData() {
        return BombInfo;
    }

    void SetTimeText() {
        LeftTime.text = (BombInfo.expTime() - DateTime.Now).Hours.ToString() + " : "
                    +   (BombInfo.expTime() - DateTime.Now).Minutes.ToString() + " : "
                    +   (BombInfo.expTime() - DateTime.Now).Seconds.ToString();
    }

    void Update() {
        if(DateTime.Now < EndTime) {
            if((EndTime - DateTime.Now).TotalSeconds >= 0)
                SetTimeText();
            else
                panel.SetActive(false);
        }
    }
}
