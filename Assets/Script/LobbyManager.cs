using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private Button singlePlayerButton, multiPlayerButton, resetButton, quitButton;
    [SerializeField] private GameObject buttonLayer, resetLayer, quitLayer;
    [SerializeField] private Button resetOkeyButton, resetCancelButton, quitOkayButton, quitCancelButton;

    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Start()
    {
        SoundManager.Instance.StopMusic(Sounds.GameTheme);
        SoundManager.Instance.PlayMusic(Sounds.LobbyTheme);
    }

    private void Awake()
    {
        singlePlayerButton.onClick.AddListener(SinglePlayer);
        multiPlayerButton.onClick.AddListener(MultiPlayer);
        resetButton.onClick.AddListener(Reset);
        resetOkeyButton.onClick.AddListener(ResetOkay);
        resetCancelButton.onClick.AddListener(ResetCancel);
        quitButton.onClick.AddListener(Quit);
        quitOkayButton.onClick.AddListener(QuitOkay);
        quitCancelButton.onClick.AddListener(QuitCancel);
    }

    private void Update()
    {
        RefreshUI();
    }

    private void RefreshUI()
    {
        highScoreText.text = "" + PlayerPrefs.GetInt("HighScore");
    }

    private void SinglePlayer()
    {
        SoundManager.Instance.PlayEffect(Sounds.ButtonClick);
        SceneManager.LoadScene(1);
    }

    private void MultiPlayer()
    {
        SoundManager.Instance.PlayEffect(Sounds.ButtonClick);
        SceneManager.LoadScene(2);
    }

    private void Reset()
    {
        SoundManager.Instance.PlayEffect(Sounds.ButtonClick);
        buttonLayer.SetActive(false);
        resetLayer.SetActive(true);
    }

    private void ResetOkay()
    {
        SoundManager.Instance.PlayEffect(Sounds.ButtonClick);
        PlayerPrefs.SetInt("HighScore", 0);
        ResetCancel();
    }

    private void ResetCancel()
    {
        SoundManager.Instance.PlayEffect(Sounds.ButtonClick);
        buttonLayer.SetActive(true);
        resetLayer.SetActive(false);
    }

    private void Quit()
    {
        SoundManager.Instance.PlayEffect(Sounds.ButtonClick);
        buttonLayer.SetActive(false);
        quitLayer.SetActive(true);
    }

    private void QuitOkay()
    {
        SoundManager.Instance.PlayEffect(Sounds.ButtonClick);
        Application.Quit();
    }

    private void QuitCancel()
    {
        SoundManager.Instance.PlayEffect(Sounds.ButtonClick);
        buttonLayer.SetActive(true);
        quitLayer.SetActive(false);
    }
}
