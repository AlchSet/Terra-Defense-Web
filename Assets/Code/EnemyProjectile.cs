using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public Rigidbody2D rigidbody;

    AudioSource sfx;
    // Use this for initialization
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sfx = GameObject.FindGameObjectWithTag("UFOSFX").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Planet>().HurtPlanet();
            gameObject.SetActive(false);

        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Shield"))
        {
            collision.GetComponent<Damageable>().Damage(1);
            sfx.PlayOneShot(sfx.clip);
            gameObject.SetActive(false);
        }
    }


    public void ResetVel()
    {
        rigidbody.velocity = Vector2.zero;
    }
}
