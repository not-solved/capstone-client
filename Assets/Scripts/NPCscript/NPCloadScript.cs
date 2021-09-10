using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCloadScript : MonoBehaviour {
    [SerializeField] GameObject ARgameCanvas;
    [SerializeField] GameObject InDuckCanvas;
    public void LoadInduck() {
        InDuckCanvas.SetActive(true);
        ARgameCanvas.SetActive(false);
    }
}
