using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour
{
    [Header("Coin Settings")]
    [Tooltip("The amount of points rewarded for collecting this coin.")]
    public int scoreValue = 3;

    [Tooltip("How high the coin floats up during the collection animation.")]
    public float floatDistance = 1.5f;

    [Tooltip("Duration of the collection animation in seconds.")]
    public float animationDuration = 0.4f;

    [Header("Auto Disable Settings")]
    [Tooltip("If checked, the coin will disappear after a random period if not collected.")]
    public bool enableAutoDisable = true;

    [Tooltip("Minimum time in seconds before the coin disappears.")]
    public float minLifetime = 1.0f;

    [Tooltip("Maximum time in seconds before the coin disappears.")]
    public float maxLifetime = 3.0f;

    private bool isCollected = false;
    private Collider2D coinCollider;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        coinCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (coinCollider != null)
        {
            coinCollider.isTrigger = true;
        }

        if (enableAutoDisable)
        {
            StartCoroutine(AutoDisableRoutine());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCollected) return;

        if (other.GetComponent<BirdController>() != null)
        {
            Collect();
        }
    }

    private void Collect()
    {
        isCollected = true;

        if (coinCollider != null)
        {
            coinCollider.enabled = false;
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(scoreValue);
        }

        StartCoroutine(CollectAnimation());
    }

    private IEnumerator CollectAnimation()
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + Vector3.up * floatDistance;
        Vector3 startScale = transform.localScale;

        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / animationDuration;

            float tScaled = 1f - Mathf.Pow(1f - t, 3);
            transform.position = Vector3.Lerp(startPos, targetPos, tScaled);

            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, t);

            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a = Mathf.Lerp(1f, 0f, t);
                spriteRenderer.color = color;
            }

            yield return null;
        }

        Destroy(gameObject);
    }

    private IEnumerator AutoDisableRoutine()
    {

        float lifetime = Random.Range(minLifetime, maxLifetime);
        yield return new WaitForSeconds(lifetime);

        if (!isCollected)
        {
            isCollected = true;

            if (coinCollider != null)
            {
                coinCollider.enabled = false;
            }

            StartCoroutine(FadeOutRoutine());
        }
    }

    private IEnumerator FadeOutRoutine()
    {
        Vector3 startScale = transform.localScale;
        float elapsed = 0f;
        float fadeDuration = 0.3f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, t);

            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a = Mathf.Lerp(1f, 0f, t);
                spriteRenderer.color = color;
            }

            yield return null;
        }

        Destroy(gameObject);
    }
}
