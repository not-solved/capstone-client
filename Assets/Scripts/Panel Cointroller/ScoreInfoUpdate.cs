using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreInfoUpdate : MonoBehaviour {

    public Text SmallBombCnt;
    public Text MidBombCnt;
    public Text LargeBombCnt;
    public Text EliminateBombCnt;
    public Text AttackPlayerCnt;
    public Text TotalScore;

    

    private int SmallBomb;
    private int MidBomb;
    private int LargeBomb;
    private int Eliminatebomb;
    private int AttackPlayer;
    private int Score;


    public void SetSmallCnt(int cnt) {
        SmallBomb = cnt;
        SmallBombCnt.text = SmallBomb.ToString();
    }
    public void SetMidCnt(int cnt) {
        MidBomb = cnt;
        MidBombCnt.text = MidBomb.ToString();
    }
    public void SetLargeCnt(int cnt) {
        LargeBomb = cnt;
        LargeBombCnt.text = LargeBomb.ToString();
    }
    public void SetEliminateCnt(int cnt) {
        Eliminatebomb = cnt;
        EliminateBombCnt.text = Eliminatebomb.ToString();
    }

    public void SetAttackPlayerCnt(int cnt) {
        AttackPlayer = cnt;
        AttackPlayerCnt.text = AttackPlayer.ToString();
    }

    public void SetScore() {
        Score = SmallBomb * 75 + MidBomb * 150 + LargeBomb * 225 + Eliminatebomb * 150 + AttackPlayer * 200;
        TotalScore.text = Score.ToString();
    }

    void Start() {
        SmallBomb = 0;
        MidBomb = 0;
        LargeBomb = 0;
        Eliminatebomb = 0;
        Score = 0;
    }
}
