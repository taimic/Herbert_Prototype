using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    Ship[] ships;
    
	// Use this for initialization
	void Start () {
        ships = FindObjectsOfType<Ship>();
    }
	
	// Update is called once per frame
	void Update () {
        

        if (ships.Length == 0) return;

        Vector3 center = Vector3.zero;
        
        foreach (Ship s in ships)
        {
            center += s.transform.position;
        }

        center /= ships.Length;
        center.z = transform.position.z;
        transform.position = center;

        float max = 0f;
        Vector3 origin = Vector3.zero;

        foreach (Ship s in ships)
        {
            if (Vector3.Distance(center, s.transform.position) > max)
            {
                max = Vector3.Distance(center, s.transform.position);
                origin = s.transform.position;
            }
        }


        Vector3 viewP = gameObject.GetComponent<Camera>().WorldToViewportPoint(origin);
        float x = Mathf.Abs(viewP.x - 0.5f);
        if (x > 0.4f)
        {
            Debug.Log(true);
        }


    }
}
