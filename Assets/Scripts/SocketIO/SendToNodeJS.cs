using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;
using ARLocation;
using System;

//  JSON으로 보낼 폭탄 정보
public class BombInformation {
    public string com;
    public string bombCode;
    public string bombID;
    public string installUser;
    public double latitude;
    public double longitude;
    public string InjectTime;
    public string ExploseTime;
}

public class SendToNodeJS : MonoBehaviour {

    public WebSocket ws;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject startMenuBtn;
    [SerializeField] GameObject ComponentCanvas;
    [SerializeField] GameObject SelectMessagePanel;
    [SerializeField] GameObject MessagePanel;
    [SerializeField] GameObject DamagedPanel;
    [SerializeField] GameObject NPCcanvas;

    [SerializeField] GameObject MapBtn;

    //  BombList 콘텐츠
    [SerializeField] GameObject InjectContent;
    [SerializeField] GameObject EliminateContent;
    
    //  폭탄 오브젝트 모델
    [SerializeField] GameObject smallBomb;
    [SerializeField] GameObject mediumBomb;
    [SerializeField] GameObject largeBomb;

    //  피격 씬으로 전환
    [SerializeField] GameObject SmallBombExpScene;
    [SerializeField] GameObject MediumBombExpScene;
    [SerializeField] GameObject LargeBombExpScene;

    //  유저 본인이 탐지한 폭탄 리스트 (Explose, Reomve 관련)
    private List<BombInformation> detectedBombs = new List<BombInformation>();
    private List<BombInformation> SocketJson = new List<BombInformation>();
    private List<string> SocketCommand = new List<string>();

    private string Uname;
    private bool isDamaged = false;
    private DateTime deadTime;
    private BombInformation rcvJson = new BombInformation();
    private BombInformation SocketData = new BombInformation();
    private LocationInfo myLocation;
    private Location loc = new Location() {
        Latitude = 0,
        Longitude = 0,
        Altitude = 0,
        AltitudeMode = AltitudeMode.GroundRelative
    };
    private int detectedBombCount = 0;

    private bool NameDuplicateCheck =false;
    private string NameDuplicatedResult = "";
    
    private GameObject selectedBomb;

    //  초기 이름 설정
    public void setInitialName(string inputName) {
        rcvJson.installUser = inputName;
        rcvJson.com = "InitialConnection";
        ws.Send(JsonUtility.ToJson(rcvJson));
    }

    //  서버와의 연결
    public void Initialize() {
        // ws = new WebSocket("ws://127.0.0.1:6010");
        ws = new WebSocket("ws://49.50.163.145:6010");
        ws.OnMessage += ws_OnMessage;
        ws.OnOpen += ws_OnOpen;
        ws.OnClose += ws_OnClose;
        ws.Connect();
    }
    
    //  서버로부터 메시지 수신
    void ws_OnMessage(object sender, MessageEventArgs e) {
        SocketData = JsonUtility.FromJson<BombInformation>(e.Data);
        if(SocketData.com == "Connect") {
            NameDuplicateCheck = true;
            NameDuplicatedResult = "Connect";
            Uname = SocketData.installUser;
            canvas.GetComponent<UserState>().SetName(Uname);
        }
        else if(SocketData.com == "NameDuplicated") {
            NameDuplicateCheck = true;
            NameDuplicatedResult = "NameDuplicated";
        }
        else if(SocketData.com == "inject") {
            //  범위 내 폭탄이 이미 존재하면 설치 실패, 없으면 설치 성공
            if(SocketData.bombCode == "Failed") {
                MessagePanel.GetComponent<ShowMessage>().injectFailMessage();
            }
            else {
                SocketJson.Add(SocketData);
                SocketCommand.Add("inject");
            }
        }
        else if(SocketData.com == "search") {
            bool isInstalled = false;
            foreach(BombInformation item in detectedBombs) {
                if(item.bombID == SocketData.bombID) {
                    isInstalled = true;
                    break;
                }
            }
            if(!isInstalled && SocketData.bombCode != "NoBomb") {
                SocketJson.Add(SocketData);
                SocketCommand.Add("search");
                return;
            }
            MessagePanel.GetComponent<ShowMessage>().detectedMessage(detectedBombCount);
        }
        else if(SocketData.com == "explose") {
            SocketJson.Add(SocketData);
            SocketCommand.Add("explose");
        }
        else if(SocketData.com == "remove") {
            SocketJson.Add(SocketData);
            SocketCommand.Add("remove");
        }
        else if(SocketData.com == "attack") {
            canvas.GetComponent<UserState>().AddAttackPlayerCnt();
            canvas.GetComponent<UserState>().AddScore("Attack");
        }
        else if(SocketData.com == "SessionOut") {
            Destroy(GameObject.Find(SocketData.bombID));
        }
    }

