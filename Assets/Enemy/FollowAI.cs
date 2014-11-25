using UnityEngine;
using System.Collections;

public class FollowAI : MonoBehaviour {

	Transform target; //the enemy's target
	float moveSpeed = 10; //move speed
	float rotationSpeed = 5; //speed of turning
	float range =1000000f;
	float range2 =100000f;
	float stop =0;
	Transform myTransform; //current transform data of this enemy

	void Awake()
	{
		myTransform = transform; //cache transform data for easy access/preformance
	}
	
	void Start()
	{
		target = GameObject.FindWithTag("Player").transform; //target the player
		
	}
	
	void Update() {
		// Make enemy float
		myTransform.rigidbody.AddForce (Vector3.up * 60, ForceMode.Force);

		float distance = Vector3.Distance(myTransform.position, target.position);
		if (distance<=range2 &&  distance>=range){
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
			                                        Quaternion.LookRotation(target.position - myTransform.position), 30*rotationSpeed*Time.deltaTime);
		}
		
		
		else if(distance<=range && distance>stop){
			//move towards the player
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
                                   Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed*Time.deltaTime);
			myTransform.rigidbody.AddForce(myTransform.forward*30*moveSpeed +(Vector3.up*Mathf.Cos(Time.fixedTime)*200), ForceMode.Force);
			
		}
		else if (distance<=stop) {
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
			                                        Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed*Time.deltaTime);
		}
		
		
	}
}
