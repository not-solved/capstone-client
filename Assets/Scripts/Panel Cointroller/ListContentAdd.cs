using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListContentAdd : MonoBehaviour {
    
    public void UpdateList(List<BombInformation> list, string com) {
        if(com == "Inject") {
            for(int i = 0; i < list.Count; i++) {
                GameObject.Find("InjectList (" + i.ToString() + ")").SetActive(true);
                GameObject.Find("InjectList (" + i.ToString() + ")").GetComponent<ListInfoUpdate>().SetListInformation(list[i]);
            }
        }
        else {
            for(int i = 0; i < list.Count; i++) {
                GameObject.Find("RemoveList (" + i.ToString() + ")").SetActive(true);
                GameObject.Find("RemoveList (" + i.ToString() + ")").GetComponent<ListInfoUpdate>().SetListInformation(list[i]);
            }
        }
    }
}
