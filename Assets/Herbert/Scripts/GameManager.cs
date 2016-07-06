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
    [SerializeField]
    private bool[] joinedPlayers = new bool[7];

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
        for(int i = 1; i <= 6; i++)
        {
            if(Input.GetButtonDown("p" + i + "_Join")){
                if (joinedPlayers[i])
                {
                    print("Player " +i+ " has already joined." );
                    return;
                }
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
                go.GetComponent<Component>().playerID = i;
                joinedPlayers[i] = true;
            }
        }
    }

    public void AddShip(Ship ship)
    {
        ships.Add(ship);
    }
}
