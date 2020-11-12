using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {


    Transform planet;

    Rigidbody2D rigidbody;

    ParticleSystem explode;

    public bool fragments;

    GameObject masterAsteroid;




	// Use this for initialization
	void Start () {

        planet = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.AddForce((planet.position - transform.position).normalized * 10, ForceMode2D.Impulse);

        explode = transform.Find("explode").GetComponent<ParticleSystem>();


        if(fragments)
        {
            masterAsteroid = Resources.Load("Rock") as GameObject;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void Explode()
    {
        explode.transform.SetParent(null);
        explode.Emit(30);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag=="Player")
        {
            collision.collider.GetComponent<Planet>().HurtPlanet();

            Explode();


        }

        if (collision.collider.tag == "Rocket")
        {
        
            Destroy(collision.collider.gameObject);
            if (fragments)
            {
                Instantiate(masterAsteroid, (Vector2)transform.position+(Vector2)Random.insideUnitCircle*2, Quaternion.identity);
                Instantiate(masterAsteroid, (Vector2)transform.position + (Vector2)Random.insideUnitCircle*2, Quaternion.identity);
                Instantiate(masterAsteroid, (Vector2)transform.position + (Vector2)Random.insideUnitCircle*2, Quaternion.identity);
            }

            Explode();

            

        }

        if(collision.collider.gameObject.layer==LayerMask.NameToLayer("Shield"))
        {
            collision.collider.GetComponent<Damageable>().Damage(1);
            Explode();
        }

    }


}
