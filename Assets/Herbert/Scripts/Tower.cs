using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
	private Attach attach = null;

	private int Energy = 100;
	private bool active = false;

    private List<GameObject> targets; 


	void Start()
    {
        targets = new List<GameObject>();


        gameObject.AddComponent<Laser>();
    }

    void Update()
    {
        if (targets.Count > 0)
        {
            // TODO
        }

    }


	public void Activate() { active = true; }

	void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.gameObject.CompareTag("Ship"))
        if (!other.gameObject.CompareTag("Tower"))
        {
            Debug.Log(other.gameObject + " entered the trigger");

            //TODO add targets
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // if (other.gameObject.CompareTag("Ship"))
        if (!other.gameObject.CompareTag("Tower"))
        {
            Debug.Log(other.gameObject + " is within the trigger");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // if (other.gameObject.CompareTag("Ship"))
        if (!other.gameObject.CompareTag("Tower"))
        {
            Debug.Log(other.gameObject + " left the trigger");

            //TODO delete targets
        }
    }



}


