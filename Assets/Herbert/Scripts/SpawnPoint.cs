using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPoint : MonoBehaviour {
    public List<Collision2D> collisions;

    public bool HasCollision()
    {
        return (collisions.Count > 0);
    }
	// Use this for initialization
	void Start () {
	    collisions = new List<Collision2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        collisions.Add(other);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        collisions.Remove(other);
    }


}
