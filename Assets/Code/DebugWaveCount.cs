using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugWaveCount : MonoBehaviour {

    Slider slider;

    int min = 0;
    int max = 200;


    public WaveManager waveman;
	// Use this for initialization
	void Start () {

        slider = GetComponent<Slider>();

        slider.onValueChanged.AddListener(delegate { SetWaveCounter(); });
	}

    // Update is called once per frame
    void Update()
    {


    }


    public void SetWaveCounter()
    {
        WaveManager.WavesSurvived = (int)Mathf.Lerp(min,max,slider.value);
        waveman.waveLb.text = ""+WaveManager.WavesSurvived;
    }
}
