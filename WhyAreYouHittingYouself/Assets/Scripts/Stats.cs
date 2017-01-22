using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

	// Use this for initialization
	public int brainPower;
	public int maxPower;
	public int minPower;

	public int brainTax = 1;

	public int currentHealth;
	public int maxHealth;
	public int minHealth;
	public int brainWaveRadius;

	void Start () {

	}
	void Update () {

	}
	

	public void ApplyDamage(int amountOfDamage)
	{
		currentHealth = Mathf.Clamp(currentHealth - amountOfDamage,
									minHealth,
									maxHealth);
	}


	private void ChangeBrainPower(int delta)
	{
		brainPower = Mathf.Clamp(brainPower + delta, 0, maxPower);

	}

	public void ConsumeBrainPower(int comsumedPower)
	{
		ChangeBrainPower(-comsumedPower);
	}

	public void ReplenishBrainPower(int replenishedPower)
	{
		ChangeBrainPower(replenishedPower);
	}
}