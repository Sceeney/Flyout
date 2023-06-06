using UnityEngine;
using UnityEngine.Events;

public class Collision_trigger : MonoBehaviour
{
    public event UnityAction TriggerOUT;
    public event UnityAction TriggerWIRE;

    private void OnCollisionEnter(Collision other)
    {
        if (gameObject.TryGetComponent(out OutZone zone))
            TriggerOUT?.Invoke();

        if (gameObject.TryGetComponent(out Wire wire))
            TriggerWIRE?.Invoke();
    }
}
