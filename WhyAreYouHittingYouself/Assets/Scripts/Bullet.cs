using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public int damageAmount;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
    void OnTriggerEnter(Collider other)
    {
        Stats otherStats = other.GetComponent<Stats>();
        if (otherStats != null)
        {
            otherStats.ApplyDamage(damageAmount);
        }
        
    }
}
