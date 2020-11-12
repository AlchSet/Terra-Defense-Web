using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial1 : MonoBehaviour
{

    public static bool enableTut = true;

    public TextMeshProUGUI[] objects;

    bool dragcheck;

    bool dragtutfin;

    bool placedragcheck;

    bool placetutfin;

    bool checkrelease;


    AudioSource sfx;

    bool lastpart;
    // Use this for initialization
    void Start()
    {

        sfx = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {


        if (dragcheck)
        {
            if ((Input.GetTouch(0).deltaPosition).sqrMagnitude > 8)
            {
                //objects[1].SetActive(false);
                StartCoroutine(DragTutComplete());

            }

        }

        if (placedragcheck)
        {

            if(TouchManager.myTouches.Length>0)
            {
                if ((Input.GetTouch(0).phase == TouchPhase.Began) && !checkrelease)
                {

                    if (!checkrelease)
                    {
                        //Debug.Log("RELEASE");
                        objects[4].gameObject.SetActive(false);
                        objects[5].gameObject.SetActive(true);
                        checkrelease = true;
                    }







                    //objects[1].SetActive(false);
                    //StartCoroutine(DragTutComplete());

                }
                else if (checkrelease)
                {
                    if ((Input.GetTouch(0).phase == TouchPhase.Ended))
                    {
                        //Debug.Log("RELEASE");
                        StartCoroutine(PlaceWeaponComplete());

                    }
                }

            }
            

        }



    }



    public void MoveShieldTut()
    {

        if (enableTut&&!dragtutfin)
        {
            objects[1].gameObject.SetActive(false);
            objects[2].gameObject.SetActive(true);
            dragcheck = true;
        }


    }

    public void TapTut()
    {
        StartCoroutine(TapTutComplete());
    }


    IEnumerator TapTutComplete()
    {
        objects[0].text = "YES!";
        objects[0].fontSize += 20;
        objects[0].color = Color.green;
        sfx.PlayOneShot(sfx.clip);
        yield return new WaitForSeconds(3);

        objects[0].gameObject.SetActive(false);

        

    }

    IEnumerator PlaceWeaponComplete()
    {
        placedragcheck = false;
        placetutfin = true;
        objects[5].text = "YES!";
        objects[5].fontSize += 20;
        objects[5].color = Color.green;
        sfx.PlayOneShot(sfx.clip);
        yield return new WaitForSeconds(3);

        objects[5].gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        objects[6].gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        objects[6].gameObject.SetActive(false);
    }


    IEnumerator DragTutComplete()
    {
        dragtutfin = true;
           dragcheck = false;
        objects[2].text = "YES!";
        objects[2].fontSize += 20;
        objects[2].color = Color.green;
        sfx.PlayOneShot(sfx.clip);
        yield return new WaitForSeconds(3);

        objects[2].gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        objects[3].gameObject.SetActive(true);
        lastpart = true;
    }

    public void StartPlacementTut()
    {
        if(!placetutfin&&lastpart)
        {
            objects[3].gameObject.SetActive(false);
            objects[4].gameObject.SetActive(true);

            placedragcheck = true;
        }

    }


}
