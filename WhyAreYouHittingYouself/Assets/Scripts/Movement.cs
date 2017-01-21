using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public float speed;
    public Vector3 trueMoveDirection;
    public Vector3 desireMoveDirection;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        trueMoveDirection = trueMoveDirection.normalized;
        desireMoveDirection = desireMoveDirection.normalized;

        transform.position = new Vector3
        (
            transform.position.x+(speed*Time.deltaTime*trueMoveDirection.x),
            0.0f,
            transform.position.z+(speed*Time.deltaTime*trueMoveDirection.z)
        );

        trueMoveDirection = desireMoveDirection;
	}
}
