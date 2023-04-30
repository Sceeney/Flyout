using UnityEngine;
using UnityEngine.UI;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private Text _moneyUIObject;

    public void OnUpdateMoney(int value)
    {
        _moneyUIObject.text = value.ToString();
    }
}