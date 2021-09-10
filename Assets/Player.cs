using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player : MonoBehaviour {
    [SerializeField] ParticleSystem collectParticle = null;

    public void Collect() {
        collectParticle.Play();
    }
}
