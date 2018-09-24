using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(EdgeWarpingScript))]
public class Bullet : MonoBehaviour {

    public float forceAmount, destroyTime = 2f;
    private bool shotByPlayer = false;

    private Rigidbody2D rb2D;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        Invoke("Destroy", destroyTime);
        rb2D.AddForce(transform.up * forceAmount);
    }

    void OnDisable()
    {
        CancelInvoke();
        rb2D.velocity = Vector2.zero;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.GetComponent<Player>() != null)
        {
            if(!shotByPlayer)
                coll.GetComponent<Player>().Die();
        }

        if (coll.GetComponent<Asteroid>() != null)
        {
            coll.GetComponent<Asteroid>().Destroy();
            if (shotByPlayer)
                GameManager.gM.AddPoints(coll.GetComponent<Asteroid>().objType);
            Destroy();
        }

        if(coll.GetComponent<Saucer>() != null)
        {
            if (shotByPlayer)
            {
                coll.GetComponent<Saucer>().Destroy();
                GameManager.gM.AddPoints(coll.GetComponent<Saucer>().objType);
                Destroy();
            }
        }
    }

    public void Destroy()
    {
        shotByPlayer = false;
        gameObject.SetActive(false);
    }
    
    public void DidPlayerShotTheBullet(bool didPlayerShoot)
    {
        shotByPlayer = didPlayerShoot;
    }
}
