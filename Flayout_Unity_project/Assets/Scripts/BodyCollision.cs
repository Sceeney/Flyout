using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BodyCollision : MonoBehaviour
{
    [SerializeField] private Impulse_and_Mass _impulse_And_Mass;

    public event UnityAction TriggerOUT;
    public event UnityAction TriggerWIRE;

    private void OnEnable()
    {
        TriggerOUT += _impulse_And_Mass.OnTriggerOut;
        TriggerWIRE += _impulse_And_Mass.OnTriggerWire;
    }

    private void OnDisable()
    {
        TriggerOUT -= _impulse_And_Mass.OnTriggerOut;
        TriggerWIRE -= _impulse_And_Mass.OnTriggerWire;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out OutZone zone))
            TriggerOUT?.Invoke();

        if (other.gameObject.TryGetComponent(out Wire wire))
            TriggerWIRE?.Invoke();
    }
}
