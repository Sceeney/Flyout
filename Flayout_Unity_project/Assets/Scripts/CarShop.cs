using System;
using UnityEngine;
using UnityEngine.Events;
using YG;

public class CarShop : MonoBehaviour
{
    [SerializeField] private Car[] _cars;
    [SerializeField] private Wallet _wallet;

    [SerializeField] private UnityEvent<Car> _purchased;

    private int _lastSelectedCarIndex => YandexGame.savesData.LastSelectedCarIndex;

    private void Start()
    {
        _cars = GetComponentsInChildren<Car>();
    }

    public void TryPurchase()
    {
        if (CheckPossibilityPurchase())
        {
            _purchased?.Invoke(_cars[_lastSelectedCarIndex]);
        }
        else
        {
            Console.WriteLine("purchased impossible");
        }
    }

    private bool CheckPossibilityPurchase()
    {
        return _cars[_lastSelectedCarIndex].Price <= _wallet.Money;
    }
}