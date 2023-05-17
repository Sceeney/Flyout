using System;
using UnityEngine;
using UnityEngine.UI;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private Text _moneyUIObject;
    [SerializeField] private Wallet _wallet;
    private int Money => _wallet.Money;

    private void OnValidate()
    {
        if (_wallet == null)
            throw new ArgumentNullException(nameof(_wallet));
    }

    private void OnEnable()
    {
        _wallet.UpdateMoney += OnUpdateMoney;
    }

    private void OnDisable()
    {
        _wallet.UpdateMoney -= OnUpdateMoney;
    }

    private void OnUpdateMoney(int value)
    {
        _moneyUIObject.text = value.ToString();
    }
}