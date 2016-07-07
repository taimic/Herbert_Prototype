using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D)), RequireComponent (typeof (BoxCollider2D))]
public class Rocket : MonoBehaviour {

	public GameObject target;

	Rigidbody2D rigid;
	public float maxspeed = 2;

	public FacingDirection facing = FacingDirection.UP;

	public enum FacingDirection {
		UP = 270,
		DOWN = 90,
		LEFT = 180,
		RIGHT = 0
	}

	// Use this for initialization
	void Awake () {
		rigid = gameObject.GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void FixedUpdate () {

		if(target != null){
			Vector3 dir = (target.transform.position - transform.position).normalized;
			rigid.AddForce (dir, ForceMode2D.Force);
		}

		Vector3 direction = rigid.velocity.normalized;

		if (rigid.velocity.magnitude >= maxspeed) {
			rigid.velocity = direction * maxspeed;
		}
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		angle += (float)facing;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

	}
}