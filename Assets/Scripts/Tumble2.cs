using UnityEngine;
using System.Collections;

public class Tumble2 : MonoBehaviour
{
	private Rigidbody rb;
	public float tumble;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		Vector3 axis = Random.onUnitSphere;
		rb.angularVelocity = tumble * axis;
	}

}﻿