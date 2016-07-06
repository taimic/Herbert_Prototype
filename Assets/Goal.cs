using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Goal : MonoBehaviour {

    public Text victoryText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Component comp = other.GetComponent<Component>();
        if (comp != null)
        {
            if (comp.Ship.HasObjective)
            {
                // game over
                victoryText.enabled = true;
                Time.timeScale = 0.2f;
            }
        }
    }
}
