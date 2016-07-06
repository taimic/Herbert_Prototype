using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPoint : MonoBehaviour {
    public List<Collider2D> collisions;

    public bool HasCollision()
    {
        return (collisions.Count > 0);
    }
	// Use this for initialization
	void Start () {
	    collisions = new List<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        collisions.Add(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        collisions.Remove(other);
    }


}
