using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TouchGame : MonoBehaviour {

    [SerializeField] GameObject BackButton;
    
    private List<GameObject> buttonList = new List<GameObject>();

    private int GreenCnt;
    private int RandomNumber;

    public void Init() {
        BackButton.SetActive(false);
        for(int i = 0; i < 20; i++) {
            RandomNumber = Random.Range(0, 20);
            if(i == 0) {
                if(RandomNumber % 2 == 0)
                    GameObject.Find("Button").GetComponent<Image>().color = Color.black;
                else {
                    GameObject.Find("Button").GetComponent<Image>().color = Color.green;
                    GreenCnt++;
                }
            }
            else{
                if(RandomNumber % 3 == 0) {
                    GameObject.Find("Button (" + i +")").GetComponent<Image>().color = Color.black;
                }
                else {
                    GameObject.Find("Button (" + i +")").GetComponent<Image>().color = Color.green;
                    GreenCnt++;
                }
            }
        }
        Debug.Log(GreenCnt);
    }
    public void SetBombState(BombState incomingData) {
        BackButton.GetComponent<BackToAR>().SetBombData(incomingData);
    }

    public void AddGreencnt() {
        GreenCnt++;
    }
    public void SubGreenCnt() {
        GreenCnt--;
    }

    void Update() {
        if(GreenCnt == 0)
            BackButton.SetActive(true);
        else
            BackButton.SetActive(false);
    }
}
