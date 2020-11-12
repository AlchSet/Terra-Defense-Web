using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour {

    public int health = 3;

    [HideInInspector]
    public int maxHealth;

    public UnityEvent OnDeath;

    public UnityEvent OnHit;

    // Use this for initialization
    void Start () {
        maxHealth = health;
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    public void Damage(int i)
    {
        health = Mathf.Clamp(health - i, 0, maxHealth);
        OnHit.Invoke();
        if (health<=0)
        {
            OnDeath.Invoke();
        }
    }

    public void FullHeal()
    {
        health = maxHealth;
        OnHit.Invoke();
    }


    public void ModifyMaxHealth(int mod)
    {
        maxHealth = mod;
        health = maxHealth;
        OnHit.Invoke();
    }

}
