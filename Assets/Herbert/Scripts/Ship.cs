using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : MonoBehaviour {
    [SerializeField]
    List<Component> components;
    List<PowerUp> powerUps;
    bool hasObjective;

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
	}

    void Update () {
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
}