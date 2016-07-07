using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public int CollisionId { get; set; }
    public float speed;
    public float maxLifetime = 10;
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed, ForceMode2D.Impulse);
        Destroy(this.gameObject, maxLifetime);
	}


    void OnTriggerEnter2D(Collider2D other)
    {
        Component c = other.gameObject.GetComponent<Component>();
        if (c != null && c.getShipId() != CollisionId)
        {
            c.gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.GetComponent<Rigidbody2D>().velocity*10, ForceMode2D.Force);
            c.HandleCollision();
            Destroy(this.gameObject);
        }
        
    }

    void OnCollisionEnter2D(Collision2D other) {

        // hit by bullet or ohter stuff
        //Destroy(this.gameObject);
    }
}
