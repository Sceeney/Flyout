using UnityEngine;

[CreateAssetMenu(fileName = "new Car", menuName = "Car/Create new Car")]
public class CarCellInfo : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _index;
    [SerializeField] private int _price;

    public Sprite Icon => _icon;
    public int Index => _index;
    public int Price => _price;
}
