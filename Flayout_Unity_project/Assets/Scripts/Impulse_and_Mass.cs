using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Impulse_and_Mass : MonoBehaviour
{
    Rigidbody rb;
    public static float Value_Height;
    public static float Value_Distance;
    public static bool trigger;
    AudioSource audioSource;

    private bool trigger_out;

    void Start()
    {   
        trigger = true;
        audioSource = GetComponent <AudioSource> ();
        rb = GetComponent <Rigidbody>();
        rb.velocity = (Velocity_Car.vel / 2)+(AIM_Shot.Speed_shoot * Random.Range((AIM_Shot.Force_Shoot -1), (AIM_Shot.Force_Shoot +1)));
        rb.mass = 10f;
        Value_Height = 0f;
    }

    void OnTriggerEnter(Collider other) // Касание коллизии
    {
        rb.velocity = new Vector3(0,0,0);
        rb.angularVelocity = new Vector3(0,0,0);
        trigger = false;
        if (this.gameObject.tag == "Body")
            audioSource.Play();
    }

    void Update()
    {     
        if (this.gameObject.tag == "Body" && trigger == true){
            Value_Height = transform.position.y;
            Value_Distance = transform.position.z;}
        else if(trigger_out == true)
            Value_Height = 0f;
        else if(Velocity_Car.car_collision_triggered == true && Main_Script.IsShootInfoDisplay == false)
            Value_Height = 0f;
    }

    private void OnEnable()
    {
        trigger_out = false;
        Collision_trigger.TriggerOUT += OUT;
    }

    private void OnDisable()
    {
        Collision_trigger.TriggerOUT -= OUT;
    }

    private void OUT()
    {
        trigger_out = true;
    }
}
