using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    public static bool inWave = false;

    public List<Wave> waves = new List<Wave>();


    public int wavIndex = 0;

    Wave currentWave;
    bool handle;

    public float delay = 10;

    public TextMeshProUGUI timeLb;

    public TextMeshProUGUI waveLb;

    public static int WavesSurvived=-1;

    AudioSource countBeep;


    public int minIndex = 0;
    public int maxIndex = 9;


    Planet planet;


    // Use this for initialization
    void Start()
    {
        countBeep = GetComponent<AudioSource>();

        foreach (Transform t in transform)
        {
            waves.Add(t.GetComponent<Wave>());
        }
        timeLb.gameObject.SetActive(false);
        StartCoroutine(Begining());

        planet = GameObject.FindGameObjectWithTag("Player").GetComponent<Planet>();

        planet.OnGameOver += StopWave;
        //currentWave = waves[wavIndex];
        //currentWave.StartWave();

        //wavIndex++;

    }

    // Update is called once per frame
    void Update()
    {



        if (!handle)
        {
            if (currentWave.waveEnd)
            {
                if (!handle)
                    StartCoroutine(WaveDelay());
            }
        }


        //if (!handle)
        //{
        //    if (currentWave.waveEnd && wavIndex < waves.ToArray().Length)
        //    {
        //        if (!handle)
        //            StartCoroutine(WaveDelay());
        //    }
        //}

        //if (currentWave != null)
        //    if (currentWave.waveEnd && wavIndex >= waves.ToArray().Length)
        //    {
        //        inWave = false;
        //    }

        if(WavesSurvived>20)
        {
            minIndex = 10;
            maxIndex = 20;
        }

    }


    public IEnumerator WaveDelay()
    {
        

        inWave = false;
        handle = true;
        yield return new WaitForSeconds(3);
        float timepassed = 0;
        timeLb.gameObject.SetActive(true);
        WavesSurvived++;

        waveLb.text = "" + WavesSurvived;

        if(currentWave!=null)
        {
            currentWave.ResetWave();
        }

        bool beeped=false;
        float beeptime = 0;
        countBeep.PlayOneShot(countBeep.clip);

        while (true)
        {
           
           


            timepassed += Time.deltaTime;
            beeptime += Time.deltaTime;

            if (beeptime > 1)
            {
                countBeep.PlayOneShot(countBeep.clip);
                beeptime = 0;
            }

            int t = (int)delay - (int)timepassed;

            timeLb.text = "" + t;
            if (timepassed >= delay)
            {
                timeLb.gameObject.SetActive(false);
                break;
            }
            






            //if (!beeped)
            //{
            //    countBeep.PlayOneShot(countBeep.clip);
            //    beeped = true;
            //    i = t;
            //}
            //else
            //{

            //}

            yield return new WaitForEndOfFrame();
        }


        //yield return new WaitForSeconds(delay);
        currentWave = waves[wavIndex];
        currentWave.StartWave();
        inWave = true;
        //wavIndex++;

        //wavIndex = (wavIndex + 1) % waves.ToArray().Length;

        NextWave();




        handle = false;
    }



    public IEnumerator Begining()
    {
        handle = true;
        yield return new WaitForSeconds(3);

        StartCoroutine(WaveDelay());

    }




    public void NextWave()
    {
        wavIndex++;

        if (wavIndex > maxIndex)
            wavIndex = minIndex;
    }

    public void StopWave()
    {
        this.enabled = false;
    }
}
