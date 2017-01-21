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

	/// <summary>
    /// 
    /// </summary>
    /// <param name="availableBrainPower">Amount of Brain Power the Player Currently has</param>
    /// <param name="targetTeamMembers"></param>
    /// <returns>Remaining Brain Power after Mind Control performed</returns>
	public void EnslaveTargetMindControl(Stats enslaverStats, params TeamMember[] targetTeamMembers)
	{
		foreach (TeamMember member in targetTeamMembers)
		{
			if (member == null) {continue;}
			if (enslavedTargets.Contains(member)) {continue;}
			//it's possible this enemy is too powerful, but there's another weaker one we can grab
			if (enslaverStats.brainPower - member.statsComp.brainTax <= 0) {continue;} 

			//enslaverStats.brainPower -= member.statsComp.brainTax;
			//consume necessary power to enslave enemy
			enslaverStats.ConsumeBrainPower(member.statsComp.brainTax);
			//add to player team
			TeamManager._instance.AddToTeam(_teamMemberComp.currentTeam,
											member);
			enslavedTargets.Add(member);

		}
	}

	public void ReleaseTargetMindControl(Stats enslaverStats, params TeamMember[] targetTeamMembers)
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
					//give back the brain juice that was taken
					enslaverStats.ReplenishBrainPower(member.statsComp.brainTax);
					break;
			}
			enslavedTargets.Remove(member);
		}
	}
}
