using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

	// Use this for initialization
	public int brainPower;
	public int maxPower;
	public int minPower;
	public int healthState;
	public int maxHealth;
	public int minHealth;
	public int damagePoint;
	public int brainWaveRadius;
	public bool death;

	void Start () {

	}
	void Update () {
		//damagePoint = 
	}

	void OnTriggerEnter(Collider other){
		Damage otherDamageScript = other.gameObject.GetComponent<Damage>();
		if(otherDamageScript!=null){
			healthState-=otherDamageScript.damageAmount;
		}
	}

	void Health(){
		healthState=healthState-damagePoint;
		if (healthState>maxHealth){
			healthState=maxHealth;
		}else if(healthState<=minHealth){
			healthState=minHealth;
			death=true;
			Destroy(gameObject);
		}
	}
	void Power(){
		if (brainPower>maxPower){
			brainPower=maxPower;
		}else if(brainPower<minPower){
			brainPower=minPower;
		}
	}

}