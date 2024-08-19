using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _chunkPrefabs; // Массив префабов чанков
    [SerializeField] private int _initialChunks = 5; // Количество начальных чанков
    [SerializeField] private Transform _playerTransform; // Позиция игрока для определения зоны видимости
    [SerializeField] private float _safeZone = 60f; // Расстояние от игрока, за которое чанк считается невидимым

    private Queue<GameObject> _chunkPool = new Queue<GameObject>();
    private float _chunkWidth = 40f; // Ширина одного чанка
    private float _spawnZ; // Позиция для следующего спавна
    
    // Start is called before the first frame update
    void Start()
    {
        // Спавн первого уникального чанка
        SpawnInitialChunk();
        for (int i = 0; i < _initialChunks; i++)
        {
            SpawnChunk();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Проверяем, нужно ли создать новый чанк
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
            // Выбор случайного префаба из массива (начиная с индекса 1, чтобы исключить первый чанк)
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
        // Спавн первого уникального чанка
        GameObject initialChunk = Instantiate(_chunkPrefabs[0]);
        initialChunk.transform.position = new Vector3(0, 0, _spawnZ);
        _spawnZ += _chunkWidth;
    }
}
