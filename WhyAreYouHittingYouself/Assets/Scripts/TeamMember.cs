using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
public class TeamMember : MonoBehaviour {

	public enum Team{
		Player = 0,
		Enemy = 1,
	}

	public static Team GetOpposingTeam(Team friendlyTeam)
	{
		switch(friendlyTeam)
		{
			case Team.Enemy:
				return Team.Player;
			case Team.Player:
				return Team.Enemy;

			default:
				Debug.LogErrorFormat("Supplied unaccounted for team: {0}", friendlyTeam.ToString());
				return friendlyTeam;
		}
	}

	private Stats _statsComp;
	public Stats statsComp
	{
		get {return _statsComp;}
	}


	public Team currentTeam;

	private Team _originalTeam; //helps with keeping track of enslavement status
	public Team originalTeam
	{
		get {return _originalTeam;}
	}

	public bool isCurrentlyEnslaved
	{
		get {return currentTeam != _originalTeam;}
	}

	void Awake()
	{
		_statsComp = GetComponent<Stats>();
	}

	// Use this for initialization
	void Start () {
		_originalTeam = currentTeam;
		TeamManager._instance.AddToTeam(currentTeam, this);
	}

	void OnDestroy() {
		TeamManager._instance.RemoveFromAllTeams(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void ChangeTeam(Team targetTeam)
	{
		if (currentTeam == targetTeam) {return;}

		currentTeam = targetTeam;
	}
}
