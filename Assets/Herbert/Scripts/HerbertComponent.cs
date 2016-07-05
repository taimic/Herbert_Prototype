using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif 

[RequireComponent (typeof (Rigidbody2D)), RequireComponent (typeof (BoxCollider2D))]
public class HerbertComponent : MonoBehaviour {

	public static List<HerbertComponent> parts = new List<HerbertComponent>();
	//public Vector3 direction = Vector3.zero;

	//up, right, down, left
	public Vector2[] anchors = new Vector2[]{
		new Vector2(0.0f,0.5f),
		new Vector2(0.5f,0.0f),
		new Vector2(0.0f,-0.5f),
		new Vector2(-0.5f,0.0f)
	};

	public AnimationCurve gravity = AnimationCurve.EaseInOut(0,0,1,1);
	Rigidbody2D rigid;
	BoxCollider2D collider;

	public float radius = 3f;
	public float force = 1f;

	public bool isDocked = false;
	
	public virtual void OnEnable() {
		parts.Add (this);
	}
	public virtual void OnDisable() {
		parts.Remove(this);
	}


    // Use this for initialization
    public virtual void Awake () {
		rigid = GetComponent<Rigidbody2D>();
		rigid.gravityScale = 0.0f;

		collider = GetComponent<BoxCollider2D>();
	}

	void Start(){
		rigid.AddForce (Vector2.up * 2, ForceMode2D.Impulse);
	}

	void FixedUpdate() {
		//rigid.MovePosition(transform.position + direction * speed * Time.deltaTime);

		if (!isDocked) {

			foreach (HerbertComponent part in parts) {

				if (part != this) {
					float distance = Vector2.Distance ((Vector2)transform.position, (Vector2)part.transform.position);
					if (radius >= distance) {
						float max = float.PositiveInfinity;
						Vector2 goal = Vector2.zero;
						foreach (Vector2 obj in anchors) {

							Vector2 anchor = RotatePointAroundPivot(obj, Vector2.zero, transform.rotation);


							float anchordistance = Vector2.Distance ((Vector2)transform.position, (Vector2)part.transform.position + anchor);
							if (anchordistance < max) {
								max = anchordistance;
								goal = (Vector2)part.transform.position + anchor;

							}
						}

						Vector2 direction = (goal - (Vector2)transform.position).normalized;
					
						Debug.Log ( direction );

						rigid.AddForce (direction .normalized * GetForce (distance), ForceMode2D.Force);
					}
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Vector2 GetAchnor(){
		return Vector2.zero;
	}

	public float GetForce(float distance){
		
		float value = (distance/radius) > 1 ? 0 : 1-(distance / radius);
		return force*gravity.Evaluate(value);
	}

	public Vector2 RotatePointAroundPivot(Vector2 point, Vector2 pivot, Quaternion angles) {
		Vector2 dir = point - pivot; // get point direction relative to pivot

		dir = angles * dir; // rotate it
		//dir = Quaternion.Euler(angles) * dir; // rotate it
		point = dir + pivot; // calculate rotated point
		return point; // return it
	}

	#if UNITY_EDITOR
	void OnDrawGizmos (){
		Handles.color = new Color(1,0,0,0.1f);
		Handles.DrawSolidDisc(transform.position, Vector3.forward, radius);

		foreach (Vector2 obj in anchors) {

			Vector2 anchor = RotatePointAroundPivot(obj, Vector2.zero, transform.rotation);

			Handles.color = new Color(0,0,1,0.1f);
			Handles.DrawSolidDisc((Vector2)transform.position + anchor, Vector3.forward, 0.1f);
		}

		/*if (!EditorApplication.isPlaying) {
			_radius = radius;
		}
		Handles.color = new Color(1,0,0,0.1f);
		Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);*/
	}
	#endif 
}
