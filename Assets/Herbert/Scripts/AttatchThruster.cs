using UnityEngine;
using System.Collections;
using System;

public class AttatchThruster : MonoBehaviour, iAttach {

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
        body.AddForce(transform.rotation * Vector3.up * 5);
        afterBurner.SetActive(true);
        if (!audio.isPlaying) {
            audio.Play();
        }
    }

    public void StopAction() {
        afterBurner.SetActive(false);
        audio.Stop();
    }

    public void Rotate(float a) {
        body.AddTorque(a * Time.deltaTime * 400);
    }
}