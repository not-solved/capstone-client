using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBomb : MonoBehaviour {
    
    public GameObject addBombPanel;

    public void SmallBomb() {
        addBombPanel.GetComponent<BombSelectInfo>().selectSmallBomb();
    }

    public void MediumBomb() {
        addBombPanel.GetComponent<BombSelectInfo>().selectMediumBomb();
    }

    public void LargeBomb() {
        addBombPanel.GetComponent<BombSelectInfo>().selectLargeBomb();
    }
}