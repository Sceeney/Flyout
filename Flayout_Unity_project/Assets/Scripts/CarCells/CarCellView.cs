using TMPro;
using UnityEngine;
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

        SetImage();
        TrySetPriceText();
    }

    public void OnPointerClick()
    {
        _shop.OnCellClick(_info.Index);
    }

    public void OnPointerEnter()
    {
        if (_imgBUY != null && !_shop.IsBuyed(_info.Index))
            _imgBUY.SetActive(true);
    }

    public void OnPointerExit() 
    {
        if (_imgBUY != null && _imgBUY.activeSelf)
            _imgBUY.SetActive(false);
    }

    private void SetImage()
    {
        _image = GetComponent<Image>();
        _image.sprite = _info.Icon;
    }

    private void TrySetPriceText()
    {
        if (_priceText != null)
        {
            _priceText.text = _info.Price.ToString();

            if (_shop.IsBuyed(_info.Index))
            {
                _priceText.gameObject.SetActive(false);
            }
            else
            {
                SetPriceColor();
            }
        }
    }

    private void SetPriceColor()
    {
        if (_shop.IsEnoughMoney(_info.Index))
            _priceText.color = Color.green;
        else
            _priceText.color = Color.red;
    }
}