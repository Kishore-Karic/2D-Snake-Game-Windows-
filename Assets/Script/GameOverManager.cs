using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private bool isMultiplayer;

    private bool snake1Attacked = false;
    private bool snake2Attacked = false;

    [SerializeField] private SinglePlayerUIManager singlePlayerUIManager;
    [SerializeField] private MultiPlayerUIManager multiPlayerUIManager;

    [SerializeField] private TextMeshProUGUI player1Score;
    [SerializeField] private TextMeshProUGUI player2Score;


    [SerializeField] private GameObject player1Win;
    [SerializeField] private GameObject player2Win;
    
    [SerializeField] private Button restartButton, mainmenuButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(Restart);
        mainmenuButton.onClick.AddListener(MainMenu);
    }

    private void Update()
    {
        RefreshUI1();

        if (snake1Attacked)
        {
            player1Win.SetActive(true);
        }
        else if (snake2Attacked)
        {
            player2Win.SetActive(true);
        }
    }

    public void RefreshUI1()
    {
        player1Score.text = "" + singlePlayerUIManager.GetScore();
        if (isMultiplayer)
        {
            player2Score.text = "" + multiPlayerUIManager.GetScore();
        }
    }

    public void SetSnake1Attacked(bool s1)
    {
        SoundManager.Instance.StopMusic(Sounds.GameTheme);
        SoundManager.Instance.PlayEffect(Sounds.Win);
        snake1Attacked = s1;
    }

    public void SetSnake2Attacked(bool s2)
    {
        SoundManager.Instance.StopMusic(Sounds.GameTheme);
        SoundManager.Instance.PlayEffect(Sounds.Win);
        snake2Attacked = s2;
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
