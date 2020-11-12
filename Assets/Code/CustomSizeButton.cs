using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomSizeButton : MonoBehaviour {
    public Image theButton;
    // Use this for initialization
    void Start () {
        theButton = GetComponent<Image>();
        theButton.alphaHitTestMinimumThreshold =.1f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
