using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(movementasdf))]
public class InputController : MonoBehaviour {

    movementasdf movComp;

    public float directionHorizontal;
    public float directionVertical;

    void Awake()
    {
        movComp = GetComponent<movementasdf>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        directionHorizontal = Input.GetAxisRaw("Horizontal");
        directionVertical = Input.GetAxisRaw("Vertical");

        movComp.desireMoveDirection = new Vector3(directionHorizontal,
                                                  directionVertical,
                                                  0.0f);
    }
}
