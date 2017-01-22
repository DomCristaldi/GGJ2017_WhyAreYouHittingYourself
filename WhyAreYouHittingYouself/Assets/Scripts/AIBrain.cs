using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(TeamMember))]
public class AIBrain : MonoBehaviour {

	public BulletSpawner bulletSpawnerRef;

	private NavMeshAgent _navAgentComp;
	private NavMeshAgent navAgentComp{get{return _navAgentComp;}}
	private TeamMember _teamMemberComp;
	public TeamMember teamMemberComp{get {return _teamMemberComp;}}

	[Space]
	[Header("Follow Settings")]
	public Transform targetTransform;

	[Space]
	[Header("Shoot Settings")]
	public float shootRange = 5.0f;
	public float shootAngle = 0.1f;


	void Awake()
	{
		_navAgentComp = GetComponent<NavMeshAgent>();
		_teamMemberComp = GetComponent<TeamMember>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (targetTransform != null)
		{
			_navAgentComp.destination = targetTransform.position;
			_teamMemberComp = GetComponent<TeamMember>();
		}
		if (targetTransform == null)
		{
			TargetClosestEnemy();
		}

		//Debug.LogFormat("Can Shoot: {0}", CheckCanAttack());
		if (CheckCanAttack())
		{
			bulletSpawnerRef.FireBullet(transform.forward);
		}
	}

	public bool CheckCanAttack()
	{
		//sanity check
		if (targetTransform == null) {return false;}

		Vector3 planarHeading = Vector3.Normalize(
									Vector3.ProjectOnPlane(targetTransform.position - transform.position,
														   Vector3.up)
												 );
		Debug.DrawRay(transform.position, planarHeading * shootRange * 2.0f, Color.blue);
		Debug.DrawRay(transform.position, transform.forward * shootRange * 2.0f, Color.green);

		//checking with squares is faster
		if (Vector3.SqrMagnitude(planarHeading) <= Mathf.Pow(shootRange, 2.0f)
		 && (1.0f - Vector3.Dot(planarHeading, transform.forward)) <= shootAngle)
		{
			return true;
		}

		//failed all checks, return false
		return false;
	}

	public ActorController FindClosestMemberOfTeam(TeamMember.Team targetTeam)
	{
		//_navAgentComp.CalculatePath
		List<ActorController> teamToSearch = TeamManager.instance.GetTargetTeam(targetTeam);

		//shortcut b/c there's no other possible solutions in these cases
		if (teamToSearch.Count == 0) {return null;}
		if (teamToSearch.Count == 1) {return teamToSearch[0];}

		//we have at least 2, find the closest one
		float shortestDist = Mathf.Infinity;
		int indexOfClosestTarget = 0;
		//NavMeshPath calcPath = new NavMeshPath();
		//foreach (ActorController actor in teamToSearch)
		for (int i = 0; i < teamToSearch.Count; ++i)
		{
			if (teamToSearch[i] == GetComponent<ActorController>()) {continue;}

			float dist = Vector3.SqrMagnitude(teamToSearch[i].transform.position - transform.position);
			if (dist < shortestDist)
			{
				indexOfClosestTarget = i;
				shortestDist = dist;
			}
			
			/*
			navAgentComp.CalculatePath(actor.transform.position,
									   calcPath);
			
			*/
		}

		return teamToSearch[indexOfClosestTarget];
	}

	public ActorController FindClosestFriendly()
	{
		return FindClosestMemberOfTeam(teamMemberComp.currentTeam);
	}
	public ActorController FindClosestEnemy()
	{
		return FindClosestMemberOfTeam(TeamMember.GetOpposingTeam(_teamMemberComp.currentTeam));
	}

	public void TargetClosestEnemy() {
		ActorController closestEnemy = FindClosestEnemy();
		if (closestEnemy != null)
		{
			targetTransform = closestEnemy.transform;
		}
		else
		{
			targetTransform = null;
		}
		//targetTransform = FindClosestEnemy().transform;
	}

}
