using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TeamMember))]
public class MindControl : MonoBehaviour {

	private TeamMember myTeam;

	public List<TeamMember> enslavedTargets;

	
	void Awake()
	{
		myTeam = GetComponent<TeamMember>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void EnslaveTargetMindControl(params TeamMember[] targetTeamMembers)
	{
		foreach (TeamMember member in targetTeamMembers)
		{
			if (member == null) {continue;}
			if (enslavedTargets.Contains(member)) {continue;}

			TeamManager.instance.AddToTeam(myTeam.currentTeam);
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
					TeamManager.instance.AddToTeam(TeamMember.Team.Player, member);
					break;
				case TeamMember.Team.Player:
					TeamManager.instance.AddToTeam(TeamMember.Team.Enemy, member);
					break;
			}
		}
	}
}
