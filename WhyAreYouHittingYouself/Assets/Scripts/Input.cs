using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imput : MonoBehaviour {
    public float directionHorizontal;
    public float directionVertical;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        directionHorizontal = Input.GetAxis("Horizontal");
        directionVertical = Input.GetAxis("Vertical");
    }
}
