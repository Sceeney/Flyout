using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using YG;

public class CarShop : MonoBehaviour
{
    [SerializeField] private CarList _cars;
    [SerializeField] private Wallet _wallet;

    private int _lastSelectedCarIndex = 0;
    private int _currentSelectedCarIndex;

    public event UnityAction<int> DisplayedCarChanged;
    public event UnityAction<Car> Purchased;

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
        if(_currentSelectedCarIndex == index)
        {
            if (_cars.Cars[index].IsBuyed)
            {
                SelectCar(index);
            }
            else
            {
                if (CanPurchase(index))
                {
                    Purchase(index);
                    SelectCar(index);
                }
                else
                {
                    Debug.Log("Purchase impossible");
                }
            }
        }
        else
        {
            ShowCar(index);
        }
    }

    public void OnDataSaving()
    {
        SelectCar(_lastSelectedCarIndex);

        YandexGame.savesData.LastSelectedCarIndex = _lastSelectedCarIndex;

        bool[] temp = new bool[_cars.Cars.Length];
        for (int i = 0; i < _cars.Cars.Length; i++)
            temp[i] = _cars.Cars[i].IsBuyed;

        YandexGame.savesData.BuyedCar = temp;
    }

    public bool CanPurchase(int index)
    {
        return CheckPossibilityPurchase(index) 
            && !_cars.Cars[index].IsBuyed
            && CheckSelectedCar(index);
    }

    private void OnDataUpdated()
    {
        for (int i = 0; i < YandexGame.savesData.BuyedCar.Length; i++)
        {
            bool isBuyed = YandexGame.savesData.BuyedCar[i];
            if (isBuyed)
                _cars.Cars[i].Purchase();
        }


        _currentSelectedCarIndex = YandexGame.savesData.LastSelectedCarIndex;

        SelectCar(_currentSelectedCarIndex);
    }

    private void Purchase(int index)
    {
        Car purchasedCar = _cars.Cars[index];
        purchasedCar.Purchase();

        Purchased?.Invoke(purchasedCar);
    }

    private void ShowCar(int index)
    {
        _currentSelectedCarIndex = index;
        DisplayedCarChanged?.Invoke(_currentSelectedCarIndex);
    }

    private void SelectCar(int index)
    {
        _lastSelectedCarIndex = _currentSelectedCarIndex = index;
        DisplayedCarChanged?.Invoke(_lastSelectedCarIndex);
    }

    private bool CheckPossibilityPurchase(int index)
    {
        return _cars.Cars[index].Price <= _wallet.Money;
    }

    private bool CheckSelectedCar(int index)
    {
        return _currentSelectedCarIndex == index;
    }
}