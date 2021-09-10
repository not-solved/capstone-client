using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTouch : MonoBehaviour {

    [SerializeField] GameObject button;
    [SerializeField] GameObject GameManager;
    
    public void Touch() {
        if(button.GetComponent<Image>().color == Color.black) {
            button.GetComponent<Image>().color = new Color(0, 1, 0);
            GameManager.GetComponent<TouchGame>().AddGreencnt();
        }
        else {
            button.GetComponent<Image>().color = new Color(0, 0, 0);
            GameManager.GetComponent<TouchGame>().SubGreenCnt();
        }
    }
}
