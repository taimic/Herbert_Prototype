using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {

    public Text victoryText;
    private float waitSecForSceneChange = 7;


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
            if (comp.Ship == null)
                return;

            if (comp.Ship.HasObjective)
            {
                // game over
                victoryText.enabled = true;
                Time.timeScale = 0.333f;
                StartCoroutine(ChangeSceneDelayed());
            }
        }
    }

    private IEnumerator ChangeSceneDelayed() {
        yield return new WaitForSeconds(waitSecForSceneChange * Time.timeScale);
        SceneManager.LoadScene("End");
    }
}
