using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity_Car : MonoBehaviour
{
    Rigidbody rb;
    private Vector3 v3Velocity;
    public static Vector3 vel;
    public static bool car_collision_triggered;
    public static bool end_track;
    
    void Start()
    {
        rb = GetComponent <Rigidbody>();
        car_collision_triggered = false;
        end_track = false;
    }
    
    void Update()
    {
        Vector3 v3Velocity = rb.velocity;
        vel = v3Velocity;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
            car_collision_triggered = true;
        if (collision.gameObject.tag == "Finish")
            end_track = true;
    }
    public void OnCollisionExit(Collision collision)
    {
        car_collision_triggered = false;
    }

}
