using UnityEngine;
using System.Collections;

public class Objective : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Component comp = other.GetComponent<Component>();
        print("Collect");
        if (comp == null)
            return;
        if (comp.Ship == null)
            return;
        
        comp.Ship.HasObjective = true;

        Destroy(gameObject);

    }
}
