using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/HealthBuff")]
public class HealthBuff : PowerupEffect
{
    public int amount;
    public int Mamount;

    public override void Apply(GameObject target)
    {
        if (target.GetComponent<ShipBehavior>().currentHealth + amount >= target.GetComponent<ShipBehavior>().maxHealth + Mamount) 
        {
            target.GetComponent<ShipBehavior>().maxHealth += Mamount;
            target.GetComponent<ShipBehavior>().currentHealth = target.GetComponent<ShipBehavior>().maxHealth;
        } else 
        {
            target.GetComponent<ShipBehavior>().currentHealth += amount;
        }
        target.GetComponent<ShipBehavior>().healthBar.SetHealth(target.GetComponent<ShipBehavior>().currentHealth);
        target.GetComponent<ShipBehavior>().healthBar.SetMaxHealth(target.GetComponent<ShipBehavior>().maxHealth);
        //target.GetComponent<ShipBehavior>().currentHealth += amount;
        //target.GetComponent<ShipBehavior>().maxHealth += Mamount;
        
    }
}

