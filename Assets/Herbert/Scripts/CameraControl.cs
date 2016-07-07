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

        float size = (float)(Mathf.Abs(max - min) * Screen.height / Screen.width * 0.5);
        if (size < 5) size = 5;
        size = FindRequiredSize(center);
        cam.orthographicSize = size;
    }

    private float FindRequiredSize(Vector3 _center)
    {
        // Find the position the camera rig is moving towards in its local space.
        Vector3 desiredLocalPos = transform.InverseTransformPoint(_center);

        // Start the camera's size calculation at zero.
        float size = 0f;

        // Go through all the targets...
        for (int i = 0; i < Ship.ships.Count; i++)
        {
            // ... and if they aren't active continue on to the next target.
            if (!Ship.ships[i].gameObject.activeSelf)
                continue;

            // Otherwise, find the position of the target in the camera's local space.
            Vector3 targetLocalPos = transform.InverseTransformPoint(Ship.ships[i].transform.position);

            // Find the position of the target from the desired position of the camera's local space.
            Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

            // Choose the largest out of the current size and the distance of the tank 'up' or 'down' from the camera.
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));

            // Choose the largest out of the current size and the calculated size based on the tank being to the left or right of the camera.
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / cam.aspect);
        }

        // Add the edge buffer to the size.
        size += 2;

        // Make sure the camera's size isn't below the minimum.
        size = Mathf.Max(size, 5);

        return size;
    }

}
