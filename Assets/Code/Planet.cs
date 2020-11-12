using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{


    public AudioMixerSnapshot unpausedSound;
    public AudioMixerSnapshot pausedSound;

    public CinemachineVirtualCamera shake;
    public Slider healthslider;
    public Slider energyslider;

    public Slider XPSlider;
    public Slider XPFXSlider;

    public Renderer ShieldBar;

    public float health = 10;

    public int cash = 100;

    float energy;

    ParticleSystem explode;

    public float energyrate = 2;

    public TextMeshProUGUI cashLb;
    public TextMeshProUGUI levelLb;



    public float EXP;
    public float EXPtoLV = 100;
    public int Level = 1;

    AudioSource sfx;
    AudioSource sfx2;

    public Gradient cashFlash;
    public Gradient cashFlash2;

    public Button shieldBtn;

    Image shieldBtnImg;

    bool rechargeShield;

    ChangeColour planetColor;

    public AudioClip[] sounds;


    public GameObject UpgradePanel;


    public Damageable shieldHealth;

    public GameObject shieldOBJECT;


    public GameObject waveWPN;
    public GameObject laserWPN;


    public GameObject GameOverScreen;
    CanvasGroup gameovercgroup;
    Renderer mesh;

    public GameObject brokenPlanet;


    bool gameover;


    public Score score;


    public TextMeshProUGUI waveScoreLB;
    public TextMeshProUGUI highScoreLB;


    public bool Immunity;



    public UnlockWpnManager unlockmanager;



    public static List<TowerInterface> towersInScene = new List<TowerInterface>();


    public int towersNO;


    public GameObject PauseScreen;

    [Range(0, 100)]
    public int testVar;



    public ParticleSystem atmosphere;
    public SpriteRenderer skyImg;

    public ParticleSystem explodePlanet;


    public ParticleSystem ShieldIsFullEffect;

    public GameObject ShieldFullBlinker;


    [Range(0f, 1f)]
    public float clip;

    public delegate void Action();

    public Action OnLevelUp;


    public Action OnGameOver;



    public CanvasGroup quitT;


    public Pool MissileTowerPool;
    public Pool WaveTowerPool;
    public Pool LaserTowerPool;


    Renderer earthMesh;


    Collider2D earthCol;

    public static bool unlockLaser;
    public static bool unlockWave;


    // Use this for initialization
    void Start()
    {
        earthMesh = transform.Find("Earth").GetComponent<MeshRenderer>();
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        explode = transform.Find("explode").GetComponent<ParticleSystem>();

        cashLb.text = "" + cash;

        sfx = GetComponent<AudioSource>();
        sfx2 = transform.Find("Source2").GetComponent<AudioSource>();

        //planetColor = GetComponent<ChangeColour>();
        planetColor = earthMesh.transform.GetComponent<ChangeColour>();

        mesh = GetComponent<Renderer>();

        brokenPlanet = transform.Find("BrokenSphere").gameObject;

        gameovercgroup = GameOverScreen.GetComponent<CanvasGroup>();

        shieldBtnImg = shieldBtn.GetComponent<Image>();

        earthCol = GetComponent<Collider2D>();
        //waveWPN = GameObject.FindGameObjectWithTag("WaveWPN");
        //laserWPN = GameObject.FindGameObjectWithTag("LaserWPN");
    }

    // Update is called once per frame
    void Update()
    {



        //cash = Mathf.MoveTowards(cash, 100, Time.deltaTime * energyrate);
        //energyslider.value = cash / 100;


        //Debug.Log(4 + Mathf.Log(testVar, 2));
        //Debug.Log(4 + Mathf.Log(2,testVar));
        //Debug.Log(4 + (Mathf.Log(2, testVar)/1000));

        //Debug.Log(4 + (Mathf.Log(testVar)*1.5f));

        //Debug.Log(Mathf.Pow(1.039f, testVar) * 4); //THIS ONE


        //Debug.Log(Mathf.Pow(1.039f, testVar) *4); //THIS ONE


        //Debug.Log(Mathf.Log(1.039f, testVar));


        towersNO = towersInScene.ToArray().Length;

        //Debug.Log(Mathf.Pow(1.5f, Level) * 100);


        //Recharge shield if down

        if (rechargeShield)
        {
            energy += Time.deltaTime * energyrate;

            energyslider.value = energy / 100;
            ShieldBar.material.SetFloat("_ClipValue", 1.0f - energyslider.value);




            if (energy >= 100)
            {
                shieldBtn.interactable = true;
                shieldBtnImg.raycastTarget = true;
                rechargeShield = false;
                sfx.PlayOneShot(sounds[3]);
                ShieldIsFullEffect.Play();
                ShieldFullBlinker.SetActive(true);
            }

        }

        //Heal when not in wave

        if (!WaveManager.inWave)
        {

            health = Mathf.Clamp(health + Time.deltaTime * 0.1f, 0, 10);
            healthslider.value = (float)health / 10;
        }




    }


    public void HurtPlanet()
    {
        StartCoroutine(Shake());

        if (!Immunity)
        {
            health = Mathf.Clamp(health - 1, 0, 10);
            healthslider.value = (float)health / 10;

            planetColor.ChangeColourDMG(healthslider.value);


        }


        if (health <= 0)
        {
            if (!gameover)
            {
                explode.Emit(100);
                GameOverScreen.SetActive(true);
                mesh.enabled = false;
                earthMesh.enabled = false;
                brokenPlanet.SetActive(true);

                StartCoroutine(ShowGameOver());

                earthCol.enabled = false;
            }

        }

    }


    IEnumerator Shake()
    {
        shake.Priority = 100;
        sfx.PlayOneShot(sounds[2]);
        yield return new WaitForSeconds(0.25f);
        shake.Priority = 0;


    }

    public void CollectCash(int amount)
    {
        cash += amount;
        cashLb.text = "" + cash;
        sfx.PlayOneShot(sounds[0]);
        StartCoroutine(CashYellowFlash());

    }


    public void CollectShield()
    {
        sfx.PlayOneShot(sounds[6]);
        if (rechargeShield)
        {
            energy = 100;

            energyslider.value = energy / 100;

            shieldBtn.interactable = true;
            rechargeShield = false;
            shieldBtnImg.raycastTarget = true;
            ShieldBar.material.SetFloat("_ClipValue", 1.0f - energyslider.value);
            ShieldIsFullEffect.Play();
            ShieldFullBlinker.SetActive(true);
        }
        else
        {
            shieldHealth.FullHeal();
            ShieldIsFullEffect.Play();


        }


        //shieldBtn.SetActive(true);
        //shieldBtn.interactable = true;
    }


    public void ActivateShield()
    {
        energy = 0;
        energyslider.value = energy / 100;
        shieldBtn.interactable = false;
        ShieldBar.material.SetFloat("_ClipValue", 1.0f - energyslider.value);
        sfx.PlayOneShot(sounds[4]);
    }
    public void PayCash(int amount)
    {
        cash -= amount;
        cashLb.text = "" + cash;
    }


    public void RechargeShield()
    {
        rechargeShield = true;
    }

    public void HealPlanet(float h)
    {
        sfx.PlayOneShot(sounds[7]);
        health = Mathf.Clamp(health + h, 0, 10);
        healthslider.value = (float)health / 10;
    }



    public void RefillTowerAmmo()
    {
        sfx.PlayOneShot(sounds[8]);
        //foreach (TowerInterface t in towersInScene)
        //{
        //    t.RefillAmmo();
        //}



        for (int i = 0; i < MissileTowerPool.towerInterfaceList.ToArray().Length; i++)
        {
            MissileTowerPool.towerInterfaceList[i].RefillAmmo();
        }



        //try
        //{
        foreach (TowerInterface t in MissileTowerPool.towerInterfaceList)
        {

            t.RefillAmmo();
            //try
            //{
            //    t.RefillAmmo();
            //}
            //catch (Exception e)
            //{

            //}
        }
        //}
        //catch (Exception e) { }

        //try
        //{
        foreach (TowerInterface t in WaveTowerPool.towerInterfaceList)
        {
            t.RefillAmmo();
            //try
            //{
            //    t.RefillAmmo();
            //}
            //catch (Exception e)
            //{

            //}
        }
        //}
        //catch (Exception e) { }


        //try
        //{

        foreach (TowerInterface t in LaserTowerPool.towerInterfaceList)
        {
            //try
            //{
            t.RefillAmmo();
            //}
            //catch (Exception e)
            //{

            //}
        }

    //}
    //catch (Exception e) { }
}


    public void VanishTowers()
    {
        foreach (TowerInterface t in towersInScene)
        {
            t.HideSelf();
        }
    }


    public void LowCash()
    {
        StartCoroutine(CashRedFlash());
        PlayErrorSound();
    }


    public void AddEXP(float xp)
    {
        if (!gameover)
        {
            EXP += xp;

            //StopCoroutine(UpdateXPBar());
            //StartCoroutine(UpdateXPBar());

            //XPSlider.value = EXP / EXPtoLV;

            if (EXP >= EXPtoLV)
            {
                Level++;
                EXP = 0;
                levelLb.text = "" + Level;
                //XPSlider.value = 0;
                sfx.PlayOneShot(sounds[1]);


                EXPtoLV = Mathf.Pow(1.5f, Level) * 100;

                StartCoroutine(ShowUpgradePanel());
                OnLevelUp.Invoke();
            }

            StopCoroutine(UpdateXPBar());
            StartCoroutine(UpdateXPBar());

        }
    }


    public void AddProportionEXP(float portion)
    {

        if (!gameover)

        {
            float p = EXPtoLV * Mathf.Clamp01(portion);

            EXP += p;

            if (EXP >= EXPtoLV)
            {
                Level++;
                EXP = 0;
                levelLb.text = "" + Level;
                //XPSlider.value = 0;
                sfx.PlayOneShot(sounds[1]);


                EXPtoLV = Mathf.Pow(1.5f, Level) * 100;

                StartCoroutine(ShowUpgradePanel());
                OnLevelUp.Invoke();
            }

            StopCoroutine(UpdateXPBar());
            StartCoroutine(UpdateXPBar());
        }
    }


    IEnumerator CashRedFlash()
    {
        float ti = 0;
        float sp = 1.5f;
        while (true)
        {
            ti += Time.deltaTime * sp;
            cashLb.color = cashFlash.Evaluate(ti);

            if (ti >= 1)
            {
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
    IEnumerator CashYellowFlash()
    {
        float ti = 0;
        float sp = 1.5f;
        while (true)
        {
            ti += Time.deltaTime * sp;
            cashLb.color = cashFlash2.Evaluate(ti);

            if (ti >= 1)
            {
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }


    IEnumerator ShowUpgradePanel()
    {

        pausedSound.TransitionTo(0.1f);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.85f);

        UpgradePanel.SetActive(true);


        CanvasGroup g = UpgradePanel.GetComponent<CanvasGroup>();
        pausedSound.TransitionTo(0f);
        while (true)
        {
            g.alpha += Time.unscaledDeltaTime * 2;

            if (g.alpha >= 1)
            {
                break;
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }




        yield return null;
    }



    public void Resume()
    {
        CanvasGroup g = UpgradePanel.GetComponent<CanvasGroup>();
        g.alpha = 0;
        UpgradePanel.SetActive(false);
        Time.timeScale = 1;
        unpausedSound.TransitionTo(0.1f);
    }

    public void UnlockWaveWPN()
    {
        waveWPN.SetActive(true);
        unlockmanager.UnlockWaveUpgrades();
        sfx.PlayOneShot(sounds[5]);
    }

    public void UnlockLaserWPN()
    {

        laserWPN.SetActive(true);
        unlockmanager.UnlockLaserUpgrades();
        sfx.PlayOneShot(sounds[5]);
    }


    public void ReloadGame()

    {
        MissileTower.ResetTower();
        WaveTower.ResetTower();
        Laser.ResetTower();
        SceneManager.LoadScene(1);
        WaveManager.WavesSurvived = -1;
        unlockWave = false;
        unlockLaser = false;
    }

    public void PlayErrorSound()
    {
        sfx2.PlayOneShot(sounds[10]);
    }


    public void BackToMainMenu()
    {
        StartCoroutine(QuitGame());
    }


    IEnumerator UpdateXPBar()
    {
        if (EXP > 0)
        {

            XPFXSlider.value = EXP / EXPtoLV;

            float target = XPFXSlider.value;


            while (true)
            {
                XPSlider.value = Mathf.MoveTowards(XPSlider.value, target, Time.unscaledDeltaTime * .5f);


                if (XPSlider.value >= target)
                {
                    break;
                }


                yield return new WaitForSecondsRealtime(0.01f);
            }




        }
        else
        {
            XPSlider.value = 0;

            while (true)
            {
                XPFXSlider.value = Mathf.MoveTowards(XPFXSlider.value, 0, Time.unscaledDeltaTime * .5f);

                if (XPFXSlider.value <= 0)
                {
                    break;
                }

                yield return new WaitForSecondsRealtime(0.01f);
            }



        }

    }


    IEnumerator ShowGameOver()
    {

        OnGameOver.Invoke();
        atmosphere.Stop();
        skyImg.enabled = false;
        explodePlanet.Play();
        sfx.PlayOneShot(sounds[9]);
        VanishTowers();
        shieldOBJECT.SetActive(false);

        gameover = true;
        waveScoreLB.text = "" + WaveManager.WavesSurvived;

        if (WaveManager.WavesSurvived > score.highScore)
        {
            score.SetHighScore(WaveManager.WavesSurvived);
            StartCoroutine(NewHighScore());
        }
        highScoreLB.text = "" + score.highScore;
        while (true)
        {
            gameovercgroup.alpha += Time.deltaTime * 0.5f;


            yield return new WaitForEndOfFrame();
        }



    }


    IEnumerator NewHighScore()
    {


        Color[] c = new Color[2];

        c[0] = Color.white;
        c[1] = Color.yellow;

        int index = 0;

        while (true)
        {
            highScoreLB.color = c[index];
            index = (index + 1) % c.Length;
            yield return new WaitForSecondsRealtime(0.3f);
        }

    }


    IEnumerator QuitGame()
    {

        quitT.gameObject.SetActive(true);

        while (true)
        {
            quitT.alpha += Time.deltaTime * 2.5f;

            if (quitT.alpha >= 1)
            {
                break;
            }


            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene(0);
        yield return null;
    }

    public void ShowPauseMenu()
    {
        Time.timeScale = 0;
        pausedSound.TransitionTo(0.1f);
        PauseScreen.SetActive(true);
    }

    public void HidePauseMenu()
    {
        Time.timeScale = 1;
        unpausedSound.TransitionTo(0.1f);
        PauseScreen.SetActive(false);
    }

}
