using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/TwoWeaponsBuff")]
public class TwoWeaponsBuff : PowerupEffect
{

    public override void Apply(GameObject target)
    {
        target.GetComponent<ShipBehavior>().oneCannon.SetActive(false);
        target.GetComponent<ShipBehavior>().twoCannon.SetActive(true);
    }
}