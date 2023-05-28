using System;
using TMPro;
using UnityEngine;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyUIObject;
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
        Debug.Log("moneyUpdated");
        _moneyUIObject.text = value.ToString();
    }
}