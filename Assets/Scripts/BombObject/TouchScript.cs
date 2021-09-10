using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchScript : MonoBehaviour {

    private string bombName;
    private GameObject myCanvas;
    void TouchDetect() {

        RaycastHit hitObj;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

        if(Physics.Raycast(ray, out hitObj, Mathf.Infinity)) {
            SceneManager.LoadScene("bombLarge");
        }
    }

    public void SetData(GameObject canvas, string input) {
        myCanvas = canvas;
        bombName = input;
    }
    void Update() {
        // if(Input.touchCount > 0)
            // TouchDetect();
    }
}
