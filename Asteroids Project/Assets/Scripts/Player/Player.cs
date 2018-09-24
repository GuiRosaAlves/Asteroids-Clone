using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(EdgeWarpingScript), typeof(Animator))]
public class Player : MonoBehaviour {

    public int maxLives = 3;
    private int currLives;
    public float thrustForce, rotSpeed, fireRate = 5f, respawnTime = 2f, teleportTime = 2f;
    public GameObject engine;

    private float timeUntilFire = 1;
    private Rigidbody2D rb2D;
    private Animator anim;

    void Start()
    {
        currLives = maxLives;
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (timeUntilFire >= 0)
            timeUntilFire -= fireRate * Time.deltaTime;
    }

    public void Rotate(float rotAxis)
    {
        rb2D.AddTorque(rotAxis * rotSpeed * -1f);
    }

    public void Thrust(Vector2 moveAxis)
    {
        if (moveAxis.y > 0)
        {
            engine.SetActive(true);
            rb2D.AddForce(transform.up * thrustForce);
        }else
        {
            engine.SetActive(false);
        }
    }

    public void Fire()
    {
        if(timeUntilFire <= 0)
        {
            timeUntilFire = 1;
            Transform bullet = ObjectPooling.pool.GetBullet();
            if (bullet == null)
            {
                Debug.Log("No bullet found!");
                return;
            }
            bullet.position = transform.position + (transform.up*0.7f);
            bullet.rotation = transform.rotation;
            bullet.GetComponent<Bullet>().DidPlayerShotTheBullet(true);
            bullet.gameObject.SetActive(true);
            AudioManager.playerAS.PlayOneShot(AudioManager.aM.playerFireSound);
        }
    }

    public void Spawn()
    {
        transform.position = Vector3.zero;
        gameObject.SetActive(true);
    }

    public void Die()
    {
        LoseLife();

        if (currLives != 0)
            Invoke("Spawn", respawnTime);
        else
        {
            AudioManager.playerAS.PlayOneShot(AudioManager.aM.gameOverSound);
            UIManager.instance.ActivateGameOverCanvas();
        }
        gameObject.SetActive(false);
    }

    public void Teleport()
    {
        AudioManager.playerAS.PlayOneShot(AudioManager.aM.teleportSound);
        gameObject.SetActive(false);
        Vector2 topRightCorner = EdgeWarpingScript.screenTopRight;
        Vector2 bottomLeftCorner = EdgeWarpingScript.screenBottomLeft;
        transform.position = new Vector3(Random.Range(bottomLeftCorner.x + 1f, topRightCorner.x - 0.5f), Random.Range(bottomLeftCorner.y + 1f, topRightCorner.y - 0.5f), 0);
        Invoke("ActivateShip", teleportTime);
    }

    public void ActivateShip()
    {
        AudioManager.playerAS.PlayOneShot(AudioManager.aM.teleportArriveSound);
        gameObject.SetActive(true);
        anim.SetTrigger("Teleporting");
    }

    public void AddLife()
    {
        GameObject obj = UIManager.instance.GetLifeUI(false);
        if(obj != null)
            obj.SetActive(true);
        else
            Debug.Log("There is no other life UI to activate");
        currLives++;
    }

    public void LoseLife()
    {
        if(currLives <= 3)
        {
            GameObject obj = UIManager.instance.GetLifeUI(true);
            if(obj != null)
                obj.SetActive(false);
            else
                Debug.Log("There is no other life UI to deactivate");
        }
        currLives--;
    }
}