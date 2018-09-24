using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour {

    public static EnemySpawnManager instance;

    public float minSpawnTime = 2f, maxSpawnTime = 5f;
    private Vector2 bottomLeft, topRight;
    
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        bottomLeft = EdgeWarpingScript.screenBottomLeft;
        topRight = EdgeWarpingScript.screenTopRight;

        StartSpawnTimer();
    }

    public void StartSpawnTimer()
    {
        Invoke("SpawnSaucer", Random.Range(minSpawnTime, maxSpawnTime));
    }

    void SpawnSaucer()
    {
        Transform newSaucer = ObjectPooling.pool.GetSaucer(GameManager.gM.GetSpawn());
        newSaucer.position = GetSpawnLocation();
        newSaucer.gameObject.SetActive(true);
    }

    Vector3 GetSpawnLocation()
    {
        float xPos = Random.value < 0.5f ? bottomLeft.x : topRight.x;
        
        return new Vector3(xPos, Random.Range(bottomLeft.y, topRight.y), 0);
    }
}
