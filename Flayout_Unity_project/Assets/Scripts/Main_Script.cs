using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using YG;
using System.Linq;
using IJunior.TypedScenes;
using System;

public class LevelInfo
{
    public int CurrentRound { get; private set; }
    public float[] RoundsHeight { get; private set; }

    public LevelInfo()
    {
        CurrentRound = 1;
        RoundsHeight = new float[] { 0, 0, 0};
    }

    public void SetRound(int round)
    {
        if (round < 1 || round > 3) 
            throw new ArgumentOutOfRangeException(nameof(round));

        CurrentRound = round;
    }

    public void SetRoundHeight(int roundIndex, float value)
    {
        if (roundIndex < 0 || roundIndex > 2)
            throw new ArgumentOutOfRangeException(nameof(roundIndex));

        RoundsHeight[roundIndex] = value;
    }

    public float GetTotalScore()
    {
        float total = 0;

        foreach (var score in RoundsHeight)
        {
            total += score;
        }

        return total;
    }
}

public class Main_Script : MonoBehaviour, ISceneLoadHandler<LevelInfo>
{
    public static bool IsShoot;
    public static bool IsStartBut;
    public static bool IsShootInfoDisplay;
    public static bool IsPause;

    [Header("Переход в главное меню")]
    [SerializeField] private Load_Screen _loadScreen;
    [SerializeField] private int _loadLevelIndex;
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Slider _loadingProgressBar;

    [Space(20)]

    [Header("UI экраны")]
    [SerializeField] private GameObject _startScreen;
    [SerializeField] private GameObject _pauseScreen;
    [SerializeField] private GameObject _endRoundScreen;
    [SerializeField] private GameObject _finishScreen;
    [SerializeField] private GameObject _nextRaceButtonsScreen;
    [SerializeField] private GameObject _endRoundFinishScreen;
    [SerializeField] private GameObject _shootScreen;

    [Space(20)]

    [Header("Вывод информации")]
    [SerializeField] private Toggle _pauseToggle;
    [SerializeField] private TMP_Text _textTarget;
    [SerializeField] private TMP_Text _textCrash;
    [SerializeField] private TMP_Text _textCurrentRound;
    [Space(5)]
    [SerializeField] private TMP_Text _textShootAngle;
    [SerializeField] private TMP_Text _textCurrentHeight;
    [SerializeField] private TMP_Text _textCurrentDistance;
    [Space(5)]
    [SerializeField] private Image[] _roundsInfo;
    [SerializeField] private Text[] _textsScoreRound;
    [Space(5)]
    [SerializeField] private Text _textTotalScore;
    [SerializeField] private Text _textTotalScoreFinish;
    [Space(5)]
    [SerializeField] private TMP_Text _textGoldScore;
    [SerializeField] private TMP_Text _textSilverScore;
    [SerializeField] private TMP_Text _textBronzeScore;

    [Space(20)]

    [Header("Медали")]
    [SerializeField] private List<MedalInfo> _medalsInfo;
    [SerializeField] private List<GameObject> _medals;

    [Header("Звук")]
    [SerializeField] private Slider _mainAudioSlider;
    [SerializeField] private Slider _stadiumAudioSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private AudioSource _stadiumAudioSource;
    [SerializeField] private AudioSource _musicAudioSource;

    private LevelInfo _levelInfo;
    private float _totalScore;
    private Medal _medal;
    

    private enum Medal
    {
        Gold = 0,
        Silver,
        Bronze,
        No_medal
    }

    private void OnEnable()
    {
        YandexGame.GetDataEvent += GetData;

        AIM_Shot.Crashed += OnCrashed;
        AIM_Shot.TimeHasExpired += TimeHasExpired;

        Collision_trigger.TriggerOUT += OnTriggerOUT;
        Collision_trigger.TriggerWIRE += OnTriggerWire;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= GetData;

        AIM_Shot.Crashed -= OnCrashed;
        AIM_Shot.TimeHasExpired -= TimeHasExpired;

        Collision_trigger.TriggerOUT += OnTriggerOUT;
        Collision_trigger.TriggerWIRE += OnTriggerWire;
    }

    private void Awake()
    {
        _medal = Medal.No_medal;
        Time.timeScale = 1f;
        IsShoot = false;
        IsShootInfoDisplay = false;
        IsStartBut = false;
        IsPause = false;
        _endRoundScreen.SetActive(false);
        _endRoundFinishScreen.SetActive(false);
        _shootScreen.SetActive(false);
        _pauseScreen.SetActive(false);
    }

