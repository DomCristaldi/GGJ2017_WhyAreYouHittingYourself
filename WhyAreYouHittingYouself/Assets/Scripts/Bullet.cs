using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public TeamMember.Team owningTeam;

    public float bulletLifetime = 3.0f;

    public int damageAmount;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("boom");

        Stats otherStats = other.GetComponent<Stats>();
        if (otherStats != null)
        {
            otherStats.ApplyDamage(damageAmount);
        }

        Destroy(gameObject);
    }

    private IEnumerator BulletCleanupTimer(float time)
    {
        yield return new WaitForSeconds(time);
        
        Destroy(gameObject);

        yield return null;
    }
}
