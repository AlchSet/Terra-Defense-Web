using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPlacement : MonoBehaviour
{


    public Transform planet;

    GameObject rocket;

    // Use this for initialization
    void Start()
    {
        rocket = transform.Find("Rocket").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 dif = (Vector2)planet.position - mPos;


        //float angle =Vector2.Angle(Vector2.right, dif);


        float angle = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, angle);



        if (Input.GetMouseButtonDown(0))
        {

            GameObject g=Instantiate(rocket, rocket.transform.position, rocket.transform.rotation);
            g.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            g.GetComponent<Collider2D>().isTrigger = false;
            g.GetComponent<Rigidbody2D>().AddForce(g.transform.up * 5, ForceMode2D.Impulse);
            g.transform.Find("Jet").gameObject.SetActive(true);
            
        }




    }
}
