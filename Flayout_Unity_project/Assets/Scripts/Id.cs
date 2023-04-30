using UnityEngine;

public class Id : MonoBehaviour
{
    private static int _index;

    private int _value;

    public int Value { get { return _value; } }

    public Id(string name)
    {
        _index++;
        _value = _index;

        Debug.Log(_value + name);
    }
}