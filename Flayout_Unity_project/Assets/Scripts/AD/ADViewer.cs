using System;
using System.ComponentModel;
using UnityEngine;
using YG;

[Serializable]
public class ADViewer
{
    [SerializeField] private int _requiredNumberRacesToDisplayADS;
    [SerializeField] private int _requiredNumderRestartsToDisplayADS;

    
    [SerializeField] private int _currentNumberRaces;
    [SerializeField] private int _currentNumberRestarts;

    public int RequiredNumberRacesToDisplayADS => _requiredNumberRacesToDisplayADS;
    public int RequiredNumderRestartsToDisplayADS => _requiredNumderRestartsToDisplayADS;
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
            //ShowAD();
             ShowRewAD(0);
            _currentNumberRestarts = 0;
        }
    }
}
