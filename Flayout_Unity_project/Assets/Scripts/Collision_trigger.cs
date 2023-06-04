using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collision_trigger : MonoBehaviour
{
    public static event UnityAction TriggerOUT;
    public static event UnityAction TriggerWIRE;

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent(out Body body))
        {
            if (gameObject.TryGetComponent(out OutZone zone))
                TriggerOUT?.Invoke();

            if (gameObject.TryGetComponent(out Wire wire))
                TriggerWIRE?.Invoke();
        }
   }
}
