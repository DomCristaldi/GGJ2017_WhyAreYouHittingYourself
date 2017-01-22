using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PausePanelController : MonoBehaviour {
	
	public GameObject resumeElement;
	public GameObject restartElement;
	public GameObject quitElement;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.canGetPlayer)
		{
			if (GameManager.instance.playerRef.statsComp.isDead)
			{
				resumeElement.SetActive(false);
			}
			else
			{
				resumeElement.SetActive(true);
			}
		}
	}
}
