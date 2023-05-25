using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CarCellView : MonoBehaviour
{
    [SerializeField] private CarCellInfo _info;
    [SerializeField] private CarShop _shop;
    [SerializeField] private GameObject _imgBUY;
    [SerializeField] private TMP_Text _priceText;

    private Image _image;

    private void Awake()
    {
        if (_imgBUY != null)
            _imgBUY.SetActive(false);

        _image = GetComponent<Image>();
        _image.sprite = _info.Icon;

        if (_priceText != null)
            _priceText.text = _info.Price.ToString();
    }

    public void OnPointerClick()
    {
        _shop.OnCellClick(_info.Index);
    }

    public void OnPointerEnter()
    {
        if (_imgBUY != null && _shop.CanPurchase(_info.Index))
            _imgBUY.SetActive(true);
    }

    public void OnPointerExit() 
    {
        if (_imgBUY != null && _imgBUY.activeSelf)
            _imgBUY.SetActive(false);
    }
}