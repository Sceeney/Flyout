using UnityEngine;
using UnityEngine.Events;
using YG;

public class Wallet : MonoBehaviour
{
    [SerializeField] private MoneyView _moneyView;
    private int _money;

    public int Money { get { return _money; } }

    private UnityAction<int> _updateMoney;

    private void OnEnable() => _updateMoney += _moneyView.OnUpdateMoney;
    private void OnDisable() => _updateMoney -= _moneyView.OnUpdateMoney;

    private void Start()
    {
        SetMoney(267324);
        GetMoney();
    }

    public int GetMoney()
    {
        _money = YandexGame.savesData.Money;

        return _money;
    }

    public void SetMoney(int value)
    {
        YandexGame.savesData.Money = value;

        _updateMoney.DynamicInvoke(value);
    }
}