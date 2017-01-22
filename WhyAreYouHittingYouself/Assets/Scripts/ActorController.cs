using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
[RequireComponent(typeof(TeamMember))]
public class ActorController : MonoBehaviour {

	private Stats _statsComp;
	public Stats statsComp
	{
		get {return _statsComp;}
	}

	protected TeamMember _teamMemberComp;
	public TeamMember teamMemberComp
	{
		get
		{
			return _teamMemberComp;
		}
	}


	protected virtual void Awake()
	{
		_statsComp = GetComponent<Stats>();
		_teamMemberComp = GetComponent<TeamMember>();
	}

	// Use this for initialization
	protected virtual void Start () {
		TeamManager.instance.AddToTeam(teamMemberComp.currentTeam, this);
	}
	
	protected virtual void Destroy()
	{
		TeamManager.instance.RemoveFromAllTeams(this);
	}

	// Update is called once per frame
	protected virtual void Update () {
		if (statsComp.isDead) {HandleDeath();}
	}

	protected virtual void HandleDeath()
	{

	}
}
