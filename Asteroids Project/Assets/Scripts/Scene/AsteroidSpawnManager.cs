using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnManager : MonoBehaviour {

    public static AsteroidSpawnManager instance;
    public float spawnTime = 2f;
    public int asteroidsDestroyed = 0;

    private int currAsteroidsOnScreen = 4, maxAsteroidsOnScreen;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        maxAsteroidsOnScreen = ObjectPooling.pool.bigAsteroidsQuantity;
        SpawnAsteroids();
    }

    void Update()
    {
        if (asteroidsDestroyed == (currAsteroidsOnScreen + (currAsteroidsOnScreen * 6)))
        {
            asteroidsDestroyed = 0;
            if(currAsteroidsOnScreen < maxAsteroidsOnScreen)
                currAsteroidsOnScreen++;
            Invoke("SpawnAsteroids", spawnTime);
        }
    }

    public void SpawnAsteroids()
    {
        for(int i = 0; i < currAsteroidsOnScreen; i++)
        {
            Transform newAsteroid = ObjectPooling.pool.GetBigAsteroid();
            if(newAsteroid == null)
            {
                Debug.Log("There is no big asteroid available!");
                return;
            }
            newAsteroid.position = (Vector2)GameManager.gM.playerTransform.position + Random.insideUnitCircle.normalized * (EdgeWarpingScript.screenTopRight.x);
            newAsteroid.gameObject.SetActive(true);
        }
    }

    public void DestroyAsteroid()
    {
        asteroidsDestroyed++;
    }
}
