using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BulletSpawner : MonoBehaviour {

	private AudioSource _audSrc;

	public GameObject bulletPrefab;

	public float shootRange = 5.0f;
	public float shootAngle = 0.1f;

	public float fireRate = 1.0f;
	private bool _onShootCooldown = false;
	public bool onShootCooldown {get{return _onShootCooldown;}}

	[Space]
	[Header("Effects")]
	public AudioClip shootSound;

	[Space]
	[Header("Gizmo Controls")]
	[SerializeField]
	private Color g_shootRangeColor = Color.red;

#if UNITY_EDITOR
	void OnValidate()
	{

	}
#endif

	void Awake()
	{
		_audSrc = GetComponent<AudioSource>();
	}

	



	public void FireBullet(Vector3 bulletHeading)
	{
		if (onShootCooldown) {return;}

		//normalize just in case to avoid any chance of data corruption
		//bulletHeading = bulletHeading.normalized;
		bulletHeading = transform.rotation * Vector3.forward;

		//create the bullet and store a reference to it
		GameObject spawnedBullet = Instantiate(bulletPrefab,
											   transform.position,
											   //transform.rotation)
											   Quaternion.LookRotation(bulletHeading))
								   as GameObject;
		
		if (GameManager.instance != null
		 && GameManager.instance.playerRef != null)
		{
			spawnedBullet.transform.position = new Vector3(spawnedBullet.transform.position.x,
														   GameManager.instance.playerRef.transform.position.y,
														   spawnedBullet.transform.position.z);
		}

		//make the bullet fly off in the desired direction
		spawnedBullet.GetComponent<Movement>().desireMoveDirection = bulletHeading;
		//spawnedBullet.GetComponent<Bullet>().owningTeam = transform.root.GetComponent<TeamMember>().currentTeam;

/*
		Collider[] bulletColliders = spawnedBullet.GetComponentsInChildren<Collider>();
		foreach (Collider ownerCol in transform.root.GetComponentsInChildren<Collider>())
		{
			foreach (Collider bulletCol in bulletColliders)
			{
				Physics.IgnoreCollision(ownerCol,
										bulletCol,
										true);
			}
		}
*/
		StartCoroutine(ShootCooldownRoutine(fireRate));

	//EFFECTS
		_audSrc.PlayOneShot(shootSound);
	}

	private IEnumerator ShootCooldownRoutine(float cooldownTime)
	{
		_onShootCooldown = true;

		yield return new WaitForSeconds(cooldownTime);

		_onShootCooldown = false;

		yield break;
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
