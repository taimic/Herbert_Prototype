using UnityEngine;
using System.Collections;
using System;

public class ComponentUI : MonoBehaviour {

    private Component component;
    private float maxHp;
    public SpriteRenderer lifeBar;
    public SpriteRenderer reviveBar;

	// Use this for initialization
	void Start () {
        component = GetComponentInParent<Component>();
        component.ComponentHit += OnHit;
        component.ReviveEvent += OnRevive;
        
        maxHp = component.MaxHealth;
	}

    private void OnRevive(float scale) {
        if (scale < 1)
            reviveBar.transform.localScale = Vector2.one * scale;
        else
            reviveBar.transform.localScale = Vector2.zero;
    }

    private void OnHit(float hpLeft) {
        lifeBar.transform.localScale = new Vector2(hpLeft / maxHp ,lifeBar.transform.localScale.y);
        if (lifeBar.transform.localScale.x < 0.001f) {
            lifeBar.transform.localScale = new Vector2(0, lifeBar.transform.localScale.y);
        }
        if (hpLeft <= 0) {
            reviveBar.transform.localScale = Vector2.zero;
        }
    }



    void Dispose() {
        component.ComponentHit -= OnHit;
        component.ReviveEvent -= OnRevive;
    }
}
