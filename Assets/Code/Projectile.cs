using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour {



    public UnityEvent OnFire;

    Rigidbody2D rigidbody;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization

    public void Fire(Vector2 force)
    {

        rigidbody.AddForce(force, ForceMode2D.Impulse);
        OnFire.Invoke();

    }

    public void Reset()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0;
    }
}
