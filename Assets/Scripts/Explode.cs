using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ParticleSystem ps = GetComponent<ParticleSystem> ();
		ps.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
