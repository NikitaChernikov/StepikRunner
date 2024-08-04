using System;
using System.Collections;
using UnityEngine;

public class CharacterAcceleration : MonoBehaviour
{
    [SerializeField] private float _periodToAccelerate = 30;
    [SerializeField] private float _amountOfAcceleration = 5;

    public static event Action<float> OnAccelerate;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IncreaseSpeedOverTime());
    }

    private IEnumerator IncreaseSpeedOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(_periodToAccelerate);
            OnAccelerate?.Invoke(_amountOfAcceleration);
        }
    }
}
