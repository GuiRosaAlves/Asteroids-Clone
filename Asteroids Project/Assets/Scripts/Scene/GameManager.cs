using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType { smallSaucer, bigSaucer, smallAster, mediumAster, bigAster};

public class GameManager : MonoBehaviour {

    public static GameManager gM;

    public Transform playerTransform;

    private int score = 0, addLifeCounter = 0;
    private bool shouldItSpawnBigSaucer = true;

    #region Points for each object destroyed
    public static int bigAsteroidScore = 20;
    public static int mediumAsteroidScore = 50;
    public static int smallAsteroidScore = 100;
    public static int bigSaucerScore = 200;
    public static int smallSaucerScore = 1000;
    #endregion

    void Awake()
    {
        gM = this;
    }

    void Start()
    {
        HighScoreManager.LoadScore();
    }

    void Update()
    {
        if (addLifeCounter >= 10000)
        {
            playerTransform.GetComponent<Player>().AddLife();
            addLifeCounter -= 10000;
        }

        shouldItSpawnBigSaucer = (score >= 40000) ? false : true;
    }

    public void AddPoints(ObjectType objType)
    {
        switch (objType)
        {
            case ObjectType.bigAster:
                score += bigAsteroidScore;
                addLifeCounter += bigAsteroidScore;
                break;
            case ObjectType.mediumAster:
                score += mediumAsteroidScore;
                addLifeCounter += mediumAsteroidScore;
                break;
            case ObjectType.smallAster:
                score += smallAsteroidScore;
                addLifeCounter += smallAsteroidScore;
                break;
            case ObjectType.bigSaucer:
                score += bigSaucerScore;
                addLifeCounter += bigSaucerScore;
                break;
            case ObjectType.smallSaucer:
                score += smallSaucerScore;
                addLifeCounter += smallSaucerScore;
                break;
        }
        UIManager.instance.UpdateScoreUI();
    }

    public int GetScore()
    {
        return score;
    }

    public void GameOver(string playerName)
    {
        HighScoreManager.AddScore(playerName, score);
        HighScoreManager.SaveScore();
    }

    public bool GetSpawn()
    {
        return shouldItSpawnBigSaucer;
    }
}
