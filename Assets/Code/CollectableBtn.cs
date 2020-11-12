using System.Collections;
using System.Collections.Generic;
//using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CollectableBtn : MonoBehaviour
{


    public enum CollectTypes { Cash, Shield, Heal, Ammo, Wave, Laser }

    public CollectTypes collectType;
    Planet planet;

    Button btn;

    public float amount = 10;

    public Text text;

    public GameObject icon;
    Image img;
    Color oColor;
    Color toColor;

    public Color gColor;

    public Transform grandparent;


    public UnityEvent OnClick2;
    // Use this for initialization
    void Start()
    {
        planet = GameObject.FindGameObjectWithTag("Player").GetComponent<Planet>();
        btn = GetComponent<Button>();
        btn.onClick.AddListener(CollectCash);
        text = transform.parent.Find("Text").GetComponent<Text>();
        img = GetComponent<Image>();

        oColor = img.color;
        toColor = oColor;

        toColor.a = .4F;
        grandparent = transform.parent.parent;
    }

    private void Update()
    {
        grandparent.position = Vector3.MoveTowards(grandparent.position, planet.transform.position, Time.deltaTime * 0.25f);

        if (collectType != CollectTypes.Cash)
        {
            img.color = Color.Lerp(oColor, toColor, Mathf.PingPong(Time.time * 2, 1));
        }else
        {
            img.color = Color.Lerp(oColor, gColor, Mathf.PingPong(Time.time * 2, 1));
        }
    }

    public void CollectCash()
    {
        
        switch (collectType)
        {
            case CollectTypes.Cash:
                planet.CollectCash((int)amount);
                //GameObject.Destroy(gameObject);
                break;


            case CollectTypes.Shield:
                planet.CollectShield();
                //GameObject.Destroy(gameObject);
                break;

            case CollectTypes.Heal:

                planet.HealPlanet(amount);
                //GameObject.Destroy(gameObject);

                break;

            case CollectTypes.Ammo:

                planet.RefillTowerAmmo();
                //GameObject.Destroy(gameObject);
                break;



            case CollectTypes.Wave:

                planet.UnlockWaveWPN();
                //GameObject.Destroy(gameObject);
                break;



            case CollectTypes.Laser:

                planet.UnlockLaserWPN();
                //GameObject.Destroy(gameObject);
                break;



        }
        StartCoroutine(EndSequence());

    }

    IEnumerator EndSequence()
    {

        float t = .1f;
        btn.enabled = false;
        btn.image.enabled = false;
        text.gameObject.SetActive(true);
        if (icon)
        {
            icon.SetActive(false);
        }
        yield return new WaitForSeconds(t);
        text.color = Color.red;
        yield return new WaitForSeconds(t);
        text.color = Color.yellow;
        yield return new WaitForSeconds(t);
        text.color = Color.white;
        yield return new WaitForSeconds(t);
        text.color = Color.red;
        yield return new WaitForSeconds(t);
        text.color = Color.yellow;
        yield return new WaitForSeconds(t);
        text.color = Color.white;
        yield return new WaitForSeconds(t);
        text.color = Color.red;
        yield return new WaitForSeconds(t);
        text.color = Color.yellow;
        yield return new WaitForSeconds(t);
        text.color = Color.white;
        yield return new WaitForSeconds(t);
        text.color = Color.red;
        yield return new WaitForSeconds(t);


        grandparent.gameObject.SetActive(false);

        //Cleanup


        btn.enabled = true;
        btn.image.enabled = true;
        text.gameObject.SetActive(false);
        if (icon)
        {
            icon.SetActive(true);
        }


        //Destroy(transform.root.gameObject);
    }

}
