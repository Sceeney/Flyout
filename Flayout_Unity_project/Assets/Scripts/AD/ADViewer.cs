using System;
using UnityEngine;
using YG;

[Serializable]
public class ADViewer
{
    [SerializeField] private int _requiredNumberRacesToDisplayADS = 2;
    [SerializeField] private int _requiredNumderRestartsToDisplayADS = 2;

    [SerializeField] private int _currentNumberRaces;
    [SerializeField] private int _currentNumberRestarts;

    public int CurrentNumberRaces => _currentNumberRaces;
    public int CurrentNumberRestarts => _currentNumberRestarts;

    public void UpdateNumderRaces()
    {
        _currentNumberRaces++;
        TryShowADS();
    }

    public void UpdateNumberRestarts()
    {
        _currentNumberRestarts++;
        TryShowADS();
    }

    public void ShowAD()
    {
        YandexGame.FullscreenShow();
    }

    public void ShowRewAD(int id)
    {
        YandexGame.RewVideoShow(id);
    }

    private void TryShowADS()
    {
        if (_currentNumberRaces >= _requiredNumberRacesToDisplayADS)
        {
             ShowRewAD(0);
            _currentNumberRaces = 0;
        }
        else if (_currentNumberRestarts >= _requiredNumderRestartsToDisplayADS)
        {
            ShowAD();
            _currentNumberRestarts = 0;
        }
    }
}
