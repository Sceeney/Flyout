using System;
using System.ComponentModel;
using UnityEngine;
using YG;

public class ADViewer
{
    private int _requiredNumberRacesToDisplayADS;
    private int _requiredNumderRestartsToDisplayADS;
    
    private int _currentNumberRaces;
    private int _currentNumberRestarts;

    public int RequiredNumberRacesToDisplayADS => _requiredNumberRacesToDisplayADS;
    public int RequiredNumderRestartsToDisplayADS => _requiredNumderRestartsToDisplayADS;
    public int CurrentNumberRaces => _currentNumberRaces;
    public int CurrentNumberRestarts => _currentNumberRestarts;

    public ADViewer(ADInfo adInfo)
    {
        _requiredNumberRacesToDisplayADS = adInfo.RequireRaces;
        _requiredNumderRestartsToDisplayADS = adInfo.RequireRestarts;
    }

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
             //ShowRewAD(0);
            _currentNumberRestarts = 0;
        }
    }
}
