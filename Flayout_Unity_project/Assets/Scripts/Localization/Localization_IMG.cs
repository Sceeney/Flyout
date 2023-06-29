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

    void Start()
    {
        ImgOBJ = GetComponent <Image>();
        
        if (Language.Instance.CurrentLanguage == "en")
        {
            ImgOBJ.sprite = _en_IMG;
        }
        else if (Language.Instance.CurrentLanguage == "ru")
        {
            ImgOBJ.sprite = _ru_IMG;
        }
        else
        {
            ImgOBJ.sprite = _en_IMG;
        }

        
        string s = YandexGame.Instance.LanguageYG;
    }
}
