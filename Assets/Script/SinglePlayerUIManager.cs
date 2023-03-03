using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SinglePlayerUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score;

    [SerializeField] private GameObject scoreBoostUI;
    [SerializeField] private GameObject speedUI;
    [SerializeField] private GameObject shieldUI;

    [SerializeField] private Snake1 snake1;

    private void Start()
    {
        RefreshUI();
    }

    private void Update()
    {
        RefreshUI();
        PowerUpUI();
    }

    public void IncreamentScore(int i)
    {
        score += i;
        RefreshUI();
    }

    public void DecreamentScore(int d)
    {
        score -= d;
        RefreshUI();
    }

    public int GetScore()
    {
        return score;
    }

    private void RefreshUI()
    {
        if(score < 0)
        {
            score = 0;
        }

        scoreText.text = "" + score;
    }

    private void PowerUpUI()
    {
        if (snake1.GetScoreActive())
        {
            scoreBoostUI.SetActive(true);
            speedUI.SetActive(false);
            shieldUI.SetActive(false);
        }
        else
        {
            scoreBoostUI.SetActive(false);
        }

        if (snake1.GetShieldActive())
        {
            shieldUI.SetActive(true);
            scoreBoostUI.SetActive(false);
            speedUI.SetActive(false);
        }
        else
        {
            shieldUI.SetActive(false);
        }
        
        if (snake1.GetSpeedActive())
        {
            speedUI.SetActive(true);
            scoreBoostUI.SetActive(false);
            shieldUI.SetActive(false);
        }
        else
        {
            speedUI.SetActive(false);
        }
    }

    public void SetGameobjectActive(bool r)
    {
        shieldUI.SetActive(r);
    }
}
