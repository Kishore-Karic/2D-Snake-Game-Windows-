using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SinglePlayerGameOver : MonoBehaviour
{
    [SerializeField] private SinglePlayerUIManager singlePlayerUIManager;

    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private Button restartButton, mainmenuButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(Restart);
        mainmenuButton.onClick.AddListener(MainMenu);
    }

    private void Update()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        totalScoreText.text = "" + singlePlayerUIManager.GetScore();
    }

    private void Restart()
    {
        SoundManager.Instance.PlayEffect(Sounds.ButtonClick);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }

    private void MainMenu()
    {
        SoundManager.Instance.PlayEffect(Sounds.ButtonClick);
        SceneManager.LoadScene(0);
    }
}
