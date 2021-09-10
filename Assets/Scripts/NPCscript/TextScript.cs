using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour {
    
    [SerializeField] Text Text1;
    [SerializeField] Text Text2;
    [SerializeField] Text Text3;
    [SerializeField] GameObject BackToGameBtn;
    private int Cnt = 0;
    
    public void NextText() {
        if(Cnt == 0) {
            Text2.enabled = true;
            Text1.enabled = false;
        }
        else {
            Text3.enabled = true;
            Text2.enabled = false;
            BackToGameBtn.SetActive(true);
        }
        Cnt = (Cnt + 1) % 2;
    }
}