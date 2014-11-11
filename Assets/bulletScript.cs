using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {
	private float starttime;
	// Use this for initialization
	void Start () {
		starttime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - starttime > 1) {
			Destroy (this.gameObject);
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		foreach (ContactPoint contact in collision.contacts) {
			Debug.DrawRay(contact.point, contact.normal, Color.white);
		}
		if (collision.gameObject.name == "Target") {

		}
	}
}
