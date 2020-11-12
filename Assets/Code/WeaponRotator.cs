using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotator : MonoBehaviour {
    public Transform planet;


   
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 dif = (Vector2)planet.position - mPos;


        //float angle =Vector2.Angle(Vector2.right, dif);


        float angle = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;


        //transform.eulerAngles = new Vector3(0, 0, Mathf.MoveTowardsAngle(transform.eulerAngles.z,angle,Time.deltaTime*800));

        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
