using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
	private Attach attach = null;

	private int Energy = 100;
	private bool active = false;

    /** list of targets within the trigger */
    private List<GameObject> targetList;

    Attach laser;


	void Start()
    {
        targetList = new List<GameObject>();


        laser = gameObject.AddComponent<Laser>();
    }

    void Update()
    {
        if (active && targetList.Count > 0)
        {
            GameObject target = targetList[0];

            if (targetList.Count > 1)
            {
                float distance = Vector3.Distance(target.transform.position, transform.position);
                foreach (GameObject t in targetList)
                {
                    if (Vector3.Distance(t.transform.position, transform.position) < distance)
                    {
                        target = t;
                    }
                }
            }
            Fire(target);
        }

    }

    private void Fire(GameObject target)
    {
        // TODO
        
        laser.Action();
    }


	public void Activate() { active = true; }

	void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.gameObject.CompareTag("Ship"))
        if (!other.gameObject.CompareTag("Tower"))
        {
            //Debug.Log(other.gameObject + " entered the trigger");

            
            targetList.Add(other.gameObject);
            //Debug.Log(targets.Count + " targets are in the list");
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // if (other.gameObject.CompareTag("Ship"))
        if (!other.gameObject.CompareTag("Tower"))
        {
            //Debug.Log(other.gameObject + " is within the trigger");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // if (other.gameObject.CompareTag("Ship"))
        if (!other.gameObject.CompareTag("Tower"))
        {
            //Debug.Log(other.gameObject + " left the trigger");

            targetList.Remove(other.gameObject);
            //Debug.Log(targets.Count + " targets are in the list");
        }
    }



}


