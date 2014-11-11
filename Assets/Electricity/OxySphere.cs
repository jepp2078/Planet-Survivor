using UnityEngine;
using System.Collections;

public class OxySphere : MonoBehaviour {
	public Vitals vital;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0, 1, 0), Time.deltaTime/2, Space.Self);
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
