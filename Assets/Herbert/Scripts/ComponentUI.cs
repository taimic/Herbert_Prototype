using UnityEngine;
using System.Collections;
using System;

public class ComponentUI : MonoBehaviour {

    private Component component;
    private float maxHp;
    public SpriteRenderer lifeBar;

	// Use this for initialization
	void Start () {
        component = GetComponentInParent<Component>();
        component.ComponentHit += OnHit;
        maxHp = component.MaxHealth;
	}

    private void OnHit(float hpLeft) {

        lifeBar.transform.localScale = new Vector2(hpLeft / maxHp ,lifeBar.transform.localScale.y);
        if (lifeBar.transform.localScale.x < 0.001f) {
            lifeBar.transform.localScale = new Vector2(0, lifeBar.transform.localScale.y);
        }
    }

    void Dispose() {
        component.ComponentHit -= OnHit;
    }
}
