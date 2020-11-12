using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponRotation : MonoBehaviour
{
    public Transform planet;


    public bool startChange;

    public MonoBehaviour sc;

    Touch touch;


    public int touchIndex;

    // Use this for initialization
    void Start()
    {
        planet = GameObject.FindWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {



        if (startChange)
        {
            //Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Debug.Log("rotating");
            //Debug.Log(TouchManager.myTouches[touchIndex]);
            try
            {

                Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //Vector2 mPos = Camera.main.ScreenToWorldPoint(TouchManager.myTouches[touchIndex].position);

                Vector2 dif = (Vector2)planet.position - mPos;


                //float angle =Vector2.Angle(Vector2.right, dif);


                float angle = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;


                //transform.eulerAngles = new Vector3(0, 0, Mathf.MoveTowardsAngle(transform.eulerAngles.z,angle,Time.deltaTime*800));

                transform.eulerAngles = new Vector3(0, 0, angle);

                //Debug.Log(TouchManager.myTouches[touchIndex].phase);


                if(Input.GetMouseButtonUp(0))
                {
                    startChange = false;
                }


                //if (TouchManager.myTouches[touchIndex].phase == TouchPhase.Ended)
                //{
                //    startChange = false;
                //    touchIndex = 0;
                //}
            }
            catch (Exception e)
            {
                startChange = false;
                touchIndex = 0;
                //Debug.Log("CANCEL ROT");
            }







            //if (Input.GetMouseButtonUp(0))
            //{
            //    startChange = false;
            //    //sc.enabled = true;
            //}


            //Debug.Log(TouchManager.myTouches[touchIndex].phase);

            //if (touch.phase == TouchPhase.Ended)
            //{
            //    startChange = false;

            //}


            //if (touch.phase == TouchPhase.Ended)
            //{
            //    startChange = false;

            //}



        }

    }


    public void ChangeWpnRotation()
    {
        //sc.enabled = false;
        startChange = true;
    }

    public void ChangeWpnRotationTouch()
    {

       
        startChange = true;
        //Debug.Log("ROT "+ TouchManager.myTouches.Length);
        if (TouchManager.myTouches.Length > 0)
            //touch = TouchManager.myTouches[Input.touchCount - 1];
            touchIndex =Mathf.Clamp(Input.touchCount - 1,0,10);
    }


}
