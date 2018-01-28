using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSpawner : MonoBehaviour {

	public GameObject Projectile;
	public Transform shotSpawn;
	public float Speed;
	public float FireRate;

	float nextFire;
	// Use this for initialization
	void Start () {
		nextFire = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time >= nextFire) 
		{
			nextFire = Time.time + FireRate;
			GameObject shot = Instantiate (Projectile, shotSpawn.position, shotSpawn.rotation);
			shot.GetComponent<Rigidbody> ().velocity = /*GetComponent<Rigidbody>().velocity + */Speed * (shotSpawn.transform.position - GetComponent<Rigidbody>().transform.position);
		}
	}
}
