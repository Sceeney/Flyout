using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CarShop : MonoBehaviour
{
    [SerializeField] private Car[] _cars;
    [SerializeField] private Wallet _wallet;

    public event UnityAction<int> LasrSelectedCarIndexChanged;
    public event UnityAction<Car> Purchased;

    public int LastSelectedCarIndex = 0;

    public void OnTryPurchase()
    {
        if (CheckPossibilityPurchase())
        {
            Car purchasedCar = _cars[LastSelectedCarIndex];
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
        int index = LastSelectedCarIndex + key;

        if (index < 0)
        {
            index = _cars.Length - 1;
        }
        else if(index >= _cars.Length)
        {
            index = 0;
        }

        LastSelectedCarIndex = index;
        LasrSelectedCarIndexChanged?.Invoke(LastSelectedCarIndex);
    }

    public void OnDataUpdated()
    {
        LasrSelectedCarIndexChanged?.Invoke(LastSelectedCarIndex);

        Car car = _cars.First(c => c.IsDefaultActive == true);

        if (car.Index != LastSelectedCarIndex)
        {
            car.gameObject.SetActive(false);
            _cars[LastSelectedCarIndex].gameObject.SetActive(true);
        }
    }

    private bool CheckPossibilityPurchase()
    {
        return _cars[LastSelectedCarIndex].Price <= _wallet.Money;
    }
}