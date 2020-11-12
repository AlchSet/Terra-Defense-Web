using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceWeaponBtn : MonoBehaviour
{

    public GameObject Weapon;

    Planet planet;

    public int price;

    Button btn;

    public GameObject cancelBtn;
    GameObject highlight;
	// Use this for initialization
	void Start () {

        planet = GameObject.FindGameObjectWithTag("Player").GetComponent<Planet>();
        btn = GetComponent<Button>();

        btn.onClick.AddListener(PlaceWeapon);

        PlaceTower p = Weapon.transform.Find("PlaceModel").GetComponent<PlaceTower>();
        p.cost = price;
        highlight = transform.Find("highlight").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void PlaceWeapon()
    {
        if(planet.cash>=price)
        {
            Weapon.SetActive(true);
            cancelBtn.SetActive(true);
            highlight.SetActive(true);
        }
        else
        {
            planet.LowCash();
            
            Debug.Log("NO MONEY");
        }
    }
}
