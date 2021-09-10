using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackToGameScript : MonoBehaviour {

    [SerializeField] GameObject canvas;
    [SerializeField] GameObject ARGame;
    [SerializeField] Camera ARCamera;
    [SerializeField] GameObject NPCcanvas;
    [SerializeField] Text Text3;
    [SerializeField] Text Text1;
    public void BackToGame() {
        ARGame.SetActive(true);
        ARCamera.enabled= true;
        canvas.GetComponent<UserState>().AddLifeCount();
        Text1.enabled = true;
        Text3.enabled = false;
        NPCcanvas.SetActive(false);
    }
}
