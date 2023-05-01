using System.Collections.Generic;
using UnityEngine;
using YG;

public class Garage : MonoBehaviour, ISaver
{
    private List<Car> _cars = new();

    public int CarCount { get { return _cars.Count; } }

    private void OnEnable() => YandexGame.GetDataEvent += GetData;
    private void OnDisable() => YandexGame.GetDataEvent -= GetData;

    private void Start()
    {
        Car[] cars = GetComponentsInChildren<Car>();

        foreach (Car car in cars)
        {
            if (car.IsPurchased)
            {
                _cars.Add(car);
            }
        }

        if (YandexGame.SDKEnabled == true)
        {
            GetData();
        }
    }

    public void Save()
    {
        YandexGame.savesData.Garage = this;

        YandexGame.SaveLocal();

#if !UNITY_EDITOR
        YandexGame.SaveProgress();
#endif
    }

    public void OnPurchase(Car car)
    {
        car.Purchase();
        _cars.Add(car);

        Save();
    }

    public Car GetCarByIndex(int index)
    {
        if (index < 0 || index >= _cars.Count)
        {
            return null;
        }

        return _cars[index];
    }

    private void GetData()
    {
        //YandexGame.LoadLocal();

        Garage garage = YandexGame.savesData.Garage;

        if (garage != null) SyncGarage(garage);
    }

    private void SyncGarage(Garage garage)
    {
        for (int i = 0; i < garage.CarCount; i++)
        {
            _cars.Add(garage.GetCarByIndex(i));
        }
    }
}
