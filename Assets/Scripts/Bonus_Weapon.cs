using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus_Weapon : Bonus {

	override public void BonusEffect(PlayerController pc)
	{
		pc.ImproveWeapon ();
	}
}
