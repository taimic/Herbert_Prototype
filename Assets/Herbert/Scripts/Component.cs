using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public delegate void ComponentHit(float hpLeft);

public class Component : MonoBehaviour, iControll {
    public event ComponentHit ComponentHit;
    // --------------------------------
    // physic settings for basic thrusters
    [Range(0, 20)]
    public float thrusterPower;
    [Range(0, 1)]
    public float drag;
    private float maxDrag = 5.0f;
    // --------------------------------

    private float confirmationTime = 3;
    private float currentConfirmationTime = 0;

    public GameObject shipPrefab;
    private Ship ship;
    public Ship Ship {
        get {
            return ship;
        }
        set {
            ship = value;
        }
    }
    private SpriteRenderer baseGraphic;

    public GameObject[] attachmentPrefabs;
    private List<GameObject> attachments;
    private int currentAtt = 0; // stores currently selected attachment

    private Thruster[] basicThrusters;

    // hinge is used to attach stuff
    private HingeJoint2D hinge;

    iAttach attachment = null;
    public iAttach Attachment {
        get {
            return attachment;
        }
    }

    GameObject preAttach = null;

    public int playerID = 0;

    private float maxHealth = 3;
    public float MaxHealth {
        get {
            return maxHealth;
        }
    }
    private float health = 0;
    public bool componentDestroyed = false;

    void Start() {
        health = maxHealth;
        baseGraphic = GetComponentInChildren<SpriteRenderer>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.drag = drag; // do this in update for real time tests
        rb.angularDrag = drag;

        hinge = GetComponent<HingeJoint2D>();

        basicThrusters = GetComponentsInChildren<Thruster>();

        attachments = new List<GameObject>();
        foreach (GameObject item in attachmentPrefabs) {
            if (item.GetComponent<iAttach>() != null) {
                GameObject newGO = (GameObject)Instantiate(item, Vector3.zero, Quaternion.identity);
                attachments.Add(newGO);
                newGO.transform.parent = this.transform;
                newGO.SetActive(false);
            } 
        }

        // add hinge
        hinge = gameObject.AddComponent<HingeJoint2D>();

        // set first attachment as default
        preAttach = attachments[0];
        SetPreAttachment(attachments[0]);
    }

    void Update() {
        if (componentDestroyed)
            return;

        if (attachment == null) {
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

    // add component to ship
    public void AddToShip(Component otherComponent, Vector3 pos) {
        if (ship != null)
            return;

        // ohter component has a ship
        if (otherComponent.Ship != null) {
            transform.position = otherComponent.transform.position - pos * 2;
            this.ship = otherComponent.Ship;
        } // no ship exists yet, make one
        else {
            GameObject newShipGO = Instantiate(shipPrefab);
            newShipGO.transform.position = this.transform.position;
            Ship newShip = newShipGO.GetComponent<Ship>();
            this.ship = newShip;
        }
        ship.AddComponent(this);

        GetComponent<HerbertComponent>().isDocked = true;
    }

    // phase 1 controls
    // movement for phase 1
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

    public void SwitchAttach() {
        // hold to lock attachment (~3sec)
        if (Input.GetButton("p" + playerID + "_action")) {
            if (currentConfirmationTime < confirmationTime) {
                currentConfirmationTime += Time.deltaTime;
            }
            else {
                // set attachment
                attachment = preAttach.GetComponent<iAttach>();
                baseGraphic.color = Color.white;
                // deactivate all basic thrusters
                foreach (Thruster item in basicThrusters) {
                    item.StopThrust();
                }
            }
        }
        else {
            if (currentConfirmationTime > 0) {
                currentConfirmationTime = 0;
            }
        }

        if (Input.GetButtonUp("p" + playerID + "_action")) {

            SetPreAttachment(attachments[++currentAtt % attachments.Count]);

            //preAttach.SetActive(false);
            //preAttach = attachments[++currentAtt % attachments.Count];
            //preAttach.SetActive(true);

            //hinge.connectedBody = preAttach.GetComponent<Rigidbody2D>();

            //preAttach.transform.localPosition = Vector2.zero;
        }
    }

    private void SetPreAttachment(GameObject nextAttach) {
        preAttach.SetActive(false);
        preAttach = nextAttach;
        preAttach.SetActive(true);

        hinge.connectedBody = preAttach.GetComponent<Rigidbody2D>();

        preAttach.transform.localPosition = Vector2.zero;
    }

    // phase 2 controls
    public void Action() {
        if (Input.GetAxis("p" + playerID + "_action") != 0) {
            attachment.StartAction();
        }
        else {
            attachment.StopAction();
        }
    }

    public void Rotate() {
        float x = Input.GetAxis("p" + playerID + "_x");

        attachment.Rotate(x);
    }

    private void DestroyComp() {
        componentDestroyed = true;
        baseGraphic.color = Color.grey;
    }

    void OnTriggerEnter2D(Collider2D other) {
        // hit by bullet or ohter stuff
        health -= 1;

        if (health <= 0)
            DestroyComp();

        if (ComponentHit != null)
            ComponentHit(health);
    }
}