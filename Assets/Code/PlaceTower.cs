using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceTower : MonoBehaviour
{

    public GameObject tower;

    public Pool towerPool;


    public int cost = 30;

    Planet planet;

    public GameObject cancelBtn;

    public GameObject highlight;

    Pool.TowerData data;

    public AudioSource sfx;
    // Use this for initialization
    void Start()
    {
        planet = GameObject.FindGameObjectWithTag("Player").GetComponent<Planet>();
        //sfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {



        //if (TouchManager.myTouches.Length > 0)
        //{

        //    if (TouchManager.myTouches[0].phase == TouchPhase.Ended)
        //    {
        //        //Debug.Log("DEREPP");


        //        if (planet.cash >= cost)
        //        {
        //            sfx.PlayOneShot(sfx.clip);
        //            data = towerPool.GetPooledTower();

        //            data.towerRoot.transform.position = transform.root.position;
        //            data.towerRoot.transform.rotation = transform.rotation;
        //            data.towerRoot.SetActive(true);
        //            //data.towerInterface.RefillAmmo();

        //            //GameObject g = Instantiate(tower, transform.root.position, transform.rotation);

        //            //g.SetActive(true);
        //            planet.PayCash(cost);
        //            cancelBtn.SetActive(false);
        //            transform.root.gameObject.SetActive(false);

        //            highlight.SetActive(false);



        //            //Debug.Log("HovER over Ui");

        //        }
        //    }

        //}
        //if (TouchManager.myTouches[0].phase == TouchPhase.Ended)
        //{


        //    Debug.Log("DErp");
        //    //startChange = false;
        //    //touchIndex = 0;
        //}



        //if(Input.GetTouch(0).phase==TouchPhase.Ended)
        //{

        //    Debug.Log("PLACEMENT ");
        //}



        if (Input.GetMouseButtonUp(0))

        {
            if (!EventSystem.current.IsPointerOverGameObject() && planet.cash >= cost)
            {
                sfx.PlayOneShot(sfx.clip);
                data = towerPool.GetPooledTower();

                data.towerRoot.transform.position = transform.root.position;
                data.towerRoot.transform.rotation = transform.rotation;
                data.towerRoot.SetActive(true);
                //data.towerInterface.RefillAmmo();

                //GameObject g = Instantiate(tower, transform.root.position, transform.rotation);

                //g.SetActive(true);
                planet.PayCash(cost);
                cancelBtn.SetActive(false);
                transform.root.gameObject.SetActive(false);

                highlight.SetActive(false);



                //Debug.Log("HovER over Ui");

            }

        }
    }
}
