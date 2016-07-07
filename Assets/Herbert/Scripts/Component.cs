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

    private AudioSource audio;

    public AudioClip changeAttachClip;
    public AudioClip mergeComponentsClip;

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
    private bool componentDestroyed = false;
    private float downDuration = 2.0f;
    private float reviveTime = 0;

    void Start() {
        health = maxHealth;
        baseGraphic = GetComponentInChildren<SpriteRenderer>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.drag = drag; // do this in update for real time tests
        rb.angularDrag = drag;

        hinge = GetComponent<HingeJoint2D>();

        basicThrusters = GetComponentsInChildren<Thruster>();

        audio = GetComponent<AudioSource>();

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
        if (componentDestroyed) {
            ReviveComponent();
            return;
        }
            

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

    public int getShipId()
    {
        if (ship == null)
            return -1;
        return this.ship.Id;
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
            newShip.Id = playerID;
            this.ship = newShip;
        }
        ship.AddComponent(this);

        audio.clip = mergeComponentsClip;
        audio.time = 0.23f;
        audio.pitch = 1.44f;
        audio.Play();
        GetComponent<HerbertComponent>().isDocked = true;
    }

    // phase 1 controls
    // movement for phase 1
    public void Move() {
        float x = Input.GetAxis("p" + playerID + "_x");
        float y = Input.GetAxis("p" + playerID + "_y");

        //foreach (Thruster item in basicThrusters) {
        //    item.StopThrust();
        //}
        if (x > 0) {
            basicThrusters[0].Thrust();
            basicThrusters[1].StopThrust();
        }
        else if (x < 0) {
            basicThrusters[1].Thrust();
            basicThrusters[0].StopThrust();
        } else {
            basicThrusters[0].StopThrust();
            basicThrusters[1].StopThrust();
        }

        if (y > 0) {
            basicThrusters[2].Thrust();
            basicThrusters[3].StopThrust();
        }
        else if (y < 0) {
            basicThrusters[3].Thrust();
            basicThrusters[2].StopThrust();
        } else {
            basicThrusters[2].StopThrust();
            basicThrusters[3].StopThrust();
        }
    }

    public void SwitchAttach() {
        // hold to lock attachment (~3sec)
        if (Input.GetButton("p" + playerID + "_action")) {
            if (ship == null)
                return;
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
                audio.clip = mergeComponentsClip;
                audio.time = 0.33f;
                audio.pitch = 2.22f;
                audio.Play();
            }
        }
        else {
            if (currentConfirmationTime > 0) {
                currentConfirmationTime = 0;
            }
        }

        if (Input.GetButtonUp("p" + playerID + "_action")) {

            SetPreAttachment(attachments[++currentAtt % attachments.Count]);
            audio.clip = changeAttachClip;
            audio.time = 0.17f;
            audio.pitch = 1;
            audio.Play();
            //audio.PlayOneShot(changeAttachClip);
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
        if(attachment != null)
        {
            attachment.StopAction();
        }
        foreach(Thruster thruster in basicThrusters)
        {
            thruster.StopThrust();
        }
    }

    private void ReviveComponent() {
        if (componentDestroyed) {
            if (reviveTime < downDuration) {
                reviveTime += Time.deltaTime;
            } else {
                health = maxHealth;
                reviveTime = 0;
                componentDestroyed = false;
                baseGraphic.color = Color.white;
                if (ComponentHit != null)
                    ComponentHit(health);
            }
        }
    }

    public void HandleCollision()
    {
        //if (other.gameObject.layer != LayerMask.NameToLayer("BulletEnemy"))
        //return;
        // hit by bullet or ohter stuff
        //Bullet b = (Bullet)other.gameObject.GetComponent<Bullet>();
       // if (b != null && b.CollisionId == getShipId()) return;
        health -= 1;

        if (health <= 0)
            DestroyComp();

        if (ComponentHit != null)
            ComponentHit(health);
    }
   
}