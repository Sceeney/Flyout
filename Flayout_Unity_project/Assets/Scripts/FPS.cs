using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    public static float fps;
    GUIStyle style = new GUIStyle();
 
    void Start() {
        style.normal.textColor = Color.white;
        style.fontSize = 32;
        style.fontStyle = FontStyle.Bold;
    }
 
    void OnGUI() {
        fps = 1.0f / Time.deltaTime;
        GUI.Label(new Rect(10, 10, 100, 34), "FPS: " + (int)fps, style);
    }
 
}
