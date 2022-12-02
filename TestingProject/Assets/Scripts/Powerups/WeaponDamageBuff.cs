using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/UpgradedWeaponsBuff")]
public class WeaponDamageBuff : PowerupEffect
{
    public override void Apply(GameObject target)
    {
        target.GetComponent<ShipBehavior>().upgradedGuns = true;
    }
}
