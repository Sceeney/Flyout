using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Main_Script : MonoBehaviour
{
    //загрузка меню
    public string Load_level;
    public GameObject Loading_Screen;
    public Slider bar;
    // Статусы
    enum State {Gold, Silver, Bronze, No_medal}  
    State state;
    // Переменные
    private int Money;
    private int which_round;
    private float summ_score;
    // Булы
    public static bool bool_shoot;
    public static bool start_but;
    public static bool text_hight;
    private bool isDone_UI;
    public static bool pause;
    // Меню
        [Space(20)]
    public GameObject Start_UI;
    public GameObject Pause_UI;
    public GameObject Pause_Options;
    public GameObject Next_round_UI;
        public GameObject Target_text_UI;
        public GameObject Crash_text_UI;
            public GameObject Next_round_but;
            public GameObject Finish_but;
            [Space(5)]
    public GameObject Out_track_UI;
    public GameObject Finish_UI;
    public GameObject round_1;
    public GameObject round_2;
    public GameObject round_3;
        // Тексты Меню
            [Space(20)]
        [SerializeField] Text round_1_text;
        [SerializeField] Text round_2_text;
        [SerializeField] Text round_3_text;
        [SerializeField] Text total_text;
        [SerializeField] Text total_text_finish;
    // Звёзды
        [Space(20)]
    [SerializeField] int Gold_Score; 
    [SerializeField] int Silver_Score; 
    [SerializeField] int Bronze_Score; 
            [Space(5)]
        [SerializeField] int Gold_Money; 
        [SerializeField] int Silver_Money; 
        [SerializeField] int Bronze_Money; 
                [Space(5)]
            [SerializeField] Text Gold_score_text;
            [SerializeField] Text Silver_score_text;
            [SerializeField] Text Bronze_score_text;
                    [Space(5)]
                public GameObject Medal_Gold;
                public GameObject Medal_Silver;
                public GameObject Medal_Bronze;
                public GameObject No_Medal;

        
        [Space(20)]
    // Какой раунд
    [SerializeField] TextMeshProUGUI wich_round_text;
    // Выстрел
        [Space(20)]        
    [SerializeField] Text Angle_Text;
    [SerializeField] Text Height_Text;
        [Space(5)]
    public GameObject Shoot_UI;
    public GameObject Start_Button;
        [Space(20)]
    // Настройка громкости
    public Slider Slider_Main_audio;
        [Space(5)]
    public AudioSource audio_Stadium;
    public Slider Slider_Stadium;
        [Space(5)]
    public AudioSource audio_Music;
    public Slider Slidel_Music;
        [Space(5)]
    public float music_Volume = 1f;

    private void Awake()
    {        
        which_round = PlayerPrefs.GetInt("Try's");
        Money = PlayerPrefs.GetInt("Money_save");//Progress.Instance.PlayerInfo.Coins;
        state = State.No_medal;
        Time.timeScale = 1f;
        isDone_UI = false;
        bool_shoot = false;
        text_hight = false;
        start_but= false;
        pause = false;
        Shoot_UI.SetActive(false);
        Pause_Options.SetActive(false);
    }
    private void Start()
    {

        audio_Music = GetComponent<AudioSource>();
        //music_Volume = 1f;
        //Slidel_Music.value = 1f;

        if (PlayerPrefs.GetFloat("First_Start_Save") != 1f){
            Slidel_Music.value = 0.5f;
            Slider_Main_audio.value = 0.5f;
            music_Volume = 0.5f;
                PlayerPrefs.SetFloat("First_Start_Save", 1f);
        }
        else{
            Slidel_Music.value = PlayerPrefs.GetFloat("Slidel_Music.value");
            Slider_Main_audio.value = PlayerPrefs.GetFloat("Listener"); 
            music_Volume = PlayerPrefs.GetFloat("music_Volume");
        }


        wich_round_text.text = which_round.ToString();
        Gold_score_text.text = Gold_Score.ToString();
        Silver_score_text.text = Silver_Score.ToString();
        Bronze_score_text.text = Bronze_Score.ToString();

        Pause_UI.SetActive(false);
}

    private void Update()
    {             
        audio_Music.volume = music_Volume;

        if(Input.GetKeyDown(KeyCode.Escape))//&& pause == false){
            if(!pause)
                Pause();
            else{
                Resume();}


        if(Input.GetKey(KeyCode.Space)) // кнопка пробела
        {
            if(AIM_Shot.slow_mo == true)
            Button();
            if(start_but == false)
            Click_Start_Button();
        }
        Text();
    }
    
    public void Button() // Кнопка выстрела
    {
        bool_shoot = true;
        text_hight = true;
        StartCoroutine("Text_hight");
        StartCoroutine("Timer");
    }   
    IEnumerator Text_hight()
    {
        for (int i = 0; i >= 0; i++)
        {
            if(text_hight == true){
                Height_Text.text = Impulse_and_Mass.Value_Height.ToString();
                yield return null;}        
            else yield break;
        }
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(20f); 
        Next_round();
    }
    public void Pause() // Кнопка паузы
    {
        pause = true;
        Time.timeScale = 0.01f;
        Pause_Options.SetActive(true);
    }
    public void Resume() // Кнопка продолжить
    {
        pause = false;
        Pause_Options.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Restart() // Кнопка рестарт
    {
        PlayerPrefs.SetInt("Try's", 1);
        PlayerPrefs.DeleteKey ("round_1");  
        PlayerPrefs.DeleteKey ("round_2");  
        PlayerPrefs.DeleteKey ("round_3");  
        SceneManager.LoadScene(1);
    }
    public void Click_Start_Button() // Кнопка старта
    {
        start_but = !start_but;
        Start_Button.SetActive(false);
        Pause_UI.SetActive(true);
    }
    public void Next_round_button() // Кнопка сл раунд
    {
        SceneManager.LoadScene(1);
        Next_round_UI.SetActive(false);
        which_round = PlayerPrefs.GetInt("Try's")+1;
        PlayerPrefs.SetInt("Try's",which_round);
    }
    public void Main_Menu_button() // Кнопка меню
    {
        pause = false;
        AIM_Shot.is_Crash = false;
        if(state == State.Gold){
            Money = Money + Gold_Money;
            PlayerPrefs.SetInt("Money_save", Money);
                //Progress.Instance.PlayerInfo.Coins = Money;
                //Progress.Instance.Save();
                }
        else if(state == State.Silver){
            Money = Money + Silver_Money;
            PlayerPrefs.SetInt("Money_save", Money);
                //Progress.Instance.PlayerInfo.Coins = Money;
                //Progress.Instance.Save();
                }
        else if(state == State.Bronze){
            Money = Money + Bronze_Money;
            PlayerPrefs.SetInt("Money_save", Money);
                //Progress.Instance.PlayerInfo.Coins = Money;
                //Progress.Instance.Save();
                }
        else{
            Money = Money + 0;
            PlayerPrefs.SetInt("Money_save", Money);
                //Progress.Instance.PlayerInfo.Coins = Money;
                //Progress.Instance.Save();
            }


        PlayerPrefs.SetInt("Try's", 1);
        PlayerPrefs.DeleteKey ("round_1");  
        PlayerPrefs.DeleteKey ("round_2");  
        PlayerPrefs.DeleteKey ("round_3");  
        Time.timeScale = 1f;
        Audio_Listener.Listener = 0f;
        Loading_Screen.SetActive(true);
        StartCoroutine(Load_async_scene_menu()); // загрузочный экран
    }
    IEnumerator Load_async_scene_menu()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Load_level);
        while(!asyncLoad.isDone){            
            bar.value = asyncLoad.progress;            
            yield return null;}
    }
    public void One_more_time() // Кнопка ещё заезд
    {
        if(state == State.Gold){
            Money = Money + Gold_Money;
            PlayerPrefs.SetInt("Money_save", Money);
                //Progress.Instance.PlayerInfo.Coins = Money;
                //Progress.Instance.Save();
            }

            
        else if(state == State.Silver){
            Money = Money + Silver_Money;
            PlayerPrefs.SetInt("Money_save", Money);
                //Progress.Instance.PlayerInfo.Coins = Money;
                //Progress.Instance.Save();
            }

        else if(state == State.Bronze){
            Money = Money + Bronze_Money;
            PlayerPrefs.SetInt("Money_save", Money);
                //Progress.Instance.PlayerInfo.Coins = Money;
                //Progress.Instance.Save();
            }
        else{
            Money = Money + 0;
            PlayerPrefs.SetInt("Money_save", Money);
                //Progress.Instance.PlayerInfo.Coins = Money;
                //Progress.Instance.Save();
            }

        PlayerPrefs.SetInt("Try's", 1);
        PlayerPrefs.DeleteKey ("round_1");  
        PlayerPrefs.DeleteKey ("round_2");  
        PlayerPrefs.DeleteKey ("round_3");  
        state = State.No_medal;
        SceneManager.LoadScene(1);
    }

    void Text() 
    {
        if(AIM_Shot.slow_mo == true)
                Invoke("Shoot_UI_active", 0f);
            else 
                Invoke("Shoot_UI_deactive", 0f);
                
        if(!isDone_UI)
        {    
            if(Collision_trigger.trigger_wire == true){
                    Invoke("Next_round", 2f);
                    isDone_UI = true;}
                else if(AIM_Shot.is_countdown == true){
                    Impulse_and_Mass.Value_Height = 0f;
                    //Click_Start_Button();
                    Invoke("Click_Start_Button", 0.1f);
                    Invoke("Next_round", 1f);
                    isDone_UI = true;}
                else if(AIM_Shot.is_Crash == true && text_hight == false){
                    Impulse_and_Mass.Value_Height = 0f;
                    Invoke("Next_round", 1f);
                    isDone_UI = true;}
                else if(Collision_trigger.trigger_out == true){
                    Invoke("Next_round", 0.1f);
                    isDone_UI = true;}
        }
    }

    void Next_round()
    {      
        Next_round_UI.SetActive(true);
        Time.timeScale = 0.01f;
            if(AIM_Shot.is_Crash == true && Main_Script.text_hight == false){
                Crash_text_UI.SetActive(true);
                Target_text_UI.SetActive(false);}
            else{
                Crash_text_UI.SetActive(false);
                Target_text_UI.SetActive(true);}
        switch(which_round)
        {
            case 1:
                Next_round_but.SetActive(true);
                Finish_but.SetActive(false);

                round_1.SetActive(true);
                    round_1_text.text = Impulse_and_Mass.Value_Height.ToString();
                    PlayerPrefs.SetFloat("round_1",Impulse_and_Mass.Value_Height);
                round_2.SetActive(false);
                round_3.SetActive(false);

                    summ_score = PlayerPrefs.GetFloat("round_1") + PlayerPrefs.GetFloat("round_2") + PlayerPrefs.GetFloat("round_3");
                    total_text.text = summ_score.ToString();
                break;
            case 2:
                Next_round_but.SetActive(true);
                Finish_but.SetActive(false);

                round_1.SetActive(true);
                    round_1_text.text = (PlayerPrefs.GetFloat("round_1")).ToString();
                round_2.SetActive(true);
                    round_2_text.text = Impulse_and_Mass.Value_Height.ToString();
                    PlayerPrefs.SetFloat("round_2",Impulse_and_Mass.Value_Height);
                round_3.SetActive(false);

                    summ_score = PlayerPrefs.GetFloat("round_1") + PlayerPrefs.GetFloat("round_2") + PlayerPrefs.GetFloat("round_3");
                    total_text.text = summ_score.ToString();
                break;
            case 3:
                Next_round_but.SetActive(false);
                Finish_but.SetActive(true);

                round_1.SetActive(true);
                    round_1_text.text = (PlayerPrefs.GetFloat("round_1")).ToString();
                round_2.SetActive(true);
                    round_2_text.text = (PlayerPrefs.GetFloat("round_2")).ToString();
                round_3.SetActive(true);
                    round_3_text.text = Impulse_and_Mass.Value_Height.ToString();
                    PlayerPrefs.SetFloat("round_3",Impulse_and_Mass.Value_Height);

                    summ_score = PlayerPrefs.GetFloat("round_1") + PlayerPrefs.GetFloat("round_2") + PlayerPrefs.GetFloat("round_3");
                    total_text.text = summ_score.ToString();
                break;
            default:
                round_1.SetActive(true);
                    round_1_text.text = Impulse_and_Mass.Value_Height.ToString();
                    PlayerPrefs.SetFloat("round_1",Impulse_and_Mass.Value_Height);
                round_2.SetActive(false);
                round_3.SetActive(false);
                    summ_score = PlayerPrefs.GetFloat("round_1") + PlayerPrefs.GetFloat("round_2") + PlayerPrefs.GetFloat("round_3");
                    total_text.text = summ_score.ToString();
                break;
        }
    }
    void Out_track()
    {
        Out_track_UI.SetActive(true);
        Time.timeScale = 0.01f;
    }
    public void Finish()
    {
        summ_score = PlayerPrefs.GetFloat("round_1") + PlayerPrefs.GetFloat("round_2") + Impulse_and_Mass.Value_Height;
        Next_round_UI.SetActive(false);
        Finish_UI.SetActive(true);
            if(summ_score >= Gold_Score)
            {
                state = State.Gold;
                Medal_Gold.SetActive(true);
                Medal_Silver.SetActive(false);
                Medal_Bronze.SetActive(false);
                No_Medal.SetActive(false);
            }
            else if(summ_score >= Silver_Score && summ_score < Gold_Score)
            {
                state = State.Silver;
                Medal_Gold.SetActive(false);
                Medal_Silver.SetActive(true);
                Medal_Bronze.SetActive(false);
                No_Medal.SetActive(false);
            }
            else if(summ_score >= Bronze_Score && summ_score < Silver_Score)
            {
                state = State.Bronze;
                Medal_Gold.SetActive(false);
                Medal_Silver.SetActive(false);
                Medal_Bronze.SetActive(true);
                No_Medal.SetActive(false);
            }
            else if(summ_score < Bronze_Score)
            {
                state = State.No_medal;
                Medal_Gold.SetActive(false);
                Medal_Silver.SetActive(false);
                Medal_Bronze.SetActive(false);
                No_Medal.SetActive(true);
            }
        total_text_finish.text = summ_score.ToString();
    }

    void Shoot_UI_active()
    {
        Shoot_UI.SetActive(true);
        Angle_Text.text = Mathf.RoundToInt(AIM_Shot.Value_angle_shot).ToString();
    }
    void Shoot_UI_deactive()
    {
        Shoot_UI.SetActive(false);
        Angle_Text.text = "0";
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
