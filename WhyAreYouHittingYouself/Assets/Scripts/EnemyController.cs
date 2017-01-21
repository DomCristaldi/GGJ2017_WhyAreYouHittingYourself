using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : ActorController {



	protected override void Awake()
	{
		base.Awake();
	}

	// Use this for initialization
	protected override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
	}

	void OnMouseEnter()
	{
		SetAspossessionTarget();
	}

	void OnMouseExit()
	{
		ReleaseAsPossessionTarget();
	}


	private void SetAspossessionTarget()
	{
		GameManager.instance.playerRef.mindControlComp.enslaveTarget = this;
	}
	private void ReleaseAsPossessionTarget()
	{
		if (GameManager.instance.playerRef.mindControlComp.enslaveTarget == teamMemberComp)
		{
			GameManager.instance.playerRef.mindControlComp.enslaveTarget = null;
		}
	}
}
