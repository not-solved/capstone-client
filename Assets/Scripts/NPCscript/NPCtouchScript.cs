using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCtouchScript : MonoBehaviour {

    [SerializeField] GameObject NPCcanvas;
    [SerializeField] GameObject ARGameCanvas;
    
    private string Type = "InDuck";
    void Start() {
        ARGameCanvas = GameObject.Find("Components");
    }

    void Update() {
        if(Input.touchCount > 0)
            CastRay();    
    }
    public void CastRay() {
        RaycastHit hitObj;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

        if (Physics.Raycast(ray, out hitObj, Mathf.Infinity)) {
            
        }
    }
}
