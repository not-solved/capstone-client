using UnityEngine;
using GoShared;
using GoMap;

public class ObjectInMinimap : MonoBehaviour {
    
    public GameObject bombPrefab;

    public void CreateBombObject(BombInformation target) {
        GameObject bomb = GameObject.Instantiate(bombPrefab);
        bomb.SetActive(true);
        Vector3 position = new Coordinates(target.latitude, target.longitude, 0).
                                convertCoordinateToVector(bomb.transform.position.y);
        position = GOMap.AltitudeToPoint(position);

        bomb.transform.localPosition = position;
        bomb.transform.SetParent(GameObject.Find("Places").transform);
        bomb.name = target.bombID + "_minimap";
    }
}