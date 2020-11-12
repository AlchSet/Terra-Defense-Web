using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class NumbersTEST : MonoBehaviour {


    [Range(0,150)]
    public float wave;


    [Range(0.0f,10.0f)]
    public float val;

    [Range(0.0f, 10.0f)]
    public float baseVal;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(Mathf.Pow(val, wave) * baseVal); //THIS ONE


        //Debug.Log(Mathf.Pow(val, wave));

        //Debug.Log(Mathf.Log(Mathf.Clamp(wave, 1, float.PositiveInfinity), val)+ baseVal);


        //Debug.Log(Mathf.Log(wave, val));




        //spawn = baseSpawn + (int)(Mathf.Log(Mathf.Clamp(WaveManager.WavesSurvived, 1, float.PositiveInfinity), 2));
    }
}
