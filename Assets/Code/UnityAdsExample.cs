using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsExample : MonoBehaviour
{


    public GameObject[] rewards;

    public GameObject gem;
    public int[] rates;

    AudioSource sfx;
    //---------- ONLY NECESSARY FOR ASSET PACKAGE INTEGRATION: ----------//
#if UNITY_IOS
    private string gameId = "2811071";
#elif UNITY_ANDROID
    private string gameId = "2811072";
#endif
    //-------------------------------------------------------------------//


    public string placementId = "rewardedVideo";

    public GameObject wait;

    bool unlocklaser;
    bool unlockwave;

    private void Start()
    {
        sfx = GetComponent<AudioSource>();


        //---------- ONLY NECESSARY FOR ASSET PACKAGE INTEGRATION: ----------//
        //if (Advertisement.isSupported)
        //{
        //    //Advertisement.Initialize(gameId, true);
        //}
        //-------------------------------------------------------------------//


    }

    public void ShowRewardedAd()
    {
        //var options = new ShowOptions { resultCallback = HandleShowResult };
        //Advertisement.Show(gameId, options);
        //wait.SetActive(enabled);
        //if (Advertisement.IsReady(placementId))
        //{
        //    var options = new ShowOptions { resultCallback = HandleShowResult };
        //    Advertisement.Show(placementId, options);

        //    //Advertisement.Show()
        //}
    }

    //private void HandleShowResult(ShowResult result)
    //{
    //    //switch (result)
    //    //{
    //    //    case ShowResult.Finished:
    //    //        //Debug.Log("The ad was successfully shown.");

    //    //        int r = Random.Range((int)0, (int)rewards.Length-1);
    //    //        Vector3 pos = Random.insideUnitCircle.normalized;


    //    //        Debug.Log(r+"/"+(rewards.Length - 1));

    //    //        Vector2 npos = (pos * 10) + Vector3.zero;

    //    //        if(r== rewards.Length - 2)
    //    //        {
    //    //            if(!Planet.unlockWave && !Planet.unlockLaser)
    //    //            {
    //    //                float rr = Random.Range(0f, 1f);

    //    //                if(rr>0.5f)
    //    //                {
    //    //                    Instantiate(rewards[3], npos, Quaternion.identity);
    //    //                    Planet.unlockWave = true;
    //    //                }
    //    //                else if(rr<0.5f)
    //    //                {
    //    //                    Instantiate(rewards[4], npos, Quaternion.identity);
    //    //                    Planet.unlockLaser = true;
    //    //                }


    //    //            }
    //    //            else if (Planet.unlockWave & !Planet.unlockLaser)
    //    //            {
    //    //                Instantiate(rewards[4], npos, Quaternion.identity);
    //    //                Planet.unlockLaser = true;
    //    //            }
    //    //            else if(!Planet.unlockWave && Planet.unlockLaser)
    //    //            {
    //    //                Instantiate(rewards[3], npos, Quaternion.identity);
    //    //                Planet.unlockWave = true;
    //    //            }
    //    //            else
    //    //            {
    //    //                Instantiate(gem, npos, Quaternion.identity);
    //    //            }

    //    //        }
    //    //        else
    //    //        {
    //    //            Instantiate(rewards[r], npos, Quaternion.identity);
    //    //        }
               



    //    //        sfx.PlayOneShot(sfx.clip);
    //    //        wait.SetActive(false);
    //    //        //transform.position = npos;

    //    //        //
    //    //        // YOUR CODE TO REWARD THE GAMER
    //    //        // Give coins etc.
    //    //        break;
    //    //    case ShowResult.Skipped:
    //    //        wait.SetActive(false);
    //    //        //Debug.Log("The ad was skipped before reaching the end.");
    //    //        break;
    //    //    case ShowResult.Failed:
    //    //        wait.SetActive(false);
    //    //        //Debug.LogError("The ad failed to be shown.");
    //    //        break;
    //    //}
    //}
}