using System;
using UnityEngine;
using UnityEngine.Events;
using YG;

[RequireComponent(typeof(CarShop))]
public class YandexDataSaver : MonoBehaviour
{
    [SerializeField] private int _money;
    [SerializeField] private int _lastSelectedCarIndex;
    [SerializeField] private Car[] _cars;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private CarShop _carShop;

    public int Money => _money;
    public int LastSelectedCarIndex => _lastSelectedCarIndex;

    [SerializeField] public UnityEvent DataUpdated;

    private void OnValidate()
    {
        if (_wallet == null)
            throw new ArgumentNullException(nameof(_wallet));

        if (_carShop == null)
            _carShop = GetComponent<CarShop>();
    }

    private void OnEnable()
    {
        YandexGame.GetDataEvent += GetLoad;
        YandexGame.onResetProgress += OnResetProgress;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= GetLoad;
        YandexGame.onResetProgress -= OnResetProgress;
    }

    private void Awake()
    {
        if (YandexGame.SDKEnabled)
            GetLoad();
    }
    
    public bool GetIsBuyedCarByIndex(int index)
    {
        return _cars[index].IsBuyed;
    }

    public void Save()
    {
        Debug.Log(_wallet.Money);
        YandexGame.savesData.Money = _wallet.Money;
        Debug.Log(YandexGame.savesData.Money);

        YandexGame.savesData.LastSelectedCarIndex = _carShop.LastSelectedCarIndex;

        bool[] temp = new bool[_cars.Length];
        for (int i = 0; i < _cars.Length; i++)
            temp[i] = _cars[i].IsBuyed;

        YandexGame.savesData.BuyedCar = temp;

        YandexGame.SaveProgress();
    }

    private void OnResetProgress()
    {
        Debug.Log("Reset");
        YandexGame.savesData.Money = 0;
        YandexGame.savesData.LastSelectedCarIndex = 0;
        YandexGame.savesData.BuyedCar = new bool[1] { false };

        GetLoad();
    }

    private void GetLoad()
    {
        _wallet.Money = YandexGame.savesData.Money;

        _carShop.LastSelectedCarIndex = YandexGame.savesData.LastSelectedCarIndex;

        for (int i = 0; i < YandexGame.savesData.BuyedCar.Length; i++)
        {
            bool isBuyed = YandexGame.savesData.BuyedCar[i];
            if (isBuyed)
                _cars[i].Purchase();
        }

        Debug.Log($"Language - {YandexGame.savesData.language}\n" +
            $"First Session - {YandexGame.savesData.isFirstSession}\n" +
            $"Prompt Done - {YandexGame.savesData.promptDone}\n");

        DataUpdated?.Invoke();
    }
}
