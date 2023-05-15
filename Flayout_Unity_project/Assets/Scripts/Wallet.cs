using UnityEngine;
using UnityEngine.Events;

public class Wallet : YandexDataReader
{
    [SerializeField] private MoneyView _moneyView;
    private int _money;

    public int Money => _money;

    [SerializeField] private UnityEvent<int> _updateMoney;

    public void OnPurchased(Car car)
    {
        _money -= car.Price;

        _updateMoney?.Invoke(_money);
    }

    public void OnSetMoney()
    {
        _money += 100000;

        _updateMoney?.Invoke(_money);
    }

    protected override void OnDataUpdated()
    {
        _money = Saver.Money;

        _updateMoney?.Invoke(_money);
    }
}