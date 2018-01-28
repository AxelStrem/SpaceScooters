using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour {

	public float delay;
	public float force;
	public GameObject target;
	public bool pickRandomHazard;

	float time_created;

	void PickTargetHazard()
	{
		GameObject[] list = GameObject.FindGameObjectsWithTag ("hazard");
		if (list.Length > 0) {
			int ind = Random.Range (0, list.Length - 1);
			target = list [ind];
		}
	}
	// Use this for initialization
	void Start () {
		if (target == null) {
			if (pickRandomHazard) {
				PickTargetHazard ();
			}
			else
			target = GameObject.FindGameObjectWithTag ("Player");
		}
		time_created = Time.time;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Time.time > time_created + delay) {
			Vector3 vec = Vector3.forward;
			Rigidbody rb = GetComponent<Rigidbody> ();
			if (target != null) {
				vec = target.transform.position - transform.position;
				vec.y = 0.0f;
				vec.Normalize ();
			} else {
				if (pickRandomHazard) {
					PickTargetHazard ();
				}
			}
			rb.AddForce (vec * force);
		}
	}
}
