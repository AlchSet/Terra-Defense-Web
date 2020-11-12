using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : MonoBehaviour
{

    public float speed = 3;

    Rigidbody2D rigidbody;

    int lives = 1;
    public GameObject ufofire;

    float elapsedTime;

    bool fire = true;
    Transform planet;
    Planet p;

    public GameObject Loot1;
    public GameObject Loot2;
    public GameObject Loot3;
    public GameObject Loot4;
    public GameObject Loot5;
    public GameObject Loot6;

    int[] items = { 40, 40, 40, 30, 25 };


    GameObject[] loottable;


    public Pool[] poolLoot;



    bool unlock1;
    bool unlock2;

    Vector2 dir = Vector2.right;

    ParticleSystem explode;

    GameObject model;
    Collider2D box;

    AudioSource sfx;
    AudioSource gunsfx;

    public AudioClip deathSFX;

    bool isDead;


    public Pool firepool;

    Pool laserUpg;

    // Use this for initialization
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        box = GetComponent<Collider2D>();
        explode = transform.Find("explode").GetComponent<ParticleSystem>();
        model = transform.Find("Model").gameObject;
        planet = GameObject.FindGameObjectWithTag("Player").transform;
        p = planet.GetComponent<Planet>();

        loottable = new GameObject[5];

        loottable[0] = Loot1;
        loottable[1] = Loot2;
        loottable[2] = Loot3;
        loottable[3] = Loot4;
        loottable[4] = Loot5;

        poolLoot = new Pool[5];

        poolLoot[0] = GameObject.FindWithTag("GemPurple").GetComponent<Pool>();
        poolLoot[1] = GameObject.FindWithTag("ShieldItem").GetComponent<Pool>();
        poolLoot[2] = GameObject.FindWithTag("HealItem").GetComponent<Pool>();
        poolLoot[3] = GameObject.FindWithTag("AmmoItem").GetComponent<Pool>();
        poolLoot[4] = GameObject.FindWithTag("WaveUpgrade").GetComponent<Pool>();

        laserUpg = GameObject.FindWithTag("LaserUpgrade").GetComponent<Pool>();



        sfx = GetComponent<AudioSource>();
        gunsfx = transform.Find("GunSound").GetComponent<AudioSource>();
        gameObject.SetActive(false);
    }

    //private void OnBecameInvisible()
    //{
    //    speed = speed * -1;
    //    Debug.Log("CHANGE");
    //}

    // Update is called once per frame
    void Update()
    {
        float d = Vector2.Distance(transform.position, planet.position);



        if (d < 33 && !isDead)
        {
            if (!fire)
            {
                //GameObject g = Instantiate(ufofire, transform.position, Quaternion.identity);


                EnemyProjectile g = firepool.GetPooledEnemyProjectile();

                g.transform.position = transform.position;

                g.gameObject.SetActive(true);

                g.ResetVel();



                Vector2 dir = planet.position - transform.position;

                g.rigidbody.AddForce(dir.normalized * Mathf.Clamp((4 + Mathf.Pow(1.017f, WaveManager.WavesSurvived)), 0, 20), ForceMode2D.Impulse);
                //g.GetComponent<Rigidbody2D>().AddForce(dir * 1, ForceMode2D.Impulse);


                fire = true;
                gunsfx.PlayOneShot(gunsfx.clip);
            }
            else
            {
                elapsedTime += Time.deltaTime;

                if (elapsedTime >= 5)
                {
                    fire = false;
                    elapsedTime = 0;
                }
            }
        }




    }


    private void FixedUpdate()
    {
        rigidbody.velocity = dir * speed;
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Rocket")
        {
            lives--;
            //Destroy(collision.collider.gameObject);

            collision.collider.gameObject.SetActive(false);
            if (lives <= 0)
            {
                //p.AddEXP(20);
                p.AddProportionEXP(0.25f);
                LootTable();
                //Destroy(gameObject);
                Explode();
            }
            //Destroy(gameObject);

        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wave")
        {
            lives--;
            Destroy(collision.gameObject);
            if (lives <= 0)
            {
                //p.AddEXP(20);
                p.AddProportionEXP(0.25f);
                LootTable();
                Explode();
                //Destroy(gameObject);
            }
            //Destroy(gameObject);

        }
        //    if (collision.tag == "LaserWPN")
        //    {
        //        lives--;
        //        Destroy(collision.gameObject);
        //        if (lives <= 0)
        //        {
        //            //p.AddEXP(20);
        //            p.AddProportionEXP(0.25f);
        //            LootTable();
        //            Destroy(gameObject);
        //        }
        //        //Destroy(gameObject);

        //    }
    }

    public void LootTable()
    {


        //if (Random.Range(0f, 1f) <= dropChance)
        //{
        int range = 0;

        for (int i = 0; i < items.Length; i++)
        {
            range += items[i];
        }

        int rand = Random.Range(0, range);

        int top = 0;

        int selected = 0;
        for (int i = 0; i < items.Length; i++)
        {
            top += items[i];
            if (rand < top)
            {
                selected = i;
                break;
            }
        }


        if (selected == 4)
        {
            //Debug.Log("UNLOCK WPN");

            if (!unlock1 && !unlock2)
            {
                float r = Random.Range(0f, 1f);

                if (r > 0.5f)
                {
                    //Debug.Log("Unlock Wave");
                    //Instantiate(Loot4, transform.position, Quaternion.identity);

                    GameObject g = poolLoot[4].GetPooledCollectable();

                    g.transform.position = transform.position;
                    g.SetActive(true);

                    unlock1 = true;
                }
                else if (r < 0.5f)
                {
                    //Debug.Log("Unlock Laser");

                    GameObject g = laserUpg.GetPooledCollectable();

                    g.transform.position = transform.position;
                    g.SetActive(true);
                    //Instantiate(Loot5, transform.position, Quaternion.identity);
                    unlock2 = true;
                }



            }
            else if (unlock1 && !unlock2)
            {
                //Debug.Log("Unlock Laser");
                //Instantiate(Loot5, transform.position, Quaternion.identity);

                GameObject g = laserUpg.GetPooledCollectable();

                g.transform.position = transform.position;

                g.SetActive(true);
                unlock2 = true;
            }
            else if (!unlock1 && unlock2)
            {
                //Debug.Log("Unlock Wave ," + unlock1 + "," + unlock2);
                //Instantiate(Loot4, transform.position, Quaternion.identity);


                GameObject g = poolLoot[4].GetPooledCollectable();

                g.transform.position = transform.position;
                g.SetActive(true);
                unlock1 = true;
            }
            else if (unlock1 && unlock2)
            {
                //Instantiate(Loot1, transform.position, Quaternion.identity);


                int s = Random.Range((int)0, (int)3);

                GameObject g = poolLoot[s].GetPooledCollectable();

                g.transform.position = transform.position;

                g.SetActive(true);
            }


        }
        else
        {
            //Instantiate(loottable[selected], transform.position, Quaternion.identity);

            GameObject g = poolLoot[selected].GetPooledCollectable();

            g.transform.position = transform.position;
            g.SetActive(true);
        }

        //if (selected != 0)
        //{
        //    Instantiate(loottable[selected], transform.position, Quaternion.identity);
        //}

        //}



    }

    public void AwardEXP()
    {
        p.AddProportionEXP(0.25f);
    }

    public void Explode()
    {
        //Destroy(gameObject);
        StartCoroutine(DestructionSequence());
    }

    IEnumerator DestructionSequence()
    {
        model.SetActive(false);
        box.enabled = false;
        explode.Emit(50);
        sfx.Stop();
        gunsfx.PlayOneShot(deathSFX);
        isDead = true;
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);

        //Destroy(gameObject);
    }


    public void Revitalize()
    {
        isDead = false;
        gameObject.SetActive(true);
        sfx.Play();
        model.SetActive(true);
        box.enabled = true;

    }

    public void SetRandomStartPoint()
    {

        int m = Random.Range(1, 8);

        //Debug.Log("SPAWN " + m);



        switch (m)
        {

            case 1:
                Vector2 pos1 = new Vector2(-40, 17);
                transform.position = pos1;
                dir = Vector2.right;
                transform.rotation = Quaternion.identity;

                break;

            case 2:
                Vector2 pos2 = new Vector2(-30, -17);
                transform.position = pos2;
                dir = Vector2.right;
                transform.rotation = Quaternion.identity;


                break;
            case 3:


                transform.position = new Vector2(40, -18);
                dir = Vector2.left;
                transform.rotation = Quaternion.identity;

                break;
            case 4:
                transform.position = new Vector2(40, 22);
                dir = Vector2.left;
                transform.rotation = Quaternion.identity;
                break;
            case 5:
                transform.position = new Vector2(19, -32);
                transform.eulerAngles = new Vector3(0, 0, -90);
                dir = Vector2.up;

                break;
            case 6:

                transform.position = new Vector2(-19, -32);
                transform.eulerAngles = new Vector3(0, 0, -90);
                dir = Vector2.up;
                break;
            case 7:
                transform.position = new Vector2(-19, 32);
                transform.eulerAngles = new Vector3(0, 0, -90);
                dir = Vector2.down;

                break;
            case 8:
                transform.position = new Vector2(19, 32);
                transform.eulerAngles = new Vector3(0, 0, -90);
                dir = Vector2.down;

                break;

        }


    }
}
