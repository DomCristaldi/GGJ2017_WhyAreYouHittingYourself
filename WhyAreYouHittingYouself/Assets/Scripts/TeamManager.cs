using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour {

	public static TeamManager _instance;
	public static TeamManager instance 
	{
		get
		{
			return _instance;
		}
	}


	public List<TeamMember> playerTeam;
	public List<TeamMember> enemyTeam;


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

	public void AddToTeam(TeamMember.Team newTeam, params TeamMember[] membersToAdd)
	{
		foreach (TeamMember member in membersToAdd) 
		{
			//if (member.currentTeam != newTeam) //only do work if it's a differnt team
			//{
				switch (newTeam)
				{
					case TeamMember.Team.Player:	//add to player team
						if (playerTeam.Contains(member)) {return;}
						enemyTeam.Remove(member);
						playerTeam.Add(member);
						break;
					case TeamMember.Team.Enemy:		//add to enemy team
						if (enemyTeam.Contains(member)) {return;}
						playerTeam.Remove(member);
						enemyTeam.Add(member);
						break;
				}

				member.ChangeTeam(newTeam);		//signal the change on the team member
			//}
		}
	}

	public void RemoveFromAllTeams(params TeamMember[] membersToRemove)
	{
		foreach(TeamMember member in membersToRemove)
		{
			playerTeam.Remove(member);
			enemyTeam.Remove(member);
		}
	}
}
