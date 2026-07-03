using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [Tooltip("The pipe prefab containing top and bottom pipes and a score trigger.")]
    public GameObject pipePrefab;

    [Tooltip("Time in seconds between each pipe spawn.")]
    public float spawnRate = 2f;

    [Tooltip("Minimum vertical offset for spawning.")]
    public float minHeight = -1.5f;

    [Tooltip("Maximum vertical offset for spawning.")]
    public float maxHeight = 2.5f;

    private float timer = 0f;

    void Start()
    {

        ResetSpawner();
    }

    void Update()
    {

        if (GameManager.Instance == null || !GameManager.Instance.isPlaying)
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            SpawnPipe();
            timer = 0f;
        }
    }

    public void SpawnPipe()
    {
        float randomY = Random.Range(minHeight, maxHeight);
        Vector3 spawnPosition = new Vector3(transform.position.x, randomY, transform.position.z);

        GameObject spawnedPipe = Instantiate(pipePrefab, spawnPosition, Quaternion.identity);

        PipeMovement movement = spawnedPipe.GetComponent<PipeMovement>();
        if (movement != null && GameManager.Instance != null)
        {
            movement.moveSpeed = GameManager.Instance.gameSpeed;
        }
    }

    public void ResetSpawner()
    {
        timer = spawnRate;
    }
}
