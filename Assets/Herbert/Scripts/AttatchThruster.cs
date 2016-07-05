﻿using UnityEngine;
using System.Collections;
using System;

public class AttatchThruster : MonoBehaviour, iAttatch {

    private Rigidbody2D body;
    private GameObject afterBurner;
    private AudioSource audio;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        afterBurner = transform.GetChild(0).gameObject;
        afterBurner.SetActive(false);
        audio = GetComponent<AudioSource>();
    }

    public void StartAction() {
        body.AddForce(transform.rotation * Vector3.up * 10);
        afterBurner.SetActive(true);
    }

    public void StopAction() {
        afterBurner.SetActive(false);
    }

    public void Rotate(float a) {
        body.AddTorque(a * Time.deltaTime * 100);
    }
}