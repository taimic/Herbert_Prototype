using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    private static int playerID = 1;
    public GameObject baseComponentPrefab;
    [SerializeField]
    private SpawnPoint[] spawnPoints;
    [SerializeField]
    private List<Ship> ships;

	// Use this for initialization
	void Start () {
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        ships = new List<Ship>();
	}
	
	// Update is called once per frame
	void Update () {
        JoinPlayers();
	}

    private void JoinPlayers() {
        // join players
        if (Input.GetButtonDown("Join")) {
            //foreach(string item in Input.GetJoystickNames())
            //    print("JOIN " + item);
            Vector3 spawnPosition = Vector2.zero;
            Quaternion spawnRotation = new Quaternion();
            foreach (SpawnPoint spawnPoint in spawnPoints)
            {
                if (!spawnPoint.HasCollision())
                {
                    spawnPosition = spawnPoint.transform.position;
                    spawnRotation = spawnPoint.transform.rotation;
                    break;
                }
            }
            GameObject go = Instantiate(baseComponentPrefab, spawnPosition, spawnRotation) as GameObject;
            go.GetComponent<Component>().playerID = playerID++;
        }
    }

    public void AddShip(Ship ship)
    {
        ships.Add(ship);
    }
}
