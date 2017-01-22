using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GameplayUIController : MonoBehaviour {

	[System.Serializable]
	public class UIPanelGroup
	{
		public GameManager.GameStatus panelType;
		public GameObject[] panels;

		public void SetActiveStatus(bool isActive)
		{
			foreach (GameObject go in panels)
			{
				go.SetActive(isActive);
			}
		}
		
	}

	public UIPanelGroup pausedPanel;

	public UIPanelGroup activePanelGroup;

	public List<UIPanelGroup> panelGroups;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//HACK: This really shouldn't be polling constantly, just subscribe to an event from the Gameplay Manager
		if (GameManager.instance != null)
		{


			/*
			if (activePanelGroup.panelType != GameManager.instance.currentGameStatus)
			{
				SetActivePanelGroup(GameManager.instance.currentGameStatus);
			}
			*/
		}
	}

	public void SetActivePanelGroup(GameManager.GameStatus panelType)
	{
		foreach (UIPanelGroup pg in panelGroups)
		{
			if (pg.panelType == panelType)
			{
				pg.SetActiveStatus(true);
				activePanelGroup = pg;
			}
			else
			{
				pg.SetActiveStatus(false);
			}
		}
	}

	public void SetIsPaused(bool pausedStatus)
	{
		pausedPanel.SetActiveStatus(pausedStatus);
	}

}
