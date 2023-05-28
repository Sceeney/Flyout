using UnityEngine.Events;
using UnityEngine;
using System;
using YG;

public class Wallet : MonoBehaviour
{
    [SerializeField] private CarShop _shop;

    public int Money;

    public event UnityAction<int> UpdateMoney;

    private void OnValidate()
    {
        if (_shop == null)
            throw new ArgumentNullException(nameof(_shop));
    }

    private void OnEnable()
    {
        YandexGame.GetDataEvent += OnDataUpdated;
        _shop.Purchased += OnPurchased;
    }

    private void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            OnDataUpdated();
        }
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= OnDataUpdated;
        _shop.Purchased -= OnPurchased;
    }

    public void OnSetMoney()
    {
        Money += 100000;

        UpdateMoney?.Invoke(Money);
    }

    private void OnDataUpdated()
    {
        Money = YandexGame.savesData.Money;

        UpdateMoney?.Invoke(Money);
    }

    private void OnPurchased(Car car)
    {
        Money -= car.Price;

        UpdateMoney?.Invoke(Money);
    }
}