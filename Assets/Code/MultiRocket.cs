using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiRocket : MonoBehaviour {

    public enum FireType { Single,Multi}

    public FireType fireType;

    public float duration=2;
    float elapsed;
    float rate=0.1f;

    float elapsedRate;

    bool isFire;

    bool fired;

    Planet planet;

    public float energyCost = 10;

	// Use this for initialization
	void Start () {

        planet = GameObject.FindGameObjectWithTag("Player").GetComponent<Planet>();
    }
	
	// Update is called once per frame
	void Update () {
		


        switch(fireType)
        {


            case FireType.Single:


                if (Input.GetMouseButtonDown(0) && !isFire&&planet.cash>20)
                {

                    GameObject g = Instantiate(gameObject, transform.position, transform.rotation);
                    g.GetComponent<MultiRocket>().enabled = false;
                    g.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    g.GetComponent<Collider2D>().isTrigger = false;
                    g.GetComponent<Rigidbody2D>().AddForce(g.transform.up * 5, ForceMode2D.Impulse);
                    g.transform.Find("Jet").gameObject.SetActive(true);
                 
                    isFire = true;
                    //planet.cash = Mathf.Clamp(planet.cash - energyCost, 0, 100);
                }

                if(isFire)
                {
                    elapsed += Time.deltaTime;

                    if(elapsed>=0.25f)
                    {
                        isFire=false;
                        elapsed = 0;
                    }

                }


                break;


            case FireType.Multi:


                if (Input.GetMouseButtonDown(0) && !isFire)
                {


                    isFire = true;
                }


                if (isFire)
                {
                    if (!fired)
                    {
                        GameObject g = Instantiate(gameObject, transform.position, transform.rotation);
                        g.GetComponent<MultiRocket>().enabled = false;
                        g.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        g.GetComponent<Collider2D>().isTrigger = false;
                        g.GetComponent<Rigidbody2D>().AddForce(g.transform.up * 5, ForceMode2D.Impulse);
                        g.transform.Find("Jet").gameObject.SetActive(true);
                        fired = true;
                    }
                    else
                    {
                        elapsedRate += Time.deltaTime;

                        if (elapsedRate >= rate)
                        {
                            fired = false;
                            elapsedRate = 0;
                        }
                    }

                    elapsed += Time.deltaTime;


                    if (elapsed >= duration)
                    {
                        isFire = false;
                        elapsed = 0;
                    }
                }
                break;
        }

	}
}
