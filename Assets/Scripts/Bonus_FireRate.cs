using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus_FireRate : Bonus {
	
	override public void BonusEffect(PlayerController pc)
	{
		pc.IncreaseFireRate ();
	}

}
