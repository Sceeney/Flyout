﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/Mobile/Button")]
public class RCC_UIController1 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	internal float input;
	private float sensitivity{get{return RCC_Settings.Instance.UIButtonSensitivity;}}
	private float gravity{get{return RCC_Settings.Instance.UIButtonGravity;}}
	public bool pressing;


	public void OnPointerDown(PointerEventData eventData){

		pressing = true;

	}

	public void OnPointerUp(PointerEventData eventData){
		 
		pressing = false;
		
	}

	void OnPress (bool isPressed){

		if(isPressed)
			pressing = true;
		else
			pressing = false;

	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.D)){print("dddd");}
		
		if(pressing)
			input += Time.deltaTime * sensitivity;
		else
			input -= Time.deltaTime * gravity;
		
		if(input < 0f)
			input = 0f;
		if(input > 1f)
			input = 1f;
		
	}

	void OnDisable(){

		input = 0f;
		pressing = false;

	}

}
