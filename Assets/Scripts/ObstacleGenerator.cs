using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _obstaclePrefabs; // ������� �����������
    [SerializeField] private int _poolSize = 2; // ������ ����
    [SerializeField] private int _obstaclesPerChunk = 3; // ���������� ����������� �� ����

    private Queue<GameObject> _obstaclePool = new Queue<GameObject>(); // ��� �����������
    private float[] _lanes = { -2f, 2.5f, 7.5f }; // ���������� �� ��� X ��� �����

    private void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        // ������������� ���� ��������
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
            float positionZ = Random.Range(0, 40f); // ������� Z ������ �����

            obstacle.transform.position = new Vector3(laneX, 2.5f, chunkTransform.position.z + positionZ);
            obstacle.transform.parent = chunkTransform; // ����������� ����������� � �����
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
            // ���� ��� ����, ����� ������� ����� ������ ��� ��������� ���
            GameObject newObstacle = Instantiate(_obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Length)]);
            newObstacle.SetActive(false);
            return newObstacle;
        }
    }

    public void RecycleObstacle(GameObject obstacle)
    {
        obstacle.SetActive(false);
        obstacle.transform.parent = null; // ���������� �� �����
        _obstaclePool.Enqueue(obstacle);
    }
}
