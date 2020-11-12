using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveTower : MonoBehaviour, TowerInterface
{

    bool fired;

    float elapsedRate;
    float rate = 2.0f;


    GameObject rocket;

    public Transform rocketRef;

    public int ammo;

    public static int maxAmmo = 20;

    Slider energy;

    public string Projectile;


    public ParticleSystem p;

    bool charge;


    public static float projSpeed = 5;
    public Button upgradeBtn;

    int towerLV = 1;

    public static float ProjSize = 1;

    public AudioClip waveSfx;

    AudioSource sfx;


    public Pool wavepool;



    // Use this for initialization
    void Start()
    {
        rocket = Resources.Load(Projectile) as GameObject;

        //rocketRef = transform.Find("RocketRef");

        energy = transform.parent.Find("Canvas").Find("Energy").GetComponent<Slider>();
        ammo = maxAmmo;


        //Planet.towersInScene.Add(this);


        sfx = GetComponent<AudioSource>();

        wavepool = GameObject.FindGameObjectWithTag("WavePool").GetComponent<Pool>();
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
                    //GameObject g = Instantiate(rocket, rocketRef.position, rocketRef.rotation);



                    Projectile proj = wavepool.GetPooledProjectile();
                    proj.transform.position = rocketRef.position;
                    proj.transform.rotation = rocketRef.rotation;
                    proj.gameObject.SetActive(true);
                    proj.Reset();
                    proj.Fire(-transform.right * projSpeed);



                    proj.transform.localScale = new Vector3(ProjSize, ProjSize, ProjSize);

                    sfx.PlayOneShot(waveSfx);

                    ////g.GetComponent<MultiRocket>().enabled = false;
                    //g.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    //g.GetComponent<Collider2D>().isTrigger = false;
                    //g.GetComponent<Rigidbody2D>().AddForce(-transform.right * 5, ForceMode2D.Impulse);
                    ////g.transform.Find("Jet").gameObject.SetActive(true);

                    //g.GetComponent<Projectile>().Fire(-transform.right * projSpeed)


                    //;
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
                //Planet.towersInScene.Remove(this);
                //Destroy(transform.parent.gameObject);

                RefillAmmo();
                transform.parent.gameObject.SetActive(false);
            }

        }

    }
    public void UpgradeWaveTower()
    {
        towerLV++;


        switch (towerLV)
        {



            case 2:
                WaveTower.ProjSize = 1.5f;
                //Debug.Log("WAVE LV 2");
                break;


            case 3:


                WaveTower.ProjSize = 2;

                //UpgBtnText.text = "Max Lv.";

                //Debug.Log("WAVE LV 3");
                upgradeBtn.interactable = false;
                break;
        }
    }


    public void UpgradeWaveAmmo()
    {
        WaveTower.maxAmmo += 5;
    }

    public void RefillAmmo()
    {
        ammo = maxAmmo;
        if (energy != null)
            energy.value = (float)ammo / (float)maxAmmo;
    }

    public static void ResetTower()
    {
        WaveTower.ProjSize = 1;
        WaveTower.maxAmmo = 20;
    }

    public void HideSelf()
    {
        transform.root.gameObject.SetActive(false);
    }
}
