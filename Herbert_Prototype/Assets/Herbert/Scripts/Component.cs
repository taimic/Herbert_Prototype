﻿using UnityEngine;
using System.Collections;
using System;

public class Component : MonoBehaviour, iControll {

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Interface Methods
    public void Action()
    {
        throw new NotImplementedException();
    }

    public void Move()
    {
        throw new NotImplementedException();
    }

    public void Rotate()
    {
        throw new NotImplementedException();
    }

    public void SwitchAttach()
    {
        throw new NotImplementedException();
    }
}
