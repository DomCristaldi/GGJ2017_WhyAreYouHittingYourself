using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour {

	public GameObject bulletPrefab;

	public float shootRange = 5.0f;
	public float shootAngle = 0.1f;

	public float fireRate = 1.0f;
	private bool _onShootCooldown = false;
	public bool onShootCooldown {get{return _onShootCooldown;}}


	[Space]
	[Header("Gizmo Controls")]
	[SerializeField]
	private Color g_shootRangeColor = Color.red;

#if UNITY_EDITOR
	void OnValidate()
	{

	}
#endif


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void FireBullet(Vector3 bulletHeading)
	{
		if (onShootCooldown) {return;}

		//normalize just in case to avoid any chance of data corruption
		bulletHeading = bulletHeading.normalized;

		//create the bullet and store a reference to it
		GameObject spawnedBullet = Instantiate(bulletPrefab,
											   transform.position,
											   Quaternion.LookRotation(bulletHeading))
								   as GameObject;
		
		//make the bullet fly off in the desired direction
		spawnedBullet.GetComponent<Movement>().desireMoveDirection = bulletHeading;

		StartCoroutine(ShootCooldownRoutine(fireRate));
	}

	private IEnumerator ShootCooldownRoutine(float cooldownTime)
	{
		_onShootCooldown = true;

		yield return new WaitForSeconds(cooldownTime);

		_onShootCooldown = false;

		yield return null;
	}

#if UNITY_EDITOR
	void OnDrawGizmos()
	{
		Color originalGizmoColor = Gizmos.color;
		Gizmos.color = g_shootRangeColor;

		Gizmos.DrawRay(transform.position, transform.forward * shootRange);

		Gizmos.color = originalGizmoColor;
	}
#endif
}
