using UnityEngine;
using UnityEngine.UI;


public class Select_cars_mainMenu : MonoBehaviour
{
    public Text Price_text;
    public GameObject Select_button;
    public GameObject By_button;
        [Space(20)]
    public GameObject[] All_Cars; 
    private int car_index;

    private void Awake()
    {
        car_index = PlayerPrefs.GetInt("Selected_car");

        All_Cars = new GameObject[transform.childCount];
        Price_Car();

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

    public void Select_Left()
    {
        All_Cars[car_index].SetActive(false);
        car_index--;
        if(car_index < 0)
        car_index = All_Cars.Length - 1;
        All_Cars[car_index].SetActive(true);
        Price_Car();
    }
    public void Select_Right()
    {
        All_Cars[car_index].SetActive(false);
        car_index++;
        if(car_index == All_Cars.Length)
        car_index = 0;
        All_Cars[car_index].SetActive(true);
        Price_Car();
    }

    public void Select_Car()
    {
        PlayerPrefs.SetInt("Selected_car", car_index);
        //Progress.Instance.PlayerInfo.Cars = car_index;
    }

    public void By_Car()
    {
        ////by_Car = 0;
        //if(by_Car < 0)
        //    print("need more money");
        //else
        //{
        //    PlayerPrefs.SetInt("Money_save", by_Car);
        //    //Progress.Instance.PlayerInfo.Cars = car_index; 
        //    PlayerPrefs.SetInt("Selected_car", car_index);
        //    PlayerPrefs.SetInt("Byed_car"+car_index, car_index);
        //    Price_Car();
        //}
    }

    void Price_Car()
    {
        //Progress.Instance.PlayerInfo.Cars = car_index;
        PlayerPrefs.GetInt("Byed_car"+car_index);
        switch(car_index)
        {
            case 0:
            Price_text.text = "Select";
            Select_button.SetActive(true);
            By_button.SetActive(false);
                break;
            case 1:
                if(PlayerPrefs.GetInt("Byed_car"+car_index) == car_index)
                {
                    Price_text.text = "Select";
                    Select_button.SetActive(true);
                    By_button.SetActive(false);
                }
                else
                {    
                    //price = Price_1_car;
                    //Price_text.text = price.ToString();
                    //Select_button.SetActive(false);
                    //By_button.SetActive(true);
                }
                break;
            case 2:
                if(PlayerPrefs.GetInt("Byed_car"+car_index) == car_index)
                {
                    Price_text.text = "Select";
                    Select_button.SetActive(true);
                    By_button.SetActive(false);
                }
                else
                {    
                    //price = Price_2_car;
                    //Price_text.text = price.ToString();
                    //Select_button.SetActive(false);
                    //By_button.SetActive(true); 
                }
                break;
        }
    }
}
