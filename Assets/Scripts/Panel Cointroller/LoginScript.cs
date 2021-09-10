using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour {
    [SerializeField] Text Name;
    [SerializeField] Text Alert_Text;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject ARGameCanvas;

    private bool received = false;

    public void check_name() {
        if(Name.text.Length < 5) {
            Alert_Text.text = "Name must more than 5 letters";
        }
        else {
            canvas.GetComponent<SendToNodeJS>().setInitialName(Name.text);
        }
    }
    
    public void Confirm() {
        Alert_Text.text = "";
        ARGameCanvas.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void Denied() {
        Alert_Text.text = "Duplicated name, choose another name";
    }
}
