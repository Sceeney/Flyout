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
    }

    private void OnDisable()
    {
        DataSaving -= _carShop.OnDataSaving;
        DataSaving -= _wallet.OnDataSaving;
    }

    public void Save()
    {
        DataSaving?.Invoke();

        YandexGame.SaveProgress();
    }

    public void OnResetProgress()
    {
        YandexGame.ResetSaveProgress();

        YandexGame.savesData.Money = 0;
        YandexGame.savesData.LastSelectedCarIndex = 0;
        YandexGame.savesData.BuyedCar = new bool[1] { false };

        YandexGame.SaveProgress();

        Debug.Log("Reset");
    }
}
