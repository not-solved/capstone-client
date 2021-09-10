using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARLocation;

public class TestInitScript : MonoBehaviour {
    public GameObject newTarget;
    LocationInfo GPSlocation;
    double detailed_num = 1.0;

    void Start() {
        GPSlocation = Input.location.lastData;
        
        var opt = new PlaceAtLocation.PlaceAtOptions() {
            HideObjectUntilItIsPlaced = true,
            MaxNumberOfLocationUpdates = 2,
            MovementSmoothing = 0.5f,
            UseMovingAverage = false
        };
        

        var loc1 = new Location() {
            Latitude = 37.45226199755244,
            Longitude = 126.65321594324752,
            Altitude = 0,
            AltitudeMode = AltitudeMode.GroundRelative
        };
        var loc2 = new Location() {
            Latitude = 37.452180570129514,
            Longitude = 126.65322734007191,
            Altitude = 0,
            AltitudeMode = AltitudeMode.GroundRelative
        };
        var loc3 = new Location() {
            Latitude = 37.45231628245183,
            Longitude = 126.65331091678769,
            Altitude = 0,
            AltitudeMode = AltitudeMode.GroundRelative
        };
        var loc4 = new Location() {
            Latitude = 37.45213231679968,
            Longitude = 126.65331091678769,
            Altitude = 0,
            AltitudeMode = AltitudeMode.GroundRelative
        };

        newTarget.SetActive(true);
        PlaceAtLocation.CreatePlacedInstance(newTarget, loc1, opt).SetActive(true);
        PlaceAtLocation.CreatePlacedInstance(newTarget, loc2, opt).SetActive(true);
        PlaceAtLocation.CreatePlacedInstance(newTarget, loc3, opt).SetActive(true);
        PlaceAtLocation.CreatePlacedInstance(newTarget, loc4, opt).SetActive(true);
        ARLocationManager.Instance.Restart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}