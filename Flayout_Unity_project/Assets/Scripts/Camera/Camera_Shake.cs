using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Shake : MonoBehaviour
{
    public Main_Script Main_Script;
    public Transform _camera;
    public float offsetX = 0.25f;
    public float offsetY = 0.25f;
    public float offsetZ = 0.25f;
    public float speed = 0.25f;

    void Update()
    {
        //Shake();
    }

    void Shake()
    {
        if(Main_Script.IsStartBut == false)
        {
            Quaternion _rotate = Quaternion.Euler(Random.Range(-offsetX, offsetX), Random.Range(-offsetY, offsetY), Random.Range(-offsetZ, offsetZ));
            _camera.transform.localRotation = Quaternion.Slerp(_camera.localRotation, _camera.localRotation * _rotate, speed);    
        }
        else
        {
            Quaternion _rotate = Quaternion.Euler(0f,0f,0f);
            _camera.transform.localRotation = Quaternion.Slerp(_camera.localRotation, _camera.localRotation * _rotate, speed);
        }
    } 

    void Stop_Shake()
    {
        _camera.transform.localRotation = Quaternion.Euler(0f,0f,0f);
    }
}
    