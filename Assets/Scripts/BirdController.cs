using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdController : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("The vertical force applied when the player flaps.")]
    public float flapForce = 5.5f;

    [Tooltip("The speed at which the bird rotates toward its movement direction.")]
    public float rotationSpeed = 10f;

    [Tooltip("Maximum upward rotation angle (in degrees).")]
    public float maxUpwardAngle = 30f;

    [Tooltip("Minimum downward rotation angle (in degrees).")]
    public float minDownwardAngle = -75f;

    [Header("State")]
    public bool isDead = false;

    private Rigidbody2D rb;
    private Quaternion minRotation;
    private Quaternion maxRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        minRotation = Quaternion.Euler(0, 0, minDownwardAngle);
        maxRotation = Quaternion.Euler(0, 0, maxUpwardAngle);

        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void Update()
    {
        if (isDead)
        {

            transform.rotation = Quaternion.Lerp(transform.rotation, minRotation, rotationSpeed * Time.deltaTime);
            return;
        }

        if (rb.bodyType == RigidbodyType2D.Kinematic)
        {
            if (GetJumpInput())
            {
                StartPhysics();
                Flap();
            }
            return;
        }

        if (GetJumpInput())
        {
            Flap();
        }

        ApplyRotation();
    }

    private bool GetJumpInput()
    {
        bool pointerPressed = Pointer.current != null && Pointer.current.press.wasPressedThisFrame;

        bool spacePressed = Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame;

        return pointerPressed || spacePressed;
    }

    public void StartPhysics()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.linearVelocity = Vector2.zero;
    }

    private void Flap()
    {
        rb.linearVelocity = Vector2.up * flapForce;
    }

    private void ApplyRotation()
    {
        if (rb.linearVelocity.y > 0.5f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, maxRotation, rotationSpeed * 3f * Time.deltaTime);
        }
        else
        {
            float fallProgress = Mathf.Clamp(-rb.linearVelocity.y, 1f, 15f) * 0.4f;
            transform.rotation = Quaternion.Lerp(transform.rotation, minRotation, rotationSpeed * fallProgress * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("Score") || other.GetComponent<ScoreTrigger>() != null)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore();
            }
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        rb.linearVelocity = Vector2.zero;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
    }
}
