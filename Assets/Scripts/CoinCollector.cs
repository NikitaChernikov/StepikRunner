using System;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public static event Action OnCollectCoin;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            OnCollectCoin?.Invoke();
            other.gameObject.SetActive(false);
        }
    }
}
