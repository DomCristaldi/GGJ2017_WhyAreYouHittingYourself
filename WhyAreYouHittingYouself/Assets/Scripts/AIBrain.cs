using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(TeamMember))]
public class AIBrain : MonoBehaviour {

	public BulletSpawner bulletSpawnerRef;

	private NavMeshAgent _navAgentComp;
	private NavMeshAgent navAgentComp{get{return _navAgentComp;}}
	private TeamMember _teamMemberComp;
	public TeamMember teamMemberComp{get {return _teamMemberComp;}}
	
	private bool _canshoot = false;
	public bool canShoot {get{return _canshoot;}}

	private bool _isShooting = false;
	public bool isShooting {get{return _isShooting;}}

	[Space]
	[Header("Follow Settings")]
	public Transform targetTransform;
	public Vector3 targetHeading
	{
		get{
			if (targetTransform == null) {return Vector3.zero;}
			//difference in postion, projected onto Up plane, and normalized
			return Vector3.Normalize(
				   Vector3.ProjectOnPlane((targetTransform.position - transform.position),
										  Vector3.up));
		}
	}

	public float rotateTowardsTargetSpeed = 1.0f;

	[HideInInspector]
	public bool onlyShootAtStoppingDistance = true;

	[Space]
	[Header("Shoot Settings")]
	public float shootRange = 5.0f;
	public float shootAngle = 0.1f;

	public float lineUpShotTime = 0.1f;
	public float shootRecoveryTime = 0.2f;


	void Awake()
	{
		_navAgentComp = GetComponent<NavMeshAgent>();
		_teamMemberComp = GetComponent<TeamMember>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (TeamManager.instance != null
		&& TeamManager.instance.enemyTeam.Count == 0)
		{
			this.enabled = false;
			return;
		}

		RotateTowardsTarget();

		if (targetTransform != null)
		{

			_navAgentComp.destination = targetTransform.position;
			_teamMemberComp = GetComponent<TeamMember>();
		}
		if (targetTransform == null)
		{
			TargetClosestEnemy();
		}

		//Debug.LogFormat("Can Shoot: {0}", CheckCanAttack());
		if (CheckCanAttack())
		{
			StartCoroutine(AttemptAttackRoutine(lineUpShotTime));
		}

	}

	private void RotateTowardsTarget()
	{
		if (targetTransform == null) {return;}

		//calculate the new facing direction using vectors
		Vector3 newForward = Vector3.MoveTowards(transform.forward,
												 targetHeading,
												 rotateTowardsTargetSpeed * Time.deltaTime).normalized;
												 
		//create Quaternion representation for assignment
		transform.rotation = Quaternion.LookRotation(newForward,
													 Vector3.up);
	}

	public bool CheckCanAttack()
	{
		//sanity check
		if (targetTransform == null) {return false;}
		if (canShoot) {return false;}
		if (TeamManager.instance.enemyTeam.Count == 0) {return false;}

/*
		if (onlyShootAtStoppingDistance
		 && navAgentComp.)
*/

		Vector3 planarHeading = Vector3.ProjectOnPlane(targetTransform.position - transform.position,
													   Vector3.up);


		//checking with squares is faster
		//if (Vector3.Distance(targetTransform.position, transform.position) <= shootRange /*Mathf.Pow(shootRange, 2.0f)*/
		// && (1.0f - Vector3.Dot(planarHeading.normalized, transform.forward)) <= shootAngle)
		//)
		
		//Debug.Log(Vector3.Magnitude(planarHeading));
		
		if (Vector3.Magnitude(planarHeading) <= shootRange);
		{

			//Debug.Log("attack");
			return true;
		}

		//failed all checks, return false
		return false;
	}

	private IEnumerator AttemptAttackRoutine(float timeToLineUpShot)
	{
		//float oldSpeed = _navAgentComp.speed;
		//_navAgentComp.speed = 0.0f;
		
		_navAgentComp.Stop();
		_canshoot = true;
		_isShooting = true;

		yield return new WaitForSeconds(timeToLineUpShot);

		bulletSpawnerRef.FireBullet(transform.forward);

		_navAgentComp.Resume();
		_isShooting = false;

		yield return new WaitForSeconds(shootRecoveryTime);

		_canshoot = false;

		//_navAgentComp.speed = oldSpeed;
		yield break;
	}

	public ActorController FindClosestMemberOfTeam(TeamMember.Team targetTeam)
	{
		//_navAgentComp.CalculatePath
		List<ActorController> teamToSearch = TeamManager.instance.GetTargetTeam(targetTeam);

		//shortcut b/c there's no other possible solutions in these cases
		if (teamToSearch.Count == 0) {return null;}
		if (teamToSearch.Count == 1) {return teamToSearch[0];}

		//we have at least 2, find the closest one
		float shortestDist = Mathf.Infinity;
		int indexOfClosestTarget = 0;
		//NavMeshPath calcPath = new NavMeshPath();
		//foreach (ActorController actor in teamToSearch)
		for (int i = 0; i < teamToSearch.Count; ++i)
		{
			if (teamToSearch[i] == null
				|| teamToSearch[i] == GetComponent<ActorController>())
			{continue;}

			float dist = Vector3.SqrMagnitude(teamToSearch[i].transform.position - transform.position);
			if (dist < shortestDist)
			{
				indexOfClosestTarget = i;
				shortestDist = dist;
			}
			
			/*
			navAgentComp.CalculatePath(actor.transform.position,
									   calcPath);
			
			*/
		}

		return teamToSearch[indexOfClosestTarget];
	}

	public ActorController FindClosestFriendly()
	{
		return FindClosestMemberOfTeam(teamMemberComp.currentTeam);
	}
	public ActorController FindClosestEnemy()
	{
		return FindClosestMemberOfTeam(TeamMember.GetOpposingTeam(_teamMemberComp.currentTeam));
	}

	public void TargetClosestEnemy() {
		ActorController closestEnemy = FindClosestEnemy();
		if (closestEnemy != null)
		{
			targetTransform = closestEnemy.transform;
		}
		else
		{
			targetTransform = null;
		}
		//targetTransform = FindClosestEnemy().transform;
	}

}
