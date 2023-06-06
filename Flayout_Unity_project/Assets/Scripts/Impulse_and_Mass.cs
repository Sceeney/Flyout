using UnityEngine;
using UnityEngine.Events;

public enum Value
{
    Height,
    Distance
}

public class Impulse_and_Mass : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;

    private ValueGetter[] _valueGetter;
    private bool _isTriggered;

    public bool Trigger { get; private set; }

    public event UnityAction TriggerWire;
    public event UnityAction TriggerOut;

    private delegate float ValueGetter();

    void Start()
    {
        _isTriggered = false;
        Trigger = true;
        audioSource = GetComponent <AudioSource> ();
        rb = GetComponent <Rigidbody>();
        rb.velocity = (Velocity_Car.vel / 2)+(AIM_Shot.Speed_shoot * Random.Range((AIM_Shot.Force_Shoot -1), (AIM_Shot.Force_Shoot +1)));
        rb.mass = 10f;

        ValueGetter height = new(GetHeight);
        ValueGetter distance = new(GetDistance);

        _valueGetter = new ValueGetter[]
        {
            height,
            distance
        };
    }

    public void OnTriggerWire()
    {
        if (_isTriggered)
            return;
        _isTriggered = true;

        Collision();
        TriggerWire?.Invoke();
    }

    public void OnTriggerOut()
    {
        if (_isTriggered)
            return;
        _isTriggered = true;

        Collision();
        TriggerOut?.Invoke();
    }

    public float GetValue(Value value)
    {
        return _valueGetter[(int)value].Invoke();
    }

    private void Collision()
    {
        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);
        Trigger = false;

        audioSource.Play();
    }

    private float GetHeight() => transform.position.y;

    private float GetDistance() => transform.position.z;
}
