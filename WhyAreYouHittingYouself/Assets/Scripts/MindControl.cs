using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TeamMember))]
public class MindControl : MonoBehaviour
 {
	//private TeamMember _teamMemberComp;

	public ActorController enslaveTarget;

	public List<ActorController> enslavedTargets;

	
	void Awake()
	{
		//_teamMemberComp = GetComponent<TeamMember>();
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
	public void EnslaveTargetMindControl(ActorController enslaverStats, params ActorController[] targetTeamMembers)
	{
		foreach (ActorController member in targetTeamMembers)
		{
			if (member == null) {continue;}
			if (enslavedTargets.Contains(member)) {continue;}
			//it's possible this enemy is too powerful, but there's another weaker one we can grab
			if (enslaverStats.statsComp.brainPower - member.statsComp.brainTax <= 0) {continue;} 

			//enslaverStats.brainPower -= member.statsComp.brainTax;
			//consume necessary power to enslave enemy
			enslaverStats.statsComp.ConsumeBrainPower(member.statsComp.brainTax);
			//add to player team
			TeamManager.instance.AddToTeam(enslaverStats.teamMemberComp.currentTeam,
											member);
			enslavedTargets.Add(member);

		}
	}

	public void ReleaseTargetMindControl(Stats enslaverStats, params ActorController[] targetTeamMembers)
	{
		foreach(ActorController actor in targetTeamMembers)
		{
			if (actor == null) {continue;}
			if (!enslavedTargets.Contains(actor)) {continue;}

			switch(actor.teamMemberComp.currentTeam)
			{
				case TeamMember.Team.Enemy:
					TeamManager.instance.AddToTeam(TeamMember.Team.Player, actor);
					break;
				case TeamMember.Team.Player:
					TeamManager.instance.AddToTeam(TeamMember.Team.Enemy, actor);
					//give back the brain juice that was taken
					enslaverStats.ReplenishBrainPower(actor.statsComp.brainTax);
					break;
			}
			enslavedTargets.Remove(actor);
		}
	}
}
