using UnityEngine;
using System.Collections;

public class turret_script : MonoBehaviour {
	public GameObject turret_Body, turret_Gunmount, turret_Gun1, turret_Gun2, turret_GunPoint;
	public float RotationSpeed;
	public float shotDelay;
	public Rigidbody projectile; 
	public float gunpower;

	private Quaternion startYaw, startPitch, start_turret_GunPoint; 
	private Quaternion _lookRotation;
	private Vector3 _direction;

	private float lastShotTime;

	// Use this for initialization
	void Start () {
		startYaw = turret_Body.transform.rotation;
		startPitch = turret_Gunmount.transform.rotation;
		start_turret_GunPoint = turret_GunPoint.transform.rotation;

		lastShotTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject Enemy = FindClosestEnemy();
		if(Enemy != null){

			_direction = (Enemy.transform.position - transform.position).normalized;
			_direction.y = 0;
			_lookRotation = Quaternion.LookRotation(_direction);
			Quaternion slerpRot = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
			transform.rotation = slerpRot;

			_direction = (Enemy.transform.position - turret_Gunmount.transform.position).normalized;
			_lookRotation = Quaternion.LookRotation(_direction);

			_lookRotation *= Quaternion.Euler(90, -90, 0);

			slerpRot = Quaternion.Slerp(turret_Gunmount.transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
			turret_Gunmount.transform.rotation = slerpRot;
		}

		if(Time.time - lastShotTime > shotDelay){
			Debug.Log("bang!");
			lastShotTime = Time.time;
			shoot (Enemy);
		}
	}

	void shoot(GameObject target){
		Rigidbody tempProjectile;
		tempProjectile = Instantiate(projectile, turret_GunPoint.transform.position, turret_GunPoint.transform.rotation) as Rigidbody;
		tempProjectile.velocity = turret_GunPoint.transform.TransformDirection(Vector3.forward * gunpower);
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
