using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HazardPattern
{
	public virtual bool Empty()
	{
		return true;	
	}

	public virtual Vector3 NextSpawn()
	{
		return new Vector3(0.0f, 0.0f, 0.0f);	
	}

	public virtual void Reset()
	{	
	}
}

public class HP_Random : HazardPattern
{
	int hc;
	int count;

	public HP_Random(int hazard_count)
	{
		hc = hazard_count;
		count = hazard_count;
	}
	
	public override bool Empty()
	{
		return !(count>0);	
	}

	public override Vector3 NextSpawn()
	{
		count--;
		return new Vector3 (Random.Range (-1.0f, 1.0f), 1.0f, 1.0f);	
	}

	public override void Reset()
	{
		count = hc;	
	}
}


public class HP_Random2 : HazardPattern
{
	int hc;
	int count;

	public HP_Random2(int hazard_count)
	{
		hc = hazard_count;
		count = hazard_count;
	}

	public override bool Empty()
	{
		return !(count>0);	
	}

	public override Vector3 NextSpawn()
	{
		return new Vector3 (Random.Range (-1.0f, 1.0f), 1.0f, ((count--)%2)*1.0f);	
	}

	public override void Reset()
	{
		count = hc;	
	}
}


public class HP_Random4 : HazardPattern
{
	int hc;
	int count;

	public HP_Random4(int hazard_count)
	{
		hc = hazard_count;
		count = hazard_count;
	}

	public override bool Empty()
	{
		return !(count>0);	
	}

	public override Vector3 NextSpawn()
	{
		return new Vector3 (Random.Range (-1.0f, 1.0f), 1.0f, (((count--)%4)==0)?1.0f:0.0f);	
	}

	public override void Reset()
	{
		count = hc;	
	}
}

public class HP_Dick : HazardPattern
{
	int hc;
	int count;
	float xs;
	float w;

	int tx,ty;
	int nx,ny;

	Texture2D map;

	void Step()
	{
		nx++;
		if (nx >= map.width) {
			nx = 0;
			ny++;
			if (ny >= map.height)
				ny = 0;
		}
	}

	void FindNextSpawn()
	{
		while (map.GetPixel (nx, ny).grayscale > 0.5f) {
			Step ();
		}
	}

	public HP_Dick(float x_shift, float width, Texture2D patternMap)
	{
		map = patternMap;
		xs = x_shift;
		hc = 22;
		count = hc;
		w = width;
		tx = 0;
		ty = 0;
		nx = 0;
		ny = 0;
		FindNextSpawn();
	}

	public override bool Empty()
	{
		return ny<ty;	
	}

	public override Vector3 NextSpawn()
	{
		tx = nx;
		ty = ny;
		Step ();
		FindNextSpawn ();
		return new Vector3 (xs + (tx/((float)map.width)-0.5f)*w, 1.0f, (ty>=ny)?0.0f:1.0f);	
	}

	public override void Reset()
	{
		tx = 0;
		ty = 0;
		nx = 0;
		ny = 0;
		FindNextSpawn ();
	}
}

public class HazardWave
{
	HazardPattern p;
	int t;
	float d;
	Object bt;
	Vector2 bp;
	bool bonus_spawned;

	public HazardWave(HazardPattern pattern, int type, float delay)
	{
		p = pattern;
		d = delay;
		t = type;
		bt = null;
		bonus_spawned = true;
	}

	public HazardWave(HazardPattern pattern, int type, float delay, Object bonus_type, Vector2 bonus_pos)
	{
		p = pattern;
		d = delay;
		t = type;
		bt = bonus_type;
		bp = bonus_pos;
		bonus_spawned = false;
	}

	public float Generate(GameController parent, GameObject[] hazards, Vector3 spawnValues)
	{
		if (!bonus_spawned) {
			parent.PublicInstantiate(bt, new Vector3(bp.x,0.0f,bp.y), Quaternion.identity);
			bonus_spawned = true;
		}
		Vector3 v = p.NextSpawn ();
		Vector3 sp = new Vector3 (spawnValues.x * v.x, 0.0f, spawnValues.z * v.y);
		parent.PublicInstantiate(hazards[t], sp, Quaternion.identity);
		return d * v.z;
	}

	public bool Empty()
	{
		return p.Empty ();
	}

	public void Reset()
	{
		if (bt != null)
			bonus_spawned = false;
		p.Reset ();
	}
}

public class GameController : MonoBehaviour {

	public Text scoreText;
	public Text hpText;

	public GameObject ui_gameover;
	public GameObject[] hazards;

	public Texture2D[] DickMap;

	public Vector3 spawnValues;

	public float startWait;
	public float waveWait;
	public float spawnWait;

	public int hazardCount;

	private int score;

	private bool game_over;

	private int difficulty;

	private List<HazardWave> waves;
	private int cw;

	void ShowGameOverText(bool visible)
	{
		ui_gameover.GetComponent<CanvasGroup> ().alpha = visible?1:0;
	}

