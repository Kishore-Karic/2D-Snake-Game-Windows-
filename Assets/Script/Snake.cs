using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector3 direction;

    [SerializeField] private SinglePlayerUIManager singlePlayerUIManager;
    [SerializeField] private MultiPlayerUIManager multiPlayerUIManager;
    [SerializeField] private GameOverManager gameOverManager;
    [SerializeField] private GameObject gameOverUI;

    [SerializeField] private Snake snake;
    [SerializeField] private bool isMultiplayer;

    [SerializeField] private GameObject PowerUpUI;
    [SerializeField] private GameObject ScoreUI;

    [SerializeField] private GameObject PowerUpUI2;
    [SerializeField] private GameObject ScoreUI2;

    [SerializeField] private GameObject powerUp;
    private bool speed = false;
    private bool shield = false;
    private bool scoreBooster = false;

    private List<GameObject> snakeBody;
    [SerializeField] private GameObject snakeBodyPrefab;

    float moveSpeed;
    float originalSpeed;
    bool[] directionArrays = new bool[4];

    private enum SnakeType { snake1, snake2 }
    [SerializeField] private SnakeType snakeType;

    private void Start()
    {
        Time.timeScale = 0.2f;
        originalSpeed = 1f;
        moveSpeed = originalSpeed;
        switch (snakeType)
        {
            case SnakeType.snake1:
                direction = new Vector3(moveSpeed, 0, 0);
                directionArrays[3] = true;
                break;

            case SnakeType.snake2:
                direction = new Vector3(-moveSpeed, 0, 0);
                directionArrays[2] = true;
                break;
        }
        snakeBody = new List<GameObject>();
        snakeBody.Add(this.gameObject);
        StartCoroutine("StartingProtection");
        SoundManager.Instance.StopMusic(Sounds.LobbyTheme);
        SoundManager.Instance.PlayMusic(Sounds.GameTheme);
    }

    IEnumerator StartingProtection()
    {
        shield = true;
        yield return new WaitForSeconds(0.5f);
        shield = false;
        switch (snakeType)
        {
            case SnakeType.snake1:
                singlePlayerUIManager.SetGameobjectActive(false);
                break;

            case SnakeType.snake2:
                multiPlayerUIManager.SetGameobjectActive(false);
                break;
        }
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
        switch (snakeType)
        {
            case SnakeType.snake1:
                if (Input.GetKeyDown(KeyCode.UpArrow) && !directionArrays[1])
                {
                    direction = new Vector3(0, moveSpeed, 0);
                    SetDirectionalBool(0);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow) && !directionArrays[0])
                {
                    direction = new Vector3(0, moveSpeed * (-1), 0);
                    SetDirectionalBool(1);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) && !directionArrays[3])
                {
                    direction = new Vector3(moveSpeed * (-1), 0, 0);
                    SetDirectionalBool(2);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) && !directionArrays[2])
                {
                    direction = new Vector3(moveSpeed, 0, 0);
                    SetDirectionalBool(3);
                }
                break;

            case SnakeType.snake2:
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
                break;
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
                switch (snakeType)
                {
                    case SnakeType.snake1:
                        singlePlayerUIManager.IncreamentScore(30);
                        break;

                    case SnakeType.snake2:
                        multiPlayerUIManager.IncreamentScore(30);
                        break;
                }
            }
            else
            {
                switch (snakeType)
                {
                    case SnakeType.snake1:
                        singlePlayerUIManager.IncreamentScore(10);
                        break;

                    case SnakeType.snake2:
                        multiPlayerUIManager.IncreamentScore(10);
                        break;
                }
            }
        }

        if (collision.CompareTag("FoodBurner"))
        {
            if (snakeBody.Count > 1)
            {
                SoundManager.Instance.PlayEffect(Sounds.AntiFood);
                Shrink();
                switch (snakeType)
                {
                    case SnakeType.snake1:
                        singlePlayerUIManager.DecreamentScore(10);
                        break;

                    case SnakeType.snake2:
                        multiPlayerUIManager.DecreamentScore(10);
                        break;
                }
            }
            else
            {
                if (shield == false)
                {
                    gameOverUI.SetActive(true);
                    Time.timeScale = 0.0f;
                    PowerUpUI.SetActive(false);
                    ScoreUI.SetActive(false);
                    if (isMultiplayer)
                    {
                        switch (snakeType)
                        {
                            case SnakeType.snake1:
                                gameOverManager.SetSnake2Attacked(true);
                                break;

                            case SnakeType.snake2:
                                gameOverManager.SetSnake1Attacked(true);
                                break;
                        }
                        PowerUpUI2.SetActive(false);
                        ScoreUI2.SetActive(false);
                    }
                }
            }
        }

        if (collision.CompareTag("PowerUp"))
        {
            int randomNumber = -1;
            switch (snakeType)
            {
                case SnakeType.snake1:
                    int rand1 = Random.Range(0, 3);
                    randomNumber = rand1;
                    break;

                case SnakeType.snake2:
                    int rand2 = Random.Range(0, 3);
                    randomNumber = rand2;
                    break;
            }
            SoundManager.Instance.PlayEffect(Sounds.PowerUp);
            if (randomNumber == 0)
            {
                speed = true;
                moveSpeed = 1.1f;
            }
            if (randomNumber == 1)
            {
                shield = true;
            }
            if (randomNumber == 2)
            {
                scoreBooster = true;
            }
            powerUp.SetActive(false);
            StartCoroutine("PowerUpTimer");
        }

        if (collision.CompareTag("SnakeBody1"))
        {
            if (shield == false)
            {
                if (isMultiplayer && snake.GetShieldActive() == false)
                {
                    Time.timeScale = 0.0f;
                    gameOverUI.SetActive(true);
                    PowerUpUI.SetActive(false);
                    ScoreUI.SetActive(false);
                    gameOverManager.SetSnake2Attacked(true);
                    PowerUpUI2.SetActive(false);
                    ScoreUI2.SetActive(false);
                }
                else if (!isMultiplayer)
                {
                    Time.timeScale = 0.0f;
                    gameOverUI.SetActive(true);
                    PowerUpUI.SetActive(false);
                    ScoreUI.SetActive(false);
                }
            }
        }

        if (collision.CompareTag("SnakeBody2"))
        {
            if (snakeType == 0 && snake.GetShieldActive() == false || snakeType != 0 && shield == false)
            {
                gameOverManager.SetSnake1Attacked(true);
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
