using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtByContact : MonoBehaviour {

	public int DamageDealt;

	private int df;

	public void Start()
	{
		GameObject gco = GameObject.FindWithTag ("GameController");

		df = 0;
		if (gco != null) {
			GameController gc = gco.GetComponent<GameController> ();
			df = gc.GetCurrentDifficulty ();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		GameUnit gu = other.GetComponent<GameUnit>();
		if(gu!=null)
		{
			gu.InflictDamage (DamageDealt);	
		}
	}
}
