using System;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    public float speed = 5f;
    private float leftEdge;


    public TextMeshProUGUI gameOverText;

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x-800f;
    }

    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);

        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           
            GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            GameOver();
        }
    }


    private void GameOver()
    {
        Time.timeScale = 0f; // freezes entire game

        if (gameOverText != null)
        {
            gameOverText.text = "Game Over";
            gameOverText.gameObject.SetActive(true);
        }
    }


}