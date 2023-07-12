using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class Localization_TEXT : MonoBehaviour
{
    [SerializeField] string _en;
    [SerializeField] string _ru;

    private void OnEnable()
    {
        YandexGame.GetDataEvent += OnDataUpdated;
    }

    private void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            OnDataUpdated();
        }
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= OnDataUpdated;
    }

    void OnDataUpdated()
    {
        if (YandexGame.Instance.LanguageYG == "en")
        {
            GetComponent<TextMeshProUGUI>().text = _en;
        }
        else if (YandexGame.Instance.LanguageYG == "ru")
        {
            GetComponent<TextMeshProUGUI>().text = _ru;
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = _en;
        }
    }
}
