using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameUnit : MonoBehaviour {

	public int initHealth;
	private int health;

	protected void Start()
	{
		health = initHealth;
	}

	virtual public void InflictDamage(int damage)
	{
		health -= damage;
		OnHPChange ();
		if (health <= 0)
			Kill();
		if (health > initHealth)
		{
			health = initHealth;
			OnHPChange ();
		}
	}

	public int GetHP()
	{
		return health;
	}

	virtual public void Kill()
	{
		Destroy (gameObject);
	}

	virtual public void OnHPChange()
	{
	}
}
