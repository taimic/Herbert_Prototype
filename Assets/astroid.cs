using UnityEngine;
using System.Collections;

public class astroid : MonoBehaviour {

    public float RotationSpeed { get; set; }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, RotationSpeed * Time.deltaTime * 10);
    }
}
