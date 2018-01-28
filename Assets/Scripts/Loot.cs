using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour {
	
	public GameObject[] LootList;

	// Use this for initialization
	void Start () {
		int pick = Random.Range (0, LootList.Length);
		Instantiate (LootList [pick], transform.position, transform.rotation);
		Destroy (gameObject);
	}
}
