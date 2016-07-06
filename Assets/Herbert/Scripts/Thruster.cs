using UnityEngine;
using System.Collections;

public class Thruster : MonoBehaviour {

    private Component ship;

    private Rigidbody2D body;
    private GameObject afterBurner;
    private AudioSource audio;

    private bool isThrusting;

    // Use this for initialization
    void Start() {
        body = GetComponent<Rigidbody2D>();
        ship = GetComponentInParent<Component>();
        afterBurner = transform.GetChild(0).gameObject;
        afterBurner.SetActive(false);
        audio = GetComponent<AudioSource>();
    }

    public void Thrust() {
        body.AddForce(transform.rotation * Vector3.up * ship.thrusterPower);
        isThrusting = true;

        if (!audio.isPlaying)
            audio.Play();
    }

    public void StopThrust() {
        isThrusting = false;
        audio.Stop();
    }

    // Update is called once per frame
    void Update() {
        if (isThrusting) {
            afterBurner.SetActive(true);
        }
        else {
            afterBurner.SetActive(false);
        }
    }
}