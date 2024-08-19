using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab; // ������ ������
    [SerializeField] private int _poolSize = 50; // ������ ���� ��� �����
    [SerializeField] private float _coinSpacing = 2f; // ���������� ����� ��������

    private Queue<GameObject> _coinPool = new Queue<GameObject>(); // ��� �����
    private float[] _lanes = { -2.5f, 2.5f, 7.5f }; // ���������� �� ��� X ��� �����
    private float _coinHeight = 4.3f; // ������ ������ �� ��� Y

    // Start is called before the first frame update
    void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        // ������������� ���� �����
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
            // ���� ��� ����, ������� ����� ������
            GameObject newCoin = Instantiate(_coinPrefab);
            newCoin.SetActive(false);
            return newCoin;
        }
    }

    public void SpawnCoins(Transform chunkTransform)
    {
        // �������� ��������� �����
        float laneX = _lanes[Random.Range(0, _lanes.Length)];
        // ��������� ���������� ����� �� 3 �� 5
        int coinsToSpawn = Random.Range(3, 6);
        // ��������� ������� �� Z
        float startZ = Random.Range(0, 36f); // 36f ����� 5 ����� ��������� � �����

        for (int i = 0; i < coinsToSpawn; i++)
        {
            GameObject coin = GetCoinFromPool();

            float positionZ = startZ + i * _coinSpacing; 

            coin.transform.position = new Vector3(laneX, _coinHeight, chunkTransform.position.z + positionZ);
            coin.transform.parent = chunkTransform; // ����������� ������ � �����
            coin.SetActive(true);
        }
    }

    public void RecycleCoin(GameObject coin)
    {
        coin.SetActive(false);
        coin.transform.parent = null; // ���������� �� �����
        _coinPool.Enqueue(coin);
    }
}
