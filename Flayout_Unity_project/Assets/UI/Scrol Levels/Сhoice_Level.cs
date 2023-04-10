using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Сhoice_Level : MonoBehaviour
{
    public static bool Start_Level_1;
    void Start()
    {
        Start_Level_1 = false;
    }
    public void Start_Level()
    {
        Start_Level_1 = true;
    }
}
