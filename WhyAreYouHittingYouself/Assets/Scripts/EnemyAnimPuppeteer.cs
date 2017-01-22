using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

[RequireComponent(typeof(AIBrain))]
[RequireComponent(typeof(Stats))]
public class EnemyAnimPuppeteer : AnimationPuppeteer {

	private AIBrain _brain;

	private Stats _stats;

	public Sprite idleSprite;
	public Sprite shootingSprite;
	
	public Vector3 heldMeshRotation;
	
	public Transform armPivotPoint;

	public float armRotSpeed = 1.0f;


	protected override void Awake()
	{
		_brain = GetComponent<AIBrain>();
		_stats = GetComponent<Stats>();
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
		HandleFlippingSprite();
		

		if (_stats.isDead) 
		{
			armSpriteRen.enabled = false;
		}
		else 
		{
			//we need to transition to the correct anim
			SetAnim_AimMode(_brain.isShooting);

			if (_brain.isShooting)
			{
				armSpriteRen.enabled = true;
			}
			else {
				armSpriteRen.enabled = false;
			}
		}

		//if (_brain.isShooting)
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

	private void SetAnim_AimMode(bool setting)
	{
		animComp.SetBool("Bool_IsShooting", setting);
	}

	private void HandleFlippingSprite()
	{
		Plane rightPlane = new Plane(Vector3.right, Vector3.zero);


			if ((rightPlane.GetSide(transform.forward) && animComp.transform.localScale.x < 0.0f)
			|| (!rightPlane.GetSide(transform.forward) && animComp.transform.localScale.x > 0.0f))
			{
				animComp.transform.localScale = new Vector3(-animComp.transform.localScale.x,
				animComp.transform.localScale.y,
				animComp.transform.localScale.z);
			}
			/*
			animComp.transform.localRotation = transform.localRotation * Quaternion.Euler(90.0f, 
													   		0.0f,
													   		0.0f);
															   */
		


		//if (Vector3.Dot(transform.forward, _brain.targetHeading.normalized) > 0.0f)
		//{
			/*
			animComp.transform.rotation = Quaternion.Euler(Vector3.Dot(transform.forward, _brain.targetHeading.normalized) > 0.0f ?  90.0f : -90.0f, 
													   		0.0f,
													   		0.0f);
															   */
		//}
	}
}
