using UnityEngine;
using UnityEngine.Events;

public class CarShop : YandexDataReader
{
    [SerializeField] private Car[] _cars;
    [SerializeField] private Wallet _wallet;

    [SerializeField] private UnityEvent<Car> _purchased;
    [SerializeField] private UnityEvent _carChanged;

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
            _purchased?.Invoke(_cars[_lastSelectedCarIndex]);
            _carChanged?.Invoke();
        }
        else
        {
            Debug.Log("Purchase impossible");
        }
    }

    public void OnPurchase(Car car)
    {
        car.Purchase();
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
        _carChanged?.Invoke();
    }

    protected override void OnDataUpdated()
    {
        _lastSelectedCarIndex = Saver.LastSelectedCarIndex;
    }

    private bool CheckPossibilityPurchase()
    {
        return _cars[_lastSelectedCarIndex].Price <= _wallet.Money;
    }
}