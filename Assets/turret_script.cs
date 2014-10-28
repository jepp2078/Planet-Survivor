using UnityEngine;
using System.Collections;

public class turret_script : MonoBehaviour {
	public GameObject turret_Body, turret_Gunmount, turret_Gun1, turret_Gun2;

	private Quaternion startYaw, startPitch; 

	// Use this for initialization
	void Start () {
		startYaw = turret_Body.transform.rotation;
		startPitch = turret_Gunmount.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject Enemy = FindClosestEnemy();
		if(Enemy != null){
			turret_Body.transform.Rotate(0, 1, 0);
		}
	}

	GameObject FindClosestEnemy() {
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}
}
