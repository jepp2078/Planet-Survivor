using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {
	public int remainingOutput = 1000;
	public bool activated = false;
	public Wire[] wiresArray = new Wire[10];
	public int connectedWires = 0;
	public DisplayScript display;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		display.set (remainingOutput.ToString ());
	}
	public int connectWire(Wire inWire){
		if(connectedWires < 10){
			wiresArray[connectedWires] = inWire;
			connectedWires++;
		}
		return connectedWires - 1;
	}

	public void connect(int power){
		if (remainingOutput >= power) {
			remainingOutput -= power;
		}
	}

	public bool canConnect(int power){
		if (remainingOutput >= power) {
			return true;
		}
		return false;
	}

	public void disconnect(int power, int wireNum){
		remainingOutput += power;
		wiresArray [wireNum] = null;
		connectedWires--;
	}

	public void power(){
		if(!activated){
			activated = true;
			for(int i = 0; i<10; i++){
				if(wiresArray[i] != null){
					Debug.Log(i);
					wiresArray[i].updatePower();
					Debug.Log("nr"+wiresArray[i].getWireNum()+" on");
					if(i == 1){
					Debug.Log("nr "+wiresArray[i-1].getWireNum()+" on");
					}
				}
			}
		}else{
			activated = false;
			for(int i = 0; i<10; i++){
				if(wiresArray[i] != null){
					Debug.Log(i);
					wiresArray[i].updatePower();
					Debug.Log("nr "+wiresArray[i].getWireNum()+" off");
					if(i == 1){
						Debug.Log("nr "+wiresArray[i-1].getWireNum()+" off");
					}
				}
			}
		}
	}
}

