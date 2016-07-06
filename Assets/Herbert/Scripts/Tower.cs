using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
	public Laser laser;

	private int Energy = 100;
	private bool active = false;

    /** list of targets within the trigger */
    private List<GameObject> targetList;


	void Start()
    {
        targetList = new List<GameObject>();
        Activate();       
    }

	public void Activate() { active = true; }

	void OnTriggerEnter2D(Collider2D other)
    {
        if (active)
        {
            // if (other.gameObject.CompareTag("Ship"))
            if (!other.gameObject.CompareTag("Tower"))
            {
                targetList.Add(other.gameObject);
                //Debug.Log(other.gameObject + " entered the trigger");
                //Debug.Log(targetList.Count + " targets are in the list");
            }
        }        
    }

    //void OnTriggerStay2D(Collider2D other)
    //{
    //    // if (other.gameObject.CompareTag("Ship"))
    //    if (!other.gameObject.CompareTag("Tower"))
    //    {
    //        //Debug.Log(other.gameObject + " is within the trigger");
    //    }
    //}

    void OnTriggerExit2D(Collider2D other)
    {
        if (active)
        {
            // if (other.gameObject.CompareTag("Ship"))
            if (!other.gameObject.CompareTag("Tower"))
            {
                targetList.Remove(other.gameObject);
                //Debug.Log(other.gameObject + " left the trigger");
                //Debug.Log(targetList.Count + " targets are in the list");
            }
        }
        
    }

    public void Update()
    {
        if (active && targetList.Count > 0)
        {
            laser.Target = (targetList.Count > 1) ? FindTarget() : targetList[0];
            
            if (laser.IsShooting)
            {
                Debug.Log(laser.Target.name + " is set as target.");
                laser.StartAction();
            }
            
        } else
        {
            if (laser.IsShooting)
            {
                laser.StopAction();
                Debug.Log(gameObject.name + " is not shooting any more.");
            }
        }
    }

    private GameObject FindTarget()
    {
        GameObject target = targetList[0];
        float distance = Vector3.Distance(target.transform.position, transform.position);
        foreach (GameObject t in targetList)
        {
            if (Vector3.Distance(t.transform.position, transform.position) < distance)
            {
                target = t;
            }
        }
        return target;
    }


}


