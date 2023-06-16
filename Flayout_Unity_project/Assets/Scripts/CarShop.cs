using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using YG;

public class CarShop : MonoBehaviour
{
    [SerializeField] private CarList _cars;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private GameObject[] _characteristicsCar;
 
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
        else
        {
            if (IsBuyed(index))
            {
                Debug.Log("Buyed");
                SelectCar(index);
            }
            else
            {
                ShowCar(index);
            }
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
        return IsEnoughMoney(index) 
            && !IsBuyed(index)
            && IsSelectedCar(index);
    }

    private void ShowCharacteristic(int index)
    {
        _characteristicsCar.First(c => c.activeSelf == true)
            .SetActive(false);

        _characteristicsCar[index].SetActive(true);
    }

    private void OnDataUpdated()
    {
        //YandexGame.ResetSaveProgress();

        for (int i = 0; i < YandexGame.savesData.BuyedCar.Length; i++)
        {
            bool isBuyed = YandexGame.savesData.BuyedCar[i];
            if (isBuyed)
                _cars.Cars[i].Purchase();
        }


        _currentSelectedCarIndex = YandexGame.savesData.LastSelectedCarIndex;

        SelectCar(_currentSelectedCarIndex);
    }

    public bool IsBuyed(int index)
    {
        return _cars.Cars[index].IsBuyed;
    }

    public bool IsEnoughMoney(int index)
    {
        return _cars.Cars[index].Price <= _wallet.Money;
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
        ShowCharacteristic(index);
    }

    private void SelectCar(int index)
    {
        _lastSelectedCarIndex = _currentSelectedCarIndex = index;
        DisplayedCarChanged?.Invoke(_lastSelectedCarIndex);
        ShowCharacteristic(index);
    }

    private bool IsSelectedCar(int index)
    {
        return _currentSelectedCarIndex == index;
    }
}