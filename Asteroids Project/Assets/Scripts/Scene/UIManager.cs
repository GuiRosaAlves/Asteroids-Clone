using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    public GameObject gameOverCanvas;
    public Transform livesPanel;
    public GameObject lifePrefab;
    public InputField nameInputField;
    public Text scoreTxt;
    
    private List<GameObject> livesUIPool;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        livesUIPool = new List<GameObject>();
        for(int i = 0; i < 3; i++)
        {
            GameObject newObj = Instantiate(lifePrefab);
            newObj.transform.SetParent(livesPanel);
            livesUIPool.Add(newObj);
        }
    }

    public void UpdateScoreUI()
    {
        scoreTxt.text = GameManager.gM.GetScore().ToString();
    }

    public GameObject GetLifeUI(bool activeState)
    {
        foreach(GameObject obj in livesUIPool)
        {
            if (obj.activeSelf == activeState)
                return obj;
        }
        return null;
    }

    public void ActivateGameOverCanvas()
    {
        gameOverCanvas.SetActive(true);
        nameInputField.ActivateInputField();
    }
}
