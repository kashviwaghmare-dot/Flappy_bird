using System.IO;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int score;
    private void Gameover()
    {
        Debug.Log("Game Over");
    }

    // Update is called once per frame
    public void increasescore()
    {
        score++;
    }
}
