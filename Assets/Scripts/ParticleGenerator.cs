using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ParticleGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _particlePrefab;
    [SerializeField] private int _poolSize = 10;

    private Queue<GameObject> _particlePool = new Queue<GameObject>();

    private void Start()
    {
        InitializePool();
    }


    private void OnEnable()
    {
        CoinCollector.OnCollectCoin += CoinCollector_OnCollectCoin; // Подписка на событие
    }

    private void OnDisable()
    {
        CoinCollector.OnCollectCoin -= CoinCollector_OnCollectCoin; // Отписка от события
    }

    private void CoinCollector_OnCollectCoin(Vector3 position)
    {
        GameObject particle = GetParticleFromPool();
        particle.transform.position = position;
        particle.SetActive(true);

        StartCoroutine(DeactivateParticle(particle));
    }

    private void InitializePool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject particle = Instantiate(_particlePrefab);
            particle.SetActive(false);
            _particlePool.Enqueue(particle);
        }
    }

    private GameObject GetParticleFromPool()
    {
        if (_particlePool.Count > 0)
        {
            return _particlePool.Dequeue();
        }
        else
        {
            GameObject newParticle = Instantiate(_particlePrefab);
            newParticle.SetActive(false);
            return newParticle;
        }
    }

    private IEnumerator DeactivateParticle(GameObject particle)
    {
        yield return new WaitForSeconds(1f);
        particle.SetActive(false);
        _particlePool.Enqueue(particle);
    }
}
