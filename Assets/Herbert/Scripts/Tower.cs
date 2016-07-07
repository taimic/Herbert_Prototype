using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
	public Laser laser;

	private bool active = false;
    public float coolDown = 5f;

    /** list of targets within the trigger */
    private List<GameObject> targetList;

    private float maxHealth = 3;
    public float MaxHealth
    {
        get
        {
            return maxHealth;
        }
    }
    private float health = 0;
    private bool componentDestroyed = false;

    private SpriteRenderer baseGraphic;

    void Start()
    {
        health = maxHealth;
        baseGraphic = GetComponentInChildren<SpriteRenderer>();
        targetList = new List<GameObject>();
        Activate();     // TODO Tower eventuell erst aktivieren wenn Objective eingesammelt?   
    }

	public void Activate() { active = true; }


    public void Update()
    {
        if (active && targetList.Count > 0)
        {
            laser.Target = (targetList.Count > 1) ? FindTarget() : targetList[0];
            
            if (!laser.IsShooting)
            {
                Debug.Log(laser.Target.name + " is set as target of "+gameObject.name);
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


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            targetList.Add(other.gameObject);
            //Debug.Log(other.gameObject + " entered the trigger");
            //Debug.Log(targetList.Count + " targets are in the list");
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            targetList.Remove(other.gameObject);
            //Debug.Log(other.gameObject + " left the trigger");
            //Debug.Log(targetList.Count + " targets are in the list");
        }
    }

    private void DestroyComp()
    {
        componentDestroyed = true;
        baseGraphic.color = Color.grey;
        active = false;
        laser.StopAction();
        StartCoroutine(TowerRespawn());

    }

    private void Respawn()
    {
        componentDestroyed = false;
        baseGraphic.color = Color.white;
        active = true;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("BulletPlayer"))
        {
            // hit by bullet or ohter stuff
            health -= 1;

            if (health <= 0)
                DestroyComp();
        }
    }

    IEnumerator TowerRespawn()
    {
        yield return new WaitForSeconds(coolDown);
        Respawn();
    }


}


