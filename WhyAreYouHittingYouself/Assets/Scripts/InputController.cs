using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class InputController : MonoBehaviour {

    Movement movComp;

    public float directionHorizontal;
    public float directionVertical;

    void Awake()
    {
        movComp = GetComponent<Movement>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        directionHorizontal = Input.GetAxisRaw("Horizontal");
        directionVertical = Input.GetAxisRaw("Vertical");

        movComp.desireMoveDirection = new Vector3(directionHorizontal,
                                                  0.0f,
                                                  directionVertical);
    }
}
