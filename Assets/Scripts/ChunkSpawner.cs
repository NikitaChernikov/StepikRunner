using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _chunkPrefabs; // ������ �������� ������
    [SerializeField] private int _initialChunks = 5; // ���������� ��������� ������
    [SerializeField] private Transform _playerTransform; // ������� ������ ��� ����������� ���� ���������
    [SerializeField] private float _safeZone = 60f; // ���������� �� ������, �� ������� ���� ��������� ���������

    private Queue<GameObject> _chunkPool = new Queue<GameObject>();
    private float _chunkWidth = 40f; // ������ ������ �����
    private float _spawnZ; // ������� ��� ���������� ������
    
    // Start is called before the first frame update
    void Start()
    {
        // ����� ������� ����������� �����
        SpawnInitialChunk();
        for (int i = 0; i < _initialChunks; i++)
        {
            SpawnChunk();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ���������, ����� �� ������� ����� ����
        if (_playerTransform.position.z + _safeZone > _spawnZ - (_initialChunks * _chunkWidth))
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        GameObject chunk;

        if (_chunkPool.Count > 0)
        {
            chunk = _chunkPool.Dequeue();
            chunk.SetActive(true);
        }
        else
        {
            // ����� ���������� ������� �� ������� (������� � ������� 1, ����� ��������� ������ ����)
            int randomIndex = Random.Range(1, _chunkPrefabs.Length);
            chunk = Instantiate(_chunkPrefabs[randomIndex]);
        }

        chunk.transform.position = new Vector3(0, 0, _spawnZ);
        _spawnZ += _chunkWidth;
    }

    public void RecycleChunk(GameObject chunk)
    {
        chunk.SetActive(false);
        _chunkPool.Enqueue(chunk);
    }

    private void SpawnInitialChunk()
    {
        // ����� ������� ����������� �����
        GameObject initialChunk = Instantiate(_chunkPrefabs[0]);
        initialChunk.transform.position = new Vector3(0, 0, _spawnZ);
        _spawnZ += _chunkWidth;
    }
}
