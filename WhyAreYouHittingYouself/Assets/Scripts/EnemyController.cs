using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TeamMember))]
public class EnemyController : MonoBehaviour {

	private TeamMember _teamComp;
	public TeamMember teamComp
	{
		get {return _teamComp;}
	}

	void Awake()
	{
		_teamComp = GetComponent<TeamMember>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseEnter()
	{
		HandlePossession();
	}

	public void HandlePossession()
	{
		GameManager.instance.playerRef.mindControlComp.enslaveTarget = teamComp;
	}
}
