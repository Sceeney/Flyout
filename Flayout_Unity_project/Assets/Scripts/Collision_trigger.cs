using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_trigger : MonoBehaviour
{
    public static bool trigger_out;
    public static bool trigger_wire;

    void Start()
    {
        trigger_out = false;
        trigger_wire = false;
    }
    void OnCollisionEnter(Collision other) // Касание коллизии
    {
        if(other.gameObject.tag == "Body" && this.gameObject.tag == "Out_zone")
            trigger_out = true;
        if(other.gameObject.tag == "Body" && this.gameObject.tag == "Wire")
            trigger_wire = true;
   }
}
