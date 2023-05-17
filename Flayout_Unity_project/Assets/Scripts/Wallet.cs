using UnityEngine.Events;
using UnityEngine;
using System;

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
        _shop.Purchased += OnPurchased;
    }

    private void OnDisable()
    {
        _shop.Purchased -= OnPurchased;
    }

    public void OnSetMoney()
    {
        Money += 100000;

        UpdateMoney?.Invoke(Money);
    }

    private void OnPurchased(Car car)
    {
        Money -= car.Price;

        UpdateMoney?.Invoke(Money);
    }
    
    public void OnDataUpdated()
    {
        //Money = Saver.Money;
        Debug.Log($"Wallet.Money = {Money}");
        UpdateMoney?.Invoke(Money);
    }
}