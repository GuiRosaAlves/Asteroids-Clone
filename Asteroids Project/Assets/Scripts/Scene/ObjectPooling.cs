using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour {

    public static ObjectPooling pool;

    [Range(1, 50)]      
    public int bulletsQuantity = 1, bigAsteroidsQuantity = 8, mediumAsteroidsQuantity = 16, smallAsteroidsQuantity = 32;
    public bool expandPool = false;

    [Header("Prefabs")]
    #region Prefabs
    public GameObject bulletPrefab;
    public GameObject bigAsteroidPrefab;
    public GameObject mediumAsteroidPrefab;
    public GameObject smallAsteroidPrefab;
    public GameObject bigSaucerPrefab;
    public GameObject smallSaucerPrefab;
    #endregion

    #region Pools
    private Transform enemyBullet;
    private List<Transform> saucerPool;
    private List<Transform> bulletsPool;
    private List<Transform> bigAsteroidsPool;
    private List<Transform> mediumAsteroidsPool;
    private List<Transform> smallAsteroidsPool;
    #endregion

    void Awake()
    {
        pool = this;
    }

	void OnEnable () {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        Transform newObj;

        saucerPool = new List<Transform>();
        bulletsPool = new List<Transform>();
        bigAsteroidsPool = new List<Transform>();
        mediumAsteroidsPool = new List<Transform>();
        smallAsteroidsPool = new List<Transform>();

        #region Enemy Bullet
        enemyBullet = Instantiate(bulletPrefab).transform;
        enemyBullet.gameObject.SetActive(false);
        #endregion

        #region Saucers
        newObj = Instantiate(bigSaucerPrefab).transform;
        newObj.gameObject.SetActive(false);
        saucerPool.Add(newObj);
        newObj = Instantiate(smallSaucerPrefab).transform;
        newObj.gameObject.SetActive(false);
        saucerPool.Add(newObj);
        #endregion

        #region Bullets
        for (int i = 0; i < bulletsQuantity; i++)
        {
            newObj = Instantiate(bulletPrefab).transform;
            newObj.gameObject.SetActive(false);
            bulletsPool.Add(newObj);
        }
        #endregion

        #region Big Asteroids
        for (int i = 0; i < bigAsteroidsQuantity; i++)
        {
            newObj = Instantiate(bigAsteroidPrefab).transform;
            newObj.gameObject.SetActive(false);
            bigAsteroidsPool.Add(newObj);
        }
        #endregion

        #region Medium Asteroids
        for (int i = 0; i < mediumAsteroidsQuantity; i++)
        {
            newObj = Instantiate(mediumAsteroidPrefab).transform;
            newObj.gameObject.SetActive(false);
            mediumAsteroidsPool.Add(newObj);
        }
        #endregion

        #region Small Asteroids
        for (int i = 0; i < smallAsteroidsQuantity; i++)
        {
            newObj = Instantiate(smallAsteroidPrefab).transform;
            newObj.gameObject.SetActive(false);
            smallAsteroidsPool.Add(newObj);
        }
        #endregion
    }

    public Transform GetBullet()
    {
        foreach(Transform obj in bulletsPool)
        {
            if (!obj.gameObject.activeInHierarchy)
                return obj;
        }

        return null;
    }

    public Transform GetEnemyBullet()
    {
        return enemyBullet;
    }

    public Transform GetBigAsteroid()
    {
        foreach (Transform obj in bigAsteroidsPool)
        {
            if (!obj.gameObject.activeInHierarchy)
                return obj;
        }

        return null;
    }

    public Transform GetMediumAsteroid()
    {
        foreach (Transform obj in mediumAsteroidsPool)
        {
            if (!obj.gameObject.activeInHierarchy)
                return obj;
        }

        if (expandPool)
        {
            Transform newAsteroid = Instantiate(mediumAsteroidPrefab).transform;
            mediumAsteroidsPool.Add(newAsteroid);
            return newAsteroid;
        }else
        {
            return null;
        }
    }

    public Transform GetSmallAsteroid()
    {
        foreach (Transform obj in smallAsteroidsPool)
        {
            if (!obj.gameObject.activeInHierarchy)
                return obj;
        }
        if (expandPool)
        {
            Transform newAsteroid = Instantiate(smallAsteroidPrefab).transform;
            smallAsteroidsPool.Add(newAsteroid);
            return newAsteroid;
        }
        else
        {
            return null;
        }
    }

    public Transform GetSaucer(bool randomSpawn)
    {
        if (randomSpawn)
        {
            return saucerPool[Random.Range(0, saucerPool.Count)];
        }else
        {
            foreach(Transform saucer in saucerPool)
            {
                if (saucer.GetComponent<Saucer>().objType == ObjectType.smallSaucer)
                    return saucer;
            }
            return null;
        }
    }
}
