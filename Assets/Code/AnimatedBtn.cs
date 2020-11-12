using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnimatedBtn : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{



    Animator anim;
    Button btn;

  
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();

        //btn.on
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        anim.SetBool("isTouching", true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        anim.SetBool("isTouching", false);
    }

}
