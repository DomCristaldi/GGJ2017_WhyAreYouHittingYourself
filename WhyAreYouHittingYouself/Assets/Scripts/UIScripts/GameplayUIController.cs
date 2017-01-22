using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GameplayUIController : MonoBehaviour {

	[System.Serializable]
	public class UIPanelGroup
	{
		
		/*
		public enum PanelType
		{
			None = 0,
			Death = 1,
			Controls = 2,
			Pause = 3,
		}
		

		public PanelType panelType;
		*/
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

	public List<UIPanelGroup> panelGroups;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetActivePanelGroup(GameManager.GameStatus panelType)
	{
		foreach (UIPanelGroup pg in panelGroups)
		{
			if (pg.panelType == panelType)
			{
				pg.SetActiveStatus(true);
			}
			else
			{
				pg.SetActiveStatus(false);
			}
		}
	}
}
