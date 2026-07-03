using UnityEngine;
using UnityEngine.InputSystem;

public class ObstacleManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;   // prefabs from Assets/Pipe folder (Pipe, Pole_1, Pole_2, etc.)
    public InputAction spawnAction;        // optional manual/debug spawn

    public float spawnInterval = 2f;
    public float spawnX = 20f;
    public float minY = -2f;
    public float maxY = 2f;

    private int lastObstacleIndex = -1;    // prevents back-to-back repeats
    private float timer = 0f;

    void Start()
    {
        spawnAction.Enable();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnObstacle();
        }

        if (spawnAction.triggered)
        {
            SpawnObstacle();
        }
    }

    private void SpawnObstacle()
    {
        if (obstaclePrefabs == null || obstaclePrefabs.Length == 0) return;

        int index;
        do
        {
            index = Random.Range(0, obstaclePrefabs.Length);
        } while (obstaclePrefabs.Length > 1 && index == lastObstacleIndex);

        lastObstacleIndex = index;

        float y = Random.Range(minY, maxY);

        Instantiate(
            obstaclePrefabs[index],
            new Vector3(spawnX, y, 0),
            obstaclePrefabs[index].transform.rotation
        );
    }
}