using UnityEngine;

public class Chunk : MonoBehaviour
{
    private ChunkSpawner _chunkSpawner;
    private ObstacleGenerator _obstacleGenerator;
    private CoinGenerator _coinGenerator;
    private float _recycleDelay = 1.0f; // �������� � ��������

    private void Awake()
    {
        _chunkSpawner = FindObjectOfType<ChunkSpawner>();
        _obstacleGenerator = FindObjectOfType<ObstacleGenerator>(); // ������� ���������
        _coinGenerator = FindObjectOfType<CoinGenerator>(); // ������� ��������� �����
    }
    private void Start()
    {
        _obstacleGenerator.SpawnObstacles(transform); // ���������� �����������
        _coinGenerator.SpawnCoins(transform); // ���������� ������
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("Recycle", _recycleDelay); // ���������� Invoke ��� ��������
        }
    }

    private void Recycle()
    {
        RecycleObstacle();
        RecycleCoins();
        _chunkSpawner.RecycleChunk(gameObject); // ���������� ���� � ���
    }

    private void RecycleObstacle()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Obstacle"))
            {
                _obstacleGenerator.RecycleObstacle(child.gameObject);
            }
        }
    }

    private void RecycleCoins()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Coin"))
            {
                _coinGenerator.RecycleCoin(child.gameObject);
            }
        }
    }
}