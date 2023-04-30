using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private int _price;
    [SerializeField] private bool _isDefaultActive;
    [SerializeField] private bool _isPurchased;

    public string Name { get { return _name; } }
    public int Price { get { return _price; } }
    public bool IsPurchased { get { return _isPurchased; } }

    private void Start()
    {
        _name = gameObject.name;

        if (_isDefaultActive == false)
        {
            gameObject.SetActive(false);
        }
    }

    public void Purchase()
    {
        _isPurchased = true;
    }
}
