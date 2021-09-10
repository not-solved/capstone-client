using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CalculateNumber : MonoBehaviour {

    [SerializeField] Text One_1;
    [SerializeField] Text One_2;
    [SerializeField] Text One_3;

    [SerializeField] Text Two_1;
    [SerializeField] Text Two_2;
    [SerializeField] Text Two_3;

    [SerializeField] Text Three_1;
    [SerializeField] Text Three_2;
    [SerializeField] Text Three_3;

    [SerializeField] GameObject BackButton;

    private int Place_seed;
    private int Rand_start;
    private int Rand_preiod;

    private int Answer;

    public void SetBombState(BombState bombInfo) {
        BackButton.GetComponent<BackToAR>().SetBombData(bombInfo);
    }

    public void Init() {
        Rand_start = Random.Range(1, 10);
        Rand_preiod = Random.Range(1, 10);
        Place_seed = Random.Range(1, 4);
        
        One_1.text = (Rand_start).ToString();
        One_2.text = (Rand_start + Rand_preiod * 1).ToString();
        One_3.text = (Rand_start + Rand_preiod * 2).ToString();

        Rand_start = Random.Range(1, 10);
        Rand_preiod = Random.Range(10, 20);
        Place_seed = Random.Range(1, 4);

        Two_1.text = (Rand_start).ToString();
        Two_2.text = (Rand_start + Rand_preiod * 1).ToString();
        Two_3.text = (Rand_start + Rand_preiod * 2).ToString();

        Rand_start = Random.Range(1, 10);
        Rand_preiod = Random.Range(20, 30);
        Place_seed = Random.Range(1, 4);
        if(Place_seed == 1) {
            Three_1.text = "_ _";
            Three_2.text = (Rand_start + Rand_preiod * 1).ToString();
            Three_3.text = (Rand_start + Rand_preiod * 2).ToString();
            Answer += Rand_start;
        }
        else if(Place_seed == 2) {
            Three_1.text = (Rand_start).ToString();
            Three_2.text = "_ _";
            Three_3.text = (Rand_start + Rand_preiod * 2).ToString();
            Answer += (Rand_start + Rand_preiod * 1);
        }
        else if(Place_seed == 3) {
            Three_1.text = (Rand_start).ToString();
            Three_2.text = (Rand_start + Rand_preiod * 1).ToString();
            Three_3.text = "_ _";
            Answer += (Rand_start + Rand_preiod * 2);
        }

    }

    public int GetAnswer() {
        return Answer;
    }
}
