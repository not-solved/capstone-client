using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARLocation;
using System;

public class NPCinitialize : MonoBehaviour {

    public GameObject InDuck;
    public GameObject canvas;
    private Location loc = new Location() {
        Latitude = 0,
        Longitude = 0,
        Altitude = 0,
        AltitudeMode = AltitudeMode.GroundRelative
    };

    void Start() {
        loc.Latitude = 37.44950;
        loc.Longitude = 126.65582;
        

        var opt = new PlaceAtLocation.PlaceAtOptions() {
            HideObjectUntilItIsPlaced = true,
            MaxNumberOfLocationUpdates = 2,
            MovementSmoothing = 0.5f,
            UseMovingAverage = false
        };
        GameObject duckNPC = null;
        duckNPC = PlaceAtLocation.CreatePlacedInstance(InDuck, loc, opt);
        duckNPC.GetComponent<BombState>().SetData(canvas, "InDuck", "InDuck", "InDuck", 
                                                    37.44950, 126.65582, DateTime.Now.ToString(), DateTime.Now.AddYears(500).ToString());
        duckNPC.name = "InDuck";
    }
}