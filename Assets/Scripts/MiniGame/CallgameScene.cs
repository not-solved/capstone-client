using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallgameScene : MonoBehaviour {
    
    [SerializeField] GameObject ARcanvas;
    [SerializeField] GameObject TouchButtonGame;
    [SerializeField] GameObject CalculateNumGame;
    [SerializeField] GameObject SelectColorGame;

    private BombState bombinfo;
    private int MiniGameIdx;

    public void callGame() {
        bombinfo = GameObject.Find("TouchedPanel").GetComponent<SelectedBombPanel>().GetBombData();
        MiniGameIdx = Random.Range(1, 3);
        if(MiniGameIdx == 1) {
            TouchButtonGame.SetActive(true);
            TouchButtonGame.GetComponent<TouchGame>().Init();
            TouchButtonGame.GetComponent<TouchGame>().SetBombState(bombinfo);
        }
        else if(MiniGameIdx == 2) {
            CalculateNumGame.SetActive(true);
            CalculateNumGame.GetComponent<CalculateNumber>().Init();
            CalculateNumGame.GetComponent<CalculateNumber>().SetBombState(bombinfo);
        }
        else {
            SelectColorGame.SetActive(true);
            SelectColorGame.GetComponent<TouchGame>().Init();
            SelectColorGame.GetComponent<TouchGame>().SetBombState(bombinfo);
        }
        ARcanvas.SetActive(false);
    }
}
