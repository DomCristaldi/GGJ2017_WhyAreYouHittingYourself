using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MindControl))]
public class PlayerController : ActorController {
	
	
	private MindControl _mindControlComp;
	public MindControl mindControlComp
	{
		get
		{
			return _mindControlComp;
		}
	}

    public KeyCode PossessTargetKey = KeyCode.Mouse0;
    public KeyCode ReleasePossessionKey = KeyCode.Mouse1;

	public UnityEvent FailedtoMindControlEvent;

	protected override void Awake()
	{	
		base.Awake();

		_mindControlComp = GetComponent<MindControl>();
	}

	// Use this for initialization
	protected override void Start () {
		base.Start();

		if (GameManager.instance != null)
		{
			GameManager.instance.playerRef = this;
		}
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();

		HandleInput();
	}


	private void HandleInput()
	{
		if (mindControlComp.enslaveTarget != null)
		{
			//ATTEMPT TARGET POSSESSION
			if (Input.GetKeyDown(PossessTargetKey)
			 && mindControlComp.enslaveTarget.teamMemberComp.currentTeam != teamMemberComp.currentTeam)
			{
				//SUCCESS
				if (statsComp.brainPower - mindControlComp.enslaveTarget.statsComp.brainTax > 0)
				{
					mindControlComp.EnslaveTargetMindControl(this,
															 mindControlComp.enslaveTarget);
				}
				//FAIL
				else
				{
					FailedtoMindControlEvent.Invoke();
					Debug.Log("Failed Mind Control");
				}
			}
			//RELEASE TARGET FROM POSSESSION
			if (Input.GetKeyDown(ReleasePossessionKey)									//key was hit
			 && mindControlComp.enslaveTarget.teamMemberComp.currentTeam == teamMemberComp.currentTeam //it's on our team
			 && mindControlComp.enslaveTarget.teamMemberComp.isCurrentlyEnslaved)						//and we're currently enslaving it
			{
				mindControlComp.ReleaseTargetMindControl(this.statsComp,
														 mindControlComp.enslaveTarget);//then we can release it
			}
		}
	}
}
