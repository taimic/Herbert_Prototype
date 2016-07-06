using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float speed;
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * speed, ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other) {
        // hit by bullet or ohter stuff
        Destroy(this.gameObject);
    }
}
