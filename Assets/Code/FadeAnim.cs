using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeAnim : MonoBehaviour {



    public float target = 1;
    public float speed = 1;
    CanvasGroup g;

	// Use this for initialization
	void Start () {
        g = GetComponent<CanvasGroup>();
	}
	
	// Update is called once per frame
	void Update () {
       g.alpha=Mathf.PingPong(Time.time*speed, target);
	}
}
