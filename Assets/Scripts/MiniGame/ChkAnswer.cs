using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChkAnswer : MonoBehaviour {

    [SerializeField] Text AnswerText;
    [SerializeField] Text YesOrNo;
    [SerializeField] GameObject GameManager;
    [SerializeField] GameObject GetBackButton;

    public void checkAnswer() {
        if(AnswerText.text == GameManager.GetComponent<CalculateNumber>().GetAnswer().ToString()) {
            YesOrNo.text = "Correct Answer!!";
            GetBackButton.SetActive(true);
        }
        else {
            YesOrNo.text = "Wrong Answer. Try Again";
            GetBackButton.SetActive(false);
        }
    }
}
