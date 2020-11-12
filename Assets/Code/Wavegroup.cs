using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wavegroup : Wave
{


    public Wave[] waves;


    bool check;
    float checkelapsed;

    public enum WaveGroupTypes { Simultaneous, Consecutive }

    public WaveGroupTypes grouptype = WaveGroupTypes.Simultaneous;

    int currentIndex;
    public Wave currentWave;

    public override void StartWave()
    {

        if (RandomizePos)
        {
            Vector3 pos = Random.insideUnitCircle.normalized;


            Vector2 npos = (pos * 30) + planet.position;

            transform.position = npos;
        }

        switch (grouptype)
        {
            case WaveGroupTypes.Simultaneous:
                //base.StartWave();
                foreach (Wave w in waves)
                {
                    w.StartWave();
                }
                break;


            case WaveGroupTypes.Consecutive:
                waveStart = true;
                waves[currentIndex].StartWave();
                currentWave = waves[currentIndex];
                break;
        }





    }


    public override void ResetWave()
    {
        waveEnd = false;
        currentIndex = 0;
        foreach (Wave w in waves)
        {
            w.ResetWave();
        }
    }


    private void Update()
    {

        if (!waveEnd)
        {
            switch (grouptype)
            {

                case WaveGroupTypes.Simultaneous:
                    //Check every 5 seconds if this wave has ended
                    transform.LookAt(planet, Vector3.forward);

                    //if (!waveEnd)
                    //{
                    if (!check)
                    {
                        checkelapsed += Time.deltaTime;

                        if (checkelapsed >= 5)
                        {
                            check = true;
                            checkelapsed = 0;
                        }
                    }
                    else
                    {

                        bool f = true;

                        foreach (Wave w in waves)
                        {
                            if (w.waveEnd == false)
                            {
                                f = false;
                            }
                        }

                        if (f == true)
                        {
                            waveEnd = true;
                        }


                        check = false;
                    }
                    //}

                    break;


                case WaveGroupTypes.Consecutive:

                    if(waveStart)
                    {
                        if (currentIndex >= waves.Length - 1)
                        {
                            if (currentWave.waveEnd)
                            {
                                waveEnd = true;
                            }
                        }
                        else
                        {
                            if (currentWave.waveEnd)
                            {
                                currentIndex = Mathf.Clamp(currentIndex + 1, 0, waves.Length - 1);
                                currentWave = waves[currentIndex];
                                currentWave.StartWave();
                            }



                        }
                    }
                  



                    break;




            }
        }




    }
}
