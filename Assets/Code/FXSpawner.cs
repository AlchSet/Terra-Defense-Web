using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXSpawner : MonoBehaviour {


    public GameObject fx;
    float elapsedTime;
    public float waitTime = 10.5f;
    bool fire;

    [Range(0, 30)]
    public float spawnRange;

    List<GameObject> pool = new List<GameObject>();
    int index;

    // Use this for initialization
    void Start () {
		

        for(int i=0;i<10;i++)
        {
            GameObject g = Instantiate(fx);
            g.SetActive(false);
            pool.Add(g);


        }

	}
	
	// Update is called once per frame
	void Update () {
		
        if(!fire)
        {
            float offset = Random.Range(-spawnRange, spawnRange);
            //GameObject g = Instantiate(fx, transform.position + transform.right * offset, Quaternion.identity);
            GameObject g = pool[index];

            g.transform.position = transform.position + transform.right * offset;
            g.SetActive(true);


            g.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            g.GetComponent<Rigidbody2D>().AddForce(-transform.up * 150, ForceMode2D.Impulse);

            index = (index + 1) % pool.ToArray().Length;

            fire = true;

        }
        else
        {
            elapsedTime += Time.deltaTime;

            if(elapsedTime>= waitTime)
            {
                elapsedTime = 0;
                fire = false;
            }

        }

	}


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position + transform.right * -spawnRange, transform.position + transform.right * spawnRange);
    }
}
