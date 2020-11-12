using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHitbox : MonoBehaviour {


    public bool laserOn;

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
}
