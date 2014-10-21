using UnityEngine;
using System.Collections;

public class CameraGUI : MonoBehaviour {
	public Texture2D CrossHairTex;
	public Texture2D OxygenTex;
	public GameObject[] objectArray;
	public GameObject[] WeaponArray;
	public string Oxygen = "100";

	private GameObject CurrentWeapon;
	private GameObject WeaponPosition;

	private Rect CrossHairPosition;
	private Rect OxygenPosition;
	private GameObject currentObject;
	//private GameObject CameraObject;
	private bool openMenu = false;
	
	private MouseLook CameraMouseLook;
	private MouseLook FPCMouseLook;
	
	private float tempSensX;
	private float tempSensY;
	private bool camFrozen = false;
	
	void Start(){
		GameObject CameraObject = GameObject.Find("Main Camera");
		CameraMouseLook = CameraObject.GetComponent<MouseLook>();
		
		GameObject FPCObject = GameObject.Find("First Person Controller");
		FPCMouseLook = FPCObject.GetComponent<MouseLook>();

		tempSensX = FPCMouseLook.sensitivityX;
		tempSensY = CameraMouseLook.sensitivityY;


		WeaponPosition = GameObject.Find("WeaponPosition");
		if(WeaponPosition==null)
			Debug.LogError("WeaponPosition not found!");
		//CurrentWeapon = WeaponArray[0];
		switchWeapon(WeaponArray[0]);
	}

	void switchWeapon(GameObject Weapon){
		Destroy(CurrentWeapon);

		CurrentWeapon = Instantiate(Weapon, WeaponPosition.transform.position, WeaponPosition.transform.rotation) as GameObject;
		CurrentWeapon.transform.parent = WeaponPosition.transform;
		//CurrentWeapon.transform.position = WeaponPosition.transform.position;
	}

	void Update () {
		CrossHairPosition = new Rect((Screen.width - CrossHairTex.width)/2, (Screen.height - CrossHairTex.height)/2, CrossHairTex.width, CrossHairTex.height);
		OxygenPosition = new Rect(0, Screen.height-(Screen.height/8), OxygenTex.width/2, OxygenTex.height/2);

		if(Input.GetKey(KeyCode.Alpha1))
			switchWeapon(WeaponArray[0]);
		else if(Input.GetKey(KeyCode.Alpha2))
			switchWeapon(WeaponArray[1]);


		if(Input.GetKey(KeyCode.Q)){
			if(!camFrozen){
				tempSensX = FPCMouseLook.sensitivityX;
				tempSensY = CameraMouseLook.sensitivityY;
				
				FPCMouseLook.sensitivityX = 0;
				CameraMouseLook.sensitivityY = 0;
				camFrozen = true;
			}
		}else{
			if(camFrozen){
				FPCMouseLook.sensitivityX = tempSensX;
				CameraMouseLook.sensitivityY = tempSensY;
				camFrozen = false;
			}
		}
		
		if(Input.GetKeyDown(KeyCode.Q)){
			openMenu = true;
		}
		if(Input.GetKeyUp(KeyCode.Q)){
			openMenu = false;
		}
	}
	
	void rezItem(GameObject rezObject){
		RaycastHit hit;
		Ray ray = new Ray (transform.position, transform.forward);
		//Debug.DrawRay (transform.position, transform.forward*rayLenght, Color.green, 10, true);
		
		if (Physics.Raycast(ray, out hit, 100)){
			Debug.DrawLine(ray.origin, hit.point, Color.green, 10, true);
			
			if (hit.collider){
				Instantiate(rezObject, hit.point, Quaternion.identity);
			}
		}
	}
	
	void OnGUI () {
		GUI.DrawTexture(CrossHairPosition, CrossHairTex); //Draws the crosshair
		GUI.DrawTexture(OxygenPosition, OxygenTex); //Draws the Oxygen Marker
		Vector2 OxyCenter = OxygenPosition.center;
		Rect OxyLabel = new Rect (OxyCenter.x - 10, OxyCenter.y - 10, OxygenTex.width, OxygenTex.height);
		GUI.Label(OxyLabel, "100"); //Draws the Oxygen Text
	
		if(openMenu){
			int boxL = 120; //Horizontal lenght of the menu
			
			// Make a background box
			GUI.Box(new Rect(10,10,120,30+30*objectArray.Length), "Spawn Menu");
			
			for (int i=0; i < objectArray.Length; i++) {
				if (GUI.Button (new Rect (20, 40+(30*i), boxL-20, 20), ""+objectArray[i].name)) {
					rezItem(objectArray[i]);
				}
			}
		}
	}
}
