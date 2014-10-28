using UnityEngine;
using System.Collections;

public class turret_script : MonoBehaviour {
	public GameObject turret_Body, turret_Gunmount, turret_Gun1, turret_Gun2, turret_GunPoint;

	private Quaternion startYaw, startPitch, start_turret_GunPoint; 

	// Use this for initialization
	void Start () {
		startYaw = turret_Body.transform.rotation;
		startPitch = turret_Gunmount.transform.rotation;
		start_turret_GunPoint = turret_GunPoint.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject Enemy = FindClosestEnemy();
		if(Enemy != null){
			Debug.DrawRay(turret_GunPoint.transform.position, turret_GunPoint.transform.forward, Color.red, 0.1f);
			Vector3 targetVector = Enemy.transform.position - turret_GunPoint.transform.position;
			Vector3 deltaRotation = turret_GunPoint.transform.forward - targetVector.normalized;
			turret_Body.transform.Rotate(0, deltaRotation.x, 0);
			//turret_Gunmount.transform.Rotate(0, deltaRotation.x ,0);
			Debug.DrawRay(turret_GunPoint.transform.position, targetVector.normalized, Color.green, 1);

			Debug.Log("delta rot: "+deltaRotation);

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
