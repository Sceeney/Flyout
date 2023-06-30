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
        string s = YandexGame.Instance.LanguageYG;
        
        if (YandexGame.Instance.LanguageYG == "en")
        {
            ImgOBJ.sprite = _en_IMG;
        }
        else if (YandexGame.Instance.LanguageYG == "ru")
        {
            ImgOBJ.sprite = _ru_IMG;
        }
        else
        {
            ImgOBJ.sprite = _en_IMG;
        }

        
    }
}
