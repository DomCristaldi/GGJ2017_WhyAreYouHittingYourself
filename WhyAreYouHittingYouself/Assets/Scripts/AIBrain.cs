using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(TeamMember))]
public class AIBrain : MonoBehaviour {

	private NavMeshAgent _navAgentComp;
	private NavMeshAgent navAgentComp{get{return _navAgentComp;}}
	private TeamMember _teamMemberComp;
	public TeamMember teamMemberComp{get {return _teamMemberComp;}}

	public Transform targetTransform;

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
		targetTransform = FindClosestEnemy().transform;
	}

}
