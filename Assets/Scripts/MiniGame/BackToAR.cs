using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToAR : MonoBehaviour {
    [SerializeField] GameObject MiniGameScreen;
    [SerializeField] GameObject ARGameScreen;
    public GameObject websocketScreen;
    private BombState targetBomb;

    public void SetBombData(BombState bomb) {
        targetBomb = bomb;
    }
    public void BackToARcanvas() {
        MiniGameScreen.SetActive(false);
        ARGameScreen.SetActive(true);
        websocketScreen.GetComponent<SendToNodeJS>().RemoveBomb(targetBomb);
    }
}
