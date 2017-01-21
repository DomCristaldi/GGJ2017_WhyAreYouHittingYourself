using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementasdf : MonoBehaviour {
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
            transform.position.y+(speed*Time.deltaTime*trueMoveDirection.y),
            0.0f
        );

        trueMoveDirection = desireMoveDirection;
	}
}
