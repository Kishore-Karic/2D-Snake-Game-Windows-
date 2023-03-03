using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultiPlayerUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText2;
    private int score2;

    [SerializeField] private GameObject scoreBoostUI2;
    [SerializeField] private GameObject speedUI2;
    [SerializeField] private GameObject shieldUI2;

    [SerializeField] private Snake2 snake2;

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
        score2 += i;
        RefreshUI();
    }

    public void DecreamentScore(int d)
    {
        score2 -= d;
        RefreshUI();
    }

    public int GetScore()
    {
        return score2;
    }

    private void RefreshUI()
    {
        if (score2 < 0)
        {
            score2 = 0;
        }

        scoreText2.text = "" + score2;
    }

    private void PowerUpUI()
    {
        if (snake2.GetScoreActive())
        {
            scoreBoostUI2.SetActive(true);
            speedUI2.SetActive(false);
            shieldUI2.SetActive(false);
        }
        else
        {
            scoreBoostUI2.SetActive(false);
        }

        if (snake2.GetShieldActive())
        {
            shieldUI2.SetActive(true);
            scoreBoostUI2.SetActive(false);
            speedUI2.SetActive(false);
        }
        else
        {
            shieldUI2.SetActive(false);
        }

        if (snake2.GetSpeedActive())
        {
            speedUI2.SetActive(true);
            scoreBoostUI2.SetActive(false);
            shieldUI2.SetActive(false);
        }
        else
        {
            speedUI2.SetActive(false);
        }
    }

    public void SetGameobjectActive(bool r)
    {
        shieldUI2.SetActive(r);
    }
}
