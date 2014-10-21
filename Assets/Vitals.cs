using UnityEngine;
using System.Collections;

public class Vitals : MonoBehaviour {
	public CameraGUI gui;
	public int oxygen = 100;
	public int health = 100;
	public bool inside;
	private float timeSinceLastUpdate;
	// Use this for initialization
	void Start () {
		GameObject go = GameObject.Find("Main Camera");
		gui = (CameraGUI) go.GetComponent(typeof(CameraGUI));
		gui.setVitals (1, 100);
		gui.setVitals (2, 100);
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
}
