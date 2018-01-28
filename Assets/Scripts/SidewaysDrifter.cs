using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidewaysDrifter : MonoBehaviour {

	private Rigidbody rb;
	public float speed;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		Vector3 movement = new Vector3(1.0f, 0.0f, 0.0f);
		rb.velocity += movement * Random.Range(-1.0f,1.0f) * speed;
	}
}
