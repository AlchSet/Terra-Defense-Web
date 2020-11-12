using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {


    Rigidbody2D rigidbody;
    ParticleSystem.EmissionModule em;
    public float power = 10;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        em = transform.Find("Jet").GetComponent<ParticleSystem>().emission;
        em.enabled = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		
        if(Input.GetButton("Jump"))
        {
            em.enabled = true;
            rigidbody.AddForce(Vector2.up * power, ForceMode2D.Force);
        }
        else
        {
            em.enabled = false;
        }


	}
}
