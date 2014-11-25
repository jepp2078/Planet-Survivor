﻿using UnityEngine;
using System.Collections;

public class OxyGen : MonoBehaviour , deviceInterface{
	public bool activated = false;
	private int nPower = 500;
	public GameObject sphere;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public int getNeededPower(){
		return nPower;
	}
	
	public void power(){
		if(!activated){
			activated = true;
			sphere.collider.enabled = true;
			sphere.renderer.enabled = true;
		}else{
			activated = false;
			sphere.collider.enabled = false;
			sphere.renderer.enabled = false;
		}
	}
}
