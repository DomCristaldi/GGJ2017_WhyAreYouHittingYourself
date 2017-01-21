using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamMember : MonoBehaviour {

	public enum Team{
		Player = 0,
		Enemy = 1,
	}

	public Team currentTeam;

	// Use this for initialization
	void Start () {
		TeamManager.instance.AddToTeam(currentTeam, this);
	}

	void OnDestroy() {
		TeamManager.instance.RemoveFromAllTeams(this);
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
