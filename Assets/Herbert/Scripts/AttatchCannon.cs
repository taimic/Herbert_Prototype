using UnityEngine;
using System.Collections;
using System;

public class AttatchCannon : MonoBehaviour, iAttach {

    private Rigidbody2D body;
    private AudioSource audio;
    public Bullet bulletPrefab;
    private float coolDown = 0.333f;

    private Transform graphicTransform;

    private bool cannonReady = true;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        graphicTransform = GetComponentInChildren<SpriteRenderer>().transform;
    }

    public void StartAction() {
        if (!cannonReady)
            return;

        Instantiate(bulletPrefab, graphicTransform.position, graphicTransform.rotation);
        StartCoroutine(CannonCooldown());
    }

    IEnumerator CannonCooldown() {
        cannonReady = false;
        yield return new WaitForSeconds(coolDown);
        cannonReady = true;
    }

    public void StopAction() {
        // not in use
    }

    public void Rotate(float a) {
        body.AddTorque(a * Time.deltaTime * 400);
    }
}