using System;
using UnityEngine;
using UnityEngine.Events;
using YG;

public class CarShop : MonoBehaviour, ISaver
{
    [SerializeField] private Car[] _cars;
    [SerializeField] private Wallet _wallet;

    [SerializeField] private UnityEvent<Car> _purchased;
    [SerializeField] private UnityEvent _changeCar;

    private int _lastSelectedCarIndex => YandexGame.savesData.LastSelectedCarIndex;
    
    private void OnEnable() => YandexGame.GetDataEvent += GetData;
    private void OnDisable() => YandexGame.GetDataEvent -= GetData;

    private void Start()
    {
        _cars = GetComponentsInChildren<Car>();

        if (YandexGame.SDKEnabled == true)
        {
            GetData();
        }
    }

    public void OnTryPurchase()
    {
        if (CheckPossibilityPurchase())
        {
            _purchased?.Invoke(_cars[_lastSelectedCarIndex]);
            _changeCar?.Invoke();
            Save();
        }
        else
        {
            Debug.Log("Purchase impossible");
        }
    }

    public void OnNextCar(int key)
    {
        int index = _lastSelectedCarIndex + key;

        if (index < 0)
        {
            index = _cars.Length - 1;
        }
        else if(index >= _cars.Length)
        {
            index = 0;
        }

        YandexGame.savesData.LastSelectedCarIndex = index;
        _changeCar?.Invoke();

        Save();
    }

    private void GetData()
    {
        //YandexGame.LoadLocal();

        Garage garage = YandexGame.savesData.Garage;

        if (garage != null) SyncShop(garage);
    }

    private void SyncShop(Garage garage)
    {
        for (int i = 0; i < garage.CarCount; i++)
        {
            Car saveCar = garage.GetCarByIndex(i);

            for (int j = 0; j < _cars.Length; j++)
            {
                Car car = _cars[j];

                if (car.Name == saveCar.Name)
                {
                    car.Purchase();
                }
            }
        }
    }

    private bool CheckPossibilityPurchase()
    {
        return _cars[_lastSelectedCarIndex].Price <= _wallet.Money;
    }

    public void Save()
    {
        YandexGame.SaveLocal();

#if !UNITY_EDITOR
        YandexGame.SaveProgress();
#endif
    }
}