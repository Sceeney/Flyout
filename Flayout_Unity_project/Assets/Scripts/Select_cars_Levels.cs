using UnityEngine;
using YG;

public class Select_cars_Levels : MonoBehaviour
{
    public GameObject[] All_Cars; 
    private int car_index;

    private void OnEnable() => YandexGame.GetDataEvent += GetData;
    private void OnDisable() => YandexGame.GetDataEvent -= GetData;

    private void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            GetData();
        }
    }

    private void GetData()
    {
        car_index = YandexGame.savesData.LastSelectedCarIndex;

        All_Cars[car_index].gameObject.SetActive(true);
    }
}