    public void SendBombJson(BombInformation Info) {
        if(ws == null)
            Initialize();
        
        string sendJson = JsonUtility.ToJson(Info);
        ws.Send(sendJson);
    }
    public void InsertBomb(BombInformation binfo) {
        loc.Latitude = binfo.latitude;
        loc.Longitude = binfo.longitude;

        var opt = new PlaceAtLocation.PlaceAtOptions() {
            HideObjectUntilItIsPlaced = true,
            MaxNumberOfLocationUpdates = 2,
            MovementSmoothing = 0.5f,
            UseMovingAverage = false
        };

        GameObject newBomb = null;
        if(binfo.bombCode == "Small")
            newBomb = PlaceAtLocation.CreatePlacedInstance(smallBomb, loc, opt);
        else if(binfo.bombCode == "Medium")
            newBomb = PlaceAtLocation.CreatePlacedInstance(mediumBomb, loc, opt);
        else
            newBomb = PlaceAtLocation.CreatePlacedInstance(largeBomb, loc, opt);
        newBomb.GetComponent<BombState>().SetData(canvas, binfo.bombCode, binfo.bombID, binfo.installUser, binfo.latitude, binfo.longitude, binfo.InjectTime, binfo.ExploseTime);
        newBomb.name = binfo.bombID;
        MapBtn.GetComponent<MapShowButton>().DetectedBombList.Add(binfo);
    }
    public void RemoveBomb(BombState binfo) {
        BombInformation temp_bomb = binfo.GetData();
        temp_bomb.installUser = Uname;
        temp_bomb.com = "Remove";
        DestroyImmediate(GameObject.Find(temp_bomb.bombID));
        SendBombJson(temp_bomb);
    }

    public bool checkDistance(BombInformation info, double userLat, double userLon) {
        if(info.bombCode == "Small") {
            if(Math.Sqrt(Math.Pow((info.latitude - userLat) * 111200, 2)
                        + Math.Pow((info.longitude - userLon) * 88270, 2)) <= 10)
                return true;
            return false;
        }
        else if(info.bombCode == "Medium") {
             if(Math.Sqrt(Math.Pow((info.latitude - userLat) * 111200, 2)
                        + Math.Pow((info.longitude - userLon) * 88270, 2)) <= 17.5)
                return true;
            return false;
        }
        else {
             if(Math.Sqrt(Math.Pow((info.latitude - userLat) * 111200, 2)
                        + Math.Pow((info.longitude - userLon) * 88270, 2)) <= 25)
                return true;
            return false;
        }
    }

    public double CalculateDistance(BombState info) {
        myLocation = Input.location.lastData;
        return Math.Sqrt(Math.Sqrt(Math.Pow((info.GetLatitude() - myLocation.latitude) * 111200, 2)
                        + Math.Pow((info.GetLongitude() - myLocation.longitude) * 88270, 2)));
    }

    public void ShowDamaged() {
        int lifeCnt = canvas.GetComponent<UserState>().GetLifeCount();
        if(lifeCnt > 0) {        //  단순 데미지
            deadTime = DateTime.Now.AddSeconds(20);
            DamagedPanel.GetComponent<DamageMessage>().ShowDamagedMessage(lifeCnt);
        }
        else {                              //  사망판정
            deadTime = DateTime.Now.AddMinutes(5);
            DamagedPanel.GetComponent<DamageMessage>().ShowDeadMessage();
        }
    }

    public void CallDamagedScene(string com) {
        if(com == "Small") {
            SmallBombExpScene.SetActive(true);
            SmallBombExpScene.GetComponent<LoadBombExp>().SetTime();
        }
        else if(com == "Medium") {
            MediumBombExpScene.SetActive(true);
            MediumBombExpScene.GetComponent<LoadBombExp>().SetTime();
        }
        else {
            LargeBombExpScene.SetActive(true);
            LargeBombExpScene.GetComponent<LoadBombExp>().SetTime();
        }
        DamagedPanel.GetComponent<DamageMessage>().ShowDamagedMessage(canvas.GetComponent<UserState>().GetLifeCount());
        ComponentCanvas.SetActive(false);
    }

    //  웹소켓 연결
    void ws_OnOpen(object sender, System.EventArgs e) {
        Debug.Log("open");
    }
    //  웹소켓 연결 종료
    void ws_OnClose(object sender, CloseEventArgs e) {
        try {
            if(ws.IsAlive)
                ws.Close();
            else
                return;
        }
        catch(Exception exception) {
            Debug.Log(exception.ToString());
        }
        Debug.Log("close");
    }

    public void CloseConnection() {
        try {
            if(ws.IsAlive)
                ws.Close();
            else
                return;
        }
        catch(Exception exception) {
            Debug.Log(exception.ToString());
        }
        Debug.Log("close");
    }

    void Start() {
        if(ws == null) {
            Initialize();
        }
    }
    
