using UnityEngine.Events;
using UnityEngine;
using System;

public class Wallet : YandexDataReader
{
    [SerializeField] private CarShop _shop;

    private int _money;

    public int Money => _money;

    public event UnityAction<int> UpdateMoney;

    private void OnValidate()
    {
        if (_shop == null)
            throw new ArgumentNullException(nameof(_shop));
    }

    private void OnEnable()
    {
        _shop.Purchased += OnPurchased;
    }

    private void OnDisable()
    {
        _shop.Purchased -= OnPurchased;
    }

    public void OnSetMoney()
    {
        _money += 100000;

        UpdateMoney?.Invoke(_money);
    }

    private void OnPurchased(Car car)
    {
        _money -= car.Price;

        UpdateMoney?.Invoke(_money);
    }

    protected override void OnDataUpdated()
    {
        _money = Saver.Money;

        UpdateMoney?.Invoke(_money);
    }
}