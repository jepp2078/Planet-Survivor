﻿using UnityEngine;
using System.Collections;

public class Wire : MonoBehaviour {
	public GameObject source;
	public GameObject dest;
	public bool activated;
	public deviceInterface destInt;
	public int wireNum;
	public Generator gen;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine(source.transform.position, dest.transform.position, Color.green, 1, true);
	}
	public int getWireNum(){
		return wireNum;
	}

	public void setSource(GameObject inObject){
		source = inObject;
		gen = source.GetComponent<Generator> ();
	}

	public void setDest(GameObject inObject){
		dest = inObject;
		destInt = dest.GetComponent(typeof(deviceInterface)) as deviceInterface;
	}

	public void updatePower(){
		if(!activated){
			activated = true;
			destInt.power();
		}else{
			activated = false;
			destInt.power();
		}
	}

	public void connect(){
		if(gen.canConnect(destInt.getNeededPower())){
			wireNum = gen.connectWire(this.GetComponent<Wire>());
			gen.connect (destInt.getNeededPower());
		}
	}

	public void disconnect(){
		gen.disconnect (destInt.getNeededPower (),wireNum);
	}
}
	
