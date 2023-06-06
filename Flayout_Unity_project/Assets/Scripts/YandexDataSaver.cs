using System;
using UnityEngine;
using UnityEngine.Events;
using YG;

[RequireComponent(typeof(CarShop))]
public class YandexDataSaver : MonoBehaviour
{
    [SerializeField] private CarList _cars;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private CarShop _carShop;

    public event UnityAction DataSaving;

    private void OnValidate()
    {
        if (_wallet == null)
            throw new ArgumentNullException(nameof(_wallet));

        if (_carShop == null)
            _carShop = GetComponent<CarShop>();
    }

    private void OnEnable()
    {
        DataSaving += _carShop.OnDataSaving;
        DataSaving += _wallet.OnDataSaving;
        YandexGame.onResetProgress += OnResetProgress;
    }

    private void OnDisable()
    {
        DataSaving -= _carShop.OnDataSaving;
        DataSaving -= _wallet.OnDataSaving;
        YandexGame.onResetProgress -= OnResetProgress;
    }

    public void Save()
    {
        DataSaving?.Invoke();

        YandexGame.SaveProgress();
    }

    private void OnResetProgress()
    {
        Debug.Log("Reset");
        YandexGame.savesData.Money = 0;
        YandexGame.savesData.LastSelectedCarIndex = 0;
        YandexGame.savesData.BuyedCar = new bool[1] { false };
    }
}
