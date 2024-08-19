using System;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public static event Action<Vector3> OnCollectCoin;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            OnCollectCoin?.Invoke(other.transform.position);
            other.gameObject.SetActive(false);
        }
    }
}
