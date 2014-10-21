using UnityEngine;
using System.Collections;

public class OxySphere : MonoBehaviour {
	public Vitals vital;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		vital = (Vitals) other.GetComponent (typeof(Vitals));
		vital.regenOxy (true);
	}

	void OnTriggerExit(Collider other) {
		vital = (Vitals) other.GetComponent (typeof(Vitals));
		vital.regenOxy (false);
	}
}
