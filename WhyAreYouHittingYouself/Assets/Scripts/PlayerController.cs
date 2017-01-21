using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
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

	void Awake()
	{
		_mindControlComp = GetComponent<MindControl>();
	}

	// Use this for initialization
	void Start () {
		if (GameManager.instance != null)
		{
			GameManager.instance.playerRef = this;
		}
	}
	
	// Update is called once per frame
	void Update () {
		HandleInput();
	}

	private void HandleInput()
	{

	}
}
