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
        //YandexGame.LoadLocal();

        car_index = YandexGame.savesData.LastSelectedCarIndex;
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