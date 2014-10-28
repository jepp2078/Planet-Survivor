using UnityEngine;
using System.Collections;

public class Vitals : MonoBehaviour {
	public CameraGUI gui;

	public int oxygen = 100; //procentage
	public int health = 100; //procentage

	public float ore = 0; //in kg
	public float metal = 0; //in kg

	public bool inside;
	private float timeSinceLastUpdate;

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.Find("Main Camera");
		gui = (CameraGUI) go.GetComponent(typeof(CameraGUI));
		gui.setVitals (1, health);
		gui.setVitals (2, oxygen);

		gui.setMinerals (1, ore);
		gui.setMinerals(2, metal);
	}
	
	// Update is called once per frame
	void Update () {
		if(!inside){
			timeSinceLastUpdate += Time.deltaTime;
			if (timeSinceLastUpdate > 0.1) {
				if(oxygen == 0){
					if(health == 0){
						timeSinceLastUpdate = 0;
					}else{
						health -= 5;
						gui.setVitals(2,health);
						timeSinceLastUpdate = 0;
					}
				}else{
					oxygen -= 1;
					gui.setVitals(1,oxygen);
					timeSinceLastUpdate = 0;
				}
			}
		}else{
			timeSinceLastUpdate += Time.deltaTime;
			if (timeSinceLastUpdate > 0.1) {
				if(oxygen == 100){

				}else{
					oxygen += 1;
					gui.setVitals(1,oxygen);
					timeSinceLastUpdate = 0;
				}
			}
		}
	}

	public void regenOxy(bool status){
		inside = status;
		Debug.Log (status);
	}

	public void setMinerals(int mineral, float amount){
		switch(mineral){
		case 1: 
			ore += amount;
			gui.setMinerals (mineral, ore);
			break;
		case 2: 
			metal += amount;
			gui.setMinerals (mineral, metal);
			break;
			
		}
	}
	public float getMinerals(int mineral){
		switch(mineral){
		case 1: 
			return ore;
			break;
		case 2: 
			return metal;
			break;
			
		}
		return 0;
	}
}
