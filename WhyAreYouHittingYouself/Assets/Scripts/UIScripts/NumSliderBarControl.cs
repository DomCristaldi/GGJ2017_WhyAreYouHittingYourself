using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(Slider))]
public class NumSliderBarControl : MonoBehaviour {

	public enum StatsTargetSetting
	{
		Health = 0,
		Power = 1,
	}

	public Slider _sliderBar;

	public Text numText;
	public int lowNum;
	public int highNum;

	[Space]
	public Stats targetStats;
	public StatsTargetSetting displaySetting = StatsTargetSetting.Health;

	void Awake()
	{
		_sliderBar = GetComponent<Slider>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (targetStats == null
			&& GameManager.instance != null
			&& GameManager.instance.playerRef != null)
		{
			targetStats = GameManager.instance.playerRef.statsComp;
			
			switch (displaySetting)
			{
				case StatsTargetSetting.Health:
					//MaptoRange(targetStats.currentHealth, targetStats.minHealth, targetStats.maxHealth);
					_sliderBar.minValue = targetStats.minHealth;
					_sliderBar.maxValue = targetStats.maxHealth;
					break;
				case StatsTargetSetting.Power:
					//MaptoRange(targetStats.brainPower, targetStats.minPower, targetStats.maxPower);
					_sliderBar.minValue = targetStats.minPower;
					_sliderBar.maxValue = targetStats.maxPower;
					break;
			}
		}

		if (targetStats != null)
		{
			switch (displaySetting)
			{
				case StatsTargetSetting.Health:
					//MaptoRange(targetStats.currentHealth, targetStats.minHealth, targetStats.maxHealth);
					_sliderBar.value = targetStats.currentHealth;
					break;
				case StatsTargetSetting.Power:
					//MaptoRange(targetStats.brainPower, targetStats.minPower, targetStats.maxPower);
					_sliderBar.value = targetStats.brainPower;
					break;
			}

			numText.text = _sliderBar.value + " / " + _sliderBar.maxValue;
		}
	}

	public void MaptoRange(int value, int lowValueNum, int highValueNum)
	{
		int sliderVal =  (int) ((float) value).Remap(lowValueNum, lowNum,
							  						 highValueNum, highNum);
		
		_sliderBar.value = sliderVal;

		//(highValueNum - lowValueNum) / lowValueNum
	}
}
