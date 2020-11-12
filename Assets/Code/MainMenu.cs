using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public Score score;

    public TextMeshProUGUI scoreLB;

    public CanvasGroup fade;


    public AudioSource sfx;

    public AudioClip s1;

    // Use this for initialization
    void Start () {
        scoreLB.text = ""+score.highScore;
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        sfx = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}



   
    public void StartGame()
    {
        fade.gameObject.SetActive(true);
        StartCoroutine(LoadGame());

    }

     public void StartTutorial()
    {
        fade.gameObject.SetActive(true);
        StartCoroutine(LoadTutorial());
        

    }



    IEnumerator LoadGame()
    {
        sfx.PlayOneShot(sfx.clip);
        while (true)
        {
            fade.alpha += Time.deltaTime;

            if(fade.alpha==1)
            {
                break;
            }

            yield return new WaitForEndOfFrame();
        }


        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }


    IEnumerator LoadTutorial()
    {
       
        while (true)
        {
            fade.alpha += Time.deltaTime;

            if(fade.alpha==1)
            {
                break;
            }

            yield return new WaitForEndOfFrame();
        }


        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(2);
    }



    public void OpenTwitterPage()
    {

        Application.OpenURL("https://twitter.com/LordSigma777");

    }
}
