using UnityEngine;
using UnityEngine.InputSystem;

public class Coin : MonoBehaviour
{
    public float speed = 100f;
    private float topbound;
    public InputAction CoinAction;
    private float leftEdge;

    public void Start()
    {
        CoinAction.Enable();
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 800f;
    }

    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }

    // For trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TRIGGER HIT: " + other.gameObject.name);
        Destroy(gameObject);
    }

    // Fallback for non-trigger collider
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("COLLISION HIT: " + other.gameObject.name);
        Destroy(gameObject);
    }
}