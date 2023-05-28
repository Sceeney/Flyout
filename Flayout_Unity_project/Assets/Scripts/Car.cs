using UnityEngine;
using System;

public class Car : MonoBehaviour
{
    [SerializeField] private CarCellInfo _cellInfo;
    [SerializeField] private bool _isBuyed;

    public int Index => _cellInfo.Index;
    public int Price => _cellInfo.Price;
    public bool IsBuyed => _isBuyed;

    private void OnValidate()
    {
        if (_cellInfo == null)
            throw new ArgumentNullException($"{_cellInfo}");
    }

    public void Purchase()
    {
        _isBuyed = true;
    }
}
