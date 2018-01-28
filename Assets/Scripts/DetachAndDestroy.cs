using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachAndDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.DetachChildren ();
		Destroy (gameObject);
	}

}
