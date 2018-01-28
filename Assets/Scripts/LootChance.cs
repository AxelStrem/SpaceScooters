using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootChance : MonoBehaviour {

	public GameObject LootObject;
	public float chance;

	// Use this for initialization
	void Start () {
		
		float pick = Random.Range (0.0f, 1.0f);
		if (pick <= chance) {
			Instantiate (LootObject, transform.position, transform.rotation);
		}
		Destroy (gameObject);
	}
}
