using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour {


	public virtual void BonusEffect(PlayerController pc)
	{
		pc.InflictDamage (-50);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Player")
			return;

		PlayerController pc = other.GetComponent<PlayerController> ();
		pc.PlayCoinSound ();
		BonusEffect (pc);

		Destroy (gameObject);
	}
}
