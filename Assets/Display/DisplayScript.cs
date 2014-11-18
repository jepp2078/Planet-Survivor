using UnityEngine;
using System.Collections;

public class DisplayScript : MonoBehaviour {

	public GameObject Display;
	public TextMesh text;
	public string textOnDisplay;
	// Use this for initialization
	void Start () {
		text.text = "Text Here";
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void set(string textString){
		text.text = textString;
	}
}