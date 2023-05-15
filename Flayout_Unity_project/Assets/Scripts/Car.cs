using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private int _index;
    [SerializeField] private string _name;
    [SerializeField] private int _price;
    [SerializeField] private bool _isDefaultActive;
    [SerializeField] private bool _isBuyed;

    public int Index => _index;
    public string Name => _name;
    public int Price => _price;
    public bool IsBuyed => _isBuyed;

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
        _isBuyed = true;
    }
}
