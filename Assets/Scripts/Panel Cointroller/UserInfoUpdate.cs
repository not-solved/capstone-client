using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoUpdate : MonoBehaviour {

    public Text UsrName;
    public Text LifeCnt;
    public Text ReviveTime;
    public Text SmallBombCnt;
    public Text MidBombCnt;
    public Text LargeBombCnt;
    public Text EliminateCnt;
    
    public bool isDamaged = false;
    public DateTime RecoverTime;

    public void SetUsrName(string input) {
        UsrName.text = input;
    }
    public void SetLifeCnt(int cnt) {
        LifeCnt.text = cnt.ToString();
    }
    public void SetReviveTime(DateTime reviveTime) {
        ReviveTime.text = (reviveTime - DateTime.Now).Hours.ToString() + " : "
                        + (reviveTime - DateTime.Now).Minutes.ToString() + " : "
                        + (reviveTime - DateTime.Now).Seconds.ToString();
    }
    public void SetSmallBombCnt(int cnt) {
        SmallBombCnt.text = "Small :  " + cnt.ToString();
    }
    public void SetMidBombCnt(int cnt) {
        MidBombCnt.text = "Mid :  " + cnt.ToString();
    }
    public void SetLargeBombCnt(int cnt) {
        LargeBombCnt.text = "Large :  " + cnt.ToString();
    }
    public void SetEliminateCnt(int cnt) {
        EliminateCnt.text = cnt.ToString();
    }

    public void SetDamagedState() {
        isDamaged = true;
        RecoverTime = DateTime.Now.AddMinutes(5);
    }

    void Update() {
        if(isDamaged) {
            if((RecoverTime - DateTime.Now).TotalSeconds >= 0)
                SetReviveTime(RecoverTime);
            else {
                ReviveTime.text = "00 : 00 : 00";
                isDamaged = false;
            }
        }
    }
}