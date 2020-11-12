using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour, TowerInterface
{



    public LineRenderer line;
    public LineRenderer line2;
    public bool laserOn;

    public Slider energySlider;

    public float power = 150;


    public static float maxPower = 150;
    bool handle;

    public BoxCollider2D box;
    public static float Size = 1;

    int towerLV = 1;
    public Button upgradeBtn;

    AudioSource sfx;

    LaserHitbox hitbox;

    // Use this for initialization
    void Awake()
    {
        line = transform.Find("Laser").GetComponent<LineRenderer>();

        line.enabled = false;

        box = transform.Find("Laser").GetComponent<BoxCollider2D>();

        //Planet.towersInScene.Add(this);

        line2 = transform.Find("Laser").Find("InnerLaser").GetComponent<LineRenderer>();
        line2.enabled = false;

        sfx = transform.Find("Laser").GetComponent<AudioSource>();
        hitbox = transform.Find("Laser").GetComponent<LaserHitbox>();
        //upgradeBtn = GameObject.FindGameObjectWithTag("LaserUpgrade").GetComponent<Button>();


    }

    // Update is called once per frame
    void Update()
    {
        hitbox.laserOn = laserOn;
        box.size = new Vector2(box.size.x, Size);
        if (laserOn)
        {


            power = Mathf.Clamp(power - 20 * Time.deltaTime, 0, maxPower);
            energySlider.value = power / maxPower;

            if (Input.GetMouseButtonUp(0) | power <= 0)
            {
                //line.enabled = false;

                //laserOn = false;
                StartCoroutine(StopLaser());

            }
        }



    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (laserOn)
        {
            if (collision.tag == "Asteroid")
            {
                if (collision.GetComponent<Hazzard>().isVisible)
                {
                    collision.GetComponent<Hazzard>().LootTable();
                    collision.GetComponent<Hazzard>().AwardEXP();
                    collision.GetComponent<Hazzard>().Explode();
                }


            }

            if (collision.tag == "UFO")
            {
                collision.GetComponent<Ufo>().LootTable();
                collision.GetComponent<Ufo>().AwardEXP();

                collision.GetComponent<Ufo>().Explode();
            }
        }
    }




    public void ActivateLaser()
    {
        StartCoroutine(StartLaser());
        //laserOn=true;
        //Debug.Log("ACT LASE\r");
        //line.enabled = true;
    }

    IEnumerator StartLaser()
    {
        handle = true;
        line.enabled = true;

        line2.enabled = true;
        sfx.Play();
        float laserSize = 0.1f;
        while (true)
        {
            laserSize = Mathf.MoveTowards(laserSize, Size, 10 * Time.deltaTime);



            line.startWidth = laserSize;
            line.endWidth = laserSize;

            line2.startWidth = Mathf.Clamp(laserSize * 0.5f, 0.1f, 10);
            line2.endWidth = Mathf.Clamp(laserSize * 0.5f, 0.1f, 10);



            if (laserSize >= Size)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        laserOn = true;
        handle = false;

    }

    IEnumerator StopLaser()
    {
        //Debug.Log("ENDLSER");
        handle = true;
        laserOn = false;
        //line.enabled = true;
        float laserSize = Size;
        while (true)
        {
            laserSize = Mathf.MoveTowards(laserSize, 0.1f, 10 * Time.deltaTime);
            line.startWidth = laserSize;
            line.endWidth = laserSize;

            line2.startWidth = Mathf.Clamp(laserSize * 0.5f, 0.1f, 10);
            line2.endWidth = Mathf.Clamp(laserSize * 0.5f, 0.1f, 10);


            if (laserSize <= 0.1f)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }

        line.enabled = false;
        line2.enabled = false;
        handle = false;

        if (power <= 0)
        {
            //Planet.towersInScene.Remove(this);
            //Destroy(transform.root.gameObject);

            RefillAmmo();
            transform.parent.gameObject.SetActive(false);
        }
        sfx.Stop();
        //Debug.Log("END LASER");
    }

    public void UpgradeLaserTower()
    {
        towerLV++;


        switch (towerLV)
        {



            case 2:
                Size = 3;
                //    MissileTower.rate = 2;
                //    MissileTower.projSpeed = 10;
                //Debug.Log("Laser LV 2");
                break;


            case 3:
                Size = 5;
                //MissileTower.rate = 1.5f;
                //MissileTower.projSpeed = 13;
                //UpgBtnText.text = "Max Lv.";

                //Debug.Log("MISSILE LV 3");
                upgradeBtn.interactable = false;
                break;
        }
    }


    public void UpgradeLaserAmmo()
    {
        Laser.maxPower += 25;
    }

    public void RefillAmmo()
    {
        power = maxPower;
        if (energySlider != null)
            energySlider.value = power / maxPower;
    }


    public static void ResetTower()
    {
        Laser.Size = 1;
        Laser.maxPower = 150;
    }

    public void HideSelf()
    {
        transform.root.gameObject.SetActive(false);
    }
}
