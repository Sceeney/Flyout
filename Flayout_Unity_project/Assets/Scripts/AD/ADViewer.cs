using System;
using UnityEngine;
using YG;

public class ADViewer : MonoBehaviour
{
    [SerializeField] private int _requiredNumberRacesToDisplayADS = 3;
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
            YandexGame.FullscreenShow();
            CurrentNumberRaces = 0;
        }
        else if (CurrentNumberRestarts >= _requiredNumderRestartsToDisplayADS)
        {
            YandexGame.FullscreenShow();
            CurrentNumberRestarts = 0;
        }
    }
}
