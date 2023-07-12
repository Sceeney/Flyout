using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Localization_IMG : MonoBehaviour
{
    private Image ImgOBJ;
    public Sprite _en_IMG;
    public Sprite _ru_IMG;


    private void OnEnable()
    {
        YandexGame.GetDataEvent += OnDataUpdated;
    }

    private void Start()
    {
        ImgOBJ = GetComponent <Image>();
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
            //print("en");
            ImgOBJ.sprite = _en_IMG;
        }
        else if (YandexGame.Instance.LanguageYG == "ru")
        {
            //print("ru");
            ImgOBJ.sprite = _ru_IMG;
        }
        else
        {
            //print("else");
            ImgOBJ.sprite = _en_IMG;
        }
    }
}
