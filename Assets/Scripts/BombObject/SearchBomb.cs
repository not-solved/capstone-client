using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchBomb : MonoBehaviour {

    public GameObject myCanvas;
    public LocationInfo myLocation;
    public BombInformation Info = new BombInformation();
    
    public void searchBomb() {
        myLocation = Input.location.lastData;

        Info.com = "Search";
        Info.installUser = myCanvas.GetComponent<UserState>().GetName();
        Info.latitude = myLocation.latitude;
        Info.longitude = myLocation.longitude;
        myCanvas.GetComponent<SendToNodeJS>().SendBombJson(Info);
    }
}
