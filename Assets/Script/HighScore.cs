using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    [SerializeField] private SinglePlayerUIManager singlePlayerUIManager;

    [SerializeField] private GameObject newHighScore;
    [SerializeField] private GameObject highScoreObj;
    [SerializeField] private TextMeshProUGUI highScore;

    void Start()
    {
        SoundManager.Instance.StopMusic(Sounds.GameTheme);
        SoundManager.Instance.PlayEffect(Sounds.Lose);
        Debug.Log("High Score Start");
        if (singlePlayerUIManager.GetScore() > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", singlePlayerUIManager.GetScore());
            newHighScore.SetActive(true);
        }
        else
        {
            highScoreObj.SetActive(true);
            highScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
        }
    }
}
