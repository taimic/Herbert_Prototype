using UnityEngine;
using System.Collections;
using System; 

public class Tower : Component
{
	Attach attach = null;

	private int Energy;
	private bool active = false;


	public Tower ()
	{
			
	}

	public void Activate() { active = true; }



}


