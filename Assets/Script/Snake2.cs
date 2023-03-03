using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake2 : MonoBehaviour
{
    private Vector3 direction;

    [SerializeField] private MultiPlayerUIManager multiPlayerUIManager;
    [SerializeField] private MultiPlayerGameOver multiPlayerGameOver;
    [SerializeField] private GameObject gameOverUI;

    [SerializeField] private Snake1 snake1;

    [SerializeField] private GameObject PowerUpUI;
    [SerializeField] private GameObject ScoreUI;

    [SerializeField] private GameObject PowerUpUI2;
    [SerializeField] private GameObject ScoreUI2;

    [SerializeField] private PowerUpSpawner powerUpSpawner;
    [SerializeField] private GameObject powerUp;
    private bool speed = false;
    private bool shield = false;
    private bool scoreBooster = false;

    private List<GameObject> snakeBody;
    [SerializeField] private GameObject snakeBodyPrefab;

    float moveSpeed;
    float originalSpeed;
    bool[] directionArrays = new bool[4];

    private void Start()
    {
        Time.timeScale = 0.2f;
        originalSpeed = 1f;
        moveSpeed = originalSpeed;
        direction = new Vector3(moveSpeed * (-1), 0, 0);
        directionArrays[2] = true;
        snakeBody = new List<GameObject>();
        snakeBody.Add(this.gameObject);
        StartCoroutine("StartingProtection");
    }

    IEnumerator StartingProtection()
    {
        shield = true;
        yield return new WaitForSeconds(0.5f);
        shield = false;
        multiPlayerUIManager.SetGameobjectActive(false);
        powerUp.SetActive(true);
    }

    private void Update()
    {
        Movement();
        ValidatePosition();
    }

    private void FixedUpdate()
    {
        for (int i = snakeBody.Count - 1; i > 0; i--)
        {
            snakeBody[i].transform.position = snakeBody[i - 1].transform.position;
        }

        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + direction.x, Mathf.Round(this.transform.position.y) + direction.y, 0.0f);
    }

    private void Movement()
    {
        if (Input.GetKeyDown(KeyCode.W) && !directionArrays[1])
        {
            direction = new Vector3(0, moveSpeed, 0);
            SetDirectionalBool(0);
        }
        else if (Input.GetKeyDown(KeyCode.S) && !directionArrays[0])
        {
            direction = new Vector3(0, moveSpeed * (-1), 0);
            SetDirectionalBool(1);
        }
        else if (Input.GetKeyDown(KeyCode.A) && !directionArrays[3])
        {
            direction = new Vector3(moveSpeed * (-1), 0, 0);
            SetDirectionalBool(2);
        }
        else if (Input.GetKeyDown(KeyCode.D) && !directionArrays[2])
        {
            direction = new Vector3(moveSpeed, 0, 0);
            SetDirectionalBool(3);
        }
    }

    private void SetDirectionalBool(int s)
    {
        for (int i = 0; i < 4; i++)
        {
            directionArrays[i] = false;
        }
        directionArrays[s] = true;
    }

    private void ValidatePosition()
    {
        Vector2 upperLimit = new Vector2(26, 20);
        Vector2 lowerLimit = new Vector2(-6, 0);

        if (this.transform.position.x > upperLimit.x)
        {
            this.transform.position = new Vector3(lowerLimit.x, transform.position.y, transform.position.z);
        }
        if (this.transform.position.x < lowerLimit.x)
        {
            this.transform.position = new Vector3(upperLimit.x, transform.position.y, transform.position.z);
        }
        if (this.transform.position.y > upperLimit.y)
        {
            this.transform.position = new Vector3(transform.position.x, lowerLimit.y, transform.position.z);
        }
        if (this.transform.position.y < lowerLimit.y)
        {
            this.transform.position = new Vector3(transform.position.x, upperLimit.y, transform.position.z);
        }
    }

    private void Grow()
    {
        GameObject segment = Instantiate(this.snakeBodyPrefab);
        segment.transform.position = snakeBody[snakeBody.Count - 1].transform.position;

        snakeBody.Add(segment);
    }

    private void Shrink()
    {
        Destroy(snakeBody[snakeBody.Count - 1]);
        snakeBody.RemoveAt(snakeBody.Count - 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FoodGainer"))
        {
            SoundManager.Instance.PlayEffect(Sounds.Food);
            Grow();
            if (scoreBooster == true)
            {
                multiPlayerUIManager.IncreamentScore(30);
            }
            else
            {
                multiPlayerUIManager.IncreamentScore(10);
            }
        }

        if (collision.CompareTag("FoodBurner"))
        {
            if (snakeBody.Count > 1)
            {
                SoundManager.Instance.PlayEffect(Sounds.AntiFood);
                Shrink();
                multiPlayerUIManager.DecreamentScore(10);
            }
            else
            {
                if (shield == false)
                {
                    multiPlayerGameOver.SetSnake1Attacked(true);
                    gameOverUI.SetActive(true);
                    Time.timeScale = 0.0f;
                    PowerUpUI.SetActive(false);
                    ScoreUI.SetActive(false);
                    PowerUpUI2.SetActive(false);
                    ScoreUI2.SetActive(false);
                }
            }
        }

        if (collision.CompareTag("PowerUp"))
        {
            SoundManager.Instance.PlayEffect(Sounds.PowerUp);
            if (powerUpSpawner.GetRandomNumber() == 0)
            {
                speed = true;
                moveSpeed = 1.1f;
            }
            if (powerUpSpawner.GetRandomNumber() == 1)
            {
                shield = true;
            }
            if (powerUpSpawner.GetRandomNumber() == 2)
            {
                scoreBooster = true;
            }
            powerUp.SetActive(false);
            StartCoroutine("PowerUpTimer");
        }

        if (collision.CompareTag("SnakeBody2"))
        {
            if (shield == false)
            {
                multiPlayerGameOver.SetSnake1Attacked(true);
                Time.timeScale = 0.0f;
                gameOverUI.SetActive(true);
                PowerUpUI.SetActive(false);
                ScoreUI.SetActive(false);
                PowerUpUI2.SetActive(false);
                ScoreUI2.SetActive(false);
            }
        }

        if (collision.CompareTag("SnakeBody1"))
        {
            if (!snake1.GetShieldActive())
            {
                multiPlayerGameOver.SetSnake2Attacked(true);
                Time.timeScale = 0.0f;
                gameOverUI.SetActive(true);
                PowerUpUI.SetActive(false);
                ScoreUI.SetActive(false);
                PowerUpUI2.SetActive(false);
                ScoreUI2.SetActive(false);
            }
        }
    }

    IEnumerator PowerUpTimer()
    {
        yield return new WaitForSeconds(2.5f);
        moveSpeed = originalSpeed;
        speed = false;
        shield = false;
        scoreBooster = false;
        powerUp.SetActive(true);
    }

    public bool GetSpeedActive()
    {
        return speed;
    }

    public bool GetScoreActive()
    {
        return scoreBooster;
    }

    public bool GetShieldActive()
    {
        return shield;
    }
}

