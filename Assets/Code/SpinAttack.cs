using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAttack : MonoBehaviour {


    WeaponRotator rotator;
    bool spin;
    float oangle;
    float toangle;
    float t;
    public float speed = 1;

    public float rotAmount = 180;

	// Use this for initialization
	void Start () {
        rotator = GetComponent<WeaponRotator>();
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetMouseButtonDown(0)&&!spin)
        {
            rotator.enabled = false;
            spin = true;
            oangle = transform.eulerAngles.z;
            toangle = oangle - rotAmount;
        }


        if (spin)
        {
            float angle = transform.eulerAngles.z;

            angle += Time.deltaTime * 40;
            t += Time.deltaTime*speed;

            transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(oangle, toangle, t));

            if(t>=1)
            {
                spin = false;
                rotator.enabled = true;
                t = 0;
            }


            //if(angle==oangle)
            //{
            //    Debug.Log("STOP");
            //}

        }
	}
}
