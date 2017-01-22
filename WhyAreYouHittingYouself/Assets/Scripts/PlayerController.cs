using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MindControl))]
[RequireComponent(typeof(AnimationPuppeteer))]
[RequireComponent(typeof(InputController))]
[RequireComponent(typeof(Movement))]
public class PlayerController : ActorController {
	
	
	private MindControl _mindControlComp;
	public MindControl mindControlComp
	{
		get
		{
			return _mindControlComp;
		}
	}
	
	private AnimationPuppeteer _animPup;
	public AnimationPuppeteer animPup{get{return _animPup;}}

	private InputController _inputControl;

	private Movement _mov;

	public float normalTimeSpeed = 1.0f;
	public float slowTimeSpeed = 0.2f;

	public float slomoEnergy = 3.0f;
	public float maxSlomoEnergy = 3.0f;
	private bool _isInSlomo = false;
	public bool isInSlomo{get{return _isInSlomo;}}


	[Header("Input")]
    public KeyCode PossessTargetKey = KeyCode.Mouse0;
    public KeyCode ReleasePossessionKey = KeyCode.Mouse1;

	public KeyCode SlowDownTime = KeyCode.Space;

	[Space]
	public UnityEvent FailedtoMindControlEvent;

	protected override void Awake()
	{	
		base.Awake();

		_mindControlComp = GetComponent<MindControl>();
		_animPup = GetComponent<AnimationPuppeteer>();
		_inputControl = GetComponent<InputController>();
		_mov = GetComponent<Movement>();
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
		
		HandleSlomo();


	}

	protected override void HandleDeath()
	{
		base.HandleDeath();

		_mov.desireMoveDirection = Vector3.zero;
		animPup.SetDeathState(true);
	}


	private void HandleInput()
	{

		if (statsComp.currentHealth <= 0) {return;}

		_inputControl.UpdateInput();

	//POSSESSION
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

		//SLOW DOWN TIME
			if (Input.GetKeyDown(SlowDownTime))
			{
				EnterSlomo();
			}
			if (Input.GetKeyUp(SlowDownTime))
			{
				ExitSlomo();
			}
	}

	private void HandleSlomo()
	{
		slomoEnergy = Mathf.Clamp(isInSlomo ? slomoEnergy - Time.unscaledDeltaTime
											: slomoEnergy + Time.unscaledDeltaTime,
								  0.0f,
								  maxSlomoEnergy);

		if (slomoEnergy <= 0.0f)
		{
			ExitSlomo();
		}
	}
	private void EnterSlomo()
	{
		GameManager.instance.SetTimeCoefficient(slowTimeSpeed);
		_isInSlomo = true;
	}

	private void ExitSlomo()
	{
		GameManager.instance.SetTimeCoefficient(normalTimeSpeed);
		_isInSlomo = false;
	}
}
