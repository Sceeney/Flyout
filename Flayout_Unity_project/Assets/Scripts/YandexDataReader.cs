using System;
using UnityEngine;

public abstract class YandexDataReader : MonoBehaviour
{
    [SerializeField] protected YandexDataSaver Saver;

    private void OnValidate()
    {
        if (Saver == null)
            throw new ArgumentNullException(nameof(Saver));
    }

    private void OnEnable()
    {
        //Saver.DataUpdated += OnDataUpdated;
    }

    private void OnDisable()
    {
        //Saver.DataUpdated -= OnDataUpdated;
    }

    protected abstract void OnDataUpdated();
}
