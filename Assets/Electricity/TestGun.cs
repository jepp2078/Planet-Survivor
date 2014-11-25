using UnityEngine;
using System.Collections;

public class TestGun : MonoBehaviour {
	GameObject focus1;
	GameObject focus2;
	GameObject focus3;
	Vector3 pos1,pos2;
	public GameObject prefab;
	private RaycastHit hit;

	GameObject GunPoint;
	GameObject CameraView;
	private LineRenderer linerender;

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
				//if (hit.collider.gameObject.name == "Generator(Clone)") {
				if (hit.collider.gameObject.tag == "Generator") {
					focus2 = hit.collider.gameObject;
					pos2 = focus2.gameObject.transform.position;
					if(focus1 != null)
						make ();
				}

				//if(hit.collider.gameObject.name == "Lamp(Clone)"){
				if (hit.collider.gameObject.tag == "Lamp") {
					focus1 = hit.collider.gameObject;
					pos1 = focus1.gameObject.transform.position;
					if(focus2 != null)
						make ();
				}
				//if (hit.collider.gameObject.name == "OxyGen(Clone)") {
				if (hit.collider.gameObject.tag == "OxyGen") {
					focus1 = hit.collider.gameObject;
					pos1 = focus1.gameObject.transform.position;
					if(focus2 != null)
						make ();
				}
				//if (hit.collider.gameObject.name == "OreRefinery(Clone)") {
				if (hit.collider.gameObject.tag == "OreRefinery") {
					focus1 = hit.collider.gameObject;
					pos1 = focus1.gameObject.transform.position;
					if(focus2 != null)
						make ();
				}
				if (hit.collider.gameObject.name == "Door(Clone)") {
					focus1 = hit.collider.gameObject;
					pos1 = focus1.gameObject.transform.position;
					if(focus2 != null)
						make ();
				}
			}
		}
		if(Input.GetKeyDown (KeyCode.E)){
			//if (Physics.Raycast (ray, out hit) && hit.collider.gameObject.name == "Generator(Clone)") {
			if (Physics.Raycast (ray, out hit) && hit.collider.gameObject.tag == "Generator") {

				focus3 = hit.collider.gameObject;
				Generator gen = focus3.GetComponent<Generator>();
				gen.power();
				focus3 = null;
			}
			//else if (Physics.Raycast (ray, out hit) && hit.collider.gameObject.name == "OreRefinery(Clone)") {
			else if (Physics.Raycast (ray, out hit) && hit.collider.gameObject.tag == "OreRefinery") {

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
		linerender = tempWire.GetComponent<LineRenderer> ();
		linerender.SetPosition(0,pos1);
		linerender.SetPosition(1,pos2);
		focus1 = null;
		focus2 = null;
	}
}
