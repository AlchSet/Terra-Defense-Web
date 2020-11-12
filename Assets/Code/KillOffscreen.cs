using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOffscreen : MonoBehaviour {


    private void OnBecameInvisible()
    {
        //Debug.Log("Kill");
        Destroy(transform.root.gameObject);
    }
}
