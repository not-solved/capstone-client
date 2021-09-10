using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadARgame : MonoBehaviour {
    
    [SerializeField] GameObject SmallbombExpScene;
    [SerializeField] GameObject MediumbombExpScene;
    [SerializeField] GameObject LargebombExpScene;

    [SerializeField] GameObject ARgameCanvas;

    public void LoadExpScene() {
        SmallbombExpScene.SetActive(true);
        SmallbombExpScene.GetComponent<LoadBombExp>().SetTime();
        ARgameCanvas.SetActive(false);
    }
}
