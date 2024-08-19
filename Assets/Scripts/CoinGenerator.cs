using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab; // Префаб монеты
    [SerializeField] private int _poolSize = 50; // Размер пула для монет
    [SerializeField] private float _coinSpacing = 2f; // Расстояние между монетами

    private Queue<GameObject> _coinPool = new Queue<GameObject>(); // Пул монет
    private float[] _lanes = { -2.5f, 2.5f, 7.5f }; // Координаты по оси X для линий
    private float _coinHeight = 4.3f; // Высота монеты по оси Y

    // Start is called before the first frame update
    void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        // Инициализация пула монет
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject coin = Instantiate(_coinPrefab);
            coin.SetActive(false);
            _coinPool.Enqueue(coin);
        }
    }

    private GameObject GetCoinFromPool()
    {
        if (_coinPool.Count > 0)
        {
            return _coinPool.Dequeue();
        }
        else
        {
            // Если пул пуст, создаем новую монету
            GameObject newCoin = Instantiate(_coinPrefab);
            newCoin.SetActive(false);
            return newCoin;
        }
    }

    public void SpawnCoins(Transform chunkTransform)
    {
        // Выбираем случайную линию
        float laneX = _lanes[Random.Range(0, _lanes.Length)];
        // Случайное количество монет от 3 до 5
        int coinsToSpawn = Random.Range(3, 6);
        // Начальная позиция по Z
        float startZ = Random.Range(0, 36f); // 36f чтобы 5 монет умещались в чанке

        for (int i = 0; i < coinsToSpawn; i++)
        {
            GameObject coin = GetCoinFromPool();

            float positionZ = startZ + i * _coinSpacing; 

            coin.transform.position = new Vector3(laneX, _coinHeight, chunkTransform.position.z + positionZ);
            coin.transform.parent = chunkTransform; // Привязываем монету к чанку
            coin.SetActive(true);
        }
    }

    public void RecycleCoin(GameObject coin)
    {
        coin.SetActive(false);
        coin.transform.parent = null; // Отвязываем от чанка
        _coinPool.Enqueue(coin);
    }
}
