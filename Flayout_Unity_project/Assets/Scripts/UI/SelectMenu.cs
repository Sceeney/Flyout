using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMenu : MonoBehaviour
{
    [SerializeField] private GameObject _MainMenu;
    [SerializeField] private GameObject _SelectCar;
    [SerializeField] private GameObject _StartRace;
    [SerializeField] private GameObject _Character;
    [SerializeField] private GameObject _Options;
    [SerializeField] private GameObject _Back;


    public void MainMenu()
    {
        _Back.SetActive(false);
        _SelectCar.SetActive(false);
        _StartRace.SetActive(false);
        _Character.SetActive(false);
        _Options.SetActive(false);
        _MainMenu.SetActive(true);

    }
    public void SelectCar()
    {
        _MainMenu.SetActive(false);
        _SelectCar.SetActive(true);
        _Back.SetActive(true);
    }
    public void StartRace()
    {
        _MainMenu.SetActive(false);
        _StartRace.SetActive(true);
        _Back.SetActive(true);
    }
    public void Character()
    {
        _MainMenu.SetActive(false);
        _Character.SetActive(true);
        _Back.SetActive(true);
    }
    public void Options()
    {
        _MainMenu.SetActive(false);
        _Options.SetActive(true);
        _Back.SetActive(true);
    }
}
