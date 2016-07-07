using UnityEngine;
using System.Collections;
using System;

public class Laser : MonoBehaviour, iAttach
{
    private float speed = 5f;

    public GameObject bulletPrefab;

    private float coolDown = 0.333f;
    private bool cannonReady = true;


    private bool isShooting;
    public bool IsShooting
    {
        get { return isShooting;  }
        set { isShooting = value; }
    }

    private GameObject target;

    public GameObject Target
    {
        get { return target;  }
        set { target = value; }
    }

    public void Rotate(float a)
    {
        // Rotation happens in Update        
    }

    public void StartAction()
    {
        isShooting = true;
    }

    public void StopAction()
    {
        isShooting = false;
    }

    public void Update()
    {
        if (target != null)
        {
            Vector3 vectorToTarget = target.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, q, Time.deltaTime * speed);
            
            if (IsShooting && cannonReady)
            {
                Quaternion dir = transform.localRotation * Quaternion.Euler(0, 0, -90);
                GameObject go = (GameObject)Instantiate(bulletPrefab, transform.position, dir);
                go.GetComponent<Bullet>() .CollisionId = -1;
                StartCoroutine(CannonCooldown());
            }
        }
    }

    IEnumerator CannonCooldown()
    {
        cannonReady = false;
        yield return new WaitForSeconds(coolDown);
        cannonReady = true;
    }
}

