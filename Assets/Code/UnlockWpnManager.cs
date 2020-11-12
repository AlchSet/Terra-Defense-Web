using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockWpnManager : MonoBehaviour {


    public Image waveIcon;
    public Image laserIcon;

    public GameObject waveUpgrades;
    public GameObject laserUpgrades;



    public void UnlockWaveUpgrades()
    {
        //waveIcon.color = Color.green;
        waveIcon.gameObject.SetActive(true);
        waveUpgrades.SetActive(true);
    }

    public void UnlockLaserUpgrades()
    {
        //laserIcon.color = Color.red;
        laserIcon.gameObject.SetActive(true);
        laserUpgrades.SetActive(true);
    }


}
