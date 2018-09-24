using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour {

    public void ConfirmInput(Text input)
    {
            if (!string.IsNullOrEmpty(input.text))
            {
                GameManager.gM.GameOver(input.text);
                LoadScene("HighScores");
            }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
