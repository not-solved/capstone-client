using UnityEngine;

public class QuitGame : MonoBehaviour {

    public GameObject canvas;
    public void OnClickExit() {
        canvas.GetComponent<SendToNodeJS>().CloseConnection();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    void Update () {
        if(Input.GetKeyDown(KeyCode.Escape)){
            canvas.GetComponent<SendToNodeJS>().CloseConnection();
            Application.Quit();
        }
    }
}