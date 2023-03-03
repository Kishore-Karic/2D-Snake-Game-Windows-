using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D spawnArea;

    private int randomNumber;

    private void RandomizePosition()
    {
        Bounds bounds = this.spawnArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);

        randomNumber = Random.Range(0, 3);
    }

    public int GetRandomNumber()
    {
        return randomNumber;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Snake1") || collision.CompareTag("FoodGainer") || collision.CompareTag("FoodBurner") || collision.CompareTag("SnakeBody1") || collision.CompareTag("Snake2") || collision.CompareTag("SnakeBody2"))
        {
            RandomizePosition();
        }
    }
}
