using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapReturnButton : MonoBehaviour {
    
    public Camera ARCamera;
    public GameObject MinimapCanvas;
    public GameObject ARGameCanvas;

    public void returnToAR() {
        ARGameCanvas.SetActive(true);
        ARCamera.enabled = true;
        MinimapCanvas.SetActive(false);
    }
}
