using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    public GameObject[] tutPanels;

    public int index = 0;


    AudioSource sfx;
    // Use this for initialization
    void Start()
    {
        sfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void NextPage()
    {
        sfx.PlayOneShot(sfx.clip);
        index++;
        //Debug.Log("NEXTPage");
        if (index > tutPanels.Length - 1)
        {
            index = 0;

            foreach (GameObject g in tutPanels)
            {
                g.SetActive(false);
            }
            tutPanels[index].SetActive(true);
            gameObject.SetActive(false);
            //Debug.Log("HIDE");




            return;
        }

        foreach (GameObject g in tutPanels)
        {
            g.SetActive(false);
        }
        tutPanels[index].SetActive(true);

    }

    public void PreviousPage()
    {
        sfx.PlayOneShot(sfx.clip);
        index--;

        if (index < 0)
        {
            index = 0;

            foreach (GameObject g in tutPanels)
            {
                g.SetActive(false);
            }
            tutPanels[index].SetActive(true);
            gameObject.SetActive(false);
            //Debug.Log("HIDE");


            return;
        }

        foreach (GameObject g in tutPanels)
        {
            g.SetActive(false);
        }
        tutPanels[index].SetActive(true);
    }

}
