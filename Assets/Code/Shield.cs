using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour {


    int shieldLV=1;

    Damageable d;
    public Button upgradeBtn;
    private void Awake()
    {
        d = GetComponent<Damageable>();
    }


    public void UpgradeShield()
    {
        shieldLV++;
        switch (shieldLV)
        {



            case 2:
                //WaveTower.ProjSize = 1.5f;

                Vector3 newSize = new Vector3(0.8f, 0.8f, 0.8f);

                transform.localScale = newSize;


                //Debug.Log("Shield LV 2");
                break;


            case 3:


              

                transform.localScale = Vector3.one;


                //Debug.Log("Shield LV 3");
                upgradeBtn.interactable = false;
                break;
        }
    }

    public void IncreaseHealth()
    {
        d.ModifyMaxHealth(10);
    }



}
