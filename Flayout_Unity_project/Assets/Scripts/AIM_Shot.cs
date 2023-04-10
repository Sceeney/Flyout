using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AIM_Shot: MonoBehaviour
{   
    // Булы
    public static bool slow_mo;
    public static bool is_Crash;
    public static bool end_track;
    public static bool is_Click;

    // Гейм обжекты
    public GameObject Player; // тело
        [Space(20)]
    public GameObject Camera_Car;
    public GameObject Camera_Player;
        [Space(20)]
    public GameObject Target_AIM;
        [Space(20)]

    // Переменные
        [SerializeField] float Angle_rotation_up_down = 90f; // значение угла 
        //[SerializeField] int speed_rot_Angle = 3;
        public static float Force_Shoot = 11f; // сила импульса
        float angle;
        private Vector3 Vector_rot;
        public static Vector3 Speed_shoot;
        public static float Value_angle_shot;

        float countdown = 15.0f;
        public static bool is_countdown;

    void Start()
    {
        Player.SetActive(false);
        Camera_Car.SetActive(true);
        Camera_Player.SetActive(false);
        Time.timeScale = 1f;

        slow_mo = false;
        is_Crash = false;
        is_Click = false;
        end_track = false;
        is_countdown = false;
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        if(!is_countdown){
            if(countdown <= 0.0f && Main_Script.start_but == false){
                Crash();
                is_Crash = true; 
                is_countdown = true;}
        }

        Angle_rotation_right_left(); // чекаем куда поворачивает тачка

        if(!is_Click)// чекаем когда придёт нажатие кнопки
            {
                if(Main_Script.bool_shoot == true){
                is_Click = true;
                Shoot();}
            }
        
        if(!is_Crash)
        {
            if(Velocity_Car.car_collision_triggered == true){ // чекаем если чел втащится
                is_Crash = true; 
                Crash();}
        }

        if(Velocity_Car.end_track == true) // чекаем когда конец трека
        {
            end_track = true; 
        }
    }

    public void OnTriggerEnter(Collider other) // вошёл в триггер, замедляю время и даю выбор угла
    {
        if(is_Crash == false)
        {        
            if(other.gameObject.name != "trigger") return;//state != State.no_Work && 
            slow_mo = true;
            StartCoroutine("action");
        }    
    }   
    IEnumerator action()
    {
        //state = State.is_Work;
        Time.timeScale = 0.2f;
        for (int i = 0; i >= 0; i++)
        {
            if(slow_mo == true)
            {  
                print("1");
                Value_angle_shot = Mathf.PingPong(i,Angle_rotation_up_down);
                Vector_rot = new Vector3(-Value_angle_shot, angle, 0f);
                transform.rotation = Quaternion.Euler(Vector_rot);
                yield return new WaitForSeconds(0.002f); // кол-во повторов 
            }
            else yield break;
        }
    }
    
    void Angle_rotation_right_left() // вычесляю угол поворота машины
    {
        if(slow_mo == false)
        {
            Vector3 targetDir = new Vector3(0,0,0) - transform.position;
            Vector3 forward = transform.forward;
            angle = Vector3.SignedAngle(targetDir, -forward, Vector3.up);
        }
    }

    void Shoot() // стреляю человеком
    {
        slow_mo = false;
        is_Crash = true;
        Player.SetActive(true);
        Camera_Car.SetActive(false);
        Camera_Player.SetActive(true);
        Speed_shoot = Target_AIM.transform.position - transform.position;
        Time.timeScale = 1f;
    }

    void Crash() // если втащился
    {
        slow_mo = false;
        Time.timeScale = 1f;
    }

}
