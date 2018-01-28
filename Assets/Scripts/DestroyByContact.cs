using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject spawn_object;
	public int ScoreCost;
	public int DamageDealt;
	public int MaxCollisions;

	private int collisions;
	private int df;

	public void Start()
	{
		GameObject gco = GameObject.FindWithTag ("GameController");

		df = 0;
		if (gco != null) {
			GameController gc = gco.GetComponent<GameController> ();
			df = gc.GetCurrentDifficulty ();
			Rigidbody rg = GetComponent<Rigidbody> ();
			if (rg != null) {
				rg.velocity -= new Vector3 (0.0f, 0.0f, df);
			}
		}

		collisions = -df;
	}

	void OnTriggerEnter(Collider other)
	{
		if ((other.tag == "boundary")||(other.tag == "hazard"))
			return;

		collisions++;

		GameUnit gu = other.GetComponent<GameUnit>();
		if ((gu == null))
		{
			if(other.tag!="powerbolt")
				Destroy(other.gameObject);
		} 
		else
		{
			gu.InflictDamage (DamageDealt);	
		}

		if ((collisions < MaxCollisions)&&(other.tag != "Player"))
			return;

		Destroy (gameObject);

		Instantiate (explosion, transform.position, transform.rotation);

		GameObject gameControllerObj = GameObject.FindWithTag ("GameController");
		if (gameControllerObj != null) 
		{
			GameController gc = gameControllerObj.GetComponent<GameController> ();
			if (gc != null) 
			{
				gc.IncreaseScore (ScoreCost);
			}
		}

		if (spawn_object != null) 
		{
			Instantiate (spawn_object, transform.position, transform.rotation);
		}
	}
}﻿