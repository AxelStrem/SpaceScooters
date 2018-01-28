using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSpeeds : MonoBehaviour {

	public Vector3 init_speed;
	// Use this for initialization
	void Start () {
		Rigidbody[] rbl = GetComponentsInChildren<Rigidbody> (false);
		Vector3 vsp = init_speed;
		for (int i = 0; i < rbl.Length; ++i) {
			rbl [i].velocity += 2.0f * rbl [i].transform.localPosition;
		}
		transform.DetachChildren ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
