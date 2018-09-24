using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class PlayerInfo
{
    public string name;
    public int score;

    public PlayerInfo(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}

public class HighScoreManager: MonoBehaviour{

    public GameObject scoreFieldPrefab;

    private static List<PlayerInfo> playerScoresDB = new List<PlayerInfo>();


    void Start()
    {
        LoadScore();
        int i = 0;
        while(i < 10 && i < playerScoresDB.Count)
        {
            Transform newScore = Instantiate(scoreFieldPrefab).transform;
            newScore.SetParent(transform);
            newScore.GetComponent<Text>().text = (i+1)+"°"+playerScoresDB[i].name + "...................." + playerScoresDB[i].score;
            i++;
        }
    }

    public static void SaveScore()
    {
        BinaryFormatter bF = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerScores.dat", FileMode.Create);

        playerScoresDB = playerScoresDB.OrderByDescending(d => d.score).ToList();

        if (file != null)
        {
            bF.Serialize(file, playerScoresDB);
            file.Close();
        }
        else
        {
            Debug.Log("Error! Couldn't create a file");
        }
    }

    public static void LoadScore()
    {
        BinaryFormatter bF = new BinaryFormatter();
        FileStream file;
        if ((file = File.Open(Application.persistentDataPath + "/playerScores.dat", FileMode.Open)) != null)
        {
            playerScoresDB = (List<PlayerInfo>)bF.Deserialize(file);
            file.Close();
        }
        else
        {
            Debug.Log("File doesn't exist");
        }
    }

    public static void AddScore(string name, int score)
    {
        foreach(PlayerInfo player in playerScoresDB)
        {
            if (player.name.ToLower() == name.ToLower())
            {
                if(player.score < score)
                    player.score = score;
                return;
            }
        }
        
        playerScoresDB.Add(new PlayerInfo(name, score));
    }

    public void ResetScoreBoard()
    {
        playerScoresDB = new List<PlayerInfo>();
        SaveScore();
    }
}
