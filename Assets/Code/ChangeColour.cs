using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColour : MonoBehaviour {


    public enum ChangeColourType { Mesh,Sprite}

    public ChangeColourType type;
    public Gradient colordmg;

    Renderer rend;

    public SpriteRenderer sprite;

    public string Keyword;

	// Use this for initialization
	void Start () {

        switch(type)
        {
            case ChangeColourType.Mesh:
                rend = GetComponent<Renderer>();
                break;

            case ChangeColourType.Sprite:

                sprite = GetComponent<SpriteRenderer>();

                break;

        }
      


        //rend.material.SetColor("_EmissionColor", Color.black);

	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void ChangeColourDMG(Damageable d)
    {
        float i = (float)d.health / (float)d.maxHealth;
        rend.material.SetColor(Keyword, colordmg.Evaluate(i));

    }


    public void ChangeSpriteColourDMG(Damageable d)
    {
        float i = (float)d.health / (float)d.maxHealth;
        //rend.material.SetColor(Keyword, colordmg.Evaluate(i));
        sprite.color = colordmg.Evaluate(i);

    }


    public void ChangeColourDMG(float percent)
    {
        //float i = (float)d.health / (float)d.maxHealth;
        rend.material.SetColor(Keyword, colordmg.Evaluate(percent));

    }

}
