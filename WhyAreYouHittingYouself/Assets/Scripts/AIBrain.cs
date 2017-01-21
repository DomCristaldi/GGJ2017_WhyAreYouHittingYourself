using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(TeamMember))]
public class AIBrain : MonoBehaviour {

	private NavMeshAgent _navAgentComp;
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
	}

	public ActorController FindClosestMemberOfTeam(TeamMember.Team targetTeam)
	{
		//_navAgentComp.CalculatePath
		List<ActorController> teamToSearch = TeamManager.instance.GetTargetTeam(targetTeam);

		int indexOfClosestTarget = 0;
		NavMeshPath calcPath;
		foreach (ActorController actor in teamToSearch)
		{
			//tMember.
		}
		Debug.LogError("Incomplete, finish me");
		return null;
	}

	public ActorController FindClosestFriendly()
	{
		return FindClosestMemberOfTeam(teamMemberComp.currentTeam);
	}
	public ActorController FindClosestEnemy()
	{
		return FindClosestMemberOfTeam(TeamMember.GetOpposingTeam(_teamMemberComp.currentTeam));
	}

}
