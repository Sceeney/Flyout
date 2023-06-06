using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RCC_CarControllerV3Initializer : MonoBehaviour
{
    [SerializeField] private RCC_CarControllerV3 _controller;
    [SerializeField] private Main_Script _main_Script;

    private void OnValidate()
    {
        if (_controller.Main_Script == null)
            _controller.Main_Script = _main_Script;
    }
}
