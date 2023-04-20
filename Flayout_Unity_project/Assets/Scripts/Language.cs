using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class Language : MonoBehaviour
{


    [DllImport("__Internal")]
    private static extern string GetLang();

    public string CurrentLanguage;
    [SerializeField] TextMeshProUGUI _languageText;

    public static Language Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            CurrentLanguage = "ru";//GetLang();
            _languageText.text = CurrentLanguage;
        }
        else {
            Destroy(gameObject);
        }
    }

}
