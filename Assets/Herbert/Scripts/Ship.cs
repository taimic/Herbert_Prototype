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
        components = new List<Component>();
        powerUps = new List<PowerUp>();
        HasObjective = false;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddComponent(Component component)
    {
        Rigidbody2D rb = component.GetComponent<Rigidbody2D>();
        FixedJoint2D newJoint =  gameObject.AddComponent<FixedJoint2D>();
        newJoint.connectedBody = rb;
        components.Add(component);
        component.Ship = this;
    }


}
