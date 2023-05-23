using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Mover : MonoBehaviour
{
    [SerializeField] private Transform _startCameraPosition;
    [SerializeField] private Transform _defaultCameraPosition;
    
    private Transform _currentCameraPosition;
    private Coroutine _smoothMoveCameraCoroutine;

    public float speed_cam = 2; // скорость движения камеры

    private void Start()
    {
        _currentCameraPosition = _startCameraPosition;
        OnClick(_defaultCameraPosition);
    }

    public void OnChangeCameraPosition(Transform newPosition)
    {
        _currentCameraPosition = newPosition;
    }

    public void OnClick(Transform targetCameraPosition)
    {
        //targetCameraPosition = _defaultCameraPosition;
        if (_smoothMoveCameraCoroutine != null)
        {
            StopCoroutine(_smoothMoveCameraCoroutine);
        }

        _smoothMoveCameraCoroutine = StartCoroutine(SmoothMoveCamera(targetCameraPosition));
    }

    private IEnumerator SmoothMoveCamera(Transform targetCameraPosition)
    {
        while (_currentCameraPosition != targetCameraPosition)
        {
                transform.position = Vector3.Lerp(transform.position, targetCameraPosition.position, speed_cam * Time.deltaTime);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetCameraPosition.localRotation, speed_cam * Time.deltaTime);

            yield return null;
        }
    }
}
