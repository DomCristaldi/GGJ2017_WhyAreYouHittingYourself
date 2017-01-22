using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPuppeteer : MonoBehaviour {

	public Animator animComp;
	public SpriteRenderer armSpriteRen;


	public string deathBool = "Bool_IsDead";

	protected virtual void Awake()
	{

	}

	// Use this for initialization
	protected virtual void Start () {
		if (animComp == null)
		{
			Debug.LogError("Missing Animator Component Reference for Animation Puppeteer. Try assigning manually by dragging it in the Unity Editor Inspector");
			Debug.Break();
		}

	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}

	public virtual void SetDeathState(bool isDead)
	{
		animComp.SetBool(Animator.StringToHash(deathBool),
						 isDead);
	}
}
