using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpEffect : MonoBehaviour {


    Planet planet;
    Text text;

    Vector2 oPos;


    public Gradient colorLoop;


    CanvasGroup group;

	// Use this for initialization
	void Start () {
        planet = GameObject.FindGameObjectWithTag("Player").GetComponent<Planet>();

        text = transform.Find("Text").GetComponent<Text>();

        oPos = text.rectTransform.anchoredPosition;

        group = GetComponent<CanvasGroup>();

        planet.OnLevelUp += StartLevelUpEffect;
	}
	

    public void StartLevelUpEffect()
    {
        //Debug.Log("LEVEL UPP!!!");
        StartCoroutine(LevelUpFX());
    }
	

    IEnumerator LevelUpFX()
    {
        Vector2 endPos = oPos+Vector2.up*150;
        float i = 0;
        StartCoroutine(LoopColors());
        while(true)
        {
            i += Time.unscaledDeltaTime;
         

            text.rectTransform.anchoredPosition = Vector2.Lerp(oPos, endPos, i);
           
            if(i>=1)
            {
                break;
            }

            yield return new WaitForSecondsRealtime(0.01f);
        }
        yield return new WaitForSecondsRealtime(0.80f);
        
        while (true)
        {
            i -= Time.unscaledDeltaTime;

            group.alpha = i;

            if (i<= 0)
            {
                break;
            }

            yield return new WaitForSecondsRealtime(0.01f);
        }
        StopCoroutine(LoopColors());
        text.rectTransform.anchoredPosition = oPos;
        group.alpha = 1;
        yield return null;
    }

    IEnumerator LoopColors()
    {
        float c = 0;

        while(true)
        {
            c = (c + Time.unscaledDeltaTime * 3) % 1;

            text.color = colorLoop.Evaluate(c);

            yield return new WaitForSecondsRealtime(0.01f);
        }

      
    }

}
