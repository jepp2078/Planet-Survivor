using UnityEngine;
using System.Collections;

public class OreRefinery : MonoBehaviour , deviceInterface{
	public bool activated = false;
	private int nPower = 250;
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
		}else{
			activated = false;
		}
	}

	public bool run(){
		return activated;
	}
}
