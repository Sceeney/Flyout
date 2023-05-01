using UnityEngine;
using UnityEngine.Events;
using YG;

public class Wallet : MonoBehaviour, ISaver
{
    [SerializeField] private MoneyView _moneyView;
    private int _money;

    public int Money { get { return GetMoney(); } }

    [SerializeField] private UnityEvent<int> _updateMoney;

    private void OnEnable() => YandexGame.GetDataEvent += GetData;
    private void OnDisable() => YandexGame.GetDataEvent -= GetData;

    private void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            GetData();
        }
    }

    private void GetData()
    {
        GetMoney();
    }

    private int GetMoney()
    {
        //YandexGame.LoadLocal();

        _money = YandexGame.savesData.Money;

        return _money;
    }

    public void OnPurchased(Car car)
    {
        int price = car.Price;

        _money -= price;

        SetMoney(_money);
    }

    private void SetMoney(int value)
    {
        YandexGame.savesData.Money = value;

        _updateMoney?.Invoke(value);

        Save();
    }

    public void Save()
    {
        YandexGame.SaveLocal();

#if !UNITY_EDITOR
        YandexGame.SaveProgress();
#endif
    }
}