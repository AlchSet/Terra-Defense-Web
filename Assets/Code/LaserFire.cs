using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFire : MonoBehaviour {

    bool isFiring;

    float elapsedTime;
    public float waitTime=3 ;


    public float laserLength = 50;

    LineRenderer line;

    

	// Use this for initialization
	void Start () {

        line = GetComponent<LineRenderer>();
        line.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
		

        if(Input.GetMouseButtonDown(0)&&!isFiring)
        {


            StartCoroutine(FireLaser());

        }


	}



    IEnumerator FireLaser()
    {
        line.enabled = true;
        //Debug.Log("FIRE");
        isFiring = true;
        float laserSize = 0.01f;
        line.startWidth = laserSize;
        line.endWidth = laserSize;

        float lasertime = 0;

        float ll = 10;

        RaycastHit2D[] hit;


        //Switch on
        while(true)
        {
            laserSize = Mathf.MoveTowards(laserSize, 0.5f, ll * Time.deltaTime);
            line.startWidth = laserSize;
            line.endWidth = laserSize;

            if (laserSize >= 0.5f)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }

        //While On

        while (true)
        {
            hit = Physics2D.LinecastAll(transform.position, -transform.right * laserLength);


            foreach (RaycastHit2D d in hit)
            {
                if (d.collider.tag == "Asteroid")
                {

                    d.collider.GetComponent<Asteroid>().Explode();
                }
            }

            lasertime += Time.deltaTime;


            if(lasertime>=waitTime)
            {
                break;
            }


            yield return new WaitForEndOfFrame();
        }


        //Switch Off
        while (true)
        {
            laserSize = Mathf.MoveTowards(laserSize, 0, ll * Time.deltaTime);
            line.startWidth = laserSize;
            line.endWidth = laserSize;

            if (laserSize == 0)
            {
                break;
            }

            yield return new WaitForEndOfFrame();

        }


        //while (true)
        //{
        //    //Debug.Log(laserSize);
        //    laserSize = Mathf.MoveTowards(laserSize, 0.5f, 1.5f * Time.deltaTime);
        //    line.startWidth = laserSize;
        //    line.endWidth = laserSize;

        //    if(laserSize>0.2f)
        //    {

        //        hit=Physics2D.LinecastAll(transform.position, -transform.right * 100);


        //        foreach(RaycastHit2D d in hit)
        //        {
        //            if(d.collider.tag=="Asteroid")
        //            {

        //                Destroy(d.collider.gameObject);
        //            }
        //        }

        //        //Debug.Log(hit.Length);


        //    }



        //    if(laserSize>=0.5f)
        //    {
        //        break;
        //    }

        //    yield return new WaitForEndOfFrame();

        //}

        //while (true)
        //{
        //    laserSize = Mathf.MoveTowards(laserSize, 0, 1.5f * Time.deltaTime);
        //    line.startWidth = laserSize;
        //    line.endWidth = laserSize;

        //    if (laserSize < 0.7f)
        //    {

        //        hit = Physics2D.LinecastAll(transform.position, -transform.right * 100);


        //        foreach (RaycastHit2D d in hit)
        //        {
        //            if (d.collider.tag == "Asteroid")
        //            {

        //                Destroy(d.collider.gameObject);
        //            }
        //        }

        //        //Debug.Log(hit.Length);


        //    }


        //    if (laserSize == 0)
        //    {
        //        break;
        //    }

        //    yield return new WaitForEndOfFrame();

        //}







        line.enabled = false;
        yield return new WaitForSeconds(0.4f);
        isFiring = false;
        yield return null;
    }
}
