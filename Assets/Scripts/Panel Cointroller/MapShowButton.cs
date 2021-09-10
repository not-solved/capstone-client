using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoShared;
using GoMap;

public class MapShowButton : MonoBehaviour {
    public Camera ARCamera;
    public GameObject ARGameCanvas;
    public GameObject MinimapCanvas;
    public GameObject GOPlaces;

    public List<BombInformation> DetectedBombList = new List<BombInformation>();

    public void MinimapShow() {
        MinimapCanvas.SetActive(true);
        /*
        for(int i = 0; i < DetectedBombList.Count; i++) {
            if(GameObject.Find(DetectedBombList[i].bombID + "_minimap") == null) {
                GameObject bomb = GameObject.Instantiate(bombPrefab);
                bomb.SetActive(true);
                Vector3 position = new Coordinates(DetectedBombList[i].latitude, DetectedBombList[i].longitude, 0).
                                        convertCoordinateToVector(bomb.transform.position.y);
                bomb.transform.localPosition = position;
                bomb.name = DetectedBombList[i].bombID + "_minimap";
            }
        }
        for(int i = 0; i < RemovedBombList.Count; i++) {
            if(GameObject.Find(RemovedBombList[i] + "_minimap") != null) {
                Destroy(GameObject.Find(RemovedBombList[i] + "_minimap"));
                RemovedBombList.RemoveAt(i);
            }
        }
        */
        // Coordinates coordinates = new Coordinates(37.45182, 126.65366, 0);
		// GameObject place = GameObject.Instantiate(bombPrefab);
		// Vector3 position = coordinates.convertCoordinateToVector(place.transform.position.y);
    	// place.transform.localPosition = position;
        // place.transform.SetParent(GameObject.Find("placesContainer").transform);
        GOPlaces.GetComponent<GOPlaces>().ReloadMap(DetectedBombList);
        ARGameCanvas.SetActive(false);
        ARCamera.enabled = false;
    }
}