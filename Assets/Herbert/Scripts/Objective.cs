using UnityEngine;
using System.Collections;

public class Objective : MonoBehaviour {
    private AudioSource audio;
    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Component comp = other.GetComponent<Component>();
        print("Collect");
        if (comp.Ship.HasObjective || !comp.Ship.HasEnergy()) return;

        if (comp == null)
            return;
        if (comp.Ship == null)
            return;

        

        comp.Ship.HasObjective = true;

        AudioSource.PlayClipAtPoint(audio.clip, transform.position);

        Destroy(gameObject);

    }
}
