using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBarColorGradient : MonoBehaviour {


    public Gradient colorGrad;
    public Image image;
    Slider slider;
	// Use this for initialization
	void Start () {
        slider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void UpdateImageColor()
    {
        image.color = colorGrad.Evaluate(slider.value);
    }
}
