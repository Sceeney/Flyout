using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Camera_Trigger : MonoBehaviour
{
    [SerializeField] private Camera_Mover _mover;

    private event UnityAction<Transform> _changePosition;

    private void OnEnable()
    {
        _changePosition += _mover.OnChangeCameraPosition;
    }

    private void OnDisable()
    {
        _changePosition -= _mover.OnChangeCameraPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Camera_Mover mover))
        {
            _changePosition?.DynamicInvoke(transform.position);
        }
    }
}
