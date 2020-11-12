using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject spawnOBJ;

    float time;

    public float rate = 3;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;


        if (time > rate)
        {
            time = 0;


            Vector3 pos = Random.insideUnitCircle.normalized;


            Vector2 npos = (pos * 50)+transform.position;
            //Debug.Log(pos);

            //pos = transform.position + pos;

            //Vector3 pos = new Vector3(Random.Range(-1,1), Random.Range(0.5f, 1), 0);


            //pos = Camera.main.ViewportToWorldPoint(pos);
            //pos.z = 0;
            Instantiate(spawnOBJ, npos, Quaternion.identity);
        }

      


    }
}

