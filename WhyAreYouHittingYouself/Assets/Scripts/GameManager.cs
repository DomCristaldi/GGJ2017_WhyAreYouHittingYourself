using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityEvent_SingleBool : UnityEvent<bool>{}

public class GameManager : MonoBehaviour {

	public enum GameStatus
	{
		Gameplay = 0,
		Death = 1,
		Paused = 2,
		Controls = 3,
	}

	public bool controlsAreUp = false;
	public bool isPaused = false;
	public UnityEvent_SingleBool pauseGameEvent;

	private static GameManager _instance;
	public static GameManager instance
	{
		get {return _instance;}
	}
    
	public PlayerController playerRef;

	public static bool canGetPlayer
	{
		get
		{
			return instance != null && instance.playerRef != null;
		}
	}

	//INPUTS
	[HeaderAttribute("Game Specific Input")]
	public KeyCode pauseGameButton = KeyCode.Escape;
	public KeyCode gameControlsButton = KeyCode.F1;

	[SpaceAttribute]
	[HeaderAttribute("Gameplay Numbers")]
	public GameStatus currentGameStatus = GameStatus.Gameplay;

	[SerializeField]
	private float timeCoefficient = 1.0f;

	void Awake()
	{
		if (_instance == null) {_instance = this;}
	}

	// Use this for initialization
	void Start () {
		SetTimeCoefficient(timeCoefficient);
    }
	
	// Update is called once per frame
	void Update () {
		HandleInput();

        int enemyTeamSize = TeamManager.instance.enemyTeam.Count;

        if (enemyTeamSize == 0)
        {
            EndGame_PlayerWins();
        }
        if (playerRef.statsComp.currentHealth == 0)
        {
            EndGame_PlayerLoses();
        }
	}

	/// <summary>
    /// Sets the time scale
    /// </summary>
    /// <param name="timeCoefficient">desired coefficient of Time. 0.5f -> half speed, 2.0f -> double speed/param>
	public void SetTimeCoefficient(float timeCoefficient)
	{
		this.timeCoefficient = timeCoefficient;
		Time.timeScale = this.timeCoefficient;
	}

	private void EndGame_PlayerWins()
	{
        
	}

	private void EndGame_PlayerLoses()
	{
        
	}

	public void SetIsPausedFalse()
	{
		//SetIsPaused(false);
		SetTimeCoefficient(1.0f);
		pauseGameEvent.Invoke(false);
	}

	public void HandleInput()
	{	

		if (Input.GetKeyDown(pauseGameButton))
		{
			isPaused = !isPaused;

			pauseGameEvent.Invoke(isPaused);

			if (!isPaused)
			{
				SetTimeCoefficient(1.0f);
			}
			else {SetTimeCoefficient(0.0f);}
		}

		/*
		//HACK: This should be a State Machine

		if (Input.GetKeyDown(gameControlsButton))
		{
			//if ()
		}

		//if it's in Gameplay and the player is alive, pause it. 
		if (Input.GetKeyDown(pauseGameButton))
		{
			if (currentGameStatus == GameStatus.Gameplay)
			if (canGetPlayer)
			{
				if (playerRef.statsComp.isAlive)
				{
					timeCoefficient = 0.0f;	//HACK: Should be a number settable from inspector
				}
			}
			//no player was found, we'll pause it anyway
			else
			{
				timeCoefficient = 1.0f;
			}
		}
		*/
	}


}
