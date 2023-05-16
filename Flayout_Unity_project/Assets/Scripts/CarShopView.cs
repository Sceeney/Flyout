using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CarShop))]
public class CarShopView : MonoBehaviour
{
    [SerializeField] private Car[] _cars;
    [SerializeField] private Text _priceText;
    [SerializeField] private Button _selectButton;
    [SerializeField] private Button _BuyButton;

    private CarShop _shop;

    private void Awake()
    {
        _shop = GetComponent<CarShop>();
        _cars = gameObject.GetComponentsInChildren<Car>();
    }

    private void OnEnable()
    {
        _shop.LasrSelectedCarIndexChanged += OnLasrSelectedCarIndexChanged;
        _shop.Purchased += OnPurchased;
    }

    private void OnDisable()
    {
        _shop.LasrSelectedCarIndexChanged -= OnLasrSelectedCarIndexChanged;
        _shop.Purchased -= OnPurchased;
    }

    private void OnLasrSelectedCarIndexChanged(int index)
    {
        Car car = _cars.First(c => c.gameObject.activeSelf == true);
        car.gameObject.SetActive(false);

        _cars[index].gameObject.SetActive(true);

        UpdateButtonDisplay(index);
    }

    private void OnPurchased(Car car)
    {
        UpdateButtonDisplay(car.Index);
    }

    private void UpdateButtonDisplay(int index)
    {
        if (_cars[index].IsBuyed)
        {
            PossibleSelect();
        }
        else
        {
            PossibleBuy(index);
        }
    }

    private void PossibleBuy(int index)
    {
        _BuyButton.gameObject.SetActive(true);
        _selectButton.gameObject.SetActive(false);

        _priceText.text = _cars[index]
                                .Price.ToString();
    }

    private void PossibleSelect()
    {
        _BuyButton.gameObject.SetActive(false);
        _selectButton.gameObject.SetActive(true);
    }
}