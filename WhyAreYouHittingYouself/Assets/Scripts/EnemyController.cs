using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIBrain))]
public class EnemyController : ActorController {

	private AIBrain _brain;
	public AIBrain brain {get{return _brain;}}

	protected override void Awake()
	{
		base.Awake();

		_brain = GetComponent<AIBrain>();
	}

	// Use this for initialization
	protected override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
	}

	void OnMouseEnter()
	{
		SetAspossessionTarget();
	}

	void OnMouseExit()
	{
		ReleaseAsPossessionTarget();
	}


	private void SetAspossessionTarget()
	{
		GameManager.instance.playerRef.mindControlComp.enslaveTarget = this;
	}
	private void ReleaseAsPossessionTarget()
	{
		if (GameManager.instance.playerRef.mindControlComp.enslaveTarget == teamMemberComp)
		{
			GameManager.instance.playerRef.mindControlComp.enslaveTarget = null;
		}
	}

	public void AttackClosestEnemy()
	{
		brain.TargetClosestEnemy();
	}
}
