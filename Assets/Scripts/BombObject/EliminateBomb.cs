using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliminateBomb : MonoBehaviour {
    public GameObject canvas;

    public void Eliminatebomb(BombState binfo) {
        canvas.GetComponent<SendToNodeJS>().RemoveBomb(binfo);
    }
}