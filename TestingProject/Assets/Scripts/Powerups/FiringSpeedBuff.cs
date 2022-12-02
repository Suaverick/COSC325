using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Powerups/FiringSpeedBuff")]
public class FiringSpeedBuff : PowerupEffect
{
    public float amount;

    public override void Apply(GameObject target)
    {
        target.GetComponent<ShipBehavior>().fltBulletFireRate += amount;
    }
}
