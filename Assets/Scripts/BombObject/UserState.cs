using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class UserState : MonoBehaviour {

    [SerializeField] GameObject USpanel;
    [SerializeField] GameObject BLpanel;
    [SerializeField] GameObject SCpanel;

    private string UserName;
    private int Score;
    private int LifeCount;

    private bool isDamaged;

    private int SmallBombCnt;
    private int MidBombCnt;
    private int LargeBombCnt;

    private int EliminateCnt;

    private int SmallExpCnt;
    private int MidExpCnt;
    private int LargeExpCnt;
    private int AttackPlayer;

    private DateTime DamagedTime;
    private DateTime RecoverTime;

    private List<BombInformation> injectList = new List<BombInformation>();
    private List<BombInformation> removeList = new List<BombInformation>();


    public void SetName(string input) {
        UserName = input;
        USpanel.GetComponent<UserInfoUpdate>().SetUsrName(UserName);
    }
    public void AddScore(string com) {
        if(com == "Small")
            Score += 75;
        else if(com == "Medium")
            Score += 150;
        else if(com == "Large")
            Score += 225;
        else if(com == "Eliminate")
            Score += 150;
        else if(com == "Attack")
            Score += 200;
        SCpanel.GetComponent<ScoreInfoUpdate>().SetScore();
    }
    public void SetLifeCount(int cnt) {
        LifeCount += cnt;
        USpanel.GetComponent<UserInfoUpdate>().SetLifeCnt(LifeCount);
    }
    public void AddBombCnt(string com) {
        if(com == "Small") {
            SmallBombCnt++;
            USpanel.GetComponent<UserInfoUpdate>().SetSmallBombCnt(SmallBombCnt);
        }
        else if(com == "Medium") {
            MidBombCnt++;
            USpanel.GetComponent<UserInfoUpdate>().SetMidBombCnt(MidBombCnt);
        }
        else {
            LargeBombCnt++;
            USpanel.GetComponent<UserInfoUpdate>().SetLargeBombCnt(LargeBombCnt);
        }
    }
    public void AddEliminateCnt() {
        EliminateCnt++;
        USpanel.GetComponent<UserInfoUpdate>().SetEliminateCnt(EliminateCnt);
        SCpanel.GetComponent<ScoreInfoUpdate>().SetEliminateCnt(EliminateCnt);
    }
    public void AddExploseCompleteCnt(string com) {
        if(com == "Small") {
            SmallExpCnt++;
            SCpanel.GetComponent<ScoreInfoUpdate>().SetSmallCnt(SmallExpCnt);
        }
        else if(com == "Medium") {
            MidExpCnt++;
            SCpanel.GetComponent<ScoreInfoUpdate>().SetMidCnt(MidExpCnt);
        }
        else {
            LargeExpCnt++;
            SCpanel.GetComponent<ScoreInfoUpdate>().SetLargeCnt(LargeExpCnt);
        }
    }

    public void AddAttackPlayerCnt() {
        AttackPlayer++;
        SCpanel.GetComponent<ScoreInfoUpdate>().SetAttackPlayerCnt(AttackPlayer);
    }

    public void AddLifeCount() {
        RecoverTime = DateTime.Now;
    }
    
    public string GetName() {
        return UserName;
    }
    public int GetScore() {
        return Score;
    }
    public int GetLifeCount() {
        return LifeCount;
    }
    public void DiscountLifeCount() {
        if(LifeCount > 0) {
            LifeCount--;
            isDamaged = true;
            DamagedTime = DateTime.Now;
            RecoverTime = DateTime.Now.AddMinutes(5);
        }
        USpanel.GetComponent<UserInfoUpdate>().SetLifeCnt(LifeCount);
        USpanel.GetComponent<UserInfoUpdate>().SetDamagedState();
    }
    
    public int GetBombCnt(string com) {
        if(com == "Small")
            return SmallBombCnt;
        else if(com == "Medium")
            return MidBombCnt;
        else
            return LargeBombCnt;
    }
    public int GetEliminateCnt() {
        return EliminateCnt;
    }
    public int GetExploseCompleteCnt(string com) {
        if(com == "Small")
            return SmallExpCnt;
        else if(com == "Medium")
            return MidExpCnt;
        else
            return LargeExpCnt;
    }

    public void AddInjectItem(BombInformation result) {
        if(injectList.Count >= 30)
            injectList.RemoveAt(0);
        injectList.Add(result);
        BLpanel.GetComponent<ListContentAdd>().UpdateList(injectList, "Inject");
    }
    public void AddRemoveItem(BombInformation result) {
        if(removeList.Count >= 30)
            removeList.RemoveAt(0);
        removeList.Add(result);
        BLpanel.GetComponent<ListContentAdd>().UpdateList(removeList, "Remove");
    }

    public bool PlayerAlive() {
        if(LifeCount > 0)
            return true;
        return false;
    }

    void Start() {
        UserName = "";
        Score = 0;
        LifeCount = 3;
        isDamaged = false;
        
        SmallBombCnt = 0;
        MidBombCnt = 0;
        LargeBombCnt = 0;

        EliminateCnt = 0;

        SmallExpCnt = 0;
        MidExpCnt = 0;
        LargeExpCnt = 0;
    }

    void Update() {
        if(isDamaged) {
            if((RecoverTime - DateTime.Now).TotalSeconds <= 0) {
                if(LifeCount < 3)
                    LifeCount++;
                if(LifeCount != 3) {
                    DamagedTime = DateTime.Now;
                    RecoverTime = DateTime.Now.AddMinutes(5);
                    USpanel.GetComponent<UserInfoUpdate>().SetLifeCnt(LifeCount);
                    USpanel.GetComponent<UserInfoUpdate>().SetReviveTime(RecoverTime);
                }
                else {
                    USpanel.GetComponent<UserInfoUpdate>().SetLifeCnt(LifeCount);
                    USpanel.GetComponent<UserInfoUpdate>().SetReviveTime(DateTime.Now);
                }
            }
        }
    }
}