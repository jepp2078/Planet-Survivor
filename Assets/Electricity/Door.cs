using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour , deviceInterface{
	public bool activated = false;
	private int nPower = 100;
	public GameObject solid;
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
			solid.collider.enabled = false;
			solid.renderer.enabled = false;
			solid.light.enabled = false;
			
		}else{
			activated = false;
			solid.light.enabled = false;
			solid.collider.enabled = true;
			solid.renderer.enabled = true;
		}
	}
}
