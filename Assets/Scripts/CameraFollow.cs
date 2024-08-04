using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform; // —сылка на трансформ игрока
    [SerializeField] private Vector3 _offset; // —мещение камеры относительно игрока
    [SerializeField] private float _smoothSpeed = 0.125f; // —корость интерпол€ции

    private void LateUpdate()
    {
        // ÷елева€ позици€ камеры
        Vector3 desiredPosition = _playerTransform.position + _offset;

        // »нтерпол€ци€ текущей позиции камеры к целевой
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);

        // ”станавливаем новую позицию камеры
        transform.position = smoothedPosition;
    }
}