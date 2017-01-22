using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIBrain))]
public class EnemyAnimPuppeteer : AnimationPuppeteer {

	private AIBrain _brain;

	public Vector3 heldMeshRotation;
	
	public Transform armPivotPoint;

	public float armRotSpeed = 1.0f;


	protected override void Awake()
	{
		_brain = GetComponent<AIBrain>();
	}

	protected override void Start ()
	{
		base.Start();


	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();

		PreventMeshRotation();
		SetArmRotation();
	}

	private void PreventMeshRotation()
	{
		animComp.transform.rotation = Quaternion.Euler(heldMeshRotation);
	}

	private void SetArmRotation()
	{
		//Debug.LogFormat("Brain Heading {0}", _brain.targetHeading);


		Vector3 newArmForward = Vector3.RotateTowards((armPivotPoint.rotation * Vector3.forward),
													  _brain.targetHeading,
													  armRotSpeed * Time.deltaTime,
													  1.0f);
		
		newArmForward = newArmForward.normalized;

		armPivotPoint.rotation = Quaternion.LookRotation(newArmForward, Vector3.up);
	}
}
