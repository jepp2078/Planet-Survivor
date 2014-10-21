using UnityEngine;
using System.Collections;

public class Vitals : MonoBehaviour {
	public CameraGUI gui;
	public int oxygen = 100;
	public int health = 100;
	public bool outside = true;
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
		if(outside){
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
		}
	}
}
