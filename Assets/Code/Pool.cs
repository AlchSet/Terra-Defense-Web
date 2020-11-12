using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{

    public enum PoolType { Hazzard, Projectile, EnemyProjectile, Tower, Collectable }

    public PoolType poolType;

    public GameObject pooledObject;
    public int Amount;
    public List<Hazzard> poolList = new List<Hazzard>();
    public List<Projectile> projectileList = new List<Projectile>();
    public List<EnemyProjectile> eProjectileList = new List<EnemyProjectile>();

    public List<GameObject> towerList = new List<GameObject>();

    public List<TowerInterface> towerInterfaceList = new List<TowerInterface>();

    public List<GameObject> collectableList = new List<GameObject>();


    public int TowerListSum;
    public int index;



    public Transform readyTower1;
    public Transform readyTower2;


    public bool readyTowers;
    // Use this for initialization
    void Start()
    {


        switch (poolType)
        {
            case PoolType.Hazzard:
                for (int i = 0; i < Amount; i++)
                {
                    GameObject g = Instantiate(pooledObject, transform.position, Quaternion.identity, transform);
                    Hazzard h = g.GetComponent<Hazzard>();
                    h.Reset();
                    poolList.Add(h);
                }

                break;


            case PoolType.Projectile:
                for (int i = 0; i < Amount; i++)
                {
                    GameObject g = Instantiate(pooledObject, transform.position, Quaternion.identity, transform);
                    Projectile h = g.GetComponent<Projectile>();

                    h.gameObject.SetActive(false);

                    projectileList.Add(h);

                    //h.Reset();
                    //poolList.Add(h);
                }

                break;


            case PoolType.EnemyProjectile:


                for (int i = 0; i < Amount; i++)
                {
                    GameObject g = Instantiate(pooledObject, transform.position, Quaternion.identity, transform);
                    EnemyProjectile h = g.GetComponent<EnemyProjectile>();

                    h.gameObject.SetActive(false);

                    eProjectileList.Add(h);

                    //h.Reset();
                    //poolList.Add(h);
                }

                break;


            case PoolType.Tower:

                for (int i = 0; i < Amount; i++)
                {
                    GameObject g = Instantiate(pooledObject, transform.position, Quaternion.identity, transform);
                    TowerInterface h = g.transform.GetComponentInChildren<TowerInterface>();

                    towerInterfaceList.Add(h);

                    g.SetActive(false);

                    towerList.Add(g);




                    //h.Reset();
                    //poolList.Add(h);
                }
                TowerListSum = towerList.ToArray().Length;


                if(readyTowers)
                {
                    towerList[0].transform.position = readyTower1.position;
                    towerList[0].transform.rotation = readyTower1.rotation;
                    towerList[0].SetActive(true);

                    towerList[1].transform.position = readyTower2.position;
                    towerList[1].transform.rotation = readyTower2.rotation;
                    towerList[1].SetActive(true);

                    index =2;
                }

                break;


            case PoolType.Collectable:

                for (int i = 0; i < Amount; i++)
                {
                    GameObject g = Instantiate(pooledObject, transform.position, Quaternion.identity, transform);
                    TowerInterface h = g.transform.GetComponentInChildren<TowerInterface>();

                    //towerInterfaceList.Add(h);

                    g.SetActive(false);

                    collectableList.Add(g);




                    //h.Reset();
                    //poolList.Add(h);
                }


                break;
        }

    }



    public Hazzard GetPooledObject()
    {
        Hazzard h = poolList[index];
        index = (index + 1) % poolList.ToArray().Length;

        return h;
    }

    public Projectile GetPooledProjectile()
    {
        //Debug.Log("LLL");
        Projectile h = projectileList[index];
        index = (index + 1) % projectileList.ToArray().Length;

        return h;
    }

    public EnemyProjectile GetPooledEnemyProjectile()
    {

        EnemyProjectile h = eProjectileList[index];
        index = (index + 1) % eProjectileList.ToArray().Length;

        return h;
    }




    public TowerData GetPooledTower()
    {
        TowerData data = new TowerData();

        data.towerRoot= towerList[index];
        data.towerInterface = towerInterfaceList[index];
        index = (index + 1) % towerList.ToArray().Length;

        return data;
    }

    public struct TowerData {

        public GameObject towerRoot;
        public TowerInterface towerInterface;



    }


    public GameObject GetPooledCollectable()
    {
        GameObject g = collectableList[index];
        index = (index + 1) % collectableList.ToArray().Length;
        return g;
    }


}
