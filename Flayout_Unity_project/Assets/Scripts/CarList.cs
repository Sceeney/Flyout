using System.Linq;
using UnityEngine;

public class CarList : MonoBehaviour
{
    public Car[] Cars;

    [SerializeField] private CarShop _shop;

    private void OnEnable()
    {
        Cars[0].gameObject.SetActive(true);

        _shop.DisplayedCarChanged += OnDisplayedCarChanged;
    }

    private void OnDisable()
    {
        _shop.DisplayedCarChanged -= OnDisplayedCarChanged;
    }

    private void OnDisplayedCarChanged(int index)
    {
        Cars.First(c =>
        c.gameObject.activeSelf == true)
            .gameObject.SetActive(false);

        Cars[index].gameObject.SetActive(true);
    }
}
