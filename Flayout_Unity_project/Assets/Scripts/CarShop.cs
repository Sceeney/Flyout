using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CarShop : MonoBehaviour
{
    [SerializeField] private CarList _cars;
    [SerializeField] private Wallet _wallet;

    public event UnityAction<int> LastSelectedCarIndexChanged;
    public event UnityAction<Car> Purchased;

    public int LastSelectedCarIndex = 0;


    public void OnCellClick(int index)
    {
        LastSelectedCarIndex = index;
        LastSelectedCarIndexChanged?.Invoke(LastSelectedCarIndex);

        TryPurchase(index);
    }

    public bool CanPurchase(int index)
    {
        return CheckPossibilityPurchase(index) && !_cars.Cars[index].IsBuyed;
    }

    public void OnDataUpdated()
    {
        LastSelectedCarIndexChanged?.Invoke(LastSelectedCarIndex);

        Car car = _cars.Cars.First(c => c.IsDefaultActive == true);

        if (car.Index != LastSelectedCarIndex)
        {
            car.gameObject.SetActive(false);
            _cars.Cars[LastSelectedCarIndex].gameObject.SetActive(true);
        }
    }

    private void TryPurchase(int index)
    {
        if (CanPurchase(index))
        {
            Car purchasedCar = _cars.Cars[index];
            purchasedCar.Purchase();

            Purchased?.Invoke(purchasedCar);
        }
        else
        {
            Debug.Log("Purchase impossible");
        }
    }

    private bool CheckPossibilityPurchase(int index)
    {
        return _cars.Cars[index].Price <= _wallet.Money;
    }
}