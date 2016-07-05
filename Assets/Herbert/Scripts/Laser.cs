using UnityEngine;
using System.Collections;
using System;

public class Laser : Attach
{
    private GameObject target;

    public GameObject Target
    {
        get
        {
            return target;
        }

        set
        {
            target = value;
        }
    }

    public void Action()
    {
        if (Target != null)
        {
            
        } else
        {
            Debug.Log("Laser: Target has not been set.");
        }
    }
}

