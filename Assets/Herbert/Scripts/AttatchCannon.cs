using UnityEngine;
using System.Collections;
using System;

public class AttatchCannon : MonoBehaviour, iAttach {

    private Rigidbody2D body;
    private AudioSource audio;
    

    void Start() {
        body = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

    public void StartAction() {
    }

    public void StopAction() {
        // not in use
    }

    public void Rotate(float a) {
        body.AddTorque(a * Time.deltaTime * 400);
    }
}