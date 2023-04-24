using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options_Controls : MonoBehaviour
{
    public GameObject Buttons_ON;
        [Space(10)]
    public GameObject Accelerometer_ON;
        [Space(10)]
    public GameObject Wheel_ON;

    private float Controllers_type;

    void Start()
    {
			Controllers_type = PlayerPrefs.GetFloat("Controls_save");
            Choose_Controls();
    }

    void Choose_Controls()
    {
        switch(Controllers_type)
        {
        case 0:
            Button_click();
            break;
        case 1:
            Accelerometer_click();
            break;
        case 2:
            Wheel_click();
            break;
        default:
            Buttons_ON.SetActive(true);
            Accelerometer_ON.SetActive(false);
            Wheel_ON.SetActive(false);
            break;
        }
    }

    public void Button_click(){
            PlayerPrefs.SetFloat("Controls_save",0f);
            Buttons_ON.SetActive(true);
            Accelerometer_ON.SetActive(false);
            Wheel_ON.SetActive(false);
    }
    public void Accelerometer_click(){
            PlayerPrefs.SetFloat("Controls_save",1f);
            Buttons_ON.SetActive(false);
            Accelerometer_ON.SetActive(true);
            Wheel_ON.SetActive(false);
    }
    public void Wheel_click(){
            PlayerPrefs.SetFloat("Controls_save",2f);
            Buttons_ON.SetActive(false);
            Accelerometer_ON.SetActive(false);
            Wheel_ON.SetActive(true);
    }

}
