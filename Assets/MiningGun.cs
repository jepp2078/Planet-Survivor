﻿using UnityEngine;
using System.Collections;

public class MiningGun : MonoBehaviour {

	private RaycastHit hit;
	GameObject GunPoint;
	GameObject CameraView;
	GameObject VitalObj;

	public float miningSpeed = 0.10f;
	public float orePerCycle = 10;
	private float timeSinceLastUpdate;
	public Vitals vital;
	
	// Use this for initialization
	void Start () {
		GunPoint = GameObject.Find("GunPoint");
		CameraView = GameObject.Find("Main Camera");
		VitalObj = GameObject.Find("First Person Controller");
		vital = (Vitals) VitalObj.GetComponent (typeof(Vitals));
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = new Ray (CameraView.transform.position, CameraView.transform.forward);
		
		if(Input.GetButton("Fire1")){
			if(Physics.Raycast(ray, out hit)){
				if(hit.collider.gameObject.name == "Stone"){
					timeSinceLastUpdate += Time.deltaTime*2;
					Debug.Log(timeSinceLastUpdate);
					if (timeSinceLastUpdate > miningSpeed) {
						timeSinceLastUpdate = 0;
						vital.setMinerals(1, orePerCycle);
					}
				}
				if(hit.collider.gameObject.name == "tiberium"){
					timeSinceLastUpdate += Time.deltaTime*1;
					Debug.Log(timeSinceLastUpdate);
					if (timeSinceLastUpdate > miningSpeed) {
						timeSinceLastUpdate = 0;
						vital.setMinerals(2, orePerCycle);
					}
				}
			}
		}
	}
}