    private void Start()
    {
        _musicAudioSource = GetComponent<AudioSource>();
        _loadScreen = GetComponent<Load_Screen>();
        _loadScreen.Init(_loadingScreen, _loadingProgressBar);

        if (YandexGame.SDKEnabled)
            GetData();

        _textCurrentRound.text = _levelInfo.CurrentRound.ToString();
        _textGoldScore.text = _medalsInfo[(int)Medal.Gold].Score.ToString();
        _textSilverScore.text = _medalsInfo[(int)Medal.Silver].Score.ToString();
        _textBronzeScore.text = _medalsInfo[(int)Medal.Bronze].Score.ToString();

        _pauseToggle.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!IsPause)
                Pause();
            else
                Resume();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (AIM_Shot.slow_mo == true)
                Shoot();
            if (IsStartBut == false)
                StartButtonClick();
        }

        TryShowShootScreen();
    }

    private void GetData()
    {
        _musicVolumeSlider.value = YandexGame.savesData.MusicVolume;
        _mainAudioSlider.value = YandexGame.savesData.MainVolume;
    }

    public void OnSceneLoaded(LevelInfo argument)
    {
        _levelInfo = argument;
    }

    public void Shoot()
    {
        IsShoot = true;
        IsShootInfoDisplay = true;
        StartCoroutine(ShootInfoDisplay());
        StartCoroutine(Timer());
    }

    IEnumerator ShootInfoDisplay()
    {
        for (int i = 0; i >= 0; i++)
        {
            if (IsShootInfoDisplay == true)
            {
                _textCurrentHeight.text = Impulse_and_Mass.Value_Height.ToString("0.00");
                _textCurrentDistance.text = Impulse_and_Mass.Value_Distance.ToString("0.00");
                yield return null;
            }
            else yield break;
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(20f);
        Next_round();
    }

    public void Pause()
    {
        IsPause = true;
        Time.timeScale = 0.01f;
        _pauseScreen.SetActive(true);
    }

    public void Resume()
    {
        IsPause = false;
        _pauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartLevel()
    {
        _levelInfo = new LevelInfo();
        Level_Jump_Hight.Load(_levelInfo);
    }

    public void RestartRound()
    {
        Level_Jump_Hight.Load(_levelInfo);
    }

    public void StartButtonClick()
    {
        IsStartBut = !IsStartBut;
        _startScreen.SetActive(false);
        _pauseToggle.gameObject.SetActive(true);
    }

    public void NextRoundButtonClick()
    {
        _endRoundScreen.SetActive(false);
        _levelInfo.SetRound(_levelInfo.CurrentRound + 1);
        Level_Jump_Hight.Load(_levelInfo);
    }

    public void MainMenuButtonClick()
    {
        IsPause = false;

        SaveMoney(_medalsInfo[(int)_medal].Reward);

        Time.timeScale = 1f;
        Audio_Listener.Listener = 0f;
        _loadingScreen.SetActive(true);

        _loadScreen.Load();
    }

    private void SaveMoney(int money)
    {
        YandexGame.savesData.Money += money;

        YandexGame.SaveProgress();
    }

    public void One_more_time()
    {
        SaveMoney(_medalsInfo[(int)_medal].Reward);

        _medal = Medal.No_medal;
        Level_Jump_Hight.Load(_levelInfo);
    }

    private void TryShowShootScreen()
    {
        if (AIM_Shot.slow_mo == true)
            Invoke(nameof(ShootScreenShow), 0f);
        else
            Invoke(nameof(ShootScreenHide), 0f);
    }

    private void OnCrashed()
    {
        if (IsShootInfoDisplay == false)
        {
            _textCrash.gameObject.SetActive(true);
            _textTarget.gameObject.SetActive(false);
            Impulse_and_Mass.Value_Height = 0f;
            Invoke(nameof(Next_round), 1f);
        }
        else
        {
            _textCrash.gameObject.SetActive(false);
            _textTarget.gameObject.SetActive(true);
        }
    }

    private void TimeHasExpired()
    {
        Impulse_and_Mass.Value_Height = 0f;
        Invoke(nameof(StartButtonClick), 0.1f);
        Invoke(nameof(Next_round), 1f);
    }

    private void OnTriggerOUT()
    {
        Invoke(nameof(Next_round), 0.1f);
    }

    private void OnTriggerWire()
    {
        Invoke(nameof(Next_round), 2f);
    }

    private void Next_round()
    {
        _endRoundScreen.SetActive(true);
        Time.timeScale = 0.01f;

        ShowNextScreen();

        ShowRoundInfo();

        CalculateAndShowTotalScore();
    }

    private void ShowRoundInfo()
    {
        _levelInfo.SetRoundHeight(_levelInfo.CurrentRound - 1,
            Impulse_and_Mass.Value_Height);

        for (int i = 0; i < _levelInfo.CurrentRound; i++)
        {
            _roundsInfo[i].gameObject.SetActive(true);
            _textsScoreRound[i].text = _levelInfo.RoundsHeight[i].ToString();
            Debug.Log($"Height {_levelInfo.RoundsHeight[i]}");
        }
    }

    private void ShowNextScreen()
    {
        if (_levelInfo.CurrentRound < 3)
        {
            _nextRaceButtonsScreen.SetActive(true);
            _endRoundFinishScreen.SetActive(false);
        }
        else
        {
            _nextRaceButtonsScreen.SetActive(false);
            _endRoundFinishScreen.SetActive(true);
        }
    }

    private void CalculateAndShowTotalScore()
    {
        _totalScore = _levelInfo.GetTotalScore();
        _textTotalScore.text = _totalScore.ToString("0.00");
    }

    public void Finish()
    {
        _totalScore = _levelInfo.GetTotalScore();
        _endRoundScreen.SetActive(false);
        _finishScreen.SetActive(true);

        ShowMedal();

        _textTotalScoreFinish.text = _totalScore.ToString("0.00");
    }

    private void ShowMedal()
    {
        for (int i = 0; i < _medals.Count; i++)
        {
            if (_totalScore >= _medalsInfo[i].Score)
            {
                _medal = (Medal)i;
                _medals[i].SetActive(true);
                return;
            }
        }
    }

    private void ShootScreenShow()
    {
        _shootScreen.SetActive(true);
        _textShootAngle.text = Mathf.RoundToInt(AIM_Shot.Value_angle_shot).ToString();
    }
    private void ShootScreenHide()
    {
        _shootScreen.SetActive(false);
        _textShootAngle.text = "0";
    }

    public void OnMusicVolumeChanged()
    {
        _musicAudioSource.volume = _musicVolumeSlider.value;
        YandexGame.savesData.MusicVolume = _musicVolumeSlider.value;
    }
    public void OnMainVolumeChanged()
    {
        Audio_Listener.Listener = _mainAudioSlider.value;
        YandexGame.savesData.MainVolume = _mainAudioSlider.value;
    }
}