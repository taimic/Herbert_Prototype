using UnityEngine;
using System.Collections;

public class Anchor : MonoBehaviour {
    private Component component;

    public Component Component
    {
        get
        {
            return component;
        }
    }


    // Use this for initialization
    void Start () {
        component = transform.parent.parent.GetComponent<Component>();
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other) {
        Anchor otherAnchor = other.GetComponent<Anchor>();
        if (otherAnchor == null)
            return;

        component.AddToShip(otherAnchor.Component, transform.localPosition);
	}
}