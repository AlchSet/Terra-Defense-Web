using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour {


    //public List<Touch> touches = new List<Touch>();
    public static Touch[] myTouches;

    public int T;

    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        myTouches = Input.touches;
        T = myTouches.Length;

        //Debug.Log(Input.touchCount);
        //Touch[] myTouches = Input.touches;
        //for (int i = 0; i < Input.touchCount; i++)
        //{
        //    Debug.Log(myTouches[i].fingerId);
        //    //Do something with the touches
        //}
    }
}
