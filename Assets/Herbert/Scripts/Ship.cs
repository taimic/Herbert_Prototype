﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : MonoBehaviour {
    [SerializeField]
    List<Component> components;
    List<PowerUp> powerUps;
    [SerializeField]
    bool hasObjective;

    private Vector3 finalCenter = Vector3.zero;

    public static List<Ship> ships = new List<Ship>();

    public void OnEnable(){
        ships.Add(this);
    }

    public void OnDisable()
    {
        ships.Remove(this);
    }

    public bool HasObjective
    {
        get
        {
            return hasObjective;
        }

        set
        {
            hasObjective = value;
        }
    }

    // Use this for initialization
    void Start () {
        powerUps = new List<PowerUp>();
        HasObjective = false;
        FindObjectOfType<GameManager>().AddShip(this);
        
	}

    void Update () {
        //Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
        transform.position = GetCenter();
        CheckIfShipReady();
	}

    // TODO: do this when ship is ready to go (phase 2)
    bool shipReady = false;
    private void CheckIfShipReady() {
        if (shipReady)
            return;

        if (components.Count >= 2) { // if ship has min 3 components
            foreach (Component item in components) {
                if (item.Attachment != null) // check if all has allready chosen their attachment
                    item.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None; // set body constraints to none (allow rotation)
            }
        }
    }

    public void AddComponent(Component component)
    {
        Rigidbody2D rb = component.GetComponent<Rigidbody2D>();
        FixedJoint2D newJoint =  gameObject.AddComponent<FixedJoint2D>();
        newJoint.connectedBody = rb;
        if (components == null)
            components = new List<Component>();

        components.Add(component);
        component.Ship = this;
        //print("woop " + component.GetInstanceID() + " #:" + components.Count);
    }

    private Vector3 GetCenter()
    {
        if (shipReady) return finalCenter;

        Vector3 center = Vector3.zero;
        foreach (Component c in components)
        {
            center += c.transform.position;
        }
        finalCenter = center /= components.Count;
        return finalCenter;
    }
}