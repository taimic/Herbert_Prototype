using UnityEngine;
using System.Collections;

public class TowerDamageTrigger : MonoBehaviour {
    Tower tower;
    // Use this for initialization
    void Start() {
        tower = GetComponentInParent<Tower>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        // hit by bullet or ohter stuff
        tower.HitTower();

        Destroy(other.gameObject);
    }
}
