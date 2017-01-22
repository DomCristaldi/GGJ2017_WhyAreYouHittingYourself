using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour {

    private Rigidbody rigBod;

    public float speed;
    public Vector3 trueMoveDirection;
    public Vector3 desireMoveDirection;

    public float acceleration = 2.0f;
    public AnimationCurve redirectionSpeedRamp;

    public float currentHorrizontalSpeed
    {
        get {
            return speed*trueMoveDirection.x;
        }
    }

    public float currentVerticalSpeed
    {
        get{
            return speed*trueMoveDirection.z;
        }
    }

    void Awake()
    {
        rigBod = GetComponent<Rigidbody>();
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //trueMoveDirection = trueMoveDirection.normalized;
        desireMoveDirection = desireMoveDirection.normalized;

        rigBod.velocity = new Vector3(
            currentHorrizontalSpeed,
            0.0f,
            currentVerticalSpeed
        );

        /*
        transform.position = new Vector3
        (
            transform.position.x+(currentHorrizontalSpeed),
            0.0f,
            transform.position.z+(currentVerticalSpeed)
        );
        */

        trueMoveDirection = desireMoveDirection;
/*
        trueMoveDirection = Vector3.MoveTowards(trueMoveDirection,
                                                desireMoveDirection,
                                                redirectionSpeedRamp.Evaluate(trueMoveDirection.magnitude) * Time.deltaTime);
*/
	}
}

/*
#if UNITY_EDITOR
[CustomEditor(typeof(Movement))]
public class Movement_Editor : Editor
{
    Movement selfScript;
    void OnEnable()
    {
        selfScript = (Movement)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        EditorGUILayout.Vector3Field("Speed",
                                     new Vector3(selfScript.currentHorrizontalSpeed,
                                                 0.0f,
                                                 selfScript.currentVerticalSpeed));
                                                 
    }
}
#endif
*/