	void Start ()
	{
#if UNITY_ANDROID
		Screen.orientation = ScreenOrientation.LandscapeLeft;
#endif
		waves = new List<HazardWave> (0);


		//waves.Add (new HazardWave (new HP_Random2(2), 3, 6.0f));


		waves.Add (new HazardWave (new HP_Random(1), 0, 1.0f));
		waves.Add (new HazardWave (new HP_Random(3), 0, 1.0f));
		waves.Add (new HazardWave (new HP_Random(5), 0, 0.5f));
		waves.Add (new HazardWave (new HP_Random(1), 1, 1.0f));
		waves.Add (new HazardWave (new HP_Random(10), 0, 0.7f));
		waves.Add (new HazardWave (new HP_Random(2), 1, 0.1f));
		waves.Add (new HazardWave (new HP_Random2(20), 0, 0.8f));
		waves.Add (new HazardWave (new HP_Random2(50), 2, 0.2f));
		waves.Add (new HazardWave (new HP_Random2(30), 4, 0.2f));
		waves.Add (new HazardWave (new HP_Random(1), 3, 2.0f));
		waves.Add (new HazardWave (new HP_Dick(0.0f, 1.0f, DickMap[0]), 2, 0.1f));
		waves.Add (new HazardWave (new HP_Random(10), 1, 0.8f));
		waves.Add (new HazardWave (new HP_Random2(10), 1, 0.8f));
		waves.Add (new HazardWave (new HP_Dick(0.0f, 1.5f, DickMap[1]), 2, 0.1f));
		waves.Add (new HazardWave (new HP_Dick(0.0f, 1.0f, DickMap[0]), 4, 0.1f, hazards[3], new Vector2(0.0f,26.0f)));
		waves.Add (new HazardWave (new HP_Random2(100), 6, 0.3f));
		waves.Add (new HazardWave (new HP_Random2(200), 7, 0.1f));
		waves.Add (new HazardWave (new HP_Random2(8), 5, 1.0f));
		waves.Add (new HazardWave (new HP_Random2(2), 3, 6.0f));
		waves.Add (new HazardWave (new HP_Dick(0.0f, 4.1f, DickMap[0]), 5, 0.5f));
		waves.Add (new HazardWave (new HP_Random(1), 3, 2.0f));
		waves.Add (new HazardWave (new HP_Random2(20), 1, 0.6f));
		waves.Add (new HazardWave (new HP_Random(3), 8, 4.0f));
		waves.Add (new HazardWave (new HP_Dick(0.0f, 2.3f, DickMap[2]), 4, 0.1f, hazards[3], new Vector2(0.0f,35.0f)));
		waves.Add (new HazardWave (new HP_Random2(6), 8, 5.0f));
		waves.Add (new HazardWave (new HP_Random2(2), 3, 6.0f));
		waves.Add (new HazardWave (new HP_Random2(60), 9, 0.1f));
		waves.Add (new HazardWave (new HP_Random(1), 3, 1.0f));
		waves.Add (new HazardWave (new HP_Dick(0.0f, 2.3f, DickMap[1]), 9, 0.1f));
		waves.Add (new HazardWave (new HP_Random(1), 3, 1.0f));
		waves.Add (new HazardWave (new HP_Dick(0.0f, 2.3f, DickMap[2]), 9, 0.1f));
		waves.Add (new HazardWave (new HP_Random(1), 3, 1.0f));
		waves.Add (new HazardWave (new HP_Random2(2), 8, 3.0f));
		waves.Add (new HazardWave (new HP_Random4(500), 4, 0.01f));
		waves.Add (new HazardWave (new HP_Random(1), 3, 1.0f));
		waves.Add (new HazardWave (new HP_Random(3), 10, 5.0f));

		waves.Add (new HazardWave (new HP_Random2(16), 8, 3.0f));

		//waves.Add (new HazardWave (new HP_Dick(0.0f, 3.5f, DickMap[0]), 0, 0.5f)); // Large dick
		//waves.Add (new HazardWave (new HP_Dick(0.0f, 1.0f, DickMap[0]), 4, 0.1f, hazards[3], new Vector2(0.0f,26.0f))); // Small dick + bonus
		//waves.Add (new HazardWave (new HP_Dick(0.0f, 1.5f, DickMap[1]), 2, 0.1f)); // Double dick
		//waves.Add (new HazardWave (new HP_Dick(0.0f, 4.1f, DickMap[0]), 5, 0.5f)); // Large hard dick

		StartCoroutine (SpawnWaves ());
		score = 0;
		difficulty = 0;
		game_over = false;
		ShowGameOverText (false);
		UpdateScore ();
		UpdateHP (100);
		cw = 0;

	}

	void Update()
	{
		if (game_over) 
		{
			if (Input.GetButton ("Fire1"))//(Input.GetKeyDown (KeyCode.R)) 
			{
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
		}
	}

	void UpdateScore()
	{
		scoreText.text = "" + score;
	}

	public void IncreaseScore(int ns)
	{
		score += ns;
		UpdateScore ();
	}

	public void UpdateHP(int hp)
	{
		hpText.text = "HP: " + hp + "%";
	}

	public void GameOver()
	{
		ShowGameOverText (true);
		game_over = true;
	}

	public void PublicInstantiate(Object original, Vector3 pos, Quaternion rot)
	{
		Instantiate(original, pos, rot);
	}

	public int GetCurrentDifficulty()
	{
		return difficulty;
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);
		while (!game_over) 
		{
			while (!waves [cw].Empty ()) {
				float f = 0.0f;
				while ((f == 0.0f)&&(!waves [cw].Empty ())) {
					f = waves [cw].Generate (this, hazards, spawnValues);
				}
				yield return new WaitForSeconds (f);
			}

			waves [cw].Reset ();
			cw++;
			if (cw >= waves.Count) {
				difficulty++;
				cw = 0;
			}
			/*for (int i = 0; i < hazardCount; ++i) 
			{
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Instantiate (hazards[0], spawnPosition, Quaternion.identity);
				yield return new WaitForSeconds (spawnWait);
			}*/
			yield return new WaitForSeconds (waveWait);
		}
	}
}
