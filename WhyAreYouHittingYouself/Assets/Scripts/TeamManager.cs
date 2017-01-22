using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour {

	private static TeamManager _instance;
	public static TeamManager instance 
	{
		get
		{
			return _instance;
		}
	}

	public int totalCasualties = 0;

	public List<ActorController> playerTeam;
	public List<ActorController> enemyTeam;


	void Awake()
	{
		if (_instance != null) {Destroy(this);}
		_instance = this;

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public List<ActorController> GetTargetTeam(TeamMember.Team targetTeam)
	{
		switch(targetTeam) 
		{
			case TeamMember.Team.Player:
				return playerTeam;
			case TeamMember.Team.Enemy:
				return enemyTeam;

			default:
				Debug.LogErrorFormat("Supplied unaccounted for team : {0}", targetTeam.ToString());
				return enemyTeam;
		}
	}

	public void AddToTeam(TeamMember.Team newTeam, params ActorController[] membersToAdd)
	{
		//Debug.LogFormat("Adding {0} members to team", membersToAdd.Length);

		foreach (ActorController actor in membersToAdd) 
		{
			//Debug.Log("performing action on member");

			//if (member.currentTeam != newTeam) //only do work if it's a differnt team
			//{
				switch (newTeam)
				{
					case TeamMember.Team.Player:	//add to player team
						//Debug.Log("Switch to Player Team");
						if (playerTeam.Contains(actor)) {return;}
						enemyTeam.Remove(actor);
						playerTeam.Add(actor);
						actor.teamMemberComp.ChangeTeam(TeamMember.Team.Player);
						break;
					case TeamMember.Team.Enemy:		//add to enemy team
						//Debug.Log("Switch to Enemy Team");
						if (enemyTeam.Contains(actor)) {return;}
						playerTeam.Remove(actor);
						enemyTeam.Add(actor);
						actor.teamMemberComp.ChangeTeam(TeamMember.Team.Enemy);
						break;
				}

				actor.teamMemberComp.ChangeTeam(newTeam);		//signal the change on the team member
				
				if ((actor as EnemyController) != null)
				{
					(actor as EnemyController).brain.targetTransform = null;
				}
				
			//}
		}
	}

	public void RemoveFromAllTeams(params ActorController[] actorsToRemove)
	{
		foreach(ActorController actor in actorsToRemove)
		{
			playerTeam.Remove(actor);
			enemyTeam.Remove(actor);
		}
	}
}
