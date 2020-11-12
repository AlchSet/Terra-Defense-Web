using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBounds : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Asteroid")
        {
            collision.GetComponent<Hazzard>().Reset();
        }
    }
}
