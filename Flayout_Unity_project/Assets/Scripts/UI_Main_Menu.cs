using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UI_Main_Menu : MonoBehaviour
{
    public static int button_Index;
        [Space(20)]
    public GameObject Main_menu_panel;
    public GameObject Car_select_panel;
    public GameObject Track_menu_panel;
    public GameObject Options_menu_panel;
    public GameObject Options_panel;
    bool isOpen;
        [Space(20)]
    public GameObject Main_Menu_UI;
    public GameObject Car_select_UI;
    public GameObject Player_select_UI;
    public GameObject Options_UI;
    public GameObject Track_select_UI;
        [Space(20)]
    public Slider Slider_Main_audio;
        [Space(5)]
    public AudioSource audio_Music;
    public Slider Slidel_Music;
        [Space(5)]
    private float music_Volume = 1f;

    void Awake()
    {
        Time.timeScale = 1f;
        button_Index = 1;
        Show_UI();
    }
    void Start()
    {
        audio_Music = GetComponent<AudioSource>();
        music_Volume = 1f;
        Slidel_Music.value = 1f;
        Slidel_Music.value = PlayerPrefs.GetFloat("Slidel_Music.value");
        Slider_Main_audio.value = PlayerPrefs.GetFloat("Listener"); 
        music_Volume = PlayerPrefs.GetFloat("music_Volume");
        isOpen = false;
        Сhoice_Level.Start_Level_1 = false;
    }
    void Update()
    {
        audio_Music.volume = music_Volume;
    }

    public void Main_click() // выход в меню 
    {
        button_Index = 1;
        Invoke("Show_UI",1f);
    }
    public void Cars_click() // выбор машины
    {
        button_Index = 2;
        Invoke("Show_UI",1f);
        Anim_Options();
    }
    public void Player_click() // выбор перса
    {
        button_Index = 3;
        Invoke("Show_UI",1f);
        Anim_Options();
    }
    public void Options_click() // настройки
    {
        if (isOpen == false && button_Index == 4 || button_Index == 1) // если открыто, чтобы не закрыть снова
            Anim_Options();
        if(button_Index == 4) // двойное нажатие на опции
            Main_click();
        else{
            button_Index = 4;
            Invoke("Show_UI",1f);}
    }
    public void Track_click() //выбор трека
    {
        button_Index = 5;
        Invoke("Show_UI",1f);
        Anim_Options();
    }

    public void Anim_Options() // анимация выезда верхних опций
    {
        if(Options_panel != null)
        {
            Animator animator = Options_panel.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("open");
                animator.SetBool("open", !isOpen);
            }
        }
    }
    public void Anim_Main_menu() // анимация выезда главного меню
    {
            Animator animator = Main_menu_panel.GetComponent<Animator>();
                bool Open = animator.GetBool("open_menu");
                animator.SetBool("open_menu", !Open);
    }
    public void Anim_Car_Select() // анимация выезда меню выбора машин
    {
            Animator animator = Car_select_panel.GetComponent<Animator>();
                bool Open = animator.GetBool("open_car_select");
                animator.SetBool("open_car_select", !Open);
    }
    public void Anim_Options_menu() // анимация выезда меню опций
    {
            Animator animator = Options_menu_panel.GetComponent<Animator>();
                bool Open = animator.GetBool("open_options_menu");
                animator.SetBool("open_options_menu", !Open);
    }
    public void Anim_Track_menu() // анимация выезда меню выбора трека
    {
            Animator animator = Track_menu_panel.GetComponent<Animator>();
                bool Open = animator.GetBool("open_track");
                animator.SetBool("open_track", !Open);
    }

    void Show_UI()
    {
        switch(button_Index)
        {
            case 1:
                Main_Menu_UI.SetActive(true);
                Car_select_UI.SetActive(false);
                Player_select_UI.SetActive(false);
                Options_UI.SetActive(false);
                Track_select_UI.SetActive(false);
                break;
            case 2:
                Main_Menu_UI.SetActive(false);
                Car_select_UI.SetActive(true);
                Player_select_UI.SetActive(false);
                Options_UI.SetActive(false);
                Track_select_UI.SetActive(false);
                break;
            case 3:
                Main_Menu_UI.SetActive(false);
                Car_select_UI.SetActive(false);
                Player_select_UI.SetActive(true);
                Options_UI.SetActive(false);
                Track_select_UI.SetActive(false);
                break;
            case 4:
                Main_Menu_UI.SetActive(false);
                Car_select_UI.SetActive(false);
                Player_select_UI.SetActive(false);
                Options_UI.SetActive(true);
                Track_select_UI.SetActive(false);
                break;
            case 5:
                Main_Menu_UI.SetActive(false);
                Car_select_UI.SetActive(false);
                Player_select_UI.SetActive(false);
                Options_UI.SetActive(false);
                Track_select_UI.SetActive(true);
                break;
        }
    }

    public void Start_Level()
    {
        SceneManager.LoadScene(1);
    }

    public void Volume_Music()
    {
        music_Volume = Slidel_Music.value;
        PlayerPrefs.SetFloat("music_Volume", music_Volume);
        PlayerPrefs.SetFloat("Slidel_Music.value", Slidel_Music.value);
    }
    public void Main_audio()
    {
        Audio_Listener.Listener = Slider_Main_audio.value;
        PlayerPrefs.SetFloat("Listener", Audio_Listener.Listener);
        PlayerPrefs.SetFloat("Slider_Main_audio", Slider_Main_audio.value);
    }

}
