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
        if (Input.GetButtonDown("Join")) {
            print("JOIN");
            GameObject go = Instantiate(baseComponentPrefab);
            go.GetComponent<Component>().playerID = playerID++;
        }
    }
}
