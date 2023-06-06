using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BodyCollision : MonoBehaviour
{
    [SerializeField] private Impulse_and_Mass _impulse_And_Mass;

    private Rigidbody rb;
    private float _forceShoot => _impulse_And_Mass.ForceShoot;

    public event UnityAction TriggerOUT;
    public event UnityAction TriggerWIRE;

    private void OnEnable()
    {
        TriggerOUT += _impulse_And_Mass.OnTriggerOut;
        TriggerWIRE += _impulse_And_Mass.OnTriggerWire;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = (Velocity_Car.vel / 2) + (AIM_Shot.Speed_shoot * Random.Range((_forceShoot - 1), (_forceShoot + 1)));
        rb.mass = 10f;
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
