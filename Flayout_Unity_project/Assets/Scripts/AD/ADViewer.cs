using System;
using Unity.VisualScripting;
using UnityEngine;
using YG;

[Serializable]
public class ADViewer
{
    [SerializeField] private int _requiredNumberRacesToDisplayADS = 2;
    [SerializeField] private int _requiredNumderRestartsToDisplayADS = 2;

    public int CurrentNumberRaces { get; private set; }
    public int CurrentNumberRestarts { get; private set; }

    public void UpdateNumderRaces()
    {
        CurrentNumberRaces++;
        TryShowADS();
    }

    public void UpdateNumberRestarts()
    {
        CurrentNumberRestarts++;
        TryShowADS();
    }

    private void TryShowADS()
    {
        if (CurrentNumberRaces >= _requiredNumberRacesToDisplayADS)
        {
            YandexGame.RewVideoShow(0);
            CurrentNumberRaces = 0;
        }
        else if (CurrentNumberRestarts >= _requiredNumderRestartsToDisplayADS)
        {
            YandexGame.RewVideoShow(0);
            CurrentNumberRestarts = 0;
        }
    }
}
