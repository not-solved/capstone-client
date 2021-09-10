using UnityEngine;
using System;

public class InjectBomb : MonoBehaviour {

    public DateTime now;
    public GameObject myCanvas;
    public LocationInfo myLocation;
    public BombInformation Info = new BombInformation();

    public GameObject bombPanel;

    void Start() {
        Info.com = "Inject";
    }

    public void injectBomb() {
        myLocation = Input.location.lastData;
        now = DateTime.Now;

        Info.installUser = myCanvas.GetComponent<UserState>().GetName();
        Info.bombCode = bombPanel.GetComponent<BombSelectInfo>().GetBombType();
        Info.latitude = myLocation.latitude;
        Info.longitude = myLocation.longitude;
        Info.InjectTime = now.ToString();
        if(Info.bombCode == "Small")
            Info.ExploseTime = now.AddMinutes(5).ToString();
        else if(Info.bombCode == "Medium")
            Info.ExploseTime = now.AddMinutes(10).ToString();
        else
            Info.ExploseTime = now.AddMinutes(1).ToString();

        myCanvas.GetComponent<SendToNodeJS>().SendBombJson(Info);
    }
}