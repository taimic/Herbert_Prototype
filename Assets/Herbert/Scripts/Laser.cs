using UnityEngine;
using System.Collections;
using System;

public class Laser : iAttatch
{
    private bool shooting;

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

        }
        else
        {
            Debug.Log("Laser: Target has not been set.");
        }
    }

    public void Rotate(float a)
    {
        throw new NotImplementedException();
    }

    public void StartAction()
    {
        shooting = true;
    }

    public void StopAction()
    {
        shooting = false;
    }

    public void Update()
    {
        if (shooting)
        {
            //TODO
        }
    }
}

