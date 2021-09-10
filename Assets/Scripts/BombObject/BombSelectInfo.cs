using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombSelectInfo : MonoBehaviour {
   
   public Text bombText;
    public Text scoreText;
    public Text timeText;
    private string bombType = "Timer";

    void start() {
        selectSmallBomb();
    }

    public void selectSmallBomb() {
        bombText.text = "Bomb Type : Small";
        scoreText.text = "Score : 75" ;
        timeText.text = "Time : 5 minute";
        bombType = "Small";
    }

    public void selectMediumBomb() {
        bombText.text = "Bomb Type : Medium";
        scoreText.text = "Score : 150" ;
        timeText.text = "Time :  10 minute";
        bombType = "Medium";
    }

    public void selectLargeBomb() {
        bombText.text = "Bomb Type : Test";
        scoreText.text = "Score : 225" ;
        timeText.text = "Time : 1 minute";
        bombType = "Large";
    }

    public string GetBombType() {
        return bombType;
    }
}
