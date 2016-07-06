using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static int playerID = 1;
    public GameObject baseComponentPrefab;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        JoinPlayers();
	}

    private void JoinPlayers() {
        // join players
        //if (Input.GetButtonDown("Join")) {
        //    //foreach(string item in Input.GetJoystickNames())
        //    //    print("JOIN " + item);
        //    GameObject go = Instantiate(baseComponentPrefab);
        //    go.GetComponent<Component>().playerID = playerID++;
        //}
    }
}
