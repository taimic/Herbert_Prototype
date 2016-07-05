using UnityEngine;
using System.Collections;
using System;

public class Component : MonoBehaviour, iControll {
    // --------------------------------
    // physic settings
    [Range(0, 20)]
    public float thrusterPower;
    [Range(0, 1)]
    public float drag;
    private float maxDrag = 5.0f;
    // --------------------------------
    
    private Thruster[] basicThrusters;

    iAttatch attatch = null;
    iAttatch preAttatch = null;

    public int playerID = 0;

    private float maxHealts = 10;
    private float health = 0;

    void Start() {
        health = maxHealts;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.drag = drag; // do this in update for real time tests
        rb.angularDrag = drag;

        basicThrusters = GetComponentsInChildren<Thruster>();

        attatch = GetComponentInChildren<iAttatch>();
    }

    void Update()
    {
        if (attatch == null) {
            // used for phase 1
            Move();
            SwitchAttach();
        } else {
            // used for phase 2
            Action();
            Rotate();
        }
    }

    // movement for phase
    public void Move() {
        float x = Input.GetAxis("p" + (playerID + 1) + "_x");
        float y = Input.GetAxis("p" + (playerID + 1) + "_y");

        foreach(Thruster item in basicThrusters) {
            item.StopThrust();
        }
        if (x > 0) {
            basicThrusters[0].Thrust();
        } else if (x < 0) {
            basicThrusters[1].Thrust();
        }
        if (y > 0) {
            basicThrusters[2].Thrust();
        }
        else if (y < 0) {
            basicThrusters[3].Thrust();
        }
    }

    public void SwitchAttach() {
        // change pre attatch here
    }

    public void Action() {
        if (Input.GetAxis("p" + playerID + "_action") != 0) {
            attatch.StartAction();
        } else {
            attatch.StopAction();
        }
    }

    public void Rotate() {
        float x = Input.GetAxis("p" + playerID + "_x");

        attatch.Rotate(x);
    }
}