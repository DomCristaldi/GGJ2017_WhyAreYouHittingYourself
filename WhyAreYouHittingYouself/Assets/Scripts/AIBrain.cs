using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIBrain : MonoBehaviour {

	private NavMeshAgent navAgentComp;

	public Transform targetTransform;

	void Awake()
	{
		navAgentComp = GetComponent<NavMeshAgent>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (targetTransform != null)
		{
			navAgentComp.destination = targetTransform.position;
		}
	}

	void OnMouseOver()
	{

	}
}
