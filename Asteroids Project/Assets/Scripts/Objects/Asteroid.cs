using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(EdgeWarpingScript))]
public class Asteroid : MonoBehaviour {
    public ObjectType objType = ObjectType.bigAster;
    public float forceAmount = 100f;
    private Vector2 moveDir;

    private Rigidbody2D rb2D;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        moveDir = Random.insideUnitCircle.normalized;
        rb2D.AddForce(moveDir * forceAmount);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.GetComponent<Player>() != null)
        {
            coll.gameObject.GetComponent<Player>().Die();
            Destroy();
        }

        if(coll.gameObject.GetComponent<Saucer>() != null)
        {
            coll.gameObject.GetComponent<Saucer>().Destroy();
            Destroy();
        }
    }

    public void Destroy()
    {
        switch (objType)
        {
            case ObjectType.bigAster:
                for (int i = 0; i < 2; i++)
                {
                    Transform mediumAsteroid = ObjectPooling.pool.GetMediumAsteroid();
                    if (mediumAsteroid == null)
                    {
                        Debug.Log("There are no other medium asteroids available");
                        return;
                    }
                    mediumAsteroid.position = transform.position;
                    mediumAsteroid.rotation = transform.rotation;
                    mediumAsteroid.gameObject.SetActive(true);
                }
                break;
            case ObjectType.mediumAster:
                for (int i = 0; i < 2; i++)
                {
                    Transform smallAsteroid = ObjectPooling.pool.GetSmallAsteroid();
                    if (smallAsteroid == null)
                    {
                        Debug.Log("There are no other small asteroids available");
                        return;
                    }
                    smallAsteroid.position = transform.position;
                    smallAsteroid.rotation = transform.rotation;
                    smallAsteroid.gameObject.SetActive(true);
                }
                break;
        }
        AudioManager.asteroidAS.PlayOneShot(AudioManager.aM.destroySound);
        AsteroidSpawnManager.instance.DestroyAsteroid();
        gameObject.SetActive(false);
    }
	
}
