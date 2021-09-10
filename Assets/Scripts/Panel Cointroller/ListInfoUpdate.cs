using System;
using UnityEngine;
using UnityEngine.UI;

public class ListInfoUpdate : MonoBehaviour {

    [SerializeField] Text BombName;
    [SerializeField] Text Time;
    [SerializeField] Text Latitude;
    [SerializeField] Text Longitude;
    
    public void SetListInformation(BombInformation info) {
        BombName.text = info.bombID;
        Time.text = DateTime.Now.Hour.ToString() + " : " +
                    DateTime.Now.Minute.ToString() + " : " + 
                    DateTime.Now.Second.ToString();
        Latitude.text = "Lat : " + info.latitude.ToString();
        Longitude.text = "Lon : " + info.longitude.ToString();
    }
}
