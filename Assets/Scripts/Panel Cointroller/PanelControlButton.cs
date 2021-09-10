using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelControlButton : MonoBehaviour {
    public GameObject targetPanel;
    public void ShowAndHide() {
        bool isOpen = targetPanel.activeSelf;
        if(targetPanel != null) {
            targetPanel.SetActive(!isOpen);
        }
    }
}