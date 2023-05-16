using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CarShop : YandexDataReader
{
    [SerializeField] private Car[] _cars;
    [SerializeField] private Wallet _wallet;

    public event UnityAction<int> LasrSelectedCarIndexChanged;
    public event UnityAction<Car> Purchased;

    private int _lastSelectedCarIndex = 0;

    public int LastSelectedCarIndex => _lastSelectedCarIndex;

    private void Awake()
    {
        _cars = GetComponentsInChildren<Car>();
    }

    public void OnTryPurchase()
    {
        if (CheckPossibilityPurchase())
        {
            Car purchasedCar = _cars[_lastSelectedCarIndex];
            purchasedCar.Purchase();

            Purchased?.Invoke(purchasedCar);
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

        _lastSelectedCarIndex = index;
        LasrSelectedCarIndexChanged?.Invoke(_lastSelectedCarIndex);
    }

    protected override void OnDataUpdated()
    {
        _lastSelectedCarIndex = Saver.LastSelectedCarIndex;

        LasrSelectedCarIndexChanged?.Invoke(_lastSelectedCarIndex);

        Car car = _cars.First(c => c.IsDefaultActive == true);

        if (car.Index != _lastSelectedCarIndex)
        {
            car.gameObject.SetActive(false);
            _cars[_lastSelectedCarIndex].gameObject.SetActive(true);
        }
    }

    private bool CheckPossibilityPurchase()
    {
        return _cars[_lastSelectedCarIndex].Price <= _wallet.Money;
    }
}