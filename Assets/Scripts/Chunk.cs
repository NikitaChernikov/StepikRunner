using UnityEngine;

public class Chunk : MonoBehaviour
{
    private ChunkSpawner _chunkSpawner;
    private ObstacleGenerator _obstacleGenerator;
    private CoinGenerator _coinGenerator;
    private float _recycleDelay = 1.0f; // Задержка в секундах

    private void Awake()
    {
        _chunkSpawner = FindObjectOfType<ChunkSpawner>();
        _obstacleGenerator = FindObjectOfType<ObstacleGenerator>(); // Находим генератор
        _coinGenerator = FindObjectOfType<CoinGenerator>(); // Находим генератор монет
    }
    private void Start()
    {
        _obstacleGenerator.SpawnObstacles(transform); // Генерируем препятствия
        _coinGenerator.SpawnCoins(transform); // Генерируем монеты
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("Recycle", _recycleDelay); // Используем Invoke для задержки
        }
    }

    private void Recycle()
    {
        RecycleObstacle();
        RecycleCoins();
        _chunkSpawner.RecycleChunk(gameObject); // Возвращаем чанк в пул
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