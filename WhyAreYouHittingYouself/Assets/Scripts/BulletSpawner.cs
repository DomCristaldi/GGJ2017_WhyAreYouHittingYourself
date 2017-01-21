using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour {

	public GameObject bulletPrefab;

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
		//normalize just in case to avoid any chance of data corruption
		bulletHeading = bulletHeading.normalized;

		//create the bullet and store a reference to it
		GameObject spawnedBullet = Instantiate(bulletPrefab,
											   transform.position,
											   Quaternion.LookRotation(bulletHeading))
								   as GameObject;
		
		//make the bullet fly off in the desired direction
		spawnedBullet.GetComponent<Movement>().desireMoveDirection = bulletHeading;
	}
}
