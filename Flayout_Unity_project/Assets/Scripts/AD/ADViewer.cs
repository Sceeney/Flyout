using System;
using Unity.VisualScripting;
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

    private void TryShowADS()
    {
        if (_currentNumberRaces >= _requiredNumberRacesToDisplayADS)
        {
            YandexGame.RewVideoShow(0);
            _currentNumberRaces = 0;
        }
        else if (_currentNumberRestarts >= _requiredNumderRestartsToDisplayADS)
        {
            YandexGame.FullscreenShow();
            _currentNumberRestarts = 0;
        }
    }
}
