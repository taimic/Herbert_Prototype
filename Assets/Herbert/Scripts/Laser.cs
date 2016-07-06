using UnityEngine;
using System.Collections;
using System;

public class Laser : iAttach
{
    private bool isShooting;
    public bool IsShooting
    {
        get { return isShooting;  }
        set { isShooting = value; }
    }

    private GameObject target;

    public GameObject Target
    {
        get { return target;  }
        set { target = value; }
    }

    public void Rotate(float a)
    {
        throw new NotImplementedException();
    }

    public void StartAction()
    {
        isShooting = true;
    }

    public void StopAction()
    {
        isShooting = false;
    }

    public void Update()
    {
        if (IsShooting && target != null)
        {
            //TODO shoot
        }
    }
}

