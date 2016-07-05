using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Component : MonoBehaviour, iControll {
    // --------------------------------
    // physic settings
    [Range(0, 20)]
    public float thrusterPower;
    [Range(0, 1)]
    public float drag;
    private float maxDrag = 5.0f;
    // --------------------------------

    public GameObject shipPrefab;
    public Ship Ship;
    public GameObject[] attatchmentPrefabs;
    public List<GameObject> attachments;

    private Thruster[] basicThrusters;

    // hinge is used to attach stuff
    private HingeJoint2D hinge;

    iAttatch attach = null;
    GameObject preAttach = null;

    public int playerID = 0;

    private float maxHealts = 10;
    private float health = 0;

    void Start() {
        health = maxHealts;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.drag = drag; // do this in update for real time tests
        rb.angularDrag = drag;

        hinge = GetComponent<HingeJoint2D>();

        basicThrusters = GetComponentsInChildren<Thruster>();

        attachments = new List<GameObject>();
        foreach (GameObject item in attatchmentPrefabs) {
            if (item.GetComponent<iAttatch>() != null) {
                GameObject newGO = (GameObject)Instantiate(item, Vector3.zero, Quaternion.identity);
                attachments.Add(newGO);
                newGO.transform.parent = this.transform;
                newGO.SetActive(false);
            } 
        }

        preAttach = attachments[0];
        preAttach.SetActive(true);

        // add hinge
        hinge = gameObject.AddComponent<HingeJoint2D>();
        // set default attachment
        hinge.connectedBody = attachments[0].GetComponent<Rigidbody2D>();


        //attatch = GetComponentInChildren<iAttatch>();
    }

    void Update() {
        if (attach == null) {
            // used for phase 1
            Move();
            SwitchAttach();
        }
        else {
            // used for phase 2
            Action();
            Rotate();
        }
    }

    public void AddToShip(Component otherComponent) {
        if (Ship != null)
            return;

        if (otherComponent.Ship != null) { // ohter has a ship
            this.Ship = otherComponent.Ship;
        }
        else {
            GameObject newShipGO = Instantiate(shipPrefab);
            newShipGO.transform.position = this.transform.position;
            Ship newShip = newShipGO.GetComponent<Ship>();
            this.Ship = newShip;
        }

        Ship.AddComponent(this);
    }

    // movement for phase
    public void Move() {
        float x = Input.GetAxis("p" + playerID + "_x");
        float y = Input.GetAxis("p" + playerID + "_y");

        foreach (Thruster item in basicThrusters) {
            item.StopThrust();
        }
        if (x > 0) {
            basicThrusters[0].Thrust();
        }
        else if (x < 0) {
            basicThrusters[1].Thrust();
        }
        if (y > 0) {
            basicThrusters[2].Thrust();
        }
        else if (y < 0) {
            basicThrusters[3].Thrust();
        }
    }

    private int currentAtt = 0;
    public void SwitchAttach() {

        if (Input.GetButton("p" + playerID + "_action")) { // hold for lock attatchment (~3sec)

        }

        if (Input.GetButtonUp("p" + playerID + "_action")) {
            preAttach.SetActive(false);
            preAttach = attachments[++currentAtt % attachments.Count];
            preAttach.SetActive(true);

            hinge.connectedBody = preAttach.GetComponent<Rigidbody2D>();

            preAttach.transform.localPosition = Vector2.zero;
        }
    }

    public void Action() {
        if (Input.GetAxis("p" + playerID + "_action") != 0) {
            attach.StartAction();
        }
        else {
            attach.StopAction();
        }
    }

    public void Rotate() {
        float x = Input.GetAxis("p" + playerID + "_x");

        attach.Rotate(x);
    }
}