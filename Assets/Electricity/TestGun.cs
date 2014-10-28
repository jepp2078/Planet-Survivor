using UnityEngine;
using System.Collections;

public class TestGun : MonoBehaviour {
	GameObject focus1;
	GameObject focus2;
	GameObject focus3;
	public GameObject prefab;
	private RaycastHit hit;

	GameObject GunPoint;
	GameObject CameraView;

	GameObject VitalObj;
	public Vitals vital;


	// Use this for initialization
	void Start () {
		GunPoint = GameObject.Find("GunPoint");
		CameraView = GameObject.Find("Main Camera");
		VitalObj = GameObject.Find("First Person Controller");
		vital = (Vitals) VitalObj.GetComponent (typeof(Vitals));
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = new Ray (CameraView.transform.position, CameraView.transform.forward);

		if(Input.GetButtonUp("Fire1")){
			if(Physics.Raycast(ray, out hit)){
				Debug.Log (hit.collider.gameObject.name);
			//Instantiate(WirePrefab);
				if (hit.collider.gameObject.name == "Generator(Clone)") {
					focus2 = hit.collider.gameObject;
					if(focus1 != null)
						make ();
				}

				if(hit.collider.gameObject.name == "Lamp(Clone)"){
					focus1 = hit.collider.gameObject;
					if(focus2 != null)
						make ();
				}
				if (hit.collider.gameObject.name == "OxyGen(Clone)") {
					focus1 = hit.collider.gameObject;
					if(focus2 != null)
						make ();
				}
				if (hit.collider.gameObject.name == "OreRefinery(Clone)") {
					focus1 = hit.collider.gameObject;
					if(focus2 != null)
						make ();
				}
			}
		}
		if(Input.GetKeyDown (KeyCode.E)){
			if (Physics.Raycast (ray, out hit) && hit.collider.gameObject.name == "Generator(Clone)") {
				focus3 = hit.collider.gameObject;
				Generator gen = focus3.GetComponent<Generator>();
				gen.power();
				focus3 = null;
			}
			else if (Physics.Raycast (ray, out hit) && hit.collider.gameObject.name == "OreRefinery(Clone)") {
				focus3 = hit.collider.gameObject;
				OreRefinery refinery = focus3.GetComponent<OreRefinery>();
				if(refinery.run() && vital.getMinerals(1) >= 10){
					vital.setMinerals(1,-10);
					vital.setMinerals(2,2.5f);
				}
				focus3 = null;
			}
		}

	}

	void make(){
		GameObject tempWire = (GameObject)Instantiate(prefab);
		Wire wireClass = tempWire.GetComponent<Wire> ();
		wireClass.setDest (focus1);
		wireClass.setSource (focus2);
		wireClass.connect ();
		focus1 = null;
		focus2 = null;
	}
}
