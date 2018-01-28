using UnityEngine;
using System.Collections;

public class Tumble : MonoBehaviour
{
	private Rigidbody rb;
	public float tumble;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		float rf = Random.Range (-0.5f, 0.5f);
		rf += Mathf.Sign (rf) * 0.5f;
		//Vector3 axis = Mathf.Lerp(, Random.insideUnitSphere, 0.5f);
		rb.angularVelocity = /**/new Vector3 (0.0f, 1.0f, 0.0f) * tumble * rf;
	}

	void FixedUpdate()
	{
		Vector3 movement = new Vector3(0.0f, 0.0f, 1.0f);
		//rb.velocity = movement * speed;

	}
}﻿