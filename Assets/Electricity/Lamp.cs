using UnityEngine;
using System.Collections;

public class Lamp : MonoBehaviour , deviceInterface{
	public bool activated = false;
	private int nPower = 100;
	public Light myLight;
	// Use this for initialization
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
			myLight.enabled = true;
		}else{
			activated = false;
			myLight.enabled = false;
		}
	}
}
