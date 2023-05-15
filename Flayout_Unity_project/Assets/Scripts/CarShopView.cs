using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class CarShopView : MonoBehaviour
{
    [SerializeField] private Car[] _cars;
    [SerializeField] private Text _priceText;
    [SerializeField] private Button _selectButton;
    [SerializeField] private Button _BuyButton;

    private int _lastSelectCarIndex 
                => YandexGame.savesData.LastSelectedCarIndex;

    private void Awake()
    {
        _cars = gameObject.GetComponentsInChildren<Car>();
    }

    public void OnChangeCar()
    {
        Car car = _cars.First(c => c.gameObject.activeSelf == true);
        car.gameObject.SetActive(false);

        _cars[_lastSelectCarIndex].gameObject.SetActive(true);

        if (_cars[_lastSelectCarIndex].IsBuyed)
        {
            PossibleSelect();
        }
        else
        {
            PossibleBuy();
        }
    }

    private void PossibleBuy()
    {
        _BuyButton.gameObject.SetActive(true);
        _selectButton.gameObject.SetActive(false);

        _priceText.text = _cars[_lastSelectCarIndex]
                                .Price.ToString();
    }

    private void PossibleSelect()
    {
        _BuyButton.gameObject.SetActive(false);
        _selectButton.gameObject.SetActive(true);
    }
}