using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TeamMember))]
public class MindControl : MonoBehaviour {

	public TeamMember enslaveTarget;

	private TeamMember _teamMemberComp;

	public List<TeamMember> enslavedTargets;

	
	void Awake()
	{
		_teamMemberComp = GetComponent<TeamMember>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

//COME HERE, FIGURE OUT WHY THEY'RE NOT SWITCHING TEAMS

	public void EnslaveTargetMindControl(params TeamMember[] targetTeamMembers)
	{
		foreach (TeamMember member in targetTeamMembers)
		{
			if (member == null) {continue;}
			if (enslavedTargets.Contains(member)) {continue;}

			Debug.Log("Perform Add to Team");

			//add to player team
			TeamManager._instance.AddToTeam(_teamMemberComp.currentTeam,
											member);
			enslavedTargets.Add(member);
		}
	}

	public void ReleaseTargetMindControl(params TeamMember[] targetTeamMembers)
	{
		foreach(TeamMember member in targetTeamMembers)
		{
			if (member == null) {continue;}
			if (!enslavedTargets.Contains(member)) {continue;}

			switch(member.currentTeam)
			{
				case TeamMember.Team.Enemy:
					TeamManager._instance.AddToTeam(TeamMember.Team.Player, member);
					break;
				case TeamMember.Team.Player:
					TeamManager._instance.AddToTeam(TeamMember.Team.Enemy, member);
					break;
			}
		}
	}
}
