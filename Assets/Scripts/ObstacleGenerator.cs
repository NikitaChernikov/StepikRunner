using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _obstaclePrefabs; // Префабы препятствий
    [SerializeField] private int _poolSize = 2; // Размер пула
    [SerializeField] private int _obstaclesPerChunk = 3; // Количество препятствий на чанк

    private Queue<GameObject> _obstaclePool = new Queue<GameObject>(); // Пул препятствий
    private float[] _lanes = { -2f, 2.5f, 7.5f }; // Координаты по оси X для линий

    private void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        // Инициализация пула объектов
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject obstacle = Instantiate(_obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Length)]);
            obstacle.SetActive(false);
            _obstaclePool.Enqueue(obstacle);
        }
    }

    public void SpawnObstacles(Transform chunkTransform)
    {
        for (int i = 0; i < _obstaclesPerChunk; i++)
        {
            GameObject obstacle = GetObstacleFromPool();

            float laneX = _lanes[Random.Range(0, _lanes.Length)];
            float positionZ = Random.Range(0, 40f); // Позиция Z внутри чанка

            obstacle.transform.position = new Vector3(laneX, 2.5f, chunkTransform.position.z + positionZ);
            obstacle.transform.parent = chunkTransform; // Привязываем препятствие к чанку
            obstacle.SetActive(true);

        }
    }

    private GameObject GetObstacleFromPool()
    {
        if (_obstaclePool.Count > 0)
        {
            return _obstaclePool.Dequeue();
        }
        else
        {
            // Если пул пуст, можно создать новый объект или расширить пул
            GameObject newObstacle = Instantiate(_obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Length)]);
            newObstacle.SetActive(false);
            return newObstacle;
        }
    }

    public void RecycleObstacle(GameObject obstacle)
    {
        obstacle.SetActive(false);
        obstacle.transform.parent = null; // Отвязываем от чанка
        _obstaclePool.Enqueue(obstacle);
    }
}
