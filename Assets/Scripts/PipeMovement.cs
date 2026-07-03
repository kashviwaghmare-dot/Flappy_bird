using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    [Tooltip("Speed at which the pipes move. Should match the ground scrolling speed.")]
    public float moveSpeed = 3f;

    [Tooltip("The X position at which the pipe is considered off-screen and will be destroyed.")]
    public float destroyXBoundary = -15f;

    [Header("Coin Settings")]
    [Tooltip("Chance (0 to 1) of a coin appearing in this pipe column.")]
    [Range(0f, 1f)]
    public float coinSpawnChance = 0.4f;

    void Start()
    {

        Transform coinChild = transform.Find("Coin");
        if (coinChild != null)
        {
            coinChild.gameObject.SetActive(Random.value < coinSpawnChance);
        }
    }

    void Update()
    {

        if (GameManager.Instance != null && !GameManager.Instance.isPlaying)
        {
            return;
        }

        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        if (transform.position.x < destroyXBoundary)
        {
            Destroy(gameObject);
        }
    }
}
