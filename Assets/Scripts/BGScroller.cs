using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour {

	public MeshRenderer mr;
	public float ScrollSpeed;
	public float SideScroll;
	private float offset;
	private float offset_x;
	// Use this for initialization
	void Start () {
		offset = 0.0f;
		offset_x = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		offset = Mathf.Repeat(ScrollSpeed*Time.time,1.0f);
		offset_x = Mathf.Repeat(SideScroll*Time.time,1.0f);
		mr.material.SetTextureOffset ("_MainTex", new Vector2 (offset_x, offset));
	}
}