    void Update() {
        //  무적시간 종료 확인
        if(isDamaged) {
            if((deadTime - DateTime.Now).TotalSeconds <= 0) {
                isDamaged = false;
            }
        }
        
        //  이름 중복검사 확인
        if(NameDuplicateCheck) {
            NameDuplicateCheck = false;
            if(NameDuplicatedResult == "Connect") {
                startMenuBtn.GetComponent<LoginScript>().Confirm();
            }
            else if(NameDuplicatedResult == "NameDuplicated") {
                startMenuBtn.GetComponent<LoginScript>().Denied();            
            }
            return;
        }
        
        //  비동기 메시지 처리
        if(SocketJson.Count > 0) {
            rcvJson = SocketJson[0];

            if(SocketCommand[0] == "inject") {
                InsertBomb(rcvJson);
                MessagePanel.GetComponent<ShowMessage>().injectCompleteMessage();
                canvas.GetComponent<UserState>().AddBombCnt(rcvJson.bombCode);
                canvas.GetComponent<UserState>().AddInjectItem(rcvJson);
            }
            else if(SocketCommand[0] == "search") {
                InsertBomb(rcvJson);
                detectedBombs.Add(rcvJson);
                detectedBombCount++;
                MessagePanel.GetComponent<ShowMessage>().detectedMessage(detectedBombCount);
            }
            else if(SocketCommand[0] == "explose") {
                //  본인이 설치한 폭탄 폭발에 성공하면 점수 획득
                if(rcvJson.installUser == Uname) {
                    canvas.GetComponent<UserState>().AddExploseCompleteCnt(rcvJson.bombCode);
                    canvas.GetComponent<UserState>().AddScore(rcvJson.bombCode);
                }

                //  리스트에서 해당 폭탄 제거
                int idx = 0;
                foreach(BombInformation item in detectedBombs) {
                    if(rcvJson.bombID == item.bombID) {
                        Destroy(GameObject.Find(item.bombID));
                        Destroy(GameObject.Find(rcvJson.bombID + "_map"));
                        detectedBombs.RemoveAt(idx);
                        detectedBombCount--;
                        MapBtn.GetComponent<MapShowButton>().DetectedBombList.RemoveAt(idx);
                        break;
                    }
                    idx++;
                }
                
                //  폭발에 휩쓸릴 경우를 계산
                myLocation = Input.location.lastData;
                if(checkDistance(rcvJson, myLocation.latitude, myLocation.longitude)) {
                    //  폭발에 휩쓸릴 경우 이벤트 발생
                    if(!isDamaged) {
                        canvas.GetComponent<UserState>().DiscountLifeCount();
                        deadTime = DateTime.Now.AddSeconds(30);
                        isDamaged = true;
                        int lifecnt = canvas.GetComponent<UserState>().GetLifeCount();
                                            
                        //  본인의 폭탄에 당할 경우 공격으로 계산하지 않음
                        if(Uname != rcvJson.installUser) {
                            BombInformation temp = rcvJson;
                            temp.com = "Attacked";
                            ws.Send(JsonUtility.ToJson(temp));
                        }
                        CallDamagedScene(rcvJson.bombCode);
                    }
                }
            }
            else if(SocketCommand[0] == "remove") {
                int idx = 0;
                foreach(BombInformation item in detectedBombs) {
                    if(rcvJson.bombID == item.bombID) {
                        DestroyImmediate(GameObject.Find(item.bombID));
                        Destroy(GameObject.Find(rcvJson.bombID + "_minimap"));
                        detectedBombs.RemoveAt(idx);
                        detectedBombCount--;
                        MapBtn.GetComponent<MapShowButton>().DetectedBombList.RemoveAt(idx);
                        break;
                    }
                    idx++;
                }

                MessagePanel.GetComponent<ShowMessage>().eliminateMessage();
                canvas.GetComponent<UserState>().AddEliminateCnt();
                canvas.GetComponent<UserState>().AddScore("Eliminate");
                canvas.GetComponent<UserState>().AddRemoveItem(rcvJson);
            }
        
            SocketJson.RemoveAt(0);
            SocketCommand.RemoveAt(0);
        }

        //  터치로 폭탄 선택
        if(Input.touchCount > 0)
            if(ComponentCanvas.activeSelf)
                TouchDetect();
    }

    //  폭탄 오브젝트 터치 동작
    void TouchDetect() {

        RaycastHit hitObj;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

        if(Physics.Raycast(ray, out hitObj, Mathf.Infinity)) {
            selectedBomb = hitObj.collider.gameObject;
            if(selectedBomb.GetComponent<BombState>().getInstallUserName() == "InDuck")
                NPCcanvas.GetComponent<NPCloadScript>().LoadInduck();
            else if(selectedBomb.GetComponent<BombState>().getInstallUserName() == Uname)
                MessagePanel.GetComponent<ShowMessage>().ObjectTouchMessage("That is your bomb");
            else if(CalculateDistance(selectedBomb.GetComponent<BombState>()) > 5)
                MessagePanel.GetComponent<ShowMessage>().ObjectTouchMessage("Too far, Get closer!");
            else
                SelectMessagePanel.GetComponent<SelectedBombPanel>().SetBombData(selectedBomb);
        }
    }
}