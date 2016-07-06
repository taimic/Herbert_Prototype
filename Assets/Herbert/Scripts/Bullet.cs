using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float speed;
    public float maxLifetime = 10;
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed, ForceMode2D.Impulse);
        Destroy(this.gameObject, maxLifetime);
	}


    void OnCollisionEnter2D(Collision2D other) {
        // hit by bullet or ohter stuff
        Destroy(this.gameObject);
    }
}
