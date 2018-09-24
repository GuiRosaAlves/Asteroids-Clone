using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(EdgeWarpingScript))]
public class Saucer : MonoBehaviour {

    public ObjectType objType = ObjectType.bigSaucer;
    public float speed, destroyTime, fireRate;

    private float accuracy = 0f, randomDistance = 0, timeUntilFire = 1;
    private Vector3 moveDir;

    void OnEnable()
    {
        moveDir = (GameManager.gM.playerTransform.position - transform.position).normalized;
        if (objType == ObjectType.smallSaucer)
        {
            accuracy = (GameManager.gM.GetScore() / 500) * 0.1f;
            accuracy = accuracy <= 0 ? 0.1f : accuracy;
        }
        else
        {
            accuracy = 0;
        }
        Invoke("Destroy", destroyTime);
        Invoke("Move", Random.Range(0.1f, (destroyTime / 2f)));
    }

    void FixedUpdate()
    {
        transform.position += moveDir * speed * Time.deltaTime;
    }

    void Update()
    {
        if (timeUntilFire <= 0)
            Fire();

        if (timeUntilFire >= 0)
            timeUntilFire -= fireRate * Time.deltaTime;

        moveDir.y = Mathf.Lerp(moveDir.y, 0, randomDistance * Time.deltaTime);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.GetComponent<Player>() != null)
        {
            coll.gameObject.GetComponent<Player>().Die();
            Destroy();
        }
    }

    void Move()
    {
        moveDir.y = Random.value < 0.5f ? 1f : -1f;
        randomDistance = Random.value;
        Invoke("Move", Random.Range(0.1f, (destroyTime / 2)));
    }

    void Fire()
    {
        timeUntilFire = 1;
        Transform newBullet = ObjectPooling.pool.GetEnemyBullet();
        newBullet.position = transform.position;
        newBullet.eulerAngles = GetBulletRotation();
        if(objType == ObjectType.smallSaucer)
        {
            AudioManager.saucerAS.PlayOneShot(AudioManager.aM.smallSaucerFireSound);
            newBullet.Rotate(0, 0, (1 / accuracy) * 10);
        }else
        {
            AudioManager.saucerAS.PlayOneShot(AudioManager.aM.bigSaucerFireSound);
        }
        newBullet.gameObject.SetActive(true);
    }

    Vector3 GetBulletRotation()
    {
        Vector3 newPos;
        if (objType == ObjectType.bigSaucer)
            newPos = Random.insideUnitCircle;
        else
            newPos = (GameManager.gM.playerTransform.position - transform.position).normalized;

        float angle = Mathf.Atan2(newPos.x, newPos.y) * Mathf.Rad2Deg;
        return -(Vector3.forward * angle);
    }

    public void Destroy()
    {
        AudioManager.saucerAS.PlayOneShot(AudioManager.aM.destroySound);
        EnemySpawnManager.instance.StartSpawnTimer();
        gameObject.SetActive(false);
    }
}