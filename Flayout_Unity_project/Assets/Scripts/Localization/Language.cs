using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using YG;

public class Language : MonoBehaviour
{
    public string CurrentLanguage;
    [SerializeField] TextMeshProUGUI _languageText;

    public static Language Instance;

    void Start()
    {
        //Localization();
        Invoke("Localization", 0.01f);
    }

    private void Localization()
    {
        if (Instance == null)
        {
            Instance = this;
            CurrentLanguage = YandexGame.Instance.LanguageYG; //"ru";//GetLang();
            _languageText.text = CurrentLanguage;
        }
    }

}
