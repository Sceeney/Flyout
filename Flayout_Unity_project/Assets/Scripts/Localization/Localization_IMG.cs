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

    Invoke("Lang", 0.15f);
    }

    void Lang()
    {
        if (Language.Instance.CurrentLanguage == "en")
        {
            print("en");
            ImgOBJ.sprite = _en_IMG;
        }
        else if (Language.Instance.CurrentLanguage == "ru")
        {
            print("ru");
            ImgOBJ.sprite = _ru_IMG;
        }
        else
        {
            print("else");
            ImgOBJ.sprite = _en_IMG;
        }
    }
}
