using UnityEngine;
using System.Collections;

public class CameraGUI : MonoBehaviour {
	public Texture2D CrossHairTex;
	public Texture2D WindowTex;
	public GameObject[] objectArray;
	public GameObject[] WeaponArray;
	private cost price;

	private string Oxygen;
	private string Health;

	private string Ore;
	private string Metal;

	private GameObject CurrentWeapon;
	private GameObject WeaponPosition;
	private GameObject currentObject;

	private Rect CrossHairPosition;
	private Rect OxygenPosition;
	private Rect HealthPosition;
	//private GameObject CameraObject;
	private bool openMenu = false;
	private bool openMineralMenu = false;
	
	private MouseLook CameraMouseLook;
	private MouseLook FPCMouseLook;

	GameObject VitalObj;
	public Vitals vital;

	
	private float tempSensX;
	private float tempSensY;
	private bool camFrozen = false;
	
	void Start(){
		GameObject CameraObject = GameObject.Find("Main Camera");
		CameraMouseLook = CameraObject.GetComponent<MouseLook>();

		VitalObj = GameObject.Find("First Person Controller");
		vital = (Vitals) VitalObj.GetComponent (typeof(Vitals));
		
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
		OxygenPosition = new Rect(0+Screen.width/128, Screen.height-(Screen.height/8), WindowTex.width/2, WindowTex.height/2);
		HealthPosition = new Rect(Screen.width-Screen.width/8.5f, Screen.height-(Screen.height/8), WindowTex.width/2, WindowTex.height/2);

		if (currentObject != null) {
			MoveTheObject();
		}

		if(Input.GetKey(KeyCode.Alpha1))
			switchWeapon(WeaponArray[0]);
		else if(Input.GetKey(KeyCode.Alpha2))
			switchWeapon(WeaponArray[1]);
		else if(Input.GetKey(KeyCode.Alpha3))
			switchWeapon(WeaponArray[2]);


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
			Screen.lockCursor = false;
		}
		if(Input.GetKeyUp(KeyCode.Q)){
			openMenu = false;
			Screen.lockCursor = true;
		}

		if(Input.GetKeyDown(KeyCode.Z)){
			openMineralMenu = true;
			Screen.lockCursor = false;
		}
		if(Input.GetKeyUp(KeyCode.Z)){
			openMineralMenu = false;
			Screen.lockCursor = true;
		}

		if (Input.GetKeyDown("escape"))
			Screen.lockCursor = false;
		
		if (!Screen.lockCursor && wasLocked) {
			wasLocked = false;
			DidUnlockCursor();
		} else
		if (Screen.lockCursor && !wasLocked) {
			wasLocked = true;
			DidLockCursor();
		}
	}
	
	void rezItem(GameObject rezObject){
		RaycastHit hit;
		Ray ray = new Ray (transform.position, transform.forward);
		//Debug.DrawRay (transform.position, transform.forward*rayLenght, Color.green, 10, true);
		
		if (Physics.Raycast(ray, out hit, 100)){
			Debug.DrawLine(ray.origin, hit.point, Color.green, 10, true);
			
			if (hit.collider){
				cost price = (cost) rezObject.GetComponent (typeof(cost));
				int mineralType = price.getCostType();
				if(vital.getMinerals(mineralType) >= price.getPrice()){
					currentObject = (GameObject)Instantiate(rezObject, hit.point, Quaternion.identity);
					vital.setMinerals(mineralType, price.getPrice()*-1);
				}
			}
		}
	}
	private bool wasLocked = false;
	
	void OnGUI () {
		GUI.DrawTexture(CrossHairPosition, CrossHairTex); //Draws the crosshair
		GUI.DrawTexture(OxygenPosition, WindowTex); //Draws the Oxygen Marker
		GUI.DrawTexture(HealthPosition, WindowTex); //Draws the Health Marker

		Vector2 OxyCenter = OxygenPosition.center;
		Rect OxyLabel = new Rect (OxyCenter.x - 10, OxyCenter.y - 10, WindowTex.width, WindowTex.height);
		Rect OxyLabelText = new Rect (OxyCenter.x - 25, OxyCenter.y - 25, WindowTex.width, WindowTex.height);
		GUI.Label(OxyLabel, Oxygen); //Draws the Oxygen Text
		GUI.Label(OxyLabelText, "OXYGEN"); //Draws the Oxygen Text

		Vector2 HealthCenter = HealthPosition.center;
		Rect  HealthLabel = new Rect (HealthCenter.x - 10, HealthCenter.y - 10, WindowTex.width, WindowTex.height);
		Rect  HealthLabelText = new Rect (HealthCenter.x - 25, HealthCenter.y - 25, WindowTex.width, WindowTex.height);
		GUI.Label(HealthLabel, Health); //Draws the Health Text
		GUI.Label(HealthLabelText, "HEALTH"); //Draws the Health Text
	
		if(openMenu){
			int boxL = 120; //Horizontal lenght of the menu
			
			// Make a background box
			GUI.Box(new Rect(10,10,120,30+30*objectArray.Length), "Spawn Menu");
			
			for (int i=0; i < objectArray.Length; i++) {
				if (GUI.Button (new Rect (20, 40+(30*i), boxL-20, 20), ""+objectArray[i].name)) {
					Debug.Log("placing"+objectArray[i].name);
					rezItem(objectArray[i]);
				}
			}
		}

		if(openMineralMenu){
			int boxL = 120; //Horizontal lenght of the menu
			
			// Make a background box
			GUI.Box(new Rect(10,10,120,30+30*5), "Minerals");
			GUI.Label (new Rect (20, 40+(30*0), boxL-20, 20), ""+"Ore: "+Ore);
			GUI.Label (new Rect (20, 40+(30*1), boxL-20, 20), ""+"Metal: "+Metal);
		}
	}

	public void setVitals(int vital, int value){
		switch(vital){
			case 1: 
				Oxygen = value.ToString()+"%";
				break;
			case 2: 
				Health = value.ToString()+"%";
				break;

		}
	}

	public void setMinerals(int mineral, float value){
		switch(mineral){
		case 1: 
			Ore = value.ToString()+"kg";
			break;
		case 2: 
			Metal = value.ToString()+"kg";
			break;
			
		}
	}

	void DidLockCursor() {
		Debug.Log("Locking cursor");
	}
	void DidUnlockCursor() {
		Debug.Log("Unlocking cursor");
	}
	void OnMouseDown() {
		Screen.lockCursor = true;
	}

	void MoveTheObject(){
		RaycastHit hit;

		int layerMask = LayerMask.GetMask ("SpawnCollide");
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		if(Physics.Raycast(ray, out hit, 1000.0f, layerMask)) {
			if(Input.GetMouseButton(0)){
				currentObject.rigidbody.WakeUp();
				currentObject.layer = LayerMask.NameToLayer("Ground");
				currentObject = null;
				
			}else{
				Vector3 target = new Vector3(hit.point.x, hit.point.y, hit.point.z);
				currentObject.rigidbody.Sleep ();
				currentObject.transform.rotation.Set(0,0,0,0);
				
				Vector3 offset = new Vector3(0,currentObject.collider.bounds.size.y/2.0f,0);
				currentObject.transform.position = target + offset;
			}
		}
	}


}
