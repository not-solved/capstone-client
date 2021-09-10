using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BombState : MonoBehaviour {

    private GameObject myCanvas;
    private string bombCode = "";
    private string bombID = ""; 
    private string installUser = "";
    private double latitude;
    private double longitude;
    private DateTime InjectTime;
    private DateTime ExploseTime;
    private bool isInjected = false;

    public void SetData(GameObject obj, string code, string bombname, string user, double lat, double lon, string ijtTime, string expTime) {
        Debug.Log(code + ", " + user);

        myCanvas = obj;
        bombCode = code;
        bombID = bombname;
        installUser = user;
        latitude = lat;
        longitude = lon;
        InjectTime = DateTime.Parse(ijtTime);
        ExploseTime = DateTime.Parse(expTime);
        isInjected = true;

        // GameObject tempObj = GameObject.Find(bombname);
        // tempObj.GetComponent<TouchScript>().SetData(obj, bombname);
    }

    public BombInformation GetData() {
        BombInformation returnData = new BombInformation() {
            com = "",
            bombCode = bombCode,
            bombID = bombID,
            installUser = "",
            latitude = latitude,
            longitude = longitude,
            InjectTime = InjectTime.ToString(),
            ExploseTime = ExploseTime.ToString()
        };
        return returnData;
    }

    public string getName() {
        return bombID;
    }
    public string getInstallUserName() {
        return installUser;
    }
    public DateTime expTime() {
        return ExploseTime;
    }
    public double GetLatitude() {
        return latitude;
    }
    public double GetLongitude() {
        return longitude;
    }
    public bool calculateDistance() {
        LocationInfo myLocation = Input.location.lastData;
        if(Math.Sqrt(Math.Pow((myLocation.latitude - latitude)*1112000, 2)
                       + Math.Pow((myLocation.longitude - longitude)*882700, 2)) <= 5)
            return true;
        else
            return false;
    }
  
    void Update() {
        //  시간이 지나면 폭발 (오브젝트 제거로 구현)
        if(isInjected && (ExploseTime - DateTime.Now).TotalSeconds <= 0 ) {
            BombInformation exploseInfo = new BombInformation() {
                com = "Explose",
                bombCode = bombCode,
                bombID = bombID,
                installUser = "",
                latitude = latitude,
                longitude = longitude,
                InjectTime = InjectTime.ToString(),
                ExploseTime = ExploseTime.ToString()
            };
            myCanvas.GetComponent<SendToNodeJS>().SendBombJson(exploseInfo);
            isInjected = false;
            Destroy(GameObject.Find(bombID));
        }
    }
}