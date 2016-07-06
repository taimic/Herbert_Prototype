using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Highscore : MonoBehaviour {
    // The default text to set
    private static string DEFAULT_TEXT = "Highscore: ";

    // The score
    private int score;
    // The text to set
    private Text text;

	// Use this for initialization
	void Start () {
        text = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = DEFAULT_TEXT + score;
	}

    // Sets the highscore
    void setHighscore(int score){
        this.score = score;
    }
}
