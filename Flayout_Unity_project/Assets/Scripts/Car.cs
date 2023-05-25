using UnityEngine;
using System;

public class Car : MonoBehaviour
{
    [SerializeField] private CarCellInfo _cellInfo;
    [SerializeField] private bool _isBuyed;

    public int Index => _cellInfo.Index;
    public int Price => _cellInfo.Price;
    public bool IsBuyed => _isBuyed;
    public bool IsDefaultActive => _cellInfo.IsDefaultActive;

    private void OnValidate()
    {
        if (_cellInfo == null)
            throw new ArgumentNullException($"{_cellInfo}");
    }

    private void Start()
    {
        if (IsDefaultActive == false)
        {
            gameObject.SetActive(false);
        }
    }

    public void Purchase()
    {
        _isBuyed = true;
    }
}
