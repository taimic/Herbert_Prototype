﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif 

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(BoxCollider2D))]
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

    public AnimationCurve gravity = AnimationCurve.EaseInOut(0, 0, 1, 1);
    Rigidbody2D rigid;
    BoxCollider2D collider;

    public float radius = 3f;
    public float force = 1f;

    public bool isDocked = false;

    public virtual void OnEnable() {
        parts.Add(this);
    }
    public virtual void OnDisable() {
        parts.Remove(this);
    }


    // Use this for initialization
    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        rigid.gravityScale = 0.0f;

        collider = GetComponent<BoxCollider2D>();
    }

    void Start() {
        //rigid.AddForce(Vector2.up * 2, ForceMode2D.Impulse);
    }

    void FixedUpdate() {
        //rigid.MovePosition(transform.position + direction * speed * Time.deltaTime);

        if (!isDocked) {

            foreach (HerbertComponent part in parts) {

                if (part != this) {
                    float distance = Vector2.Distance((Vector2)transform.position, (Vector2)part.transform.position);
                    if (radius >= distance) {
                        //float max = float.PositiveInfinity;
                        Vector2 origin = Vector2.zero;
                        float anchor_distance = 0;
                        Vector2 direction = GetDirection(part, out origin, out anchor_distance);

                        float speed = GetForce(distance);
                        //speed = speed > anchor_distance ? anchor_distance : speed;


                        rigid.AddForceAtPosition(direction.normalized * speed, origin, ForceMode2D.Force);
                        rigid.AddForce(((Vector2)part.transform.position - (Vector2)transform.position).normalized * speed / (float)2, ForceMode2D.Force);
                    }
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        /*foreach (ContactPoint contact in collision.contacts) {
			Debug.DrawLine(contact.point, contact.normal, Color.white);
		}*/
        //Debug.Log ("Collision");
    }

    // Update is called once per frame
    //void Update () {

    //}



    public Vector2 GetDirection(HerbertComponent part, out Vector2 origin, out float max_distance) {

        max_distance = float.PositiveInfinity;
        Vector2 best_anchor_other = Vector2.zero;
        origin = Vector2.zero;

        foreach (Vector2 i in anchors) {

            Vector2 anchor_own = RotatePointAroundPivot(i, Vector2.zero, transform.rotation);


            foreach (Vector2 j in part.anchors) {
                Vector2 anchor_other = RotatePointAroundPivot(j, Vector2.zero, part.transform.rotation);

                float distance = Vector2.Distance((Vector2)transform.position + anchor_own, (Vector2)part.transform.position + anchor_other);

                if (distance < max_distance) {
                    max_distance = distance;

                    origin = (Vector2)transform.position + anchor_own;
                    best_anchor_other = (Vector2)part.transform.position + anchor_other;
                }
            }
        }

        Vector2 direction = (best_anchor_other - origin).normalized;

        return direction;
    }

    public float GetForce(float distance) {

        float value = (distance / radius) > 1 ? 0 : 1 - (distance / radius);
        return force * gravity.Evaluate(value);
    }

    public Vector2 RotatePointAroundPivot(Vector2 point, Vector2 pivot, Quaternion angles) {
        Vector2 dir = point - pivot; // get point direction relative to pivot

        dir = angles * dir; // rotate it
                            //dir = Quaternion.Euler(angles) * dir; // rotate it
        point = dir + pivot; // calculate rotated point
        return point; // return it
    }

#if UNITY_EDITOR
    void OnDrawGizmos() {
        Handles.color = new Color(1, 0, 0, 0.1f);
        //Handles.DrawSolidDisc(transform.position, Vector3.forward, radius);

        foreach (Vector2 obj in anchors) {

            Vector2 anchor = RotatePointAroundPivot(obj, Vector2.zero, transform.rotation);

            Handles.color = new Color(0, 0, 1, 0.1f);
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
