using System;
using UnityEngine;
using YG;

public abstract class YandexDataReader : MonoBehaviour
{
    private void OnEnable()
    {
        YandexGame.GetDataEvent += OnDataUpdated;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= OnDataUpdated;
    }

    protected abstract void OnDataUpdated();
}
