using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

    public int score = 0;
    public int highScore = 0;

    // Use this for initialization
    void Awake () {

        highScore = PlayerPrefs.GetInt("highScoreKey", 0);
        //PlayerPrefs.DeleteAll();

    }
	
	public void SetHighScore(int s)
    {
        highScore = s;
        PlayerPrefs.SetInt("highScoreKey", s);
        PlayerPrefs.Save();
    }
}
