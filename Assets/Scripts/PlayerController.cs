using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : GameUnit
{
	private Rigidbody rb;
	public float speed;
	public float tilt;
	public Boundary boundary;

	public Camera cam;

	public GameObject[] shots;
	public GameObject shot2;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire;
	private int WeaponLevel;

	private bool Weapon2;
	private float nextFire2;
	public float fireRate2; 

	public GameObject playerExplosion;
	public AudioSource bolt_fired;

	public GameController gameController;

	public AudioSource CoinSound;

	new protected void Start()
	{
		base.Start ();
		nextFire = 0.0f;
		nextFire2 = 0.0f;
		rb = GetComponent<Rigidbody>();
		WeaponLevel = 0;
		Weapon2 = false;
	}

	public void IncreaseFireRate()
	{
		fireRate *= 0.8f;
	}

	public void ImproveWeapon()
	{
		if ((WeaponLevel + 1) < shots.Length)
			WeaponLevel++;
		else {
		//	gameController.IncreaseScore (100);
			if (!Weapon2) {
				Weapon2 = true;
			}
			else
				fireRate2*=0.8f;
		}
	}

	override public void Kill()
	{
		Instantiate (playerExplosion, transform.position, transform.rotation);
		base.Kill ();
		gameController.GameOver ();
	}

	override public void OnHPChange()
	{
		if (GetHP () > initHealth)
			gameController.IncreaseScore (GetHP () - initHealth);
		gameController.UpdateHP (GetHP ());
	}

	bool IsFiring()
	{
		//return Input.GetButton ("Fire1");
		return true;
	}

	void Update()
	{
		if (IsFiring() && Time.time >= nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate (shots[WeaponLevel], shotSpawn.position, shotSpawn.rotation);
			bolt_fired.Play ();
		}

		if ((IsFiring () && Weapon2) && Time.time >= nextFire2) {
			nextFire2 = Time.time + fireRate2;
			Instantiate (shot2, shotSpawn.position, shotSpawn.rotation);
		}
	}

	void FixedUpdate()
	{
#if UNITY_ANDROID
		//float mx = Input.GetAxis ("Mouse X");
		//float my = Input.GetAxis ("Mouse Y");
		Vector3 mp = Input.mousePosition;

		Vector3 mpos = cam.ScreenToWorldPoint(mp);
	

		Vector3 movement = mpos - rb.position;//new Vector3 (mx, 0.0f, my);//
		movement.y = 0.0f;

		if(movement.magnitude>1.0f)
			movement.Normalize();
#else
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
#endif
		rb.velocity = movement * speed;
		rb.position= new Vector3
			(Mathf.Clamp(rb.position.x,boundary.xMin,boundary.xMax),
				0.0f,
				Mathf.Clamp(rb.position.z,boundary.zMin,boundary.zMax)

			);
		rb.rotation = Quaternion.Euler(0.0f,0.0f,rb.velocity.x* -tilt);
	}

	public void PlayCoinSound()
	{
		CoinSound.Play ();
	}
}﻿