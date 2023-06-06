using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using YG;

public class CarShop : MonoBehaviour
{
    [SerializeField] private CarList _cars;
    [SerializeField] private Wallet _wallet;

    public event UnityAction<int> LastSelectedCarIndexChanged;
    public event UnityAction<Car> Purchased;

    private int _lastSelectedCarIndex = 0;

    private void OnEnable()
    {
        YandexGame.GetDataEvent += OnDataUpdated;
    }

    private void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            OnDataUpdated();
        }
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= OnDataUpdated;
    }

    public void OnCellClick(int index)
    {
        if (_cars.Cars[index].IsBuyed)
        {
            Select(index);
        }
        else
        {
            if (CanPurchase(index))
            {
                Purchase(index);
                Select(index);
            }
            else
            {
                Debug.Log("Purchase impossible");
            }
        }
    }

    public void OnDataSaving()
    {
        YandexGame.savesData.LastSelectedCarIndex = _lastSelectedCarIndex;

        bool[] temp = new bool[_cars.Cars.Length];
        for (int i = 0; i < _cars.Cars.Length; i++)
            temp[i] = _cars.Cars[i].IsBuyed;

        YandexGame.savesData.BuyedCar = temp;
    }

    public bool CanPurchase(int index)
    {
        return CheckPossibilityPurchase(index) 
            && !_cars.Cars[index].IsBuyed;
    }

    private void OnDataUpdated()
    {
        for (int i = 0; i < YandexGame.savesData.BuyedCar.Length; i++)
        {
            bool isBuyed = YandexGame.savesData.BuyedCar[i];
            if (isBuyed)
                _cars.Cars[i].Purchase();
        }
        
        Select(YandexGame.savesData.LastSelectedCarIndex);
    }

    private void Purchase(int index)
    {
        Car purchasedCar = _cars.Cars[index];
        purchasedCar.Purchase();

        Purchased?.Invoke(purchasedCar);
    }

    private void Select(int index)
    {
        _lastSelectedCarIndex = index;
        LastSelectedCarIndexChanged?.Invoke(_lastSelectedCarIndex);
    }

    private bool CheckPossibilityPurchase(int index)
    {
        return _cars.Cars[index].Price <= _wallet.Money;
    }
}