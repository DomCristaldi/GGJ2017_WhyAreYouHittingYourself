using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
[RequireComponent(typeof(TeamMember))]
public class ActorController : MonoBehaviour {

	private Stats _statsComp;
	protected Stats statsComp
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
		
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}
}
