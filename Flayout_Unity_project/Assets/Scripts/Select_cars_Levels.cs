using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Select_cars_Levels : MonoBehaviour
{
    public GameObject[] All_Cars; 
    private int car_index;

    private void Awake()
    {
        car_index = PlayerPrefs.GetInt("Selected_car");
        All_Cars = new GameObject[transform.childCount];

        for(int i = 0; i< transform.childCount; i++)
        {
            All_Cars[i] = transform.GetChild(i).gameObject;
        }

        foreach(GameObject go in All_Cars)
        {
            go.SetActive(false);
        }
        if(All_Cars[car_index])
        {
            All_Cars[car_index].SetActive(true);
        }
    }



}
