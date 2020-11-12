using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MissileTower : MonoBehaviour, TowerInterface
{


    bool fired;

    float elapsedRate;
    public static float rate = 2.5f;


    GameObject rocket;

    public Transform rocketRef;





    public int ammo;

    public static int maxAmmo = 20;

    Slider energy;

    public string Projectile;


    public ParticleSystem p;

    bool charge;


    public static float projSpeed = 7;


    int towerLV = 1;

    public Button upgradeBtn;
    Text UpgBtnText;

    public AudioClip missileSfx;

    AudioSource sfx;

    public Pool rocketpool;



    //private void Awake()
    //{
    //    Debug.Log("TESTTT");

    //    UpgBtnText = upgradeBtn.transform.Find("Text").GetComponent<Text>();

    //    Debug.Log(UpgBtnText.text);
    //}
    //private void OnLevelLoaded()
    //{

    //}

    // Use this for initialization
    void Start()
    {
        rocket = Resources.Load(Projectile) as GameObject;

        //rocketRef = transform.Find("RocketRef");

        energy = transform.parent.Find("Canvas").Find("Energy").GetComponent<Slider>();
        ammo = maxAmmo;


        //Planet.towersInScene.Add(this);

        sfx = GetComponent<AudioSource>();

        //upgradeBtn = GameObject.FindGameObjectWithTag("MissileUpgrade").GetComponent<Button>();
        rocketpool = GameObject.FindWithTag("RocketPool").GetComponent<Pool>();
    }

    // Update is called once per frame
    void Update()
    {



        if (WaveManager.inWave)
        {

            if (ammo > 0)
            {
                if (!fired)
                {

                    sfx.PlayOneShot(missileSfx);


                    //GameObject g = Instantiate(rocket, rocketRef.position, rocketRef.rotation);
                    Projectile rocket = rocketpool.GetPooledProjectile();
                    rocket.transform.position = rocketRef.position;
                    rocket.transform.rotation = rocketRef.rotation;
                    rocket.gameObject.SetActive(true);
                    rocket.Reset();
                    rocket.Fire(-transform.right * projSpeed);

                    ////g.GetComponent<MultiRocket>().enabled = false;
                    //g.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    //g.GetComponent<Collider2D>().isTrigger = false;
                    //g.GetComponent<Rigidbody2D>().AddForce(-transform.right * 5, ForceMode2D.Impulse);
                    ////g.transform.Find("Jet").gameObject.SetActive(true);




                    //g.GetComponent<Projectile>().Fire(-transform.right * projSpeed);



                    charge = false;
                    fired = true;
                    ammo--;
                    energy.value = (float)ammo / (float)maxAmmo;
                    p.Stop();
                }
                else
                {
                    if (!charge)
                    {
                        p.Play();
                        charge = true;
                    }
                    elapsedRate += Time.deltaTime;

                    if (elapsedRate >= rate)
                    {
                        fired = false;
                        elapsedRate = 0;
                    }
                }
            }
            else
            {
                RefillAmmo();
                transform.parent.gameObject.SetActive(false);
                //Planet.towersInScene.Remove(this);
                //Destroy(transform.parent.gameObject);
            }

        }

    }


    public void UpgradeMissileTower()
    {
        towerLV++;


        switch (towerLV)
        {



            case 2:
                MissileTower.rate = 2;
                MissileTower.projSpeed = 10;
                //Debug.Log("MISSILE LV 2");
                break;


            case 3:

                MissileTower.rate = 1.5f;
                MissileTower.projSpeed = 13;
                //UpgBtnText.text = "Max Lv.";

                //Debug.Log("MISSILE LV 3");
                upgradeBtn.interactable = false;
                break;
        }
    }


    public void UpgradeMissileAmmo()
    {
        MissileTower.maxAmmo += 5;
    }

    public void RefillAmmo()
    {
        ammo = maxAmmo;
            if (energy != null)
            energy.value = (float)ammo / (float)maxAmmo;
    }

    public static void ResetTower()
    {
        MissileTower.maxAmmo = 20;
        MissileTower.projSpeed = 7;
    }

    public void HideSelf()
    {
        transform.root.gameObject.SetActive(false);
    }
}
