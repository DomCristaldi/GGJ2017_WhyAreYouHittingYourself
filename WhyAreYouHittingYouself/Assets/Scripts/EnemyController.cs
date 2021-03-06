﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Sprites;
using UnityEngine.AI;

[RequireComponent(typeof(AIBrain))]
[RequireComponent(typeof(EnemyAnimPuppeteer))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : ActorController {

	private AIBrain _brain;
	public AIBrain brain {get{return _brain;}}
	
	private EnemyAnimPuppeteer _enemyAnimPup;

	private NavMeshAgent _navAgentComp;

	protected override void Awake()
	{
		base.Awake();

		_brain = GetComponent<AIBrain>();
		_enemyAnimPup = GetComponent<EnemyAnimPuppeteer>();
		_navAgentComp = GetComponent<NavMeshAgent>();
	}

	// Use this for initialization
	protected override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();

		//make sure we're on the same plane as the player b/c bullets
		if (GameManager.canGetPlayer
			&& !Mathf.Approximately(transform.position.y, GameManager.instance.playerRef.transform.position.y))
		{
			transform.position = new Vector3(transform.position.x,
											 GameManager.instance.playerRef.transform.position.y,
											 transform.position.z);
		}
	}

	void OnMouseEnter()
	{
		SetAspossessionTarget();
	}

	void OnMouseExit()
	{
		ReleaseAsPossessionTarget();
	}

	protected override void HandleDeath()
	{
		base.HandleDeath();

		if (TeamManager.instance != null)
		{
			++TeamManager.instance.totalCasualties;
			TeamManager.instance.enemyTeam.Remove(this);
		}

		_navAgentComp.Stop();

		_enemyAnimPup.SetDeathState(true);
		_enemyAnimPup.armSpriteRen.enabled = false;

		foreach (Collider col in gameObject.GetComponentsInParent<Collider>())
		{
			col.enabled = false;
		}

		//Destroy(gameObject);
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
