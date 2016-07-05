using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConnectionSimulator : MonoBehaviour {

    public List<GameObject> components;
    public Ship ship;

	// Use this for initialization
	void Start () {
        foreach (GameObject comp in components)
        {
            ship.AddComponent(comp.GetComponent<Component>());
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
