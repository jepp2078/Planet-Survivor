using UnityEngine;
using System.Collections;

public class TestGun : MonoBehaviour {
	GameObject focus1;
	GameObject focus2;
	GameObject focus3;
	public GameObject prefab;
	private Ray rayClick;
	private RaycastHit hit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		rayClick = Camera.main.ScreenPointToRay (Input.mousePosition);
		if(Physics.Raycast(rayClick,out hit) && hit.collider.gameObject.name == "Lamp" && Input.GetKeyDown(KeyCode.E)){
			focus1 = hit.collider.gameObject;
			if(focus2 != null)
				make ();
		}
		//Instantiate(WirePrefab);
		if (Physics.Raycast (rayClick, out hit) && hit.collider.gameObject.name == "Generator" && Input.GetKeyDown (KeyCode.E)) {
			focus2 = hit.collider.gameObject;
			if(focus1 != null)
				make ();
		}
		if (Physics.Raycast (rayClick, out hit) && hit.collider.gameObject.name == "Generator" && Input.GetKeyDown (KeyCode.Q)) {
			focus3 = hit.collider.gameObject;
			Generator gen = focus3.GetComponent<Generator>();
			gen.power();
			focus3 = null;
		}

	}

	void make(){
		Instantiate (prefab);
		Wire wireClass = prefab.GetComponent<Wire> ();
		wireClass.setDest (focus1);
		wireClass.setSource (focus2);
		wireClass.connect ();
		focus1 = null;
		focus2 = null;
	}
}
