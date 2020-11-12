using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateSelf : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnBecameInvisible()
    {

        StartCoroutine(Deactivate());
    }

    IEnumerator Deactivate()
    {

        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);


    }
}
