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
    public float[] RoundsScore { get; private set; }

    public LevelInfo()
    {
        CurrentRound = 1;
        RoundsScore = new float[] { 0, 0, 0};
    }

    public void SetRound(int round)
    {
        if (round < 1 || round > 3) 
            throw new ArgumentOutOfRangeException(nameof(round));

        CurrentRound = round;
    }

    public void SetRoundScore(int roundIndex, float value)
    {
        if (roundIndex < 0 || roundIndex > 2)
            throw new ArgumentOutOfRangeException(nameof(roundIndex));

        RoundsScore[roundIndex] = value;
    }

    public float GetTotalScore()
    {
        float total = 0;

        foreach (var score in RoundsScore)
        {
            total += score;
        }

        return total;
    }
}

public class Main_Script : MonoBehaviour, ISceneLoadHandler<LevelInfo>
{
    [Header("Выстрел")]
    [SerializeField, Range(1f, 30f)] private float _forceShoot = 10f;
    [Space(20)]

    [Header("Необходимые компоненты")]
    [SerializeField] private Impulse_and_Mass _impulse_And_Mass;
    [SerializeField] private Body _player;

    [Space(20)]

    [Header("Загрузчик уровня ")]
    [SerializeField] private SceneLoader _levelLoader;

    [Space(20)]

    [Header("Переход в главное меню")]
    [SerializeField] private SceneLoader _mainMenuLoader;
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
    [SerializeField] private TMP_Text _textCurrentScore;
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

    private LevelInfo _levelInfo;
    private float _totalScore;
    private Medal _medal;
    private bool _isGameOver;
    private bool _isTriggered;
    private bool _isStartScreenNeeded;

    public bool IsShoot { get; private set; }
    public bool IsStartBut { get; private set; }
    public bool IsShootInfoDisplay { get; private set; }
    public bool IsPause { get; private set; }
    public float ForceShoot => _forceShoot;

    private float CurrentScore
    {
        get
        {
            if (!_isGameOver)
                return _impulse_And_Mass.GetValue(_levelLoader.LevelID);
            else
                return 0;
        }
        set{}
    }
    

    private enum Medal
    {
        Gold = 0,
        Silver,
        Bronze,
        No_medal
    }

    private void OnEnable()
    {
        AIM_Shot.Crashed += OnCrashed;
        AIM_Shot.TimeHasExpired += TimeHasExpired;

        _impulse_And_Mass.TriggerOut += OnTriggerOUT;
        _impulse_And_Mass.TriggerWire += OnTriggerWire;
    }

    private void OnDisable()
    {
        AIM_Shot.Crashed -= OnCrashed;
        AIM_Shot.TimeHasExpired -= TimeHasExpired;

        _impulse_And_Mass.TriggerOut += OnTriggerOUT;
        _impulse_And_Mass.TriggerWire += OnTriggerWire;
    }

    private void Awake()
    {
        _isTriggered = false;
        _medal = Medal.No_medal;
        Time.timeScale = 1f;
        IsShoot = false;
        IsShootInfoDisplay = false;
        IsStartBut = false;
        IsPause = false;
        _isGameOver = false;
        _endRoundScreen.SetActive(false);
        _endRoundFinishScreen.SetActive(false);
        _shootScreen.SetActive(false);
        _pauseScreen.SetActive(false);
    }

    private void Start()
    {
        _mainMenuLoader.Init(_loadingScreen, _loadingProgressBar);
        _levelLoader.Init(_loadingScreen, _loadingProgressBar);

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
        yield return _player.gameObject.activeSelf == true;

        for (int i = 0; i >= 0; i++)
        {
            if (IsShootInfoDisplay == true && _isTriggered == false)
            {
                _textCurrentScore.text = CurrentScore.ToString("0.00");
                _levelInfo.SetRoundScore(_levelInfo.CurrentRound - 1,
                    CurrentScore);
                yield return null;
            }
            else yield break;
        }
    }

    IEnumerator Timer()
    {
        yield return _player.gameObject.activeSelf == true;
        yield return new WaitForSeconds(20f);
        Debug.Log("TIMER");
        Next_round();
    }

    public void Pause()
    {
        IsPause = true;
        Time.timeScale = 0.01f;

        if (_startScreen.activeSelf)
        {
            _startScreen.SetActive(false);
            _isStartScreenNeeded = true;
        }

        _pauseScreen.SetActive(true);
    }

    public void Resume()
    {
        IsPause = false;
        _pauseScreen.SetActive(false);

        if (_isStartScreenNeeded)
        {
            _startScreen.SetActive(true);
            _isStartScreenNeeded = false;
        }

        Time.timeScale = 1f;
    }

    public void RestartLevel()
    {
        _levelInfo = new LevelInfo();
        _levelLoader.Load(_levelInfo);
    }

    public void RestartRound()
    {
        _levelLoader.Load(_levelInfo);
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
        _levelLoader.Load(_levelInfo);
    }

    public void MainMenuButtonClick()
    {
        IsPause = false;

        SaveMoney(_medalsInfo[(int)_medal].Reward);

        Time.timeScale = 1f;
        _loadingScreen.SetActive(true);

        _mainMenuLoader.Load();
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
        _levelLoader.Load(_levelInfo);
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
            Debug.Log("CRASH");
            _textCrash.gameObject.SetActive(true);
            _textTarget.gameObject.SetActive(false);
            _isGameOver = true;
            ShowRoundInfo();
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
        Debug.Log("TIME");
        _isGameOver = true;
        Invoke(nameof(StartButtonClick), 0.1f);
        Invoke(nameof(Next_round), 1f);
    }

    private void OnTriggerOUT()
    {
        _isTriggered = true;
        ShowRoundInfo();
        Invoke(nameof(Next_round), 0.1f);
    }

    private void OnTriggerWire()
    {
        _isTriggered = true;
        ShowRoundInfo();
        Invoke(nameof(Next_round), 2f);
    }

    private void Next_round()
    {
        _endRoundScreen.SetActive(true);
        Time.timeScale = 0.01f;

        ShowNextScreen();

        CalculateAndShowTotalScore();
    }

    private void ShowRoundInfo()
    {
        for (int i = 0; i < _levelInfo.CurrentRound; i++)
        {
            _roundsInfo[i].gameObject.SetActive(true);
            _textsScoreRound[i].text = _levelInfo.RoundsScore[i].ToString("0.00");
            Debug.Log($"Score {_levelInfo.RoundsScore[i]}");
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
}