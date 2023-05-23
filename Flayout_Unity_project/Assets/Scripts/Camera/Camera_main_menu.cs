using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_main_menu : MonoBehaviour
{
    private int move_cam;

    public float speed_cam = 2; // скорость движения камеры
        [Space(20)]
    public Transform Start_pose;
    public Transform Main;
    public Transform Cars;
    public Transform Player;
    public Transform Options;
    public Transform Track;

    void Start()
    {
        transform.position = Vector3.Lerp(transform.position, Start_pose.position, speed_cam * Time.deltaTime);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Start_pose.localRotation, speed_cam * Time.deltaTime);
        move_cam = 1;
    }
    void Update()
    {
        move_cam = UI_Main_Menu.button_Index; // чекаем индекс кнопок главного меню
        Case_camera(); // двигаем камеру по индексу кнопки
        print(move_cam);
    }

    void Case_camera()
    {
        switch(move_cam)
        {        
            case 1:
                transform.position = Vector3.Lerp(transform.position, Main.position, speed_cam * Time.deltaTime);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, Main.localRotation, speed_cam * Time.deltaTime);
                    break;
            case 2:
                transform.position = Vector3.Lerp(transform.position, Cars.position, speed_cam * Time.deltaTime);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, Cars.localRotation, speed_cam * Time.deltaTime);
                    break;
            case 3:
                transform.position = Vector3.Lerp(transform.position, Player.position, speed_cam * Time.deltaTime);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, Player.localRotation, speed_cam * Time.deltaTime);
                    break;
            case 4:
                transform.position = Vector3.Lerp(transform.position, Options.position, speed_cam * Time.deltaTime);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, Options.localRotation, speed_cam * Time.deltaTime);
                    break;
            case 5:
                transform.position = Vector3.Lerp(transform.position, Track.position, speed_cam * Time.deltaTime);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, Track.localRotation, speed_cam * Time.deltaTime);
                    break;
        }
    }
}
