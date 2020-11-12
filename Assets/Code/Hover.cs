using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour {


    public float freq = 2;
    public float mag = 2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //pos += transform.up * Time.deltaTime * MoveSpeed;
        //transform.position = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude;


        //Vector2 pos=


        transform.localPosition=Vector2.up*Mathf.Sin(Time.time*freq)*mag;

        //Vector2 pos = transform.localPosition;

        //pos.y = ( Mathf.Sin(Time.deltaTime)* freq);

        //transform.localPosition = pos;



	}
}
