using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wave : MonoBehaviour
{
    public Transform planet;

    public GameObject hazzard;

    public Pool HazzardPool;


    [Range(0, 10)]
    public float yOffset = 2;

    bool fire;
    float elapsed;

    GameObject danger;
    public Image Indicator;

    public int spawn = 20;

    public int baseSpawn = 10;


    public float spawnTimeDelay = 2;

    int currentSpawn = 0;


    public float spawnDistance = 35;

    float elapsedTime;
    float waitTime = 3.5f;

    Transform masterUi;

    public bool waveStart;
    public bool waveEnd;

    bool blink;

    AudioSource beep;

    bool handle;
    public float endDelay = 5;


    public Vector2 speedVariance = new Vector2(8, 20);


    public Vector2 sizeVariance = new Vector2(1.5f, 4);


    public bool SpawnUFO;

    public int ufoSpawn;

    public GameObject ufo;

    public Ufo UFO_script;


    public bool RandomizePos;


    public int dangerBlinkTimes = 14;


  

    // Use this for initialization
    void Start()
    {
        planet = GameObject.FindGameObjectWithTag("Player").transform;
        masterUi = GameObject.FindGameObjectWithTag("MainUI").transform;

        beep = GetComponent<AudioSource>();


        transform.LookAt(planet);

        danger = Resources.Load("DangerSign") as GameObject;


        GameObject g = Instantiate(danger);
        g.transform.SetParent(masterUi, false);



        Indicator = g.GetComponent<Image>();

        Indicator.rectTransform.anchoredPosition = Vector2.zero;

        Indicator.enabled = false;

        //ufo=FindObjectOfType
        //ufo = Resources.Load("Ufo") as GameObject;
        //ufo = GameObject.FindGameObjectWithTag("UFO");
        //UFO_script = ufo.GetComponent<Ufo>();
        //ufo.SetActive(false);



        //var fooGroup = Resources.FindObjectsOfTypeAll<Ufo>();
        //if (fooGroup.Length > 0)
        //{
        //    UFO_script = fooGroup[0];
        //    ufo = UFO_script.gameObject;


        //}







        if (SpawnUFO)
        {

            //ufoSpawn = Random.Range((int)3, (int)spawn - 3);
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(planet);



        if (waveStart)
        {
            if (elapsedTime < waitTime)
            {
                //Indicator.enabled = true;

                if (!blink)
                {
                    StartCoroutine(DangerAnimationTimed());
                }

                elapsedTime += Time.deltaTime;


                //Vector2 npos = Camera.main.WorldToScreenPoint(transform.position);

                //Vector2 sc = new Vector2(Screen.width / 2, Screen.width / 2);

                //Vector2 fpos = npos;

                //fpos.x = Mathf.Clamp(fpos.x, 100, Screen.width - 100);
                //fpos.y = Mathf.Clamp(fpos.y, 100, Screen.height - 100);


                //Indicator.rectTransform.position = fpos;


            }
            else
            {
                //Indicator.enabled = false;
                if (currentSpawn < spawn)
                {

                    if (!fire)
                    {
                        float yoffset = Random.Range(-yOffset, yOffset);

                        ////GameObject g = Instantiate(hazzard, transform.position + transform.up * yoffset, Quaternion.identity);
                        //float size=Random.Range(1.51915f, 4f);
                        float size = Random.Range(sizeVariance.x, sizeVariance.y);

                        //g.transform.localScale = new Vector3(size, size, size);
                        //g.GetComponent<Rigidbody2D>().AddForce(transform.forward * Random.Range(speedVariance.x, speedVariance.y), ForceMode2D.Impulse);


                        Hazzard h = HazzardPool.GetPooledObject();

                        h.Begin(transform.position + transform.up * yoffset);
                        h.SetSize(new Vector3(size, size, size));
                        h.Fire(transform.forward * Random.Range(speedVariance.x, speedVariance.y));

                        fire = true;


                        if (currentSpawn == ufoSpawn && SpawnUFO)
                        {
                            //Spawn uFO
                            //Debug.Log("SPAWN UFO");
                            //GameObject u = Instantiate(ufo, new Vector2(-27, 17), Quaternion.identity);

                            //u.GetComponent<Ufo>().SetRandomStartPoint();

                            UFO_script.Revitalize();
                            UFO_script.SetRandomStartPoint();

                        }

                        currentSpawn++;
                    }
                    else
                    {
                        elapsed += Time.deltaTime;

                        if (elapsed >= spawnTimeDelay)
                        {
                            elapsed = 0;
                            fire = false;
                        }
                    }
                }
                else
                {

                    if (!handle)
                    {
                        StartCoroutine(DelayEnd());
                    }
                    //END OF WAVE
                    //waveEnd = true;


                }
            }

        }





        //Indicator




        //Vector2 d = transform.position - planet.position;
        //Indicator.rectTransform.localPosition = Vector2.zero;
        //Indicator.rectTransform.localPosition = (Vector2)Indicator.rectTransform.localPosition + d.normalized * 300;

        ////Debug.Log(Indicator.transform.position);

        //Debug.Log(Camera.main.WorldToScreenPoint(transform.position));




    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + transform.up * -yOffset, transform.position + transform.up * yOffset);
    }

    public virtual void StartWave()
    {
        waveStart = true;
        DifficultyFormula();
        if (SpawnUFO)
        {

            ufoSpawn = Random.Range((int)3, (int)spawn - 3);
        }
        //spawn = 10 * (int)Mathf.Ceil(Mathf.Pow(0.6f, WaveManager.WavesSurvived));
        ////Debug.Log(Mathf.Ceil(Mathf.Pow(0.6f, WaveManager.WavesSurvived)));
        //speedVariance.x=4* Mathf.Pow(0.6f, WaveManager.WavesSurvived);
        //speedVariance.y = 6* Mathf.Pow(1.6f, WaveManager.WavesSurvived);
        //sizeVariance.x=4 / Mathf.Pow(0.5f, WaveManager.WavesSurvived);
        //sizeVariance.y = 2 / Mathf.Pow(0.5f, WaveManager.WavesSurvived);

        if (RandomizePos)
        {
            Vector3 pos = Random.insideUnitCircle.normalized;


            Vector2 nnpos = (pos * spawnDistance) + planet.position;

            transform.position = nnpos;
        }


        Vector2 npos = Camera.main.WorldToScreenPoint(transform.position);

        Vector2 sc = new Vector2(Screen.width / 2, Screen.width / 2);

        Vector2 fpos = npos;

        fpos.x = Mathf.Clamp(fpos.x, 100, Screen.width - 100);
        fpos.y = Mathf.Clamp(fpos.y, 100, Screen.height - 100);


        Indicator.rectTransform.position = fpos;

    }

    public virtual void StartWave(bool b)
    {
        waveStart = true;


        DifficultyFormula();
        if (SpawnUFO)
        {

            ufoSpawn = Random.Range((int)3, (int)spawn - 3);
        }
        //sizeVariance.x=4 / Mathf.Pow(0.5f, WaveManager.WavesSurvived);
        //sizeVariance.y = 2 / Mathf.Pow(0.5f, WaveManager.WavesSurvived);

        //if (RandomizePos)
        //{
        //    Vector3 pos = Random.insideUnitCircle.normalized;


        //    Vector2 npos = (pos * 30) + planet.position;

        //    transform.position = npos;
        //}

        Vector2 npos = Camera.main.WorldToScreenPoint(transform.position);

        Vector2 sc = new Vector2(Screen.width / 2, Screen.width / 2);

        Vector2 fpos = npos;

        fpos.x = Mathf.Clamp(fpos.x, 100, Screen.width - 100);
        fpos.y = Mathf.Clamp(fpos.y, 100, Screen.height - 100);


        Indicator.rectTransform.position = fpos;
    }

    public virtual void ResetWave()
    {
        waveStart = false;
        waveEnd = false;
        fire = false;
        currentSpawn = 0;
        blink = false;
        elapsedTime = 0;
        elapsed = 0;
        handle = false;
    }

    IEnumerator DelayEnd()
    {
        handle = true;
        yield return new WaitForSeconds(endDelay);
        waveEnd = true;
    }

    IEnumerator DangerAnimation()
    {
        blink = true;
        Indicator.enabled = true;
        beep.PlayOneShot(beep.clip);
        yield return new WaitForSeconds(0.1f);
        Indicator.enabled = false;
        yield return new WaitForSeconds(0.1f);
        beep.PlayOneShot(beep.clip);
        Indicator.enabled = true;
        yield return new WaitForSeconds(0.1f);
        Indicator.enabled = false;
        beep.PlayOneShot(beep.clip);
        yield return new WaitForSeconds(0.1f);
        Indicator.enabled = true;
        yield return new WaitForSeconds(0.1f);
        beep.PlayOneShot(beep.clip);
        Indicator.enabled = false;
        yield return new WaitForSeconds(0.1f);
        Indicator.enabled = true;
        yield return new WaitForSeconds(0.1f);
        beep.PlayOneShot(beep.clip);
        Indicator.enabled = false;
        yield return new WaitForSeconds(0.1f);
        Indicator.enabled = true;
        yield return new WaitForSeconds(0.1f);
        beep.PlayOneShot(beep.clip);
        Indicator.enabled = false;
        yield return new WaitForSeconds(0.1f);
        Indicator.enabled = true;
        yield return new WaitForSeconds(0.1f);
        beep.PlayOneShot(beep.clip);
        Indicator.enabled = false;
        yield return new WaitForSeconds(0.1f);
        Indicator.enabled = true;
        yield return new WaitForSeconds(0.1f);
        beep.PlayOneShot(beep.clip);
        Indicator.enabled = false;
        yield return new WaitForSeconds(0.1f);
        Indicator.enabled = true;
        yield return new WaitForSeconds(0.1f);
        beep.PlayOneShot(beep.clip);
        Indicator.enabled = false;
        yield return new WaitForSeconds(0.1f);
        Indicator.enabled = true;
        yield return new WaitForSeconds(0.1f);
        beep.PlayOneShot(beep.clip);
        Indicator.enabled = false;
        yield return new WaitForSeconds(0.1f);
        Indicator.enabled = true;
        yield return new WaitForSeconds(0.1f);
        beep.PlayOneShot(beep.clip);
        Indicator.enabled = false;
        yield return new WaitForSeconds(0.1f);
        Indicator.enabled = true;
        yield return new WaitForSeconds(0.1f);
        beep.PlayOneShot(beep.clip);
        Indicator.enabled = false;
        yield return new WaitForSeconds(0.1f);
        Indicator.enabled = true;
        yield return new WaitForSeconds(0.1f);
        beep.PlayOneShot(beep.clip);
        Indicator.enabled = false;
        yield return new WaitForSeconds(0.1f);
        Indicator.enabled = true;
        yield return new WaitForSeconds(0.1f);
        beep.PlayOneShot(beep.clip);
        Indicator.enabled = false;
        yield return new WaitForSeconds(0.1f);
        Indicator.enabled = true;
        yield return new WaitForSeconds(0.1f);
        Indicator.enabled = false;
    }


    IEnumerator DangerAnimationTimed()
    {
        blink = true;
        bool state = false;
        int times = 0;
        while(true)
        {
            if(state)
            {
                Indicator.enabled = true;
                beep.PlayOneShot(beep.clip);
                times++;
            }
            else
            {
                beep.PlayOneShot(beep.clip);
                Indicator.enabled = false;
            }

            

            if (times >= dangerBlinkTimes)
                break;
            state = !state;
            yield return new WaitForSeconds(0.1f);

           
        }
        Indicator.enabled = false;


    }



    public void DifficultyFormula()
    {
        //spawn = 10 * (int)Mathf.Ceil(Mathf.Pow(0.6f, WaveManager.WavesSurvived));
        ////Debug.Log(Mathf.Ceil(Mathf.Pow(0.6f, WaveManager.WavesSurvived)));
        //speedVariance.x = 4 * Mathf.Pow(0.6f, WaveManager.WavesSurvived);
        //speedVariance.y = 6 * Mathf.Pow(1.6f, WaveManager.WavesSurvived);

        spawn = baseSpawn + (int)(Mathf.Log(Mathf.Clamp(WaveManager.WavesSurvived, 1, float.PositiveInfinity), 2));
        //speedVariance.x = 4 + (Mathf.Log(Mathf.Clamp(WaveManager.WavesSurvived, 1, float.PositiveInfinity), 2));
        //speedVariance.y = 6 + (Mathf.Log(Mathf.Clamp(WaveManager.WavesSurvived, 1, float.PositiveInfinity), 2));


        // Debug.Log(Mathf.Pow(1.039f, testVar) * 4);

        speedVariance.x = Mathf.Clamp(Mathf.Pow(1.039f, WaveManager.WavesSurvived) * 3,0,40);
        speedVariance.y = Mathf.Clamp(Mathf.Pow(1.049f, WaveManager.WavesSurvived) * 4,0,40);

        //spawnTimeDelay = Mathf.Pow(1.019f, WaveManager.WavesSurvived) * 2;
        //Debug.Log(Mathf.Log(Mathf.Clamp(WaveManager.WavesSurvived, 1, float.PositiveInfinity), 2));


    }
}
