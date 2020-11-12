using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazzard : MonoBehaviour
{
    ParticleSystem explode;
    ParticleSystem flame;

    public GameObject loot;
    public GameObject loot2;
    public GameObject loot3;
    public GameObject loot4;

    public GameObject loot5;
    public GameObject loot6;



    GameObject[] loottable;

    public Pool[] lootPool;


    int[] items = { 80, 30, 15, 5, 1, 1, 1 };

    [Range(0f, 1f)]
    public float dropChance;

    Planet p;

    Collider2D collider;
    Rigidbody2D rigidbody;
    GameObject mesh;

    bool isDead;
    TrailRenderer line;


    bool appeared;

    AudioSource sfx;
    public bool isVisible;

    int immunelayer;
    int normallayer;

    // Use this for initialization
    void Awake()
    {

        collider = GetComponent<Collider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        explode = transform.Find("explode").GetComponent<ParticleSystem>();
        flame = transform.Find("Commet").GetComponent<ParticleSystem>();
        loottable = new GameObject[7];
        mesh = transform.Find("Mesh").gameObject;
        loottable[0] = null;
        loottable[1] = loot;
        loottable[2] = loot2;
        loottable[3] = loot3;
        loottable[4] = loot4;
        loottable[5] = loot5;
        loottable[6] = loot6;


        lootPool = new Pool[7];

        lootPool[0] = null;
        lootPool[1] = GameObject.FindWithTag("GemGreen").GetComponent<Pool>();
        lootPool[2] = GameObject.FindWithTag("GemRed").GetComponent<Pool>();
        lootPool[3] = GameObject.FindWithTag("GemPurple").GetComponent<Pool>();
        lootPool[4] = GameObject.FindWithTag("ShieldItem").GetComponent<Pool>();
        lootPool[5] = GameObject.FindWithTag("AmmoItem").GetComponent<Pool>();
        lootPool[6] = GameObject.FindWithTag("HealItem").GetComponent<Pool>();




        sfx = GetComponent<AudioSource>();

        p = GameObject.FindGameObjectWithTag("Player").GetComponent<Planet>();
        line = GetComponent<TrailRenderer>();


        immunelayer = LayerMask.NameToLayer("TransparentFX");
        normallayer = LayerMask.NameToLayer("Default");
        //explode.gameObject.SetActive(false);
        //loottable[3] = loot4;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Explode()
    {
        //explode.transform.SetParent(null);
        //explode.gameObject.SetActive(true);
        explode.transform.localPosition = Vector3.zero;
        explode.Emit(30);

        collider.enabled = false;
        rigidbody.bodyType = RigidbodyType2D.Static;
        mesh.SetActive(false);
        isDead = true;
        flame.Stop();
        sfx.PlayOneShot(sfx.clip);
        StartCoroutine(Cleaner());
        //Destroy(gameObject);
    }


    void LootChance()
    {
        if (Random.Range(0f, 1f) <= dropChance)
        {

            //float r=Random.Range(0f, 1f);


            Instantiate(loot, transform.position, Quaternion.identity);





        }

        //float r = Random.Range(0f, 1f);
        //Debug.Log(r);
        //if(r<=.3f&&r>.15f)
        //{
        //    Instantiate(loot, transform.position, Quaternion.identity);
        //}
        //else if(r <= .15f&&r>.05f)
        //{
        //    Instantiate(loot2, transform.position, Quaternion.identity);
        //}
        //else if(r <= .05f)
        //{
        //    Instantiate(loot3, transform.position, Quaternion.identity);
        //}


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

        if (selected != 0)
        {

            GameObject l = lootPool[selected].GetPooledCollectable();

            Debug.Log("LOOT at " + transform.position);
            l.transform.position = transform.position;

            l.SetActive(true);

            //Instantiate(loottable[selected], transform.position, Quaternion.identity);
        }

        //}



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (!isDead)
        {
            if (collision.collider.tag == "Player")
            {
                collision.collider.GetComponent<Planet>().HurtPlanet();

                Explode();


            }

            if (collision.collider.tag == "Rocket")
            {

                //Destroy(collision.collider.gameObject);
                if (isVisible)
                {
                    collision.collider.gameObject.SetActive(false);


                    p.AddEXP(10);

                    //if (fragments)
                    //{
                    //    Instantiate(masterAsteroid, (Vector2)transform.position + (Vector2)Random.insideUnitCircle * 2, Quaternion.identity);
                    //    Instantiate(masterAsteroid, (Vector2)transform.position + (Vector2)Random.insideUnitCircle * 2, Quaternion.identity);
                    //    Instantiate(masterAsteroid, (Vector2)transform.position + (Vector2)Random.insideUnitCircle * 2, Quaternion.identity);
                    //}
                    //LootChance();
                    LootTable();
                    Explode();
                }




            }






            if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Shield"))
            {
                p.AddEXP(5);
                collision.collider.GetComponent<Damageable>().Damage(1);
                //LootChance();
                LootTable();
                Explode();
            }
        }
    }

    public void AwardEXP()
    {
        p.AddEXP(10);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead)
        {
            if (collision.tag == "Wave")
            {

                if (isVisible)
                {
                    p.AddEXP(10);
                    //Destroy(collision.collider.gameObject);
                    //if (fragments)
                    //{
                    //    Instantiate(masterAsteroid, (Vector2)transform.position + (Vector2)Random.insideUnitCircle * 2, Quaternion.identity);
                    //    Instantiate(masterAsteroid, (Vector2)transform.position + (Vector2)Random.insideUnitCircle * 2, Quaternion.identity);
                    //    Instantiate(masterAsteroid, (Vector2)transform.position + (Vector2)Random.insideUnitCircle * 2, Quaternion.identity);
                    //}
                    //LootChance();
                    LootTable();
                    Explode();


                }




            }
        }

    }



    public void Reset()
    {
        isDead = false;
        collider.enabled = false;
        rigidbody.bodyType = RigidbodyType2D.Static;
        mesh.SetActive(false);
        line.enabled = false;

        flame.Stop();
        appeared = false;
    }

    public void Begin(Vector2 pos)
    {
        //line.enabled = false;
        transform.position = pos;
        //line.enabled = true;
        collider.enabled = true;
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        rigidbody.velocity = Vector2.zero;
        mesh.SetActive(true);
        line.enabled = true;
        //isDead = true;
        flame.Play();

        gameObject.layer = immunelayer;
    }


    public void SetSize(Vector3 size)
    {
        transform.localScale = size;
    }
    public void Fire(Vector2 dir)
    {

        rigidbody.AddForce(dir, ForceMode2D.Impulse);
    }

    IEnumerator Cleaner()
    {
        yield return new WaitForSecondsRealtime(4);
        //explode.gameObject.SetActive(false);
        Reset();
    }



    private void OnBecameInvisible()
    {
        isVisible = false;
        
        //Debug.Log("A");
    }

    private void OnBecameVisible()
    {
        gameObject.layer = normallayer;
        isVisible = true;
        //Debug.Log("B");
    }



    //private void OnBecameInvisible()
    //{
    //    if (appeared)
    //    {
    //        StartCoroutine(Cleaner());
    //        Debug.Log("CLEANUP");
    //    }
    //}

    //private void OnBecameVisible()
    //{
    //    appeared = true;
    //    //Debug.Log("BECAME VIS");
    //}



}
