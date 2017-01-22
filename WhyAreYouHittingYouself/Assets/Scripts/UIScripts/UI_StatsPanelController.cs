using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class UI_StatsPanelController : MonoBehaviour {

	public Text pointsText;

	public Text enemiesKilledText;
	public Text totalEnemiesText;

	// Use this for initialization
	void Start () {
		//HACK: I hate myself for using Find, but we're crunched for time
		 totalEnemiesText.text = FindObjectsOfType<EnemyController>().Length.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		if (TeamManager.instance != null)
		{
			enemiesKilledText.text = TeamManager.instance.totalCasualties.ToString();;
		}
		else
		{
			enemiesKilledText.text = "0";
		}
	}
}
