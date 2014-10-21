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

	// Use this for initialization
	void Start () {
		GunPoint = GameObject.Find("GunPoint");
		CameraView = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = new Ray (CameraView.transform.position, CameraView.transform.forward);

		if(Input.GetButtonUp("Fire1")){
			if(Physics.Raycast(ray, out hit)){
			   if(hit.collider.gameObject.name == "Lamp"){
					focus1 = hit.collider.gameObject;
					if(focus2 != null)
						make ();
				}
			
			//Instantiate(WirePrefab);
				if (hit.collider.gameObject.name == "Generator") {
					focus2 = hit.collider.gameObject;
					if(focus1 != null)
						make ();
				}

				if (hit.collider.gameObject.name == "OxyGen") {
					focus1 = hit.collider.gameObject;
					if(focus2 != null)
						make ();
				}
			}
		}
		if(Input.GetKeyDown (KeyCode.E)){
			if (Physics.Raycast (ray, out hit) && hit.collider.gameObject.name == "Generator") {
				focus3 = hit.collider.gameObject;
				Generator gen = focus3.GetComponent<Generator>();
				gen.power();
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
