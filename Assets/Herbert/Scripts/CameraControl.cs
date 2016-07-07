using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    private Camera cam;
    // Use this for initialization
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        ///Ship[] ships = FindObjectsOfType<Ship>();
        if (Ship.ships.Count == 0) return;

        Vector3 center = Vector3.zero;
        float max = float.MinValue;
        float min = float.MaxValue;
        foreach (Ship s in Ship.ships)
        {
            if (max < s.transform.position.x)
                max = s.transform.position.x;
            if (min > s.transform.position.x)
                min = s.transform.position.x;

            center += s.transform.position;
        }
        center /= Ship.ships.Count;
        cam.transform.position = new Vector3(center.x, center.y, cam.transform.position.z);
       // cam.transform.position = new Vector3(10, 10, 0);

        float size = (float)(Mathf.Abs(max - min) * Screen.height / Screen.width * 0.5) + 3;
        if (size < 5) size = 5;
        cam.orthographicSize = size;
    }

}
