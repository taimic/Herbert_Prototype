using UnityEngine;
using System.Collections;

public class astroidsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        astroid[] astroids = FindObjectsOfType<astroid>();
        foreach (astroid a in astroids)
        {
            a.RotationSpeed = Random.Range(-0.5f, 0.5f);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
