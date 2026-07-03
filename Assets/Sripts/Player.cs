using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Player : MonoBehaviour
{
    private Vector3 velocity;
    public float gravity = -9.8f;
    public float gravityScale = 10f;
    public float jumpForce = 80f;
    private bool isAlive = true;

    public TextMeshProUGUI gameOverText;

    private void Update()
    {
        if (!isAlive) return;

        velocity.y += gravity * gravityScale * Time.deltaTime;

        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame ||
            Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            velocity.y = jumpForce;
        }

        transform.position += velocity * Time.deltaTime;
    }

    public void GameOver()
    {
        if (!isAlive) return;

        isAlive = false;
        velocity = Vector3.zero;
        Time.timeScale = 0f;

        if (gameOverText != null)
        {
            gameOverText.text = "Game Over";
            gameOverText.gameObject.SetActive(true);
        }
    }
}