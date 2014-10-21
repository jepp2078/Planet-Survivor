using UnityEngine;
using System.Collections;

public class PhysicsGun : MonoBehaviour {
	public float rayLenght;
	public float rayForce;
	public float objectScale;

	private Rigidbody currentObject;
	private RaycastHit hit;
	private Vector3 currentObjectOriginalPos;
	private GameObject GunPoint;
	private GameObject CameraView;

	private bool holdingObject;

	// Use this for initialization
	void Start () {
		holdingObject = false;
		rayLenght = 100;
		rayForce = 100;
		objectScale = 1;

		Debug.Log("finding things");
		GunPoint = GameObject.Find("GunPoint");
		CameraView = GameObject.Find("Main Camera");
	}

	// Update is called once per frame
	void Update() {
		if(GunPoint==null || CameraView==null){
			GunPoint = GameObject.Find("GunPoint");
			CameraView = GameObject.Find("Main Camera");
		}

		bool fireDown = Input.GetButtonDown("Fire1");
		if(Input.GetButton("Fire1")){
			//Debug.DrawRay (CameraView.transform.position, CameraView.transform.forward*rayLenght, Color.green, 0.5f, true);
		}
		//if(fireDown && !holdingObject){
		if(Input.GetButton("Fire1") && !holdingObject){
			Ray ray = new Ray (CameraView.transform.position, CameraView.transform.forward);
			//Debug.DrawRay (transform.position, transform.forward*rayLenght, Color.green, 10, true);

			if (Physics.Raycast(ray, out hit, rayLenght)){
				if (hit.rigidbody){
					currentObject = hit.rigidbody;
					holdingObject = true;
					currentObjectOriginalPos = currentObject.position;
					rayLenght = hit.distance;
					Debug.Log ("Ray Lenght: " + rayLenght);
					
					//Debug.DrawRay (hit.point, hit.normal, Color.red, rayLenght, true);
					//Debug.Log("hit: "+ hit.rigidbody.name);
				}
			}
		}else if(Input.GetButtonUp("Fire1")){
			holdingObject = false;
			rayLenght = 100;
			Debug.Log("releasing object");
		}

		if(holdingObject){
			rayLenght += Input.GetAxis("Mouse ScrollWheel");
		
			Vector3 gunVector = CameraView.transform.TransformPoint(Vector3.forward*objectScale*rayLenght);
			Vector3 objectToGun = gunVector-currentObject.position;

			Debug.Log("Gunpoint: "+GunPoint);

			Debug.DrawLine(GunPoint.transform.position, gunVector, Color.blue, 1, true);
			Debug.DrawLine(currentObject.position, gunVector, Color.yellow, 1, true);

			Vector3 relativeHit = (hit.point-currentObjectOriginalPos)+currentObject.position;
			objectToGun.Scale(new Vector3(rayForce, rayForce, rayForce));
			
			Vector3 CurrentObjectVelocity = currentObject.velocity;
			CurrentObjectVelocity.Scale(new Vector3(10, 10, 10));
			objectToGun += -CurrentObjectVelocity;
			
			currentObject.AddForce(objectToGun, ForceMode.Acceleration);
		}
	}
}










