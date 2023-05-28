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

    public event UnityAction DataUpdated;

    private void OnValidate()
    {
        if (_wallet == null)
            throw new ArgumentNullException(nameof(_wallet));

        if (_carShop == null)
            _carShop = GetComponent<CarShop>();
    }

    private void OnEnable()
    {
        YandexGame.GetDataEvent += GetLoad;
        YandexGame.onResetProgress += OnResetProgress;
    }

    private void Start()
    {
        if (YandexGame.SDKEnabled)
            GetLoad();
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= GetLoad;
        YandexGame.onResetProgress -= OnResetProgress;
    }
    
    public bool GetIsBuyedCarByIndex(int index)
    {
        return _cars.Cars[index].IsBuyed;
    }

    public void Save()
    {
        YandexGame.savesData.Money = _wallet.Money;

        YandexGame.savesData.LastSelectedCarIndex = _carShop.LastSelectedCarIndex;

        bool[] temp = new bool[_cars.Cars.Length];
        for (int i = 0; i < _cars.Cars.Length; i++)
            temp[i] = _cars.Cars[i].IsBuyed;

        YandexGame.savesData.BuyedCar = temp;

        YandexGame.SaveProgress();
    }

    private void OnResetProgress()
    {
        Debug.Log("Reset");
        YandexGame.savesData.Money = 0;
        YandexGame.savesData.LastSelectedCarIndex = 0;
        YandexGame.savesData.BuyedCar = new bool[1] { false };

        GetLoad();
    }

    private void GetLoad()
    {
        //YandexGame.ResetSaveProgress();

        Debug.Log($"Language - {YandexGame.savesData.language}\n" +
            $"First Session - {YandexGame.savesData.isFirstSession}\n" +
            $"Prompt Done - {YandexGame.savesData.promptDone}\n");

        DataUpdated?.Invoke();
    }
}
