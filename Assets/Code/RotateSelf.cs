using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelf : MonoBehaviour {
    public float speed = 3;
    public Vector3 rotVec = Vector3.up;
    public bool delay;
    bool wait;
    float elapsedTime;

    Vector3 lastDir;

    // Use this for initialization
    void Start() {
        lastDir = transform.forward;
    }

    // Update is called once per frame
    void Update() {

        if (!delay)
        {
            transform.Rotate(rotVec * speed * Time.deltaTime);


        } else
        {
            if(!wait)
            {
                transform.Rotate(rotVec * speed * Time.deltaTime);


                //Debug.Log(Vector3.Angle(lastDir, transform.forward));

                Debug.Log(Vector3.Cross(lastDir, transform.forward).normalized);

                if(transform.eulerAngles.y>=360)
                {
                    wait = true;
                    StartCoroutine(WaitDelay());
                }
            }
            
        }



    }


    public void InvertSpeed()
    {
        speed = speed * -1;
    }

    IEnumerator WaitDelay()
    {
        yield return new WaitForSecondsRealtime(3);
        wait = false;
        transform.eulerAngles = Vector3.zero;
    }
}
