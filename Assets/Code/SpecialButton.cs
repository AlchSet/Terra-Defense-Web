using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SpecialButton : MonoBehaviour, IPointerDownHandler
{

    public UnityEvent OnButtonDown;

    


    public void OnPointerDown(PointerEventData eventData)
    {

        OnButtonDown.Invoke();

        //Debug.Log(this.gameObject.name + " Was Clicked.");
